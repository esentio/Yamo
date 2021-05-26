Imports System.Data
Imports System.Linq.Expressions
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Entity member setter factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityMemberSetterFactory

    ''' <summary>
    ''' Creates property or field setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="valueType"></param>
    ''' <returns></returns>
    Public Shared Function CreateSetter(entityType As Type, propertyOrFieldName As String, valueType As Type) As Action(Of Object, Object)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {entityParam, valueParam}

      Dim entityCasted = Expression.Convert(entityParam, entityType)
      Dim prop = Expression.PropertyOrField(entityCasted, propertyOrFieldName)
      Dim valueCasted = Expression.Convert(valueParam, prop.Type)
      Dim propAssign = Expression.Assign(prop, valueCasted)

      Dim body = Expression.Block(propAssign)

      Dim setter = Expression.Lambda(Of Action(Of Object, Object))(body, parameters)
      Return setter.Compile()
    End Function

    ''' <summary>
    ''' Creates setter that adds an item to the collection of a property or a field.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Public Shared Function CreateCollectionAddSetter(entityType As Type, propertyOrFieldName As String, itemType As Type) As Action(Of Object, Object)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {entityParam, valueParam}

      Dim entityCasted = Expression.Convert(entityParam, entityType)
      Dim valueCasted = Expression.Convert(valueParam, itemType)

      Dim prop = Expression.PropertyOrField(entityCasted, propertyOrFieldName)
      Dim propAdd = Expression.Call(prop, "Add", Nothing, valueCasted)

      Dim body = Expression.Block(propAdd)

      Dim setter = Expression.Lambda(Of Action(Of Object, Object))(body, parameters)
      Return setter.Compile()
    End Function

    ''' <summary>
    ''' Creates property or field setter that initializes a collection.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="collectionType"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Public Shared Function CreateCollectionInitSetter(entityType As Type, propertyOrFieldName As String, collectionType As Type, itemType As Type) As Action(Of Object)
      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim parameters = {entityParam}

      Dim entityCasted = Expression.Convert(entityParam, entityType)

      If collectionType.IsInterface Then
        collectionType = GetType(List(Of )).MakeGenericType(itemType)
      End If

      Dim value = Expression.[New](collectionType)
      Dim prop = Expression.PropertyOrField(entityCasted, propertyOrFieldName)
      Dim propAssign = Expression.Assign(prop, value)
      Dim isNull = Expression.Equal(prop, Expression.Constant(Nothing))
      Dim cond = Expression.IfThen(isNull, propAssign)

      Dim setter = Expression.Lambda(Of Action(Of Object))(cond, parameters)
      Return setter.Compile()
    End Function

  End Class
End Namespace