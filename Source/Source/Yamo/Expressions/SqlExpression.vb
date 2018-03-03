Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class SqlExpression
    Inherits SqlExpressionBase

    Protected Property Builder As SqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Friend Sub New(context As DbContext)
      Me.Builder = New SqlExpressionBuilder(context)
      Me.Executor = New QueryExecutor(context)
    End Sub

    Public Function ExecuteNonQuery(sql As FormattableString) As Int32
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.ExecuteNonQuery(query)
    End Function

    Public Function ExecuteNonQuery(sql As RawSqlString) As Int32
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.ExecuteNonQuery(query)
    End Function

    Public Function ExecuteScalar(Of T)(sql As FormattableString) As T
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.ExecuteScalar(Of T)(query)
    End Function

    Public Function ExecuteScalar(Of T)(sql As RawSqlString) As T
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.ExecuteScalar(Of T)(query)
    End Function

  End Class
End Namespace
