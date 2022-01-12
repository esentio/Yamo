Imports System.Data
Imports System.Data.Common
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
    Public ReadOnly Property Reader As Func(Of DbDataReader, Int32, Boolean(), Object)

    ''' <summary>
    ''' Gets contains primary key reader.<br/>
    ''' Might be <see langword="Nothing"/> in scenarios when reading primary key is not necessary.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ContainsPKReader As Func(Of DbDataReader, Int32, Int32(), Boolean)

    ''' <summary>
    ''' Gets primary key offsets.<br/>
    ''' Might be <see langword="Nothing"/> in scenarios when reading primary key is not necessary.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKOffsets As Int32()

    ''' <summary>
    ''' Gets primary key reader.<br/>
    ''' Might be <see langword="Nothing"/> in scenarios when reading primary key is not necessary.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PKReader As Func(Of DbDataReader, Int32, Int32(), Object)

    ''' <summary>
    ''' Gets whether there are other entities to which this entity is declaring entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasRelatedEntities As Boolean

    ''' <summary>
    ''' Gets indexes of all entities to which this entity is declaring entity.<br/>
    ''' Contains <see langword="Nothing"/> if <see cref="HasRelatedEntities"/> is <see langword="False"/>.<br/>
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
    ''' Gets collection initializers.<br/>
    ''' Contains <see langword="Nothing"/> if <see cref="HasCollectionNavigation"/> is <see langword="False"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CollectionInitializers As IReadOnlyList(Of Action(Of Object))

    ''' <summary>
    ''' Gets whether there is a relationship setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasRelationshipSetter As Boolean

    ''' <summary>
    ''' Gets relationship setter.<br/>
    ''' Contains <see langword="Nothing"/> if <see cref="HasRelationshipSetter"/> is <see langword="False"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RelationshipSetter As Action(Of Object, Object) ' declaring entity, related entity (this one);

    ''' <summary>
    ''' Gets whether there are included results.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasIncludedSqlResults As Boolean

    ''' <summary>
    ''' Gets reader data for included results.<br/>
    ''' Contains <see langword="Nothing"/> if <see cref="HasIncludedSqlResults"/> is <see langword="False"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IncludedSqlResultsReaderData As IReadOnlyList(Of IncludedSqlResultReaderData)

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="entityReader"></param>
    ''' <param name="includedSqlResultsReaderData"></param>
    Public Sub New(sqlResult As EntitySqlResult, readerIndex As Int32, entityReader As Func(Of DbDataReader, Int32, Boolean(), Object), includedSqlResultsReaderData As IReadOnlyList(Of IncludedSqlResultReaderData))
      MyBase.New(sqlResult, readerIndex)
      Me.Entity = sqlResult.Entity
      Me.Reader = entityReader
      Me.ContainsPKReader = Nothing
      Me.PKOffsets = Nothing
      Me.PKReader = Nothing
      Me.HasRelatedEntities = False
      Me.RelatedEntities = Nothing
      Me.HasCollectionNavigation = False
      Me.CollectionInitializers = Nothing
      Me.HasRelationshipSetter = False
      Me.RelationshipSetter = Nothing
      Me.HasIncludedSqlResults = includedSqlResultsReaderData IsNot Nothing
      Me.IncludedSqlResultsReaderData = includedSqlResultsReaderData
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="entityReader"></param>
    ''' <param name="containsPKReader"></param>
    ''' <param name="pkOffsets"></param>
    Public Sub New(sqlResult As EntitySqlResult, readerIndex As Int32, entityReader As Func(Of DbDataReader, Int32, Boolean(), Object), containsPKReader As Func(Of DbDataReader, Int32, Int32(), Boolean), pkOffsets As Int32())
      MyBase.New(sqlResult, readerIndex)
      Me.Entity = sqlResult.Entity
      Me.Reader = entityReader
      Me.ContainsPKReader = containsPKReader
      Me.PKOffsets = pkOffsets
      Me.PKReader = Nothing
      Me.HasRelatedEntities = False
      Me.RelatedEntities = Nothing
      Me.HasCollectionNavigation = False
      Me.CollectionInitializers = Nothing
      Me.HasRelationshipSetter = False
      Me.RelationshipSetter = Nothing
      Me.HasIncludedSqlResults = False
      Me.IncludedSqlResultsReaderData = Nothing
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntitySqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <param name="readerIndex"></param>
    ''' <param name="entityReader"></param>
    ''' <param name="containsPKReader"></param>
    ''' <param name="pkOffsets"></param>
    ''' <param name="pkReader"></param>
    ''' <param name="relatedEntities"></param>
    ''' <param name="collectionInitializers"></param>
    ''' <param name="relationshipSetter"></param>
    ''' <param name="includedSqlResultsReaderData"></param>
    Public Sub New(sqlResult As EntitySqlResult, readerIndex As Int32, entityReader As Func(Of DbDataReader, Int32, Boolean(), Object), containsPKReader As Func(Of DbDataReader, Int32, Int32(), Boolean), pkOffsets As Int32(), pkReader As Func(Of DbDataReader, Int32, Int32(), Object), relatedEntities As IReadOnlyList(Of Int32), collectionInitializers As IReadOnlyList(Of Action(Of Object)), relationshipSetter As Action(Of Object, Object), includedSqlResultsReaderData As IReadOnlyList(Of IncludedSqlResultReaderData))
      MyBase.New(sqlResult, readerIndex)
      Me.Entity = sqlResult.Entity
      Me.Reader = entityReader
      Me.ContainsPKReader = containsPKReader
      Me.PKOffsets = pkOffsets
      Me.PKReader = pkReader
      Me.HasRelatedEntities = relatedEntities IsNot Nothing
      Me.RelatedEntities = relatedEntities
      Me.HasCollectionNavigation = collectionInitializers IsNot Nothing
      Me.CollectionInitializers = collectionInitializers
      Me.HasRelationshipSetter = relationshipSetter IsNot Nothing
      Me.RelationshipSetter = relationshipSetter
      Me.HasIncludedSqlResults = includedSqlResultsReaderData IsNot Nothing
      Me.IncludedSqlResultsReaderData = includedSqlResultsReaderData
    End Sub

  End Class
End Namespace