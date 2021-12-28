Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Reader data factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ReaderDataFactory

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataFactory"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderDataCollection"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="entities"></param>
    ''' <returns></returns>
    Public Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, entities As SqlEntity()) As EntitySqlResultReaderDataCollection
      Dim relationships = New(RelatedEntities As List(Of Int32), CollectionNavigations As List(Of CollectionNavigation))(entities.Length - 1) {}

      For index = 0 To entities.Length - 1
        Dim entity = entities(index)

        If entity.Relationship IsNot Nothing Then
          Dim declaringEntityIndex = entity.Relationship.DeclaringEntity.Index

          If relationships(declaringEntityIndex).RelatedEntities Is Nothing Then
            relationships(declaringEntityIndex).RelatedEntities = New List(Of Int32)
          End If

          relationships(declaringEntityIndex).RelatedEntities.Add(index)

          If entity.Relationship.IsReferenceNavigation Then
            ' do nothing here
          ElseIf entity.Relationship.IsCollectionNavigation Then
            If relationships(declaringEntityIndex).CollectionNavigations Is Nothing Then
              relationships(declaringEntityIndex).CollectionNavigations = New List(Of CollectionNavigation)
            End If

            relationships(declaringEntityIndex).CollectionNavigations.Add(DirectCast(entity.Relationship.RelationshipNavigation, CollectionNavigation))
          Else
            Throw New NotSupportedException($"Relationship of unknown type.")
          End If
        End If
      Next

      Dim readerDataItems = New EntitySqlResultReaderData(entities.Length - 1) {}
      Dim readerIndex = 0

      For index = 0 To entities.Length - 1
        Dim entityReaderIndex = readerIndex
        Dim entity = entities(index)
        Dim entityType = entity.Entity.EntityType

        Dim entityReader = EntityReaderCache.GetReader(dataReaderType, dialectProvider, model, entityType)
        Dim containsPKReader = EntityReaderCache.GetContainsPKReader(dataReaderType, dialectProvider, model, entityType)
        Dim pkOffsets = GetPKOffsets(entity)
        Dim pkReader = EntityReaderCache.GetPKReader(dataReaderType, dialectProvider, model, entityType)
        Dim relatedEntities = relationships(index).RelatedEntities

        Dim collectionNavigations = relationships(index).CollectionNavigations
        Dim collectionInitializers As Action(Of Object)() = Nothing

        If collectionNavigations IsNot Nothing Then
          ' LINQ not used for performance and allocation reasons
          collectionInitializers = New Action(Of Object)(collectionNavigations.Count - 1) {}

          For i = 0 To collectionNavigations.Count - 1
            collectionInitializers(i) = EntityMemberSetterCache.GetCollectionInitSetter(model, entityType, collectionNavigations(i))
          Next
        End If

        Dim relationshipSetter As Action(Of Object, Object) = Nothing

        If entity.Relationship IsNot Nothing Then
          relationshipSetter = EntityMemberSetterCache.GetSetter(model, entity.Relationship.DeclaringEntity.Entity.EntityType, entity.Relationship.RelationshipNavigation)
        End If

        readerIndex += entity.GetColumnCount()

        Dim includedSqlResultsReaderData As IncludedSqlResultReaderData() = Nothing
        Dim entityIncludedSqlResults = entity.IncludedSqlResults

        If entityIncludedSqlResults IsNot Nothing Then
          includedSqlResultsReaderData = New IncludedSqlResultReaderData(entityIncludedSqlResults.Count - 1) {}

          ' LINQ not used for performance and allocation reasons
          For j = 0 To entityIncludedSqlResults.Count - 1
            Dim entityIncludedSqlResult = entityIncludedSqlResults(j)
            Dim setter = EntityMemberSetterCache.GetSetter(model, entityType, entityIncludedSqlResult.PropertyName, entityIncludedSqlResult.Result.ResultType)
            Dim readData = Create(dataReaderType, dialectProvider, model, entityIncludedSqlResult.Result, readerIndex)
            includedSqlResultsReaderData(j) = New IncludedSqlResultReaderData(setter, readData)
            readerIndex += readData.GetColumnCount()
          Next
        End If

        Dim readerData = New EntitySqlResultReaderData(New EntitySqlResult(entity), entityReaderIndex, entityReader, containsPKReader, pkOffsets, pkReader, relatedEntities, collectionInitializers, relationshipSetter, includedSqlResultsReaderData)
        readerDataItems(index) = readerData
      Next

      Return New EntitySqlResultReaderDataCollection(readerDataItems)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    Public Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, entity As SqlEntity) As EntitySqlResultReaderData
      Dim readerIndex = 0
      Dim entityReaderIndex = 0
      Dim entityType = entity.Entity.EntityType

      Dim entityReader = EntityReaderCache.GetReader(dataReaderType, dialectProvider, model, entityType)

      readerIndex += entity.GetColumnCount()

      Dim includedSqlResultsReaderData As IncludedSqlResultReaderData() = Nothing
      Dim entityIncludedSqlResults = entity.IncludedSqlResults

      If entityIncludedSqlResults IsNot Nothing Then
        includedSqlResultsReaderData = New IncludedSqlResultReaderData(entityIncludedSqlResults.Count - 1) {}

        ' LINQ not used for performance and allocation reasons
        For j = 0 To entityIncludedSqlResults.Count - 1
          Dim entityIncludedSqlResult = entityIncludedSqlResults(j)
          Dim setter = EntityMemberSetterCache.GetSetter(model, entityType, entityIncludedSqlResult.PropertyName, entityIncludedSqlResult.Result.ResultType)
          Dim readData = Create(dataReaderType, dialectProvider, model, entityIncludedSqlResult.Result, readerIndex)
          includedSqlResultsReaderData(j) = New IncludedSqlResultReaderData(setter, readData)
          readerIndex += readData.GetColumnCount()
        Next
      End If

      Return New EntitySqlResultReaderData(New EntitySqlResult(entity), entityReaderIndex, entityReader, includedSqlResultsReaderData)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResult As SqlResultBase) As ReaderDataBase
      Return Create(dataReaderType, dialectProvider, model, sqlResult, 0)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Public Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResult As SqlResultBase, readerIndex As Int32) As ReaderDataBase
      If TypeOf sqlResult Is AnonymousTypeSqlResult Then
        Return Create(dataReaderType, dialectProvider, model, DirectCast(sqlResult, AnonymousTypeSqlResult), readerIndex)
      ElseIf TypeOf sqlResult Is ValueTupleSqlResult Then
        Return Create(dataReaderType, dialectProvider, model, DirectCast(sqlResult, ValueTupleSqlResult), readerIndex)
      ElseIf TypeOf sqlResult Is EntitySqlResult Then
        Return Create(dataReaderType, dialectProvider, model, DirectCast(sqlResult, EntitySqlResult), readerIndex)
      ElseIf TypeOf sqlResult Is ScalarValueSqlResult Then
        Return Create(dataReaderType, dialectProvider, model, DirectCast(sqlResult, ScalarValueSqlResult), readerIndex)
      Else
        Throw New NotSupportedException($"SQL result of type {sqlResult.GetType()} is not supported.")
      End If
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="AnonymousTypeSqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResult As AnonymousTypeSqlResult, readerIndex As Int32) As AnonymousTypeSqlResultReaderData
      Dim items = Create(dataReaderType, dialectProvider, model, sqlResult.Items, readerIndex)
      Return New AnonymousTypeSqlResultReaderData(sqlResult, readerIndex, items)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ValueTupleSqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResult As ValueTupleSqlResult, readerIndex As Int32) As ValueTupleSqlResultReaderData
      Dim items = Create(dataReaderType, dialectProvider, model, sqlResult.Items, readerIndex)
      Return New ValueTupleSqlResultReaderData(sqlResult, readerIndex, items)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResult As EntitySqlResult, readerIndex As Int32) As EntitySqlResultReaderData
      Dim entity = sqlResult.Entity
      Dim entityReader = EntityReaderCache.GetReader(dataReaderType, dialectProvider, model, entity.Entity.EntityType)
      Dim containsPKReader = EntityReaderCache.GetContainsPKReader(dataReaderType, dialectProvider, model, entity.Entity.EntityType)
      Dim pkOffsets = GetPKOffsets(entity)

      Return New EntitySqlResultReaderData(sqlResult, readerIndex, entityReader, containsPKReader, pkOffsets)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ScalarValueSqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResult As ScalarValueSqlResult, readerIndex As Int32) As ScalarValueSqlResultReaderData
      Dim reader = ValueTypeReaderCache.GetReader(dataReaderType, dialectProvider, model, sqlResult.ResultType)
      Return New ScalarValueSqlResultReaderData(sqlResult, readerIndex, reader)
    End Function

    ''' <summary>
    ''' Creates new instances of <see cref="ReaderDataBase"/>.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResults"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model, sqlResults As SqlResultBase(), readerIndex As Int32) As ReaderDataBase()
      Dim result = New ReaderDataBase(sqlResults.Length - 1) {}

      For i = 0 To sqlResults.Length - 1
        Dim readerData = Create(dataReaderType, dialectProvider, model, sqlResults(i), readerIndex)
        result(i) = readerData
        readerIndex += readerData.GetColumnCount()
      Next

      Return result
    End Function

    ''' <summary>
    ''' Get primary keys offsets.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    Private Shared Function GetPKOffsets(entity As SqlEntity) As Int32()
      Dim includedColumns = entity.IncludedColumns
      Dim pks = entity.Entity.GetKeyProperties()
      Dim pkOffsets = New Int32(pks.Count - 1) {}

      If pks.Count = 0 Then
        Return pkOffsets
      End If

      Dim offset = 0
      Dim currentPkIndex = 0

      For i = 0 To pks.Last().Index
        If i = pks(currentPkIndex).Index Then
          pkOffsets(currentPkIndex) = offset
          currentPkIndex += 1
        End If

        If includedColumns(i) Then
          offset += 1
        End If
      Next

      Return pkOffsets
    End Function

  End Class
End Namespace