Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Internal.Extensions

  ''' <summary>
  ''' <see cref="Expression"/> related extension methods.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  <Extension()>
  Public Module ExpressionExtensions

    ''' <summary>
    ''' Gets property name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    <Extension>
    Public Function GetPropertyName(Of T1, T2)(propertyExpression As Expression(Of Func(Of T1, T2))) As String
      Dim expression As Expression

      If propertyExpression.Body.NodeType = ExpressionType.Convert Then
        Dim convertExpression = DirectCast(propertyExpression.Body, UnaryExpression)
        expression = convertExpression.Operand
      Else
        expression = propertyExpression.Body
      End If

      Dim memberExpression = TryCast(expression, MemberExpression)
      Return memberExpression.Member.Name
    End Function

    ''' <summary>
    ''' Gets property type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    <Extension>
    Public Function GetPropertyType(Of T1, T2)(propertyExpression As Expression(Of Func(Of T1, T2))) As Type
      Dim expression As Expression

      If propertyExpression.Body.NodeType = ExpressionType.Convert Then
        Dim convertExpression = DirectCast(propertyExpression.Body, UnaryExpression)
        expression = convertExpression.Operand
      Else
        expression = propertyExpression.Body
      End If

      Dim memberExpression = TryCast(expression, MemberExpression)
      Return memberExpression.Type
    End Function

  End Module
End Namespace

