Imports System.Linq.Expressions

Namespace Internal

  ''' <summary>
  ''' Represents a visitor that replaces single parameter in the expression.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ReplaceParameterVisitor
    Inherits ExpressionVisitor

    ''' <summary>
    ''' Original parameter expression
    ''' </summary>
    Private m_Original As ParameterExpression

    ''' <summary>
    ''' Replacement parameter expression
    ''' </summary>
    Private m_Replacement As ParameterExpression

    ''' <summary>
    ''' Replaces single parameter in the node.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="original"></param>
    ''' <param name="replacement"></param>
    ''' <returns></returns>
    Public Function Replace(node As Expression, original As ParameterExpression, replacement As ParameterExpression) As Expression
      m_Original = original
      m_Replacement = replacement

      Dim result = Visit(node)

      m_Original = Nothing
      m_Replacement = Nothing

      Return result
    End Function

    ''' <summary>
    ''' Visits parameter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Protected Overrides Function VisitParameter(node As ParameterExpression) As Expression
      If node Is m_Original Then
        node = m_Replacement
      End If

      Return MyBase.VisitParameter(node)
    End Function

  End Class
End Namespace