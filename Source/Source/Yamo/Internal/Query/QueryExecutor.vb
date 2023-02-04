Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
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
    Sub New(<DisallowNull> context As DbContext)
      m_DbContext = context
      m_DialectProvider = m_DbContext.Options.DialectProvider
    End Sub

    ''' <summary>
    ''' Executes query and returns the number of affected rows.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function Execute(<DisallowNull> query As Query) As Int32
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
    Public Function ExecuteInsert(<DisallowNull> query As InsertQuery) As Int32
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
    Private Function ExecuteAndReadDbGeneratedValues(<DisallowNull> query As InsertQuery) As Int32
      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            Dim dataReaderType = dataReader.GetType()
            Dim reader = EntityReaderCache.GetDbGeneratedValuesReader(dataReaderType, m_DialectProvider, m_DbContext.Model, query.Entity.GetType())
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
    Public Function QueryFirstOrDefault(Of T)(<DisallowNull> query As Query) As <MaybeNull> T
      Dim value As T = Nothing
      Dim resultType = GetType(T)
      Dim isObjectArray = resultType Is GetType(Object())
      Dim sqlResult = TryCreateSqlResult(m_DbContext.Model, resultType)
      Dim isValueTupleOrEntity = TypeOf sqlResult Is ValueTupleSqlResult OrElse TypeOf sqlResult Is EntitySqlResult

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            If isValueTupleOrEntity Then
              Dim dataReaderType = dataReader.GetType()
              Dim reader = SqlResultReaderCache.GetReader(Of T)(dataReaderType, m_DbContext.Model, sqlResult)
              Dim readerData = ReaderDataFactory.Create(dataReaderType, m_DialectProvider, m_DbContext.Model, sqlResult)
              value = DirectCast(reader(dataReader, readerData), T)

            ElseIf isObjectArray Then
              Dim data = New Object(dataReader.FieldCount - 1) {}
              dataReader.GetValues(data)
              value = DirectCast(DirectCast(data, Object), T)

            Else
              ' as a performance optimization, TryCreateSqlResult doesn't return ScalarValueSqlResult, we don't use ScalarValueSqlResultReaderData and access the reader directly
              Dim dataReaderType = dataReader.GetType()
              Dim reader = ValueTypeReaderCache.GetReader(Of T)(dataReaderType, m_DialectProvider, m_DbContext.Model)
              value = reader(dataReader, 0)
            End If
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
    Public Function QueryList(Of T)(<DisallowNull> query As Query) As List(Of T)
      Dim values = New List(Of T)
      Dim resultType = GetType(T)
      Dim isObjectArray = resultType Is GetType(Object())
      Dim sqlResult = TryCreateSqlResult(m_DbContext.Model, resultType)
      Dim isValueTupleOrEntity = TypeOf sqlResult Is ValueTupleSqlResult OrElse TypeOf sqlResult Is EntitySqlResult

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If isValueTupleOrEntity Then
            Dim dataReaderType = dataReader.GetType()
            Dim reader = SqlResultReaderCache.GetReader(Of T)(dataReaderType, m_DbContext.Model, sqlResult)
            Dim readerData = ReaderDataFactory.Create(dataReaderType, m_DialectProvider, m_DbContext.Model, sqlResult)

            While dataReader.Read()
              Dim value = DirectCast(reader(dataReader, readerData), T)
              values.Add(value)
            End While

          ElseIf isObjectArray Then
            While dataReader.Read()
              Dim data = New Object(dataReader.FieldCount - 1) {}
              dataReader.GetValues(data)
              values.Add(DirectCast(DirectCast(data, Object), T))
            End While

          Else
            ' as a performance optimization, TryCreateSqlResult doesn't return ScalarValueSqlResult, we don't use ScalarValueSqlResultReaderData and access the reader directly
            Dim dataReaderType = dataReader.GetType()
            Dim reader = ValueTypeReaderCache.GetReader(Of T)(dataReaderType, m_DialectProvider, m_DbContext.Model)

            While dataReader.Read()
              Dim value = reader(dataReader, 0)
              values.Add(value)
            End While
          End If
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
    Public Function ReadFirstOrDefault(Of T)(<DisallowNull> query As SelectQuery, behavior As CollectionNavigationFillBehavior) As <MaybeNull> T
      If query.Model.ContainsJoins() Then
        Return ReadJoinedFirstOrDefault(Of T)(query, behavior)
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
    Public Function ReadList(Of T)(<DisallowNull> query As SelectQuery) As List(Of T)
      If query.Model.ContainsJoins() Then
        Return ReadJoinedList(Of T)(query)
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
    Public Function ReadCustomFirstOrDefault(Of T)(<DisallowNull> query As SelectQuery) As <MaybeNull> T
      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            Dim dataReaderType = dataReader.GetType()
            Dim sqlResult = query.Model.SqlResult
            Dim reader = SqlResultReaderCache.GetReader(Of T)(dataReaderType, m_DbContext.Model, sqlResult)
            Dim readerData = ReaderDataFactory.Create(dataReaderType, m_DialectProvider, m_DbContext.Model, sqlResult)

            If readerData.ContainsNonNullColumnCheck Then
              If SqlResultReader.ContainsNonNullColumn(dataReader, readerData.ReaderIndex, sqlResult.GetColumnCount()) Then
                value = DirectCast(reader(dataReader, readerData), T)
                ' NOTE - Initialize is called in the reader
                ' NOTE - ResetDbPropertyModifiedTracking is called in the reader
              End If

            Else
              value = DirectCast(reader(dataReader, readerData), T)
              ' NOTE - Initialize is called in the reader
              ' NOTE - ResetDbPropertyModifiedTracking is called in the reader
            End If

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
      Dim value As T = Nothing

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          If dataReader.Read() Then
            Dim dataReaderType = dataReader.GetType()
            Dim entity = query.Model.MainEntity
            Dim sqlResult = query.Model.SqlResult
            Dim readerData = ReaderDataFactory.CreateAuto(dataReaderType, m_DialectProvider, m_DbContext.Model, entity, sqlResult)
            Dim includedColumns = entity.IncludedColumns

            If TypeOf readerData.ReaderData Is EntitySqlResultReaderData Then
              ' entity
              Dim entitySqlResultReaderData = DirectCast(readerData.ReaderData, EntitySqlResultReaderData)
              Dim reader = entitySqlResultReaderData.Reader

              If entity.TableSourceIsSubquery Then
                Dim containsPKReader = entitySqlResultReaderData.ContainsPKReader

                If containsPKReader(dataReader, entitySqlResultReaderData.ReaderIndex, entitySqlResultReaderData.PKOffsets) Then
                  value = DirectCast(reader(dataReader, 0, includedColumns), T)
                Else
                  Return Nothing
                End If

              Else
                ' record should always be present, so we can skip PK check
                value = DirectCast(reader(dataReader, 0, includedColumns), T)
              End If

            Else
              ' custom SQL result
              If readerData.ReaderData.ContainsNonNullColumnCheck Then
                If Not SqlResultReader.ContainsNonNullColumn(dataReader, readerData.ReaderData.ReaderIndex, sqlResult.GetColumnCount()) Then
                  Return Nothing
                End If
              End If

              Dim reader = SqlResultReaderCache.GetReader(dataReaderType, m_DbContext.Model, sqlResult)
              value = DirectCast(reader(dataReader, readerData.ReaderData), T)
            End If

            Initialize(value)
            FillIncluded(readerData, dataReaderType, dataReader, value)
            ResetDbPropertyModifiedTracking(value)
          End If
        End Using
      End Using

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value. Joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefault(Of T)(query As SelectQuery, behavior As CollectionNavigationFillBehavior) As T
      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          Dim dataReaderType = dataReader.GetType()
          Dim readerDataCollection = ReaderDataFactory.CreateAuto(dataReaderType, m_DialectProvider, m_DbContext.Model, query.Model.GetEntities())

          If readerDataCollection.HasCollectionNavigation Then
            Return ReadJoinedFirstOrDefaultWithCollectionNavigation(Of T)(dataReaderType, dataReader, readerDataCollection, behavior)
          Else
            Return ReadJoinedFirstOrDefaultWithoutCollectionNavigation(Of T)(dataReaderType, dataReader, readerDataCollection)
          End If
        End Using
      End Using
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value. Only 1:1 joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="readerDataCollection"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefaultWithoutCollectionNavigation(Of T)(dataReaderType As Type, dataReader As DbDataReader, readerDataCollection As AutoModeSqlResultReaderDataCollection) As T
      Dim value As T = Nothing

      If dataReader.Read() Then
        value = DirectCast(Read(readerDataCollection, readerDataCollection.Items(0), dataReaderType, dataReader, Nothing), T)
      End If

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns first record or a default value. 1:N joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="readerDataCollection"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function ReadJoinedFirstOrDefaultWithCollectionNavigation(Of T)(dataReaderType As Type, dataReader As DbDataReader, readerDataCollection As AutoModeSqlResultReaderDataCollection, behavior As CollectionNavigationFillBehavior) As T
      Dim cache = New ReaderEntityValueCache(readerDataCollection.Count)
      Dim pks = New Object(readerDataCollection.Count - 1) {}

      Dim value As T = Nothing

      If dataReader.Read() Then
        readerDataCollection.FillPks(dataReader, pks)
        value = DirectCast(Read(readerDataCollection, readerDataCollection.Items(0), cache, pks, dataReaderType, dataReader, Nothing), T)
      End If

      If value IsNot Nothing AndAlso Not behavior = CollectionNavigationFillBehavior.ProcessOnlyFirstRow Then
        Dim key = readerDataCollection.GetChainKey(0, pks)
        Dim processUntilMainEntityChange = behavior = CollectionNavigationFillBehavior.ProcessUntilMainEntityChange

        While dataReader.Read()
          readerDataCollection.FillPks(dataReader, pks)

          Dim sameMainEntity = key = readerDataCollection.GetChainKey(0, pks)

          If sameMainEntity Then
            Read(readerDataCollection, readerDataCollection.Items(0), cache, pks, dataReaderType, dataReader, Nothing)
          ElseIf processUntilMainEntityChange Then
            Exit While
          End If
        End While
      End If

      Return value
    End Function

    ''' <summary>
    ''' Executes query and returns list of records.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Public Function ReadCustomList(Of T)(<DisallowNull> query As SelectQuery) As List(Of T)
      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          Dim dataReaderType = dataReader.GetType()
          Dim sqlResult = query.Model.SqlResult
          Dim reader = SqlResultReaderCache.GetReader(Of T)(dataReaderType, m_DbContext.Model, sqlResult)
          Dim readerData = ReaderDataFactory.Create(dataReaderType, m_DialectProvider, m_DbContext.Model, sqlResult)

          If readerData.ContainsNonNullColumnCheck Then
            While dataReader.Read()
              If SqlResultReader.ContainsNonNullColumn(dataReader, readerData.ReaderIndex, sqlResult.GetColumnCount()) Then
                Dim value = DirectCast(reader(dataReader, readerData), T)
                ' NOTE - Initialize is called in the reader
                ' NOTE - ResetDbPropertyModifiedTracking is called in the reader
                values.Add(value)
              Else
                values.Add(Nothing)
              End If
            End While

          Else
            While dataReader.Read()
              Dim value = DirectCast(reader(dataReader, readerData), T)
              ' NOTE - Initialize is called in the reader
              ' NOTE - ResetDbPropertyModifiedTracking is called in the reader
              values.Add(value)
            End While
          End If
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
      Dim values = New List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          Dim dataReaderType = dataReader.GetType()
          Dim entity = query.Model.MainEntity
          Dim sqlResult = query.Model.SqlResult
          Dim readerData = ReaderDataFactory.CreateAuto(dataReaderType, m_DialectProvider, m_DbContext.Model, entity, sqlResult)
          Dim includedColumns = entity.IncludedColumns

          If TypeOf readerData.ReaderData Is EntitySqlResultReaderData Then
            ' entity
            Dim entitySqlResultReaderData = DirectCast(readerData.ReaderData, EntitySqlResultReaderData)
            Dim reader = entitySqlResultReaderData.Reader

            If entity.TableSourceIsSubquery Then
              Dim containsPKReader = entitySqlResultReaderData.ContainsPKReader

              While dataReader.Read()
                If containsPKReader(dataReader, entitySqlResultReaderData.ReaderIndex, entitySqlResultReaderData.PKOffsets) Then
                  Dim value = DirectCast(reader(dataReader, 0, includedColumns), T)
                  Initialize(value)
                  FillIncluded(readerData, dataReaderType, dataReader, value)
                  ResetDbPropertyModifiedTracking(value)
                  values.Add(value)
                Else
                  values.Add(Nothing)
                End If
              End While

            Else
              ' record should always be present, so we can skip PK check
              While dataReader.Read()
                Dim value = DirectCast(reader(dataReader, 0, includedColumns), T)
                Initialize(value)
                FillIncluded(readerData, dataReaderType, dataReader, value)
                ResetDbPropertyModifiedTracking(value)
                values.Add(value)
              End While
            End If

          Else
            ' custom SQL result
            Dim reader = SqlResultReaderCache.GetReader(dataReaderType, m_DbContext.Model, sqlResult)

            If readerData.ReaderData.ContainsNonNullColumnCheck Then
              While dataReader.Read()
                If SqlResultReader.ContainsNonNullColumn(dataReader, readerData.ReaderData.ReaderIndex, sqlResult.GetColumnCount()) Then
                  Dim value = DirectCast(reader(dataReader, readerData.ReaderData), T)
                  Initialize(value)
                  FillIncluded(readerData, dataReaderType, dataReader, value)
                  ResetDbPropertyModifiedTracking(value)
                  values.Add(value)
                Else
                  values.Add(Nothing)
                End If
              End While

            Else
              While dataReader.Read()
                Dim value = DirectCast(reader(dataReader, readerData.ReaderData), T)
                Initialize(value)
                FillIncluded(readerData, dataReaderType, dataReader, value)
                ResetDbPropertyModifiedTracking(value)
                values.Add(value)
              End While
            End If
          End If
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns lists of records. Joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="query"></param>
    ''' <returns></returns>
    Private Function ReadJoinedList(Of T)(query As SelectQuery) As List(Of T)
      Dim values As List(Of T)

      Using command = CreateCommand(query)
        Using dataReader = command.ExecuteReader()
          Dim dataReaderType = dataReader.GetType()
          Dim readerDataCollection = ReaderDataFactory.CreateAuto(dataReaderType, m_DialectProvider, m_DbContext.Model, query.Model.GetEntities())

          If readerDataCollection.HasCollectionNavigation Then
            Return ReadJoinedListWithCollectionNavigation(Of T)(dataReaderType, dataReader, readerDataCollection)
          Else
            Return ReadJoinedListWithoutCollectionNavigation(Of T)(dataReaderType, dataReader, readerDataCollection)
          End If
        End Using
      End Using

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns lists of records. Only 1:1 joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="readerDataCollection"></param>
    ''' <returns></returns>
    Private Function ReadJoinedListWithoutCollectionNavigation(Of T)(dataReaderType As Type, dataReader As DbDataReader, readerDataCollection As AutoModeSqlResultReaderDataCollection) As List(Of T)
      Dim values = New List(Of T)

      While dataReader.Read()
        Dim masterValue = Read(readerDataCollection, readerDataCollection.Items(0), dataReaderType, dataReader, Nothing)

        If masterValue IsNot Nothing Then
          values.Add(DirectCast(masterValue, T))
        End If
      End While

      Return values
    End Function

    ''' <summary>
    ''' Executes query and returns lists of records. 1:N joins are present in the query.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="readerDataCollection"></param>
    ''' <returns></returns>
    Private Function ReadJoinedListWithCollectionNavigation(Of T)(dataReaderType As Type, dataReader As DbDataReader, readerDataCollection As AutoModeSqlResultReaderDataCollection) As List(Of T)
      Dim cache = New ReaderEntityValueCache(readerDataCollection.Count)
      Dim pks = New Object(readerDataCollection.Count - 1) {}

      Dim values = New List(Of T)

      While dataReader.Read()
        readerDataCollection.FillPks(dataReader, pks)

        Dim masterValue = Read(readerDataCollection, readerDataCollection.Items(0), cache, pks, dataReaderType, dataReader, Nothing)

        If masterValue IsNot Nothing Then
          values.Add(DirectCast(masterValue, T))
        End If
      End While

      Return values
    End Function

    ''' <summary>
    ''' Reads entity record. Only 1:1 joins are present in the query.
    ''' </summary>
    ''' <param name="readerDataCollection"></param>
    ''' <param name="readerData"></param>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringEntity"></param>
    ''' <returns></returns>
    Private Function Read(readerDataCollection As AutoModeSqlResultReaderDataCollection, readerData As AutoModeSqlResultReaderData, dataReaderType As Type, dataReader As DbDataReader, declaringEntity As Object) As Object
      Dim value As Object
      Dim entityIndex = readerData.Entity.Index

      If readerData.Entity.IsExcludedOrIgnored Then
        Return Nothing
      End If

      If TypeOf readerData.ReaderData Is EntitySqlResultReaderData Then
        ' entity
        Dim entitySqlResultReaderData = DirectCast(readerData.ReaderData, EntitySqlResultReaderData)

        If Not entitySqlResultReaderData.ContainsPKReader(dataReader, entitySqlResultReaderData.ReaderIndex, entitySqlResultReaderData.PKOffsets) Then
          Return Nothing
        End If

        value = entitySqlResultReaderData.Reader(dataReader, entitySqlResultReaderData.ReaderIndex, entitySqlResultReaderData.Entity.IncludedColumns)

      Else
        ' custom SQL result
        Dim sqlEntity = DirectCast(readerData.Entity, NonModelEntityBasedSqlEntity)
        Dim sqlResult = sqlEntity.Entity.SqlResult

        If readerData.ReaderData.ContainsNonNullColumnCheck Then
          If Not SqlResultReader.ContainsNonNullColumn(dataReader, readerData.ReaderData.ReaderIndex, sqlResult.GetColumnCount()) Then
            Return Nothing
          End If
        End If

        Dim reader = SqlResultReaderCache.GetReader(dataReaderType, m_DbContext.Model, sqlResult)
        value = reader(dataReader, readerData.ReaderData)

        If value Is Nothing Then
          Return Nothing
        End If
      End If

      Initialize(value)

      FillRelationships(readerData, declaringEntity, value)
      FillIncluded(readerData, dataReaderType, dataReader, value)

      If readerData.HasRelatedEntities Then
        For i = 0 To readerData.RelatedEntities.Count - 1
          Dim index = readerData.RelatedEntities(i)
          Dim relatedEntityReaderData = readerDataCollection.Items(index)
          Read(readerDataCollection, relatedEntityReaderData, dataReaderType, dataReader, value)
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
    ''' <param name="dataReaderType"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="declaringEntity"></param>
    ''' <returns></returns>
    Private Function Read(readerDataCollection As AutoModeSqlResultReaderDataCollection, readerData As AutoModeSqlResultReaderData, cache As ReaderEntityValueCache, pks As Object(), dataReaderType As Type, dataReader As DbDataReader, declaringEntity As Object) As Object
      Dim value As Object = Nothing
      Dim entityIndex = readerData.Entity.Index
      Dim valueFromCache = False

      If readerData.Entity.IsExcludedOrIgnored Then
        Return Nothing
      End If

      ' NOTE: PKs and chain keys are only supported by model entities

      If TypeOf readerData.ReaderData Is EntitySqlResultReaderData Then
        ' entity

        If pks(entityIndex) Is Nothing Then
          Return Nothing
        End If

        Dim key = readerDataCollection.GetChainKey(entityIndex, pks)

        If cache.TryGetValue(entityIndex, key, value) Then
          valueFromCache = True
        Else
          Dim entitySqlResultReaderData = DirectCast(readerData.ReaderData, EntitySqlResultReaderData)

          value = entitySqlResultReaderData.Reader(dataReader, entitySqlResultReaderData.ReaderIndex, entitySqlResultReaderData.Entity.IncludedColumns)

          Initialize(value)

          cache.AddValue(entityIndex, key, value)

          FillRelationships(readerData, declaringEntity, value)
          FillIncluded(readerData, dataReaderType, dataReader, value)
        End If

      Else
        ' custom SQL result
        Dim sqlEntity = DirectCast(readerData.Entity, NonModelEntityBasedSqlEntity)
        Dim sqlResult = sqlEntity.Entity.SqlResult

        If readerData.ReaderData.ContainsNonNullColumnCheck Then
          If Not SqlResultReader.ContainsNonNullColumn(dataReader, readerData.ReaderData.ReaderIndex, sqlResult.GetColumnCount()) Then
            Return Nothing
          End If
        End If

        Dim reader = SqlResultReaderCache.GetReader(dataReaderType, m_DbContext.Model, sqlResult)
        value = reader(dataReader, readerData.ReaderData)

        If value Is Nothing Then
          Return Nothing
        End If

        Initialize(value)

        FillRelationships(readerData, declaringEntity, value)
        FillIncluded(readerData, dataReaderType, dataReader, value)
      End If

      If readerData.HasRelatedEntities Then
        For i = 0 To readerData.RelatedEntities.Count - 1
          Dim index = readerData.RelatedEntities(i)
          Dim relatedEntityReaderData = readerDataCollection.Items(index)
          Read(readerDataCollection, relatedEntityReaderData, cache, pks, dataReaderType, dataReader, value)
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
    ''' Initialize entity if it implements <see cref="IInitializable"/>.
    ''' </summary>
    ''' <param name="obj"></param>
    Private Sub Initialize(obj As Object)
      If TypeOf obj Is IInitializable Then
        DirectCast(obj, IInitializable).Initialize()
      End If
    End Sub

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
    Private Sub FillRelationships(readerData As AutoModeSqlResultReaderData, declaringEntity As Object, relatedEntity As Object)
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
    ''' <param name="readerData"></param>
    ''' <param name="dataReader"></param>
    ''' <param name="dataReaderType"></param>
    ''' <param name="declaringEntity"></param>
    Private Sub FillIncluded(readerData As AutoModeSqlResultReaderData, dataReaderType As Type, dataReader As DbDataReader, declaringEntity As Object)
      If readerData.HasIncludedSqlResults Then
        For i = 0 To readerData.IncludedSqlResultsReaderData.Count - 1
          Dim includedSqlResultsReaderData = readerData.IncludedSqlResultsReaderData(i)

          Dim sqlResult = includedSqlResultsReaderData.ReaderData.SqlResult
          Dim reader = SqlResultReaderCache.GetReader(dataReaderType, m_DbContext.Model, sqlResult)
          Dim includedReaderData = includedSqlResultsReaderData.ReaderData

          Dim value As Object = Nothing

          If includedReaderData.ContainsNonNullColumnCheck Then
            If SqlResultReader.ContainsNonNullColumn(dataReader, includedReaderData.ReaderIndex, sqlResult.GetColumnCount()) Then
              value = reader(dataReader, includedReaderData)
            End If
          Else
            value = reader(dataReader, includedReaderData)
          End If

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

        Return New EntitySqlResult(New EntityBasedSqlEntity(entity))
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
          Return New EntitySqlResult(New EntityBasedSqlEntity(entity))
        End If
      End If

      Return New ScalarValueSqlResult(type)
    End Function

  End Class
End Namespace