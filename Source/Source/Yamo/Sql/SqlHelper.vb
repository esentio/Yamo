Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Base class for API that provides support for SQL helper methods.
  ''' </summary>
  Public MustInherit Class SqlHelper

    ''' <summary>
    ''' Creates new instance of <see cref="SqlHelper"/>.
    ''' </summary>
    Protected Sub New()
    End Sub

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This method has to be overloaded.
    ''' </summary>
    ''' <param name="method">SQL helper method call expression</param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Shared Function GetSqlFormat(method As MethodCallExpression, dialectProvider As SqlDialectProvider) As SqlFormat
      Throw New Exception("This method has to be overloaded.")
    End Function

    ''' <summary>
    ''' Flattens param array arguments. Returned result contains all method arguments including all param array items in one list.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <returns></returns>
    Protected Shared Function FlattenArguments(method As MethodCallExpression) As IReadOnlyList(Of Expression)
      Dim argCount = method.Arguments.Count

      If argCount = 0 Then
        Return {}
      End If

      Dim lastArg = method.Arguments(argCount - 1)

      If lastArg.NodeType = Global.System.Linq.Expressions.ExpressionType.NewArrayBounds Then
        ' param array is empty
        Return method.Arguments.Take(argCount - 1).ToArray()
      ElseIf lastArg.NodeType = Global.System.Linq.Expressions.ExpressionType.NewArrayInit Then
        Dim newArrayExp = DirectCast(lastArg, NewArrayExpression)
        Return method.Arguments.Take(argCount - 1).Concat(newArrayExp.Expressions).ToArray()
      Else
        Throw New NotSupportedException($"Last argument of '{method.Method.Name}' method call expression is not a param array.")
      End If
    End Function

  End Class
End Namespace