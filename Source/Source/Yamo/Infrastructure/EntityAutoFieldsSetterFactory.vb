Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Entity auto fields setter factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityAutoFieldsSetterFactory

    ''' <summary>
    ''' Creates on insert setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function CreateOnInsertSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim properties = model.GetEntity(entityType).GetProperties().Where(Function(p) p.SetOnInsert).Select(Function(p) (p, p.GetOnInsertFactory())).ToArray()
      Return CreateSetter(model, entityType, properties)
    End Function

    ''' <summary>
    ''' Creates on update setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function CreateOnUpdateSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim properties = model.GetEntity(entityType).GetProperties().Where(Function(p) p.SetOnUpdate).Select(Function(p) (p, p.GetOnUpdateFactory())).ToArray()
      Return CreateSetter(model, entityType, properties)
    End Function

    ''' <summary>
    ''' Creates on soft delete setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function CreateOnDeleteSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim properties = model.GetEntity(entityType).GetProperties().Where(Function(p) p.SetOnDelete).Select(Function(p) (p, p.GetOnDeleteFactory())).ToArray()

      If properties.Length = 0 Then
        Throw New NotSupportedException($"Entity '{entityType}' doesn't support soft deleting.")
      End If

      Return CreateSetter(model, entityType, properties)
    End Function

    ''' <summary>
    ''' Creates setter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="properties"></param>
    ''' <returns></returns>
    Private Shared Function CreateSetter(model As Model, entityType As Type, properties As (Prop As [Property], Factory As Object)()) As Action(Of Object, DbContext)
      If properties.Length = 0 Then
        Return Sub(entity As Object, context As DbContext)
               End Sub
      End If

      Dim entityParam = Expression.Parameter(GetType(Object), "entity")
      Dim contextParam = Expression.Parameter(GetType(DbContext), "context")
      Dim parameters = {entityParam, contextParam}

      Dim expressions = New List(Of Expression)

      Dim entityVariable = Expression.Variable(entityType, "entityObj")
      expressions.Add(Expression.Assign(entityVariable, Expression.Convert(entityParam, entityType)))

      Dim variables = New List(Of ParameterExpression)
      variables.Add(entityVariable)

      For Each prop In properties
        Dim factoryMethod = prop.Factory
        Dim factoryMethodType = factoryMethod.GetType()
        Dim factoryVariable = Expression.Variable(factoryMethodType)
        variables.Add(factoryVariable)

        expressions.Add(Expression.Assign(factoryVariable, Expression.Constant(factoryMethod, factoryMethodType)))

        Dim invokeCall As Expression

        ' we support 2 types of factory methods: 1) without parameter 2) with descendant of DbContext as parameter
        Dim factoryMethodTypeArgs = factoryMethodType.GenericTypeArguments()
        If factoryMethodTypeArgs.Length = 1 Then
          Dim invokeMethod = factoryMethodType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance, Nothing, {}, {})
          invokeCall = Expression.Call(factoryVariable, invokeMethod)
        ElseIf factoryMethodTypeArgs.Length = 2 Then
          Dim contextType = factoryMethodTypeArgs(0)

          If Not GetType(DbContext).IsAssignableFrom(contextType) Then
            Throw New NotSupportedException($"Unsupported factory method of type '{factoryMethodType}'. Parameter must inherit from DbContext.")
          End If

          Dim invokeMethod = factoryMethodType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance, Nothing, {contextType}, {})
          invokeCall = Expression.Call(factoryVariable, invokeMethod, {Expression.Convert(contextParam, contextType)})
        Else
          Throw New NotSupportedException($"Unsupported factory method of type '{factoryMethodType}'.")
        End If

        Dim propertyValue = Expression.Property(entityVariable, prop.Prop.Name)

        expressions.Add(Expression.Assign(propertyValue, invokeCall))
      Next

      Dim body = Expression.Block(variables, expressions)

      Dim setter = Expression.Lambda(Of Action(Of Object, DbContext))(body, parameters)
      Return setter.Compile()
    End Function

  End Class
End Namespace