Imports System.Data
Imports System.Linq.Expressions
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Entity relationship setter factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityRelationshipSetterFactory

    ''' <summary>
    ''' Creates setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="relationshipNavigation"></param>
    ''' <returns></returns>
    Public Shared Function CreateSetter(model As Model, entityType As Type, relationshipNavigation As RelationshipNavigation) As Action(Of Object, Object)
      Select Case relationshipNavigation.GetType()
        Case GetType(ReferenceNavigation)
          Return CreateReferenceSetter(model, entityType, DirectCast(relationshipNavigation, ReferenceNavigation))
        Case GetType(CollectionNavigation)
          Return CreateCollectionSetter(model, entityType, DirectCast(relationshipNavigation, CollectionNavigation))
        Case Else
          Throw New NotSupportedException($"Relationship of type '{relationshipNavigation.GetType()}' is not supported.")
      End Select
    End Function

    ''' <summary>
    ''' Creates reference setter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="referenceNavigation"></param>
    ''' <returns></returns>
    Private Shared Function CreateReferenceSetter(model As Model, entityType As Type, referenceNavigation As ReferenceNavigation) As Action(Of Object, Object)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {entityParam, valueParam}

      Dim entityCasted = Expression.Convert(entityParam, entityType)
      Dim valueCasted = Expression.Convert(valueParam, referenceNavigation.RelatedEntityType)

      Dim prop = Expression.Property(entityCasted, referenceNavigation.PropertyName)
      Dim propAssign = Expression.Assign(prop, valueCasted)

      Dim body = Expression.Block(propAssign)

      Dim setter = Expression.Lambda(Of Action(Of Object, Object))(body, parameters)
      Return setter.Compile()
    End Function

    ''' <summary>
    ''' Creates collection setter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="collectionNavigation"></param>
    ''' <returns></returns>
    Private Shared Function CreateCollectionSetter(model As Model, entityType As Type, collectionNavigation As CollectionNavigation) As Action(Of Object, Object)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {entityParam, valueParam}

      Dim entityCasted = Expression.Convert(entityParam, entityType)
      Dim valueCasted = Expression.Convert(valueParam, collectionNavigation.RelatedEntityType)

      Dim prop = Expression.Property(entityCasted, collectionNavigation.PropertyName)
      Dim propAdd = Expression.Call(prop, "Add", Nothing, valueCasted)

      Dim body = Expression.Block(propAdd)

      Dim setter = Expression.Lambda(Of Action(Of Object, Object))(body, parameters)
      Return setter.Compile()
    End Function

    ''' <summary>
    ''' Creates collection init setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="collectionNavigation"></param>
    ''' <returns></returns>
    Public Shared Function CreateCollectionInitSetter(model As Model, entityType As Type, collectionNavigation As CollectionNavigation) As Action(Of Object)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim parameters = {entityParam}

      Dim entityCasted = Expression.Convert(entityParam, entityType)

      Dim collectionType = collectionNavigation.CollectionType

      If collectionType.IsInterface Then
        collectionType = GetType(List(Of )).MakeGenericType(collectionNavigation.RelatedEntityType)
      End If

      Dim value = Expression.[New](collectionType)
      Dim prop = Expression.Property(entityCasted, collectionNavigation.PropertyName)
      Dim propAssign = Expression.Assign(prop, value)
      Dim isNull = Expression.Equal(prop, Expression.Constant(Nothing))
      Dim cond = Expression.IfThen(isNull, propAssign)

      Dim setter = Expression.Lambda(Of Action(Of Object))(cond, parameters)
      Return setter.Compile()
    End Function

  End Class
End Namespace