Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Entity auto fields getter factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityAutoFieldsGetterFactory

    ''' <summary>
    ''' Creates on update getter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function CreateOnUpdateGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Dim factories = model.GetEntity(entityType).GetProperties().Where(Function(p) p.SetOnUpdate).Select(Function(p) p.GetOnUpdateFactory()).ToArray()

      If factories.Length = 0 Then
        Throw New NotSupportedException($"Entity '{entityType}' doesn't support auto fields on update.")
      End If

      Return CreateGetter(model, entityType, factories)
    End Function

    ''' <summary>
    ''' Creates on soft delete getter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function CreateOnDeleteGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Dim factories = model.GetEntity(entityType).GetProperties().Where(Function(p) p.SetOnDelete).Select(Function(p) p.GetOnDeleteFactory()).ToArray()

      If factories.Length = 0 Then
        Throw New NotSupportedException($"Entity '{entityType}' doesn't support soft deleting.")
      End If

      Return CreateGetter(model, entityType, factories)
    End Function

    ''' <summary>
    ''' Creates getter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="factories"></param>
    ''' <returns></returns>
    Private Shared Function CreateGetter(model As Model, entityType As Type, factories As Object()) As Func(Of DbContext, Object())
      Dim contextParam = Expression.Parameter(GetType(DbContext), "context")
      Dim parameters = {contextParam}

      Dim expressions = New List(Of Expression)

      Dim valuesVariable = Expression.Variable(GetType(Object()), "values")
      expressions.Add(Expression.Assign(valuesVariable, Expression.Constant(New Object(factories.Length - 1) {}, GetType(Object()))))

      Dim i = 0
      For Each factoryMethod In factories
        Dim factoryMethodType = factoryMethod.GetType()
        Dim factoryValue = Expression.Constant(factoryMethod, factoryMethodType)

        Dim invokeCall As Expression

        ' we support 2 types of factory methods: 1) without parameter 2) with descendant of DbContext as parameter
        Dim factoryMethodTypeArgs = factoryMethodType.GenericTypeArguments()
        If factoryMethodTypeArgs.Length = 1 Then
          Dim invokeMethod = factoryMethodType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance, Nothing, {}, {})
          invokeCall = Expression.Call(factoryValue, invokeMethod)
        ElseIf factoryMethodTypeArgs.Length = 2 Then
          Dim contextType = factoryMethodTypeArgs(0)

          If Not GetType(DbContext).IsAssignableFrom(contextType) Then
            Throw New NotSupportedException($"Unsupported factory method of type '{factoryMethodType}'. Parameter must inherit from DbContext.")
          End If

          Dim invokeMethod = factoryMethodType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance, Nothing, {contextType}, {})
          invokeCall = Expression.Call(factoryValue, invokeMethod, {Expression.Convert(contextParam, contextType)})
        Else
          Throw New NotSupportedException($"Unsupported factory method of type '{factoryMethodType}'.")
        End If

        Dim indexValue = Expression.Constant(i, GetType(Int32))
        Dim valuesIndex = Expression.ArrayAccess(valuesVariable, indexValue)
        Dim cast = Expression.Convert(invokeCall, GetType(Object))

        expressions.Add(Expression.Assign(valuesIndex, cast))

        i += 1
      Next

      expressions.Add(valuesVariable)

      Dim body = Expression.Block({valuesVariable}, expressions)

      Dim setter = Expression.Lambda(Of Func(Of DbContext, Object()))(body, parameters)
      Return setter.Compile()
    End Function

  End Class
End Namespace