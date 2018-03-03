Namespace Metadata.Builders

  Public Class ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)

    Private m_DeclaringEntity As Entity

    Private m_ReferenceNavigation As ReferenceNavigation

    Sub New(declaringEntity As Entity, propertyName As String, relatedEntityType As Type)
      m_DeclaringEntity = declaringEntity
      m_ReferenceNavigation = m_DeclaringEntity.AddReferenceNavigation(propertyName, relatedEntityType)
    End Sub

  End Class
End Namespace