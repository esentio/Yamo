Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class HavingSelectSqlExpression(Of T1, T2, T3)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function [And](predicate As Expression(Of Func(Of T1, Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {0})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T1, FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {0})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T2, Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {1})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T2, FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {1})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T3, Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {2})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T3, FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {2})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T1, T2, T3, Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {0, 1, 2})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T1, T2, T3, FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, {0, 1, 2})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of Join(Of T1, T2, T3), Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, Nothing)
    End Function

    Public Function [And](predicate As Expression(Of Func(Of Join(Of T1, T2, T3), FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3)
      Return InternalHaving(predicate, Nothing)
    End Function

    Public Function [And](predicate As String) As HavingSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddHaving(predicate)
      Return Me
    End Function

    Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddHaving(predicate, entityIndexHints)
      Return Me
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {1})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T3, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {2})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, T2, T3, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1, 2})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace