Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class HavingSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function [And](predicate As Expression(Of Func(Of T, Boolean))) As HavingSelectSqlExpression(Of T)
      Return InternalHaving(predicate, {0})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T, FormattableString))) As HavingSelectSqlExpression(Of T)
      Return InternalHaving(predicate, {0})
    End Function

    Public Function [And](predicate As String) As HavingSelectSqlExpression(Of T)
      Me.Builder.AddHaving(predicate)
      Return Me
    End Function

    Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of T)
      Me.Builder.AddHaving(predicate, entityIndexHints)
      Return Me
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey)), ascending As Boolean) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(keySelector, {0}, ascending)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T)
      Me.Builder.AddSelectAll(GetType(T))
      Return New SelectedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ExecuteScalar(Of Int32)(query)
    End Function

    Public Function FirstOrDefault() As T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T)(query)
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace