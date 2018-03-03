Imports System.Linq.Expressions
Imports Yamo.Internal.Extensions

Namespace Metadata.Builders

  Public Class EntityBuilder(Of TEntity As Class)

    Private m_Model As Model

    Private m_Entity As Entity

    Sub New(model As Model)
      m_Model = model
      m_Entity = m_Model.AddEntity(GetType(TEntity))
    End Sub

    Public Function ToTable(name As String) As EntityBuilder(Of TEntity)
      m_Entity.TableName = name
      Return Me
    End Function

    Public Function [Property](Of TProperty)(propertyExpression As Expression(Of Func(Of TEntity, TProperty))) As PropertyBuilder(Of TProperty)
      Dim name = propertyExpression.GetPropertyName()
      Return New PropertyBuilder(Of TProperty)(m_Entity, name)
    End Function

    Public Function HasOne(Of TRelatedEntity)(propertyExpression As Expression(Of Func(Of TEntity, TRelatedEntity))) As ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)
      Dim propertyName = propertyExpression.GetPropertyName()
      Dim relatedEntityType = GetType(TRelatedEntity)
      Return New ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)(m_Entity, propertyName, relatedEntityType)
    End Function

    Public Function HasMany(Of TRelatedEntity)(propertyExpression As Expression(Of Func(Of TEntity, IList(Of TRelatedEntity)))) As CollectionNavigationBuilder(Of TEntity, TRelatedEntity)
      Dim propertyName = propertyExpression.GetPropertyName()
      Dim relatedEntityType = GetType(TRelatedEntity)
      Dim propertyType = propertyExpression.GetPropertyType()
      Return New CollectionNavigationBuilder(Of TEntity, TRelatedEntity)(m_Entity, propertyName, relatedEntityType, propertyType)
    End Function

  End Class
End Namespace