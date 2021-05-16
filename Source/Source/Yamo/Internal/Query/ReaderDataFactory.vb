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
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="entities"></param>
    ''' <returns></returns>
    Public Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, entities As SqlEntity()) As EntitySqlResultReaderDataCollection
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
        Dim entity = entities(index)
        Dim entityType = entity.Entity.EntityType

        Dim entityReader = EntityReaderCache.GetReader(dialectProvider, model, entityType)
        Dim containsPKReader = EntityReaderCache.GetContainsPKReader(dialectProvider, model, entityType)
        Dim pkOffsets = GetPKOffsets(entity)
        Dim pkReader = EntityReaderCache.GetPKReader(dialectProvider, model, entityType)
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

        Dim readerData = New EntitySqlResultReaderData(readerIndex, entity, entityReader, containsPKReader, pkOffsets, pkReader, relatedEntities, collectionInitializers, relationshipSetter)
        readerDataItems(index) = readerData

        readerIndex += entity.GetColumnCount()
      Next

      Return New EntitySqlResultReaderDataCollection(readerDataItems)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResult As SqlResultBase) As ReaderDataBase
      Return Create(dialectProvider, model, sqlResult, 0)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ReaderDataBase"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResult As SqlResultBase, readerIndex As Int32) As ReaderDataBase
      If TypeOf sqlResult Is AnonymousTypeSqlResult Then
        Return Create(dialectProvider, model, DirectCast(sqlResult, AnonymousTypeSqlResult), readerIndex)
      ElseIf TypeOf sqlResult Is ValueTupleSqlResult Then
        Return Create(dialectProvider, model, DirectCast(sqlResult, ValueTupleSqlResult), readerIndex)
      ElseIf TypeOf sqlResult Is EntitySqlResult Then
        Return Create(dialectProvider, model, DirectCast(sqlResult, EntitySqlResult), readerIndex)
      ElseIf TypeOf sqlResult Is ScalarValueSqlResult Then
        Return Create(dialectProvider, model, DirectCast(sqlResult, ScalarValueSqlResult), readerIndex)
      Else
        Throw New NotSupportedException($"SQL result of type {sqlResult.GetType()} is not supported.")
      End If
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="AnonymousTypeSqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResult As AnonymousTypeSqlResult, readerIndex As Int32) As AnonymousTypeSqlResultReaderData
      Dim items = Create(dialectProvider, model, sqlResult.Items, readerIndex)
      Return New AnonymousTypeSqlResultReaderData(readerIndex, items)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ValueTupleSqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResult As ValueTupleSqlResult, readerIndex As Int32) As ValueTupleSqlResultReaderData
      Dim items = Create(dialectProvider, model, sqlResult.Items, readerIndex)
      Return New ValueTupleSqlResultReaderData(readerIndex, items)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResult As EntitySqlResult, readerIndex As Int32) As EntitySqlResultReaderData
      Dim entity = sqlResult.Entity
      Dim entityReader = EntityReaderCache.GetReader(dialectProvider, model, entity.Entity.EntityType)
      Dim containsPKReader = EntityReaderCache.GetContainsPKReader(dialectProvider, model, entity.Entity.EntityType)
      Dim pkOffsets = GetPKOffsets(entity)

      Return New EntitySqlResultReaderData(readerIndex, entity, entityReader, containsPKReader, pkOffsets)
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="ScalarValueSqlResultReaderData"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResult As ScalarValueSqlResult, readerIndex As Int32) As ScalarValueSqlResultReaderData
      Dim reader = ValueTypeReaderCache.GetReader(dialectProvider, model, sqlResult.ResultType)
      Return New ScalarValueSqlResultReaderData(readerIndex, reader)
    End Function

    ''' <summary>
    ''' Creates new instances of <see cref="ReaderDataBase"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResults"></param>
    ''' <param name="readerIndex"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, sqlResults As SqlResultBase(), readerIndex As Int32) As ReaderDataBase()
      Dim sqlResultReaderIndex = 0
      Dim result = New ReaderDataBase(sqlResults.Length - 1) {}

      For i = 0 To sqlResults.Length - 1
        Dim readerData = Create(dialectProvider, model, sqlResults(i), sqlResultReaderIndex)
        result(i) = readerData
        sqlResultReaderIndex += readerData.GetColumnCount()
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