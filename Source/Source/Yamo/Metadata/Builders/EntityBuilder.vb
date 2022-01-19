Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Internal.Extensions

Namespace Metadata.Builders

  ''' <summary>
  ''' Provides an API for configuring an <see cref="Entity"/>.
  ''' </summary>
  ''' <typeparam name="TEntity"></typeparam>
  Public Class EntityBuilder(Of TEntity As Class)

    ''' <summary>
    ''' Stores model.
    ''' </summary>
    Private m_Model As Model

    ''' <summary>
    ''' Stores related entity.
    ''' </summary>
    Private m_Entity As Entity

    ''' <summary>
    ''' Creates new instance of <see cref="EntityBuilder(Of TEntity)"/>.
    ''' </summary>
    ''' <param name="model"></param>
    Sub New(<DisallowNull> model As Model)
      m_Model = model
      m_Entity = m_Model.AddEntity(GetType(TEntity))
    End Sub

    ''' <summary>
    ''' Configures the database table name that the entity maps to.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function ToTable(<DisallowNull> name As String) As EntityBuilder(Of TEntity)
      m_Entity.TableName = name
      Return Me
    End Function

    ''' <summary>
    ''' Configures the database table name that the entity maps to.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="schema"></param>
    ''' <returns></returns>
    Public Function ToTable(<DisallowNull> name As String, <DisallowNull> schema As String) As EntityBuilder(Of TEntity)
      m_Entity.TableName = name
      m_Entity.Schema = schema
      Return Me
    End Function

    ''' <summary>
    ''' Returns an object that can be used to configure a property of the entity.<br/>
    ''' If the specified property is not already part of the model, it will be added.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function [Property](Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of TEntity, TProperty))) As PropertyBuilder(Of TProperty)
      Dim name = propertyExpression.GetPropertyName()
      Return New PropertyBuilder(Of TProperty)(m_Entity, name)
    End Function

    ''' <summary>
    ''' Configures a relationship where this entity has a reference to a single instance of an other entity.
    ''' </summary>
    ''' <typeparam name="TRelatedEntity"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function HasOne(Of TRelatedEntity)(<DisallowNull> propertyExpression As Expression(Of Func(Of TEntity, TRelatedEntity))) As ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)
      Dim propertyName = propertyExpression.GetPropertyName()
      Dim relatedEntityType = GetType(TRelatedEntity)
      Return New ReferenceNavigationBuilder(Of TEntity, TRelatedEntity)(m_Entity, propertyName, relatedEntityType)
    End Function

    ''' <summary>
    ''' Configures a relationship where this entity has a collection that contains instances of an other entity.
    ''' </summary>
    ''' <typeparam name="TRelatedEntity"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function HasMany(Of TRelatedEntity)(<DisallowNull> propertyExpression As Expression(Of Func(Of TEntity, IList(Of TRelatedEntity)))) As CollectionNavigationBuilder(Of TEntity, TRelatedEntity)
      Dim propertyName = propertyExpression.GetPropertyName()
      Dim relatedEntityType = GetType(TRelatedEntity)
      Dim propertyType = propertyExpression.GetPropertyType()
      Return New CollectionNavigationBuilder(Of TEntity, TRelatedEntity)(m_Entity, propertyName, relatedEntityType, propertyType)
    End Function

  End Class
End Namespace