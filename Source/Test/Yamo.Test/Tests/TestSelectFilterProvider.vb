Imports System.Linq.Expressions
Imports Yamo
Imports Yamo.Expressions.Builders
Imports Yamo.Test.Model

Namespace Tests

  Public Class TestSelectFilterProvider
    Implements ISelectFilterProvider

    Public Sub AddWhere(Of T)(builder As SelectSqlExpressionBuilder) Implements ISelectFilterProvider.AddWhere
      If GetType(T) IsNot GetType(ItemWithAllSupportedValues) Then
        Throw New NotSupportedException
      End If

      builder.AddWhere(GetFilter(), {0})
    End Sub

    Private Function GetFilter() As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))
      Return Function(x) x.IntColumn <= 3
    End Function

  End Class
End Namespace
