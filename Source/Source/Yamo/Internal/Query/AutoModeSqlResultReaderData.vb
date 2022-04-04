Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Internal.Query.Metadata

Namespace Internal.Query

  ''' <summary>
  ''' Represents reader data for SQL entities in automatic select mode.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class AutoModeSqlResultReaderData

    ''' <summary>
    ''' Gets SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As SqlEntityBase

    ''' <summary>
    ''' Gets SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ReaderData As ReaderDataBase

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
    ''' <returns>Related entities indexes or <see langword="Nothing"/> if <see cref="HasRelatedEntities"/> is <see langword="False"/>.</returns>
    Public ReadOnly Property RelatedEntities As <MaybeNull> IReadOnlyList(Of Int32)

    ''' <summary>
    ''' Gets whether there are other entities to which this entity is declaring entity and it is 1:N relationship.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasCollectionNavigation As Boolean

    ''' <summary>
    ''' Gets collection initializers.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Collection initializers or <see langword="Nothing"/> if <see cref="HasCollectionNavigation"/> is <see langword="False"/>.</returns>
    Public ReadOnly Property CollectionInitializers As <MaybeNull> IReadOnlyList(Of Action(Of Object))

    ''' <summary>
    ''' Gets whether there is a relationship setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasRelationshipSetter As Boolean

    ''' <summary>
    ''' Gets relationship setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Relationship setter or <see langword="Nothing"/> if <see cref="HasRelationshipSetter"/> is <see langword="False"/>.</returns>
    Public ReadOnly Property RelationshipSetter As <MaybeNull> Action(Of Object, Object) ' declaring entity, related entity (this one);

    ''' <summary>
    ''' Gets whether there are included results.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasIncludedSqlResults As Boolean

    ''' <summary>
    ''' Gets reader data for included results.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Reader data or <see langword="Nothing"/> if <see cref="HasIncludedSqlResults"/> is <see langword="False"/>.</returns>
    Public ReadOnly Property IncludedSqlResultsReaderData As <MaybeNull> IReadOnlyList(Of IncludedSqlResultReaderData)

    ''' <summary>
    ''' Creates new instance of <see cref="AutoModeSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="readerData"></param>
    Public Sub New(<DisallowNull> entity As SqlEntityBase, <DisallowNull> readerData As ReaderDataBase)
      Me.New(entity, readerData, Nothing, Nothing, Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="AutoModeSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="readerData"></param>
    ''' <param name="includedSqlResultsReaderData"></param>
    Public Sub New(<DisallowNull> entity As SqlEntityBase, <DisallowNull> readerData As ReaderDataBase, includedSqlResultsReaderData As IReadOnlyList(Of IncludedSqlResultReaderData))
      Me.New(entity, readerData, Nothing, Nothing, Nothing, includedSqlResultsReaderData)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="AutoModeSqlResultReaderData"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="readerData"></param>
    ''' <param name="relatedEntities"></param>
    ''' <param name="collectionInitializers"></param>
    ''' <param name="relationshipSetter"></param>
    ''' <param name="includedSqlResultsReaderData"></param>
    Public Sub New(<DisallowNull> entity As SqlEntityBase, <DisallowNull> readerData As ReaderDataBase, relatedEntities As IReadOnlyList(Of Int32), collectionInitializers As IReadOnlyList(Of Action(Of Object)), relationshipSetter As Action(Of Object, Object), includedSqlResultsReaderData As IReadOnlyList(Of IncludedSqlResultReaderData))
      Me.Entity = entity
      Me.ReaderData = readerData
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