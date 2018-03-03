Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Internal.Extensions

  <Extension()>
  Public Module ExpressionExtensions

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

