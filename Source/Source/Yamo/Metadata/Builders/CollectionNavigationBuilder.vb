Namespace Metadata.Builders

  Public Class CollectionNavigationBuilder(Of TEntity, TRelatedEntity)

    Private m_DeclaringEntity As Entity

    Private m_CollectionNavigation As CollectionNavigation

    Sub New(declaringEntity As Entity, propertyName As String, relatedEntityType As Type, collectionType As Type)
      m_DeclaringEntity = declaringEntity
      m_CollectionNavigation = m_DeclaringEntity.AddCollectionNavigation(propertyName, relatedEntityType, collectionType)
    End Sub

  End Class
End Namespace