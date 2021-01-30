Imports System.Data
Imports System.Data.Common
Imports Yamo
Imports Yamo.Infrastructure
Imports Yamo.Internal.Helpers

Namespace Internal.Query

  ''' <summary>
  ''' Executes SQL query/statement.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class QueryExecutor

    ' TODO: SIP - rewrite as static class (one allocation less...)?

    ''' <summary>
    ''' Stores context.
    ''' </summary>
    Private m_DbContext As DbContext

    ''' <summary>
    ''' Stores dialect provider.
    ''' </summary>
    Private m_DialectProvider As SqlDialectProvider

    ''' <summary>
    ''' Creates new instance of <see cref="QueryExecutor"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    Sub New(context As DbContext)
      m_DbContext = context
      m_DialectProvider = m_DbContext.Options.DialectProvider
    End Sub

    ''' <summary>
    ''' Executes query and returns the number of affected rows.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function Execute(query As Query) As Int32
      Using command = CreateCommand(query)
        Return command.ExecuteNonQuery()
      End Using
    End Function

    ''' <summary>
    ''' Executes insert statement and returns the number of affected rows.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function ExecuteInsert(query As InsertQuery) As Int32
      If query.ReadDbGeneratedValues Then
        Return ExecuteAndReadDbGeneratedValues(query)
      Else
        Return Execute(query)
      End If
    End Function

    ''' <summary>
    ''' Executes query and reads database generated values. Returns the number of affected rows.
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Private Function ExecuteAndReadDbGeneratedValues(query As InsertQuery) As Int32
      Dim reader = EntityReaderCache.GetDbGeneratedValuesReader(m_DialectProvider, m_DbContext.Model, query.Entity.GetType())

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            reader(dataReader, 0, query.Entity)
          Else
            Throw New Exception($"Unable to read DB generated values for '{query.Entity.GetType()}' entity.")
          End If
        End Using
      End Using

      Return 1
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function QueryFirstOrDefault(Of T)(query As Query) As T
      Dim value As T = Nothing
      Dim resultType = GetType(T)
      Dim isValueTuple = Types.IsValueTupleOrNullableValueTuple(resultType)
      Dim isModel = Not isValueTuple AndAlso Types.IsProbablyModel(resultType)

      Dim reader As Func(Of IDataReader, CustomEntityReadInfo(), T) = Nothing
      Dim customEntityInfos As CustomEntityReadInfo() = Nothing

      Using command = CreateCommand(query)
        If isValueTuple OrElse isModel Then
          Using dataReader = command.ExecuteReader()
            If dataReader.Read() Then
              reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, resultType)

              If isValueTuple Then
                customEntityInfos = CustomEntityReadInfo.CreateForValueTupleType(m_DialectProvider, m_DbContext.Model, resultType)
              Else
                customEntityInfos = CustomEntityReadInfo.CreateForModelType(m_DialectProvider, m_DbContext.Model, resultType)
              End If

              value = DirectCast(reader(dataReader, customEntityInfos), T)
            End If
          End Using
        Else
          ' we could use ValueType reader to avoid (un)boxing, but creating it might take more time/resources
          value = m_DialectProvider.DbValueConversion.FromDbValue(Of T)(command.ExecuteScalar())
        End If
      End Using

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns list of records.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function QueryList(Of T)(query As Query) As List(Of T)
      Dim values = New List(Of T)
      Dim resultType = GetType(T)
      Dim isValueTuple = Types.IsValueTupleOrNullableValueTuple(resultType)
      Dim isModel = Not isValueTuple AndAlso Types.IsProbablyModel(resultType)

      Dim reader As Func(Of IDataReader, CustomEntityReadInfo(), T) = Nothing
      Dim customEntityInfos As CustomEntityReadInfo() = Nothing

      If isValueTuple Then
        reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, resultType)
        customEntityInfos = CustomEntityReadInfo.CreateForValueTupleType(m_DialectProvider, m_DbContext.Model, resultType)
      ElseIf isModel Then
        reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, resultType)
        customEntityInfos = CustomEntityReadInfo.CreateForModelType(m_DialectProvider, m_DbContext.Model, resultType)
      End If

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            If isValueTuple OrElse isModel Then
              Dim value = DirectCast(reader(dataReader, customEntityInfos), T)
              values.Add(value)
            Else
              ' we could use ValueType reader to avoid (un)boxing, but creating it might take more time/resources
              Dim value = m_DialectProvider.DbValueConversion.FromDbValue(Of T)(dataReader.GetValue(0))
              values.Add(value)
            End If
          End While
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function ReadFirstOrDefault(Of T)(query As SelectQuery, behavior As CollectionNavigationFillBehavior) As T
      If query.Model.ContainsJoins() Then
        Dim entityInfos = EntityReadInfoCollection.Create(m_DialectProvider, query.Model)

        If entityInfos.HasCollectionNavigation Then
          Return ReadJoinedFirstOrDefaultWithCollectionNavigation(Of T)(query, entityInfos, behavior)
        Else
          Return ReadJoinedFirstOrDefaultWithoutCollectionNavigation(Of T)(query, entityInfos)
        End If
      Else
        Return ReadSimpleFirstOrDefault(Of T)(query)
      End If
    End Function

    ''' <summary>
    ''' Executes query and returns list of records.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function ReadList(Of T)(query As SelectQuery) As List(Of T)
      If query.Model.ContainsJoins() Then
        Dim entityInfos = EntityReadInfoCollection.Create(m_DialectProvider, query.Model)

        If entityInfos.HasCollectionNavigation Then
          Return ReadJoinedListWithCollectionNavigation(Of T)(query, entityInfos)
        Else
          Return ReadJoinedListWithoutCollectionNavigation(Of T)(query, entityInfos)
        End If
      Else
        Return ReadSimpleList(Of T)(query)
      End If
    End Function

    ''' <summary>
    ''' Executes query and returns first custom entity record or default.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function ReadCustomFirstOrDefault(Of T)(query As SelectQuery) As T
      Dim reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, GetType(T))
      Dim customEntityInfos = CustomEntityReadInfo.Create(m_DialectProvider, query.Model)

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(reader(dataReader, customEntityInfos), T)
            ' NOTE - ResetDbPropertyModifiedTracking is called in reader
          End If
        End Using
      End Using

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value. No joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Private Function ReadSimpleFirstOrDefault(Of T)(query As SelectQuery) As T
      Dim reader = EntityReaderCache.GetReader(m_DialectProvider, m_DbContext.Model, GetType(T))
      Dim includedColumns = query.Model.GetFirstEntity().IncludedColumns

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(reader(dataReader, 0, includedColumns), T)
            ResetDbPropertyModifiedTracking(value)
          End If
        End Using
      End Using

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value. Only 1:1 joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <param name="entityInfos"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefaultWithoutCollectionNavigation(Of T)(query As SelectQuery, entityInfos As EntityReadInfoCollection) As T
      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(Read(entityInfos, entityInfos.Items(0), dataReader, Nothing), T)
          End If
        End Using
      End Using

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value. 1:N joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <param name="entityInfos"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefaultWithCollectionNavigation(Of T)(query As SelectQuery, entityInfos As EntityReadInfoCollection, behavior As CollectionNavigationFillBehavior) As T
      Dim cache = New ReaderEntityValueCache(entityInfos.Count)
      Dim pks = New Object(entityInfos.Count - 1) {}

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            entityInfos.FillPks(dataReader, pks)
            value = DirectCast(Read(entityInfos, entityInfos.Items(0), cache, pks, dataReader, Nothing), T)
          End If

          Dim key = entityInfos.GetChainKey(0, pks)

          If value IsNot Nothing AndAlso Not behavior = CollectionNavigationFillBehavior.ProcessOnlyFirstRow Then
            Dim processUntilMainEntityChange = behavior = CollectionNavigationFillBehavior.ProcessUntilMainEntityChange

            While dataReader.Read()
              entityInfos.FillPks(dataReader, pks)

              Dim sameMainEntity = key = entityInfos.GetChainKey(0, pks)

              If sameMainEntity Then
                Read(entityInfos, entityInfos.Items(0), cache, pks, dataReader, Nothing)
              ElseIf processUntilMainEntityChange Then
                Exit While
              End If
            End While
          End If
        End Using
      End Using

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns list of records.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function ReadCustomList(Of T)(query As SelectQuery) As List(Of T)
      Dim reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, GetType(T))
      Dim customEntityInfos = CustomEntityReadInfo.Create(m_DialectProvider, query.Model)

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim value = DirectCast(reader(dataReader, customEntityInfos), T)
            ' NOTE - ResetDbPropertyModifiedTracking is called in reader
            values.Add(value)
          End While
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns list of records. No joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Private Function ReadSimpleList(Of T)(query As SelectQuery) As List(Of T)
      Dim reader = EntityReaderCache.GetReader(m_DialectProvider, m_DbContext.Model, GetType(T))
      Dim includedColumns = query.Model.GetFirstEntity().IncludedColumns

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim value = DirectCast(reader(dataReader, 0, includedColumns), T)
            ResetDbPropertyModifiedTracking(value)
            values.Add(value)
          End While
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns lists of records. Only 1:1 joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <param name="entityInfos"></param>
    ''' <returns></returns>
    Private Function ReadJoinedListWithoutCollectionNavigation(Of T)(query As SelectQuery, entityInfos As EntityReadInfoCollection) As List(Of T)
      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim masterValue = Read(entityInfos, entityInfos.Items(0), dataReader, Nothing)

            If masterValue IsNot Nothing Then
              values.Add(DirectCast(masterValue, T))
            End If
          End While
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns lists of records. 1:N joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <param name="entityInfos"></param>
    ''' <returns></returns>
    Private Function ReadJoinedListWithCollectionNavigation(Of T)(query As SelectQuery, entityInfos As EntityReadInfoCollection) As List(Of T)
      Dim cache = New ReaderEntityValueCache(entityInfos.Count)
      Dim pks = New Object(entityInfos.Count - 1) {}

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            entityInfos.FillPks(dataReader, pks)

            Dim masterValue = Read(entityInfos, entityInfos.Items(0), cache, pks, dataReader, Nothing)

            If masterValue IsNot Nothing Then
              values.Add(DirectCast(masterValue, T))
            End If
          End While
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Reads entity record. Only 1:1 joins are present in the query.
    ''' </summary>
    ''' <param name="entityInfos"></param>
    ''' <param name="entityInfo"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringValue"></param>
    ''' <returns></returns>
    Private Function Read(entityInfos As EntityReadInfoCollection, entityInfo As EntityReadInfo, dataReader As IDataReader, declaringValue As Object) As Object
      Dim value As Object
      Dim entityIndex = entityInfo.Entity.Index

      If entityInfo.Entity.IsExcludedOrIgnored Then
        Return Nothing
      End If

      If Not entityInfo.ContainsPKReader(dataReader, entityInfo.ReaderIndex, entityInfo.PKOffsets) Then
        Return Nothing
      End If

      value = entityInfo.Reader(dataReader, entityInfo.ReaderIndex, entityInfo.Entity.IncludedColumns)
      FillRelationships(entityInfo, value, declaringValue)

      If entityInfo.HasRelatedEntities Then
        For i = 0 To entityInfo.RelatedEntities.Count - 1
          Dim index = entityInfo.RelatedEntities(i)
          Dim relatedEntity = entityInfos.Items(index)
          Read(entityInfos, relatedEntity, dataReader, value)
        Next
      End If

      ResetDbPropertyModifiedTracking(value)

      Return value
    End Function

    ''' <summary>
    ''' Reads entity record. 1:N joins are present in the query.
    ''' </summary>
    ''' <param name="entityInfos"></param>
    ''' <param name="entityInfo"></param>
    ''' <param name="cache"></param>
    ''' <param name="pks"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringValue"></param>
    ''' <returns></returns>
    Private Function Read(entityInfos As EntityReadInfoCollection, entityInfo As EntityReadInfo, cache As ReaderEntityValueCache, pks As Object(), dataReader As IDataReader, declaringValue As Object) As Object
      Dim value As Object = Nothing
      Dim entityIndex = entityInfo.Entity.Index
      Dim valueFromCache = False

      If entityInfo.Entity.IsExcludedOrIgnored Then
        Return Nothing
      End If

      If pks(entityIndex) Is Nothing Then
        Return Nothing
      End If

      Dim key = entityInfos.GetChainKey(entityIndex, pks)

      If cache.TryGetValue(entityIndex, key, value) Then
        valueFromCache = True
      Else
        value = entityInfo.Reader(dataReader, entityInfo.ReaderIndex, entityInfo.Entity.IncludedColumns)
        cache.AddValue(entityIndex, key, value)
        FillRelationships(entityInfo, value, declaringValue)
      End If

      If entityInfo.HasRelatedEntities Then
        For i = 0 To entityInfo.RelatedEntities.Count - 1
          Dim index = entityInfo.RelatedEntities(i)
          Dim relatedEntity = entityInfos.Items(index)
          Read(entityInfos, relatedEntity, cache, pks, dataReader, value)
        Next
      End If

      If valueFromCache Then
        Return Nothing
      Else
        ResetDbPropertyModifiedTracking(value)
        Return value
      End If
    End Function

    ''' <summary>
    ''' Resets database property modified tracking on an entity if it implements <see cref="IHasDbPropertyModifiedTracking"/>.
    ''' </summary>
    ''' <param name="obj"></param>
    Private Sub ResetDbPropertyModifiedTracking(obj As Object)
      If TypeOf obj Is IHasDbPropertyModifiedTracking Then
        DirectCast(obj, IHasDbPropertyModifiedTracking).ResetDbPropertyModifiedTracking()
      End If
    End Sub

    ''' <summary>
    ''' Fills relationhip properties with instances of related entities.
    ''' </summary>
    ''' <param name="entityReadInfo"></param>
    ''' <param name="value"></param>
    ''' <param name="declaringValue"></param>
    Private Sub FillRelationships(entityReadInfo As EntityReadInfo, value As Object, declaringValue As Object)
      If entityReadInfo.HasCollectionNavigation Then
        For i = 0 To entityReadInfo.CollectionInitializers.Count - 1
          entityReadInfo.CollectionInitializers(i).Invoke(value)
        Next
      End If

      If entityReadInfo.RelationshipSetter IsNot Nothing Then
        entityReadInfo.RelationshipSetter.Invoke(declaringValue, value)
      End If
    End Sub

    ''' <summary>
    ''' Creates database command.
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Private Function CreateCommand(query As Query) As DbCommand
      Dim command = m_DbContext.Database.Connection.CreateCommand()
      command.CommandText = query.Sql
      command.Transaction = m_DbContext.Database.Transaction

      Dim timeout = m_DbContext.Options.CommandTimeout
      If timeout.HasValue Then
        command.CommandTimeout = timeout.Value
      End If

      Dim count = query.Parameters.Count
      Dim parameters = New DbParameter(count - 1) {}

      For i = 0 To count - 1
        Dim p = query.Parameters(i)

        Dim parameter = command.CreateParameter()
        parameter.ParameterName = p.Name

        If p.Value Is Nothing Then
          parameter.Value = DBNull.Value
        Else
          parameter.Value = p.Value
        End If

        If p.DbType.HasValue Then
          parameter.DbType = p.DbType.Value
        End If

        parameters(i) = parameter
      Next

      command.Parameters.AddRange(parameters)

      m_DbContext.NotifyCommandExecution(command)

      Return command
    End Function

  End Class
End Namespace