Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class CustomSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateCustomQuery()
      Return Me.Executor.ReadCustomList(Of T)(query)
    End Function

    Public Function FirstOrDefault() As T
      Dim query = Me.Builder.CreateCustomQuery()
      Return Me.Executor.ReadCustomFirstOrDefault(Of T)(query)
    End Function

  End Class
End Namespace