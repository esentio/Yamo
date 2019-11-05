Namespace Metadata.Builders

  ''' <summary>
  ''' Provides an API for configuring a reference relationship navigation.
  ''' </summary>
  ''' <typeparam name="TEntity"></typeparam>
  ''' <typeparam name="TRelatedEntity"></typeparam>
  Public Class ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)

    ''' <summary>
    ''' Stores declaring entity.
    ''' </summary>
    Private m_DeclaringEntity As Entity

    ''' <summary>
    ''' Stores related reference navigation.
    ''' </summary>
    Private m_ReferenceNavigation As ReferenceNavigation

    ''' <summary>
    ''' Creates new instance of <see cref="ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)"/>.
    ''' </summary>
    ''' <param name="declaringEntity"></param>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    Sub New(declaringEntity As Entity, propertyName As String, relatedEntityType As Type)
      m_DeclaringEntity = declaringEntity
      m_ReferenceNavigation = m_DeclaringEntity.AddReferenceNavigation(propertyName, relatedEntityType)
    End Sub

  End Class
End Namespace