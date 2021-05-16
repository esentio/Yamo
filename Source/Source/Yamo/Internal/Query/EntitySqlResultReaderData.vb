Imports System.Data
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for entity values.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntitySqlResultReaderData
    Inherits ReaderDataBase

    ''' <summary>
    ''' Gets SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As SqlEntity

    ''' <summary>
    ''' Gets entity reader.<br/>
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
    ''' Gets primary key offsets.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKOffsets As Int32()

    ''' <summary>
    ''' Gets primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKReader As Func(Of IDataReader, Int32, Int32(), Object) ' might be null! (allocation reasons)

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
    Public ReadOnly Property RelatedEntities As IReadOnlyList(Of Int32) ' might be null! (allocation reasons)

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
    Public ReadOnly Property CollectionInitializers As IReadOnlyList(Of Action(Of Object)) ' might be null! (allocation reasons)

    ''' <summary>
    ''' Gets relationship setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RelationshipSetter As Action(Of Object, Object) ' declaring entity, related entity (this one); might be null! (allocation reasons)

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="readerIndex"></param>
    ''' <param name="entity"></param>
    ''' <param name="entityReader"></param>
    ''' <param name="containsPKReader"></param>
    ''' <param name="pkOffsets"></param>
    ''' <param name="pkReader"></param>
    ''' <param name="relatedEntities"></param>
    ''' <param name="collectionInitializers"></param>
    ''' <param name="relationshipSetter"></param>
    Public Sub New(readerIndex As Int32, entity As SqlEntity, entityReader As Func(Of IDataReader, Int32, Boolean(), Object), containsPKReader As Func(Of IDataReader, Int32, Int32(), Boolean), pkOffsets As Int32(), Optional pkReader As Func(Of IDataReader, Int32, Int32(), Object) = Nothing, Optional relatedEntities As IReadOnlyList(Of Int32) = Nothing, Optional collectionInitializers As IReadOnlyList(Of Action(Of Object)) = Nothing, Optional relationshipSetter As Action(Of Object, Object) = Nothing)
      MyBase.New(readerIndex)
      Me.Entity = entity
      Me.Reader = entityReader
      Me.ContainsPKReader = containsPKReader
      Me.PKOffsets = pkOffsets
      Me.PKReader = pkReader
      Me.HasRelatedEntities = relatedEntities IsNot Nothing
      Me.RelatedEntities = relatedEntities
      Me.HasCollectionNavigation = collectionInitializers IsNot Nothing
      Me.CollectionInitializers = collectionInitializers
      Me.RelationshipSetter = relationshipSetter
    End Sub

    ''' <summary>
    ''' Gets count of columns in the resultset.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetColumnCount() As Int32
      Return Me.Entity.GetColumnCount()
    End Function

  End Class
End Namespace