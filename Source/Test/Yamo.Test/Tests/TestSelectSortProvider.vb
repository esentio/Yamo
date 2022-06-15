Imports System.Linq.Expressions
Imports Yamo
Imports Yamo.Expressions.Builders
Imports Yamo.Test.Model

Namespace Tests

  Public Class TestSelectSortProvider
    Implements ISelectSortProvider

    Public Sub AddOrderBy(Of T)(builder As SelectSqlExpressionBuilder) Implements ISelectSortProvider.AddOrderBy
      If GetType(T) IsNot GetType(Label) Then
        Throw New NotSupportedException
      End If

      builder.AddOrderBy(GetOrderBy(), {0}, True)
    End Sub

    Private Function GetOrderBy() As Expression(Of Func(Of Label, String))
      Return Function(x) x.Description
    End Function

  End Class
End Namespace
