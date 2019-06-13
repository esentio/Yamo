Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class DistinctSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T)(query)
    End Function

    Public Function FirstOrDefault() As T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T)(query)
    End Function

  End Class
End Namespace