Imports System.Data
Imports System.Data.Common
Imports Yamo
Imports Yamo.Infrastructure

' TODO: SIP - rewrite as static class (one allocation less...)?

Namespace Internal.Query

  Public Class QueryExecutor

    Private m_DbContext As DbContext

    Private m_DialectProvider As SqlDialectProvider

    Sub New(context As DbContext)
      m_DbContext = context
      m_DialectProvider = m_DbContext.Options.DialectProvider
    End Sub

    Public Function ExecuteNonQuery(query As Query) As Int32
      Using command = CreateCommand(query)
        Return command.ExecuteNonQuery()
      End Using
    End Function

    Public Function ExecuteScalar(Of T)(query As Query) As T
      Using command = CreateCommand(query)
        ' TODO: SIP - use ValueType reader instead?
        Return m_DialectProvider.DbValueConversion.FromDbValue(Of T)(command.ExecuteScalar())
      End Using
    End Function

    Public Function ExecuteInsert(query As InsertQuery) As Int32
      If query.ReadDbGeneratedValues Then
        Return ExecuteAndReadDbGeneratedValues(query)
      Else
        Return ExecuteNonQuery(query)
      End If
    End Function

    Public Function ReadFirstOrDefault(Of T)(query As SelectQuery) As T
      If query.Model.ContainsJoins() Then
        ' TODO: SIP - implement
        'Return ReadJoinedList(Of T)(query)
        Throw New NotSupportedException("Calling ReadFirstOrDefault on joined query is not supported right now.")
      Else
        Return ReadSimpleFirstOrDefault(Of T)(query)
      End If
    End Function

    Public Function ReadList(Of T)(query As SelectQuery) As List(Of T)
      If query.Model.ContainsJoins() Then
        Return ReadJoinedList(Of T)(query)
      Else
        Return ReadSimpleList(Of T)(query)
      End If
    End Function

    Private Function CreateCommand(query As Query) As DbCommand
      Dim command = m_DbContext.Database.Connection.CreateCommand()
      command.CommandText = query.Sql
      command.Transaction = m_DbContext.Database.Transaction

      Dim timeout = m_DbContext.Options.CommandTimeout
      If timeout.HasValue Then
        command.CommandTimeout = timeout.Value
      End If

      For Each p In query.Parameters
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

        command.Parameters.Add(parameter)
      Next

      m_DbContext.NotifyCommandExecution(command)

      Return command
    End Function

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

    Public Function ReadCustomFirstOrDefault(Of T)(query As SelectQuery) As T
      Dim reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, GetType(T))
      Dim customEntityInfos = CustomEntityReadInfo.Create(m_DialectProvider, query.Model)

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(reader(dataReader, customEntityInfos), T)
            ' NOTE - ResetPropertyModifiedTracking is called in reader
          End If
        End Using
      End Using

      Return value
    End Function

    Private Function ReadSimpleFirstOrDefault(Of T)(query As SelectQuery) As T
      Dim reader = EntityReaderCache.GetReader(m_DialectProvider, m_DbContext.Model, GetType(T))
      Dim includedColumns = query.Model.GetFirstEntity().IncludedColumns

      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            value = DirectCast(reader(dataReader, 0, includedColumns), T)
            ResetPropertyModifiedTracking(value)
          End If
        End Using
      End Using

      Return value
    End Function

    Public Function ReadCustomList(Of T)(query As SelectQuery) As List(Of T)
      Dim reader = CustomResultReaderCache.GetResultFactory(Of T)(m_DbContext.Model, GetType(T))
      Dim customEntityInfos = CustomEntityReadInfo.Create(m_DialectProvider, query.Model)

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim value = DirectCast(reader(dataReader, customEntityInfos), T)
            ' NOTE - ResetPropertyModifiedTracking is called in reader
            values.Add(value)
          End While
        End Using
      End Using

      Return values
    End Function

    Private Function ReadSimpleList(Of T)(query As SelectQuery) As List(Of T)
      Dim reader = EntityReaderCache.GetReader(m_DialectProvider, m_DbContext.Model, GetType(T))
      Dim includedColumns = query.Model.GetFirstEntity().IncludedColumns

      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          While dataReader.Read()
            Dim value = DirectCast(reader(dataReader, 0, includedColumns), T)
            ResetPropertyModifiedTracking(value)
            values.Add(value)
          End While
        End Using
      End Using

      Return values
    End Function

    Private Function ReadJoinedList(Of T)(query As SelectQuery) As List(Of T)
      Dim entityInfos = EntityReadInfoCollection.Create(m_DialectProvider, query.Model)

      If entityInfos.HasCollectionNavigation Then
        Return ReadJoinedListWithCollectionNavigation(Of T)(query, entityInfos)
      Else
        Return ReadJoinedListWithoutCollectionNavigation(Of T)(query, entityInfos)
      End If
    End Function

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

    Private Function Read(entityInfos As EntityReadInfoCollection, entityInfo As EntityReadInfo, dataReader As IDataReader, declaringValue As Object) As Object
      Dim value As Object
      Dim entityIndex = entityInfo.Entity.Index

      If entityInfo.Entity.IsExcluded Then
        Return Nothing
      End If

      If Not entityInfo.ContainsPKReader(dataReader, entityInfo.ReaderIndex, entityInfo.PKOffsets) Then
        Return Nothing
      End If

      value = entityInfo.Reader(dataReader, entityInfo.ReaderIndex, entityInfo.Entity.IncludedColumns)
      FillRelationships(entityInfo, value, declaringValue)

      If entityInfo.HasRelatedEntities Then
        For Each index In entityInfo.RelatedEntities
          Dim relatedEntity = entityInfos.Items(index)
          Read(entityInfos, relatedEntity, dataReader, value)
        Next
      End If

      ResetPropertyModifiedTracking(value)

      Return value
    End Function

    Private Function ReadJoinedListWithCollectionNavigation(Of T)(query As SelectQuery, entityInfos As EntityReadInfoCollection) As List(Of T)
      Dim cache = New ReaderEntityValueCache(entityInfos.Count)
      Dim pks = New Int32?(entityInfos.Count - 1) {}

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

    Private Function Read(entityInfos As EntityReadInfoCollection, entityInfo As EntityReadInfo, cache As ReaderEntityValueCache, pks As Int32?(), dataReader As IDataReader, declaringValue As Object) As Object
      Dim value As Object
      Dim entityIndex = entityInfo.Entity.Index
      Dim valueFromCache = False

      If entityInfo.Entity.IsExcluded Then
        Return Nothing
      End If

      If Not pks(entityIndex).HasValue Then
        Return Nothing
      End If

      Dim key = entityInfos.GetChainKey(entityIndex, pks)

      If cache.Contains(entityIndex, key) Then
        value = cache.GetValue(entityIndex, key)
        valueFromCache = True
      Else
        value = entityInfo.Reader(dataReader, entityInfo.ReaderIndex, entityInfo.Entity.IncludedColumns)
        cache.AddValue(entityIndex, key, value)
        FillRelationships(entityInfo, value, declaringValue)
      End If

      If entityInfo.HasRelatedEntities Then
        For Each index In entityInfo.RelatedEntities
          Dim relatedEntity = entityInfos.Items(index)
          Read(entityInfos, relatedEntity, cache, pks, dataReader, value)
        Next
      End If

      If valueFromCache Then
        Return Nothing
      Else
        ResetPropertyModifiedTracking(value)
        Return value
      End If
    End Function

    Private Sub ResetPropertyModifiedTracking(obj As Object)
      If TypeOf obj Is IHasPropertyModifiedTracking Then
        DirectCast(obj, IHasPropertyModifiedTracking).ResetPropertyModifiedTracking()
      End If
    End Sub

    Private Sub FillRelationships(entityReadInfo As EntityReadInfo, value As Object, declaringValue As Object)
      If entityReadInfo.HasCollectionNavigation Then
        For Each collectionInitializer In entityReadInfo.CollectionInitializers
          collectionInitializer(value)
        Next
      End If

      If entityReadInfo.RelationshipSetter IsNot Nothing Then
        entityReadInfo.RelationshipSetter.Invoke(declaringValue, value)
      End If
    End Sub

  End Class
End Namespace