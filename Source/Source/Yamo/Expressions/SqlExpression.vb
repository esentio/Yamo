Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SqlExpression
    Inherits SqlExpressionBase

    Protected Property Builder As SqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Friend Sub New(context As DbContext)
      Me.Builder = New SqlExpressionBuilder(context)
      Me.Executor = New QueryExecutor(context)
    End Sub

    Public Function Execute(sql As FormattableString) As Int32
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.Execute(query)
    End Function

    Public Function Execute(sql As RawSqlString) As Int32
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.Execute(query)
    End Function

    Public Function QueryFirstOrDefault(Of T)(sql As FormattableString) As T
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.QueryFirstOrDefault(Of T)(query)
    End Function

    Public Function QueryFirstOrDefault(Of T)(sql As RawSqlString) As T
      Dim query = Me.Builder.CreateQuery(sql)
      Return Me.Executor.QueryFirstOrDefault(Of T)(query)
    End Function

    Public Function Query(Of T)(sql As FormattableString) As List(Of T)
      Dim q = Me.Builder.CreateQuery(sql)
      Return Me.Executor.QueryList(Of T)(q)
    End Function

    Public Function Query(Of T)(sql As RawSqlString) As List(Of T)
      Dim q = Me.Builder.CreateQuery(sql)
      Return Me.Executor.QueryList(Of T)(q)
    End Function

  End Class
End Namespace
