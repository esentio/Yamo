Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class CustomSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Distinct() As CustomDistinctSqlExpression(Of T)
      Me.Builder.AddDistinct()
      Return New CustomDistinctSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadCustomList(Of T)(query)
    End Function

    Public Function FirstOrDefault() As T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadCustomFirstOrDefault(Of T)(query)
    End Function

  End Class
End Namespace