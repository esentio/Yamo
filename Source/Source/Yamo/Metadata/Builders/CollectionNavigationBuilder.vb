Imports System.Diagnostics.CodeAnalysis

Namespace Metadata.Builders

  ''' <summary>
  ''' Provides an API for configuring a collection relationship navigation.
  ''' </summary>
  ''' <typeparam name="TEntity"></typeparam>
  ''' <typeparam name="TRelatedEntity"></typeparam>
  Public Class CollectionNavigationBuilder(Of TEntity, TRelatedEntity)

    ''' <summary>
    ''' Stores declaring entity.
    ''' </summary>
    Private m_DeclaringEntity As Entity

    ''' <summary>
    ''' Stores related collection navigation.
    ''' </summary>
    Private m_CollectionNavigation As CollectionNavigation

    ''' <summary>
    ''' Creates new instance of <see cref="CollectionNavigationBuilder(Of TEntity, TRelatedEntity)"/>.
    ''' </summary>
    ''' <param name="declaringEntity"></param>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    ''' <param name="collectionType"></param>
    Sub New(<DisallowNull> declaringEntity As Entity, <DisallowNull> propertyName As String, <DisallowNull> relatedEntityType As Type, <DisallowNull> collectionType As Type)
      m_DeclaringEntity = declaringEntity
      m_CollectionNavigation = m_DeclaringEntity.AddCollectionNavigation(propertyName, relatedEntityType, collectionType)
    End Sub

  End Class
End Namespace