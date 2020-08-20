﻿Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents info used to read entity from SQL result.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityReadInfo
    Inherits BaseReadInfo

    ''' <summary>
    ''' Gets SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As SqlEntity

    ''' <summary>
    ''' Gets index of the reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReaderIndex As Int32

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Reader As Func(Of IDataReader, Int32, Boolean(), Object)

    ''' <summary>
    ''' Gets contains primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContainsPKReader As Func(Of IDataReader, Int32, Int32(), Boolean)

    ''' <summary>
    ''' Gets primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKReader As Func(Of IDataReader, Int32, Int32(), Object)

    ''' <summary>
    ''' Gets primary key offsets.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKOffsets As Int32()

    ''' <summary>
    ''' Gets whether there are other entities to which this entity is declaring entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasRelatedEntities As Boolean

    ''' <summary>
    ''' Gets indexes of all entities to which this entity is declaring entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RelatedEntities As IReadOnlyList(Of Int32)

    ''' <summary>
    ''' Gets whether there are other entities to which this entity is declaring entity and it is 1:N relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasCollectionNavigation As Boolean

    ''' <summary>
    ''' Gets collection initializers (might be <see langword="Nothing"/>).<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CollectionInitializers As IReadOnlyList(Of Action(Of Object)) ' might be nul!!! (it is wise? (allocation reasons))

    ''' <summary>
    ''' Gets relationship setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RelationshipSetter As Action(Of Object, Object) ' declaring entity, related entity (this one)

    ''' <summary>
    ''' Creates new instance of <see cref="EntityReadInfo"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Creates new instances of <see cref="EntityReadInfo"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Public Shared Function Create(dialectProvider As SqlDialectProvider, model As SqlModel) As EntityReadInfo()
      Dim entities = model.GetEntities()
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

      Dim result = New EntityReadInfo(entities.Length - 1) {}
      Dim readerIndex = 0

      For index = 0 To entities.Length - 1
        Dim entity = entities(index)
        result(index) = Create(dialectProvider, model.Model, entity, readerIndex, relationships(index).RelatedEntities, relationships(index).CollectionNavigations)
        readerIndex += entity.GetColumnCount()
      Next

      Return result
    End Function

    ''' <summary>
    ''' Creates new instance of <see cref="EntityReadInfo"/>.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="entity"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="relatedEntities"></param>
    ''' <param name="collectionNavigations"></param>
    ''' <returns></returns>
    Private Shared Function Create(dialectProvider As SqlDialectProvider, model As Model, entity As SqlEntity, readerIndex As Int32, relatedEntities As List(Of Int32), collectionNavigations As List(Of CollectionNavigation)) As EntityReadInfo
      Dim readInfo = New EntityReadInfo

      readInfo._Entity = entity
      readInfo._ReaderIndex = readerIndex
      readInfo._Reader = EntityReaderCache.GetReader(dialectProvider, model, entity.Entity.EntityType)
      readInfo._ContainsPKReader = EntityReaderCache.GetContainsPKReader(dialectProvider, model, entity.Entity.EntityType)
      readInfo._PKReader = EntityReaderCache.GetPKReader(dialectProvider, model, entity.Entity.EntityType)
      readInfo._PKOffsets = GetPKOffsets(entity)

      ' if relatedEntities is not null, then it contains at least one item
      readInfo._HasRelatedEntities = relatedEntities IsNot Nothing
      readInfo._RelatedEntities = relatedEntities

      ' if collectionNavigations is not null, then it contains at least one item
      If collectionNavigations IsNot Nothing Then
        readInfo._HasCollectionNavigation = True

        ' LINQ not used for performance and allocation reasons
        Dim collectionInitializers = New Action(Of Object)(collectionNavigations.Count - 1) {}

        For i = 0 To collectionNavigations.Count - 1
          collectionInitializers(i) = EntityRelationshipSetterCache.GetCollectionInitSetter(model, entity.Entity.EntityType, collectionNavigations(i))
        Next

        readInfo._CollectionInitializers = collectionInitializers
      Else
        readInfo._HasCollectionNavigation = False
      End If

      If entity.Relationship Is Nothing Then
        readInfo._RelationshipSetter = Nothing
      Else
        readInfo._RelationshipSetter = EntityRelationshipSetterCache.GetSetter(model, entity.Relationship.DeclaringEntity.Entity.EntityType, entity.Relationship.RelationshipNavigation)
      End If

      Return readInfo
    End Function

  End Class
End Namespace