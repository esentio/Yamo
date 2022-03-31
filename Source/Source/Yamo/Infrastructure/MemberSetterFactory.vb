Imports System.Data
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Member setter factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class MemberSetterFactory

    ''' <summary>
    ''' Creates property or field setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="objectType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="valueType"></param>
    ''' <returns></returns>
    Public Shared Function CreateSetter(<DisallowNull> objectType As Type, <DisallowNull> propertyOrFieldName As String, <DisallowNull> valueType As Type) As Action(Of Object, Object)
      Dim objParam = Expression.Parameter(GetType(Object), "obj")
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {objParam, valueParam}

      Dim objCasted = Expression.Convert(objParam, objectType)
      Dim prop = Expression.PropertyOrField(objCasted, propertyOrFieldName)
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
    ''' <param name="objectType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Public Shared Function CreateCollectionAddSetter(<DisallowNull> objectType As Type, <DisallowNull> propertyOrFieldName As String, <DisallowNull> itemType As Type) As Action(Of Object, Object)
      Dim objParam = Expression.Parameter(GetType(Object), "obj")
      Dim valueParam = Expression.Parameter(GetType(Object), "value")
      Dim parameters = {objParam, valueParam}

      Dim objCasted = Expression.Convert(objParam, objectType)
      Dim valueCasted = Expression.Convert(valueParam, itemType)

      Dim prop = Expression.PropertyOrField(objCasted, propertyOrFieldName)
      Dim propAdd = Expression.Call(prop, "Add", Nothing, valueCasted)

      Dim body = Expression.Block(propAdd)

      Dim setter = Expression.Lambda(Of Action(Of Object, Object))(body, parameters)
      Return setter.Compile()
    End Function

    ''' <summary>
    ''' Creates property or field setter that initializes a collection.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="objectType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="collectionType"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Public Shared Function CreateCollectionInitSetter(<DisallowNull> objectType As Type, <DisallowNull> propertyOrFieldName As String, <DisallowNull> collectionType As Type, <DisallowNull> itemType As Type) As Action(Of Object)
      Dim objParam = Expression.Parameter(GetType(Object), "obj")
      Dim parameters = {objParam}

      Dim objCasted = Expression.Convert(objParam, objectType)

      If collectionType.IsInterface Then
        collectionType = GetType(List(Of )).MakeGenericType(itemType)
      End If

      Dim value = Expression.[New](collectionType)
      Dim prop = Expression.PropertyOrField(objCasted, propertyOrFieldName)
      Dim propAssign = Expression.Assign(prop, value)
      Dim isNull = Expression.Equal(prop, Expression.Constant(Nothing))
      Dim cond = Expression.IfThen(isNull, propAssign)

      Dim setter = Expression.Lambda(Of Action(Of Object))(cond, parameters)
      Return setter.Compile()
    End Function

  End Class
End Namespace