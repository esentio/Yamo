﻿Imports System.Data
Imports System.Data.Common
Imports Yamo
Imports Yamo.Infrastructure
Imports Yamo.Internal.Helpers
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

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
      Dim sqlResult = TryCreateSqlResult(m_DbContext.Model, resultType)
      Dim isValueTupleOrEntity = TypeOf sqlResult Is ValueTupleSqlResult OrElse TypeOf sqlResult Is EntitySqlResult

      Using command = CreateCommand(query)
        If isValueTupleOrEntity Then
          Using dataReader = command.ExecuteReader()
            If dataReader.Read() Then
              Dim reader = SqlResultReaderCache.GetReader(Of T)(m_DbContext.Model, sqlResult)
              Dim readerData = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, sqlResult)
              value = DirectCast(reader(dataReader, readerData), T)
            End If
          End Using
        Else
          ' we could use ValueType reader to avoid (un)boxing, but creating it might take more time/resources (TryCreateSqlResult would need to return ScalarValueSqlResult)
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
      Dim sqlResult = TryCreateSqlResult(m_DbContext.Model, resultType)
      Dim isValueTupleOrEntity = TypeOf sqlResult Is ValueTupleSqlResult OrElse TypeOf sqlResult Is EntitySqlResult

      Dim reader As Func(Of IDataReader, ReaderDataBase, T) = Nothing
      Dim readerData As ReaderDataBase = Nothing

      If isValueTupleOrEntity Then
        reader = SqlResultReaderCache.GetReader(Of T)(m_DbContext.Model, sqlResult)
        readerData = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, sqlResult)
      End If

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            If isValueTupleOrEntity Then
              Dim value = DirectCast(reader(dataReader, readerData), T)
              values.Add(value)
            Else
              ' we could use ValueType reader to avoid (un)boxing, but creating it might take more time/resources (TryCreateSqlResult would need to return ScalarValueSqlResult)
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
        Dim readerDataCollection = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, query.Model.GetEntities())

        If readerDataCollection.HasCollectionNavigation Then
          Return ReadJoinedFirstOrDefaultWithCollectionNavigation(Of T)(query, readerDataCollection, behavior)
        Else
          Return ReadJoinedFirstOrDefaultWithoutCollectionNavigation(Of T)(query, readerDataCollection)
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
        Dim readerDataCollection = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, query.Model.GetEntities())

        If readerDataCollection.HasCollectionNavigation Then
          Return ReadJoinedListWithCollectionNavigation(Of T)(query, readerDataCollection)
        Else
          Return ReadJoinedListWithoutCollectionNavigation(Of T)(query, readerDataCollection)
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
      Dim reader = SqlResultReaderCache.GetReader(Of T)(m_DbContext.Model, query.Model.CustomSqlResult)
      Dim readerData = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, query.Model.CustomSqlResult)

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(reader(dataReader, readerData), T)
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
      Dim entity = query.Model.MainEntity
      Dim readerData = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, entity)
      Dim reader = readerData.Reader
      Dim includedColumns = entity.IncludedColumns

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(reader(dataReader, 0, includedColumns), T)
            FillIncluded(readerData, dataReader, value)
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
    ''' <param name="readerDataCollection"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefaultWithoutCollectionNavigation(Of T)(query As SelectQuery, readerDataCollection As EntitySqlResultReaderDataCollection) As T
      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(Read(readerDataCollection, readerDataCollection.Items(0), dataReader, Nothing), T)
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
    ''' <param name="readerDataCollection"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefaultWithCollectionNavigation(Of T)(query As SelectQuery, readerDataCollection As EntitySqlResultReaderDataCollection, behavior As CollectionNavigationFillBehavior) As T
      Dim cache = New ReaderEntityValueCache(readerDataCollection.Count)
      Dim pks = New Object(readerDataCollection.Count - 1) {}

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            readerDataCollection.FillPks(dataReader, pks)
            value = DirectCast(Read(readerDataCollection, readerDataCollection.Items(0), cache, pks, dataReader, Nothing), T)
          End If

          Dim key = readerDataCollection.GetChainKey(0, pks)

          If value IsNot Nothing AndAlso Not behavior = CollectionNavigationFillBehavior.ProcessOnlyFirstRow Then
            Dim processUntilMainEntityChange = behavior = CollectionNavigationFillBehavior.ProcessUntilMainEntityChange

            While dataReader.Read()
              readerDataCollection.FillPks(dataReader, pks)

              Dim sameMainEntity = key = readerDataCollection.GetChainKey(0, pks)

              If sameMainEntity Then
                Read(readerDataCollection, readerDataCollection.Items(0), cache, pks, dataReader, Nothing)
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
      Dim reader = SqlResultReaderCache.GetReader(Of T)(m_DbContext.Model, query.Model.CustomSqlResult)
      Dim readerData = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, query.Model.CustomSqlResult)

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim value = DirectCast(reader(dataReader, readerData), T)
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
      Dim entity = query.Model.MainEntity
      Dim readerData = ReaderDataFactory.Create(m_DialectProvider, m_DbContext.Model, entity)
      Dim reader = readerData.Reader
      Dim includedColumns = entity.IncludedColumns

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim value = DirectCast(reader(dataReader, 0, includedColumns), T)
            FillIncluded(readerData, dataReader, value)
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
    ''' <param name="readerDataCollection"></param>
    ''' <returns></returns>
    Private Function ReadJoinedListWithoutCollectionNavigation(Of T)(query As SelectQuery, readerDataCollection As EntitySqlResultReaderDataCollection) As List(Of T)
      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim masterValue = Read(readerDataCollection, readerDataCollection.Items(0), dataReader, Nothing)

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
    ''' <param name="readerDataCollection"></param>
    ''' <returns></returns>
    Private Function ReadJoinedListWithCollectionNavigation(Of T)(query As SelectQuery, readerDataCollection As EntitySqlResultReaderDataCollection) As List(Of T)
      Dim cache = New ReaderEntityValueCache(readerDataCollection.Count)
      Dim pks = New Object(readerDataCollection.Count - 1) {}

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            readerDataCollection.FillPks(dataReader, pks)

            Dim masterValue = Read(readerDataCollection, readerDataCollection.Items(0), cache, pks, dataReader, Nothing)

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
    ''' <param name="readerDataCollection"></param>
    ''' <param name="readerData"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringEntity"></param>
    ''' <returns></returns>
    Private Function Read(readerDataCollection As EntitySqlResultReaderDataCollection, readerData As EntitySqlResultReaderData, dataReader As IDataReader, declaringEntity As Object) As Object
      Dim value As Object
      Dim entityIndex = readerData.Entity.Index

      If readerData.Entity.IsExcludedOrIgnored Then
        Return Nothing
      End If

      If Not readerData.ContainsPKReader(dataReader, readerData.ReaderIndex, readerData.PKOffsets) Then
        Return Nothing
      End If

      value = readerData.Reader(dataReader, readerData.ReaderIndex, readerData.Entity.IncludedColumns)
      FillRelationships(readerData, declaringEntity, value)
      FillIncluded(readerData, dataReader, value)

      If readerData.HasRelatedEntities Then
        For i = 0 To readerData.RelatedEntities.Count - 1
          Dim index = readerData.RelatedEntities(i)
          Dim relatedEntityReaderData = readerDataCollection.Items(index)
          Read(readerDataCollection, relatedEntityReaderData, dataReader, value)
        Next
      End If

      ResetDbPropertyModifiedTracking(value)

      Return value
    End Function

    ''' <summary>
    ''' Reads entity record. 1:N joins are present in the query.
    ''' </summary>
    ''' <param name="readerDataCollection"></param>
    ''' <param name="readerData"></param>
    ''' <param name="cache"></param>
    ''' <param name="pks"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringEntity"></param>
    ''' <returns></returns>
    Private Function Read(readerDataCollection As EntitySqlResultReaderDataCollection, readerData As EntitySqlResultReaderData, cache As ReaderEntityValueCache, pks As Object(), dataReader As IDataReader, declaringEntity As Object) As Object
      Dim value As Object = Nothing
      Dim entityIndex = readerData.Entity.Index
      Dim valueFromCache = False

      If readerData.Entity.IsExcludedOrIgnored Then
        Return Nothing
      End If

      If pks(entityIndex) Is Nothing Then
        Return Nothing
      End If

      Dim key = readerDataCollection.GetChainKey(entityIndex, pks)

      If cache.TryGetValue(entityIndex, key, value) Then
        valueFromCache = True
      Else
        value = readerData.Reader(dataReader, readerData.ReaderIndex, readerData.Entity.IncludedColumns)
        cache.AddValue(entityIndex, key, value)
        FillRelationships(readerData, declaringEntity, value)
        FillIncluded(readerData, dataReader, value)
      End If

      If readerData.HasRelatedEntities Then
        For i = 0 To readerData.RelatedEntities.Count - 1
          Dim index = readerData.RelatedEntities(i)
          Dim relatedEntityReaderData = readerDataCollection.Items(index)
          Read(readerDataCollection, relatedEntityReaderData, cache, pks, dataReader, value)
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
    ''' <param name="readerData"></param>
    ''' <param name="declaringEntity"></param>
    ''' <param name="relatedEntity"></param>
    Private Sub FillRelationships(readerData As EntitySqlResultReaderData, declaringEntity As Object, relatedEntity As Object)
      If readerData.HasCollectionNavigation Then
        For i = 0 To readerData.CollectionInitializers.Count - 1
          readerData.CollectionInitializers(i).Invoke(relatedEntity)
        Next
      End If

      If readerData.HasRelationshipSetter Then
        readerData.RelationshipSetter.Invoke(declaringEntity, relatedEntity)
      End If
    End Sub

    ''' <summary>
    ''' Fills included properties with read values.
    ''' </summary>
    ''' <param name="entityReaderData"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringEntity"></param>
    Private Sub FillIncluded(entityReaderData As EntitySqlResultReaderData, dataReader As IDataReader, declaringEntity As Object)
      If entityReaderData.HasIncludedSqlResults Then
        For i = 0 To entityReaderData.IncludedSqlResultsReaderData.Count - 1
          Dim includedSqlResultsReaderData = entityReaderData.IncludedSqlResultsReaderData(i)

          Dim sqlResult = includedSqlResultsReaderData.ReaderData.SqlResult
          Dim reader = SqlResultReaderCache.GetReader(m_DbContext.Model, sqlResult)
          Dim readerData = includedSqlResultsReaderData.ReaderData

          Dim value = reader(dataReader, readerData)

          includedSqlResultsReaderData.Setter.Invoke(declaringEntity, value)
        Next
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

    ''' <summary>
    ''' Creates instance of <see cref="SqlResultBase"/>.<br/>
    ''' Only expected result is <see cref="ValueTupleSqlResult"/> or <see cref="EntitySqlResult"/>.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="resultType"></param>
    ''' <returns></returns>
    Private Function TryCreateSqlResult(model As Model, resultType As Type) As SqlResultBase
      ' NOTE: right now this should only be called from Query/QueryFirstOrDefault and only following types are supported:
      ' - nullable and non-nullable ValueTuples: with basic value-types or model entities as a field value
      ' - model entities

      If Helpers.Types.IsValueTupleOrNullableValueTuple(resultType) Then
        ' ValueTuple
        Dim valueTupleTypeInfo = Helpers.Types.GetValueTupleTypeInfo(resultType)
        Dim items = valueTupleTypeInfo.FlattenedArguments.Select(Function(x) CreateSqlResult(model, x)).ToArray()

        Return New ValueTupleSqlResult(resultType, items)
      ElseIf Helpers.Types.IsProbablyModel(resultType) Then
        ' entity model
        Dim entity = model.TryGetEntity(resultType)

        If entity Is Nothing Then
          Throw New Exception($"Unable to create result factory for type '{resultType}'. Only value tuples and model entities are supported.")
        End If

        Return New EntitySqlResult(New SqlEntity(entity))
      Else
        Return Nothing
      End If
    End Function

    ''' <summary>
    ''' Creates instance of <see cref="SqlResultBase"/>.<br/>
    ''' Only expected result is <see cref="EntitySqlResult"/> or <see cref="ScalarValueSqlResult"/>.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function CreateSqlResult(model As Model, type As Type) As SqlResultBase
      If Helpers.Types.IsProbablyModel(type) Then
        Dim entity = model.TryGetEntity(type)

        If entity IsNot Nothing Then
          Return New EntitySqlResult(New SqlEntity(entity))
        End If
      End If

      Return New ScalarValueSqlResult(type)
    End Function

  End Class
End Namespace