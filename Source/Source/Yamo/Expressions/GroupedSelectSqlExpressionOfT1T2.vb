Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  Public Class GroupedSelectSqlExpression(Of T1, T2)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Having(predicate As Expression(Of Func(Of T1, Boolean))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, {0})
    End Function

    Public Function Having(predicate As Expression(Of Func(Of T1, FormattableString))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, {0})
    End Function

    Public Function Having(predicate As Expression(Of Func(Of T2, Boolean))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, {1})
    End Function

    Public Function Having(predicate As Expression(Of Func(Of T2, FormattableString))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, {1})
    End Function

    Public Function Having(predicate As Expression(Of Func(Of T1, T2, Boolean))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, {0, 1})
    End Function

    Public Function Having(predicate As Expression(Of Func(Of T1, T2, FormattableString))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, {0, 1})
    End Function

    Public Function Having(predicate As Expression(Of Func(Of Join(Of T1, T2), Boolean))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, Nothing)
    End Function

    Public Function Having(predicate As Expression(Of Func(Of Join(Of T1, T2), FormattableString))) As HavingSelectSqlExpression(Of T1, T2)
      Return InternalHaving(predicate, Nothing)
    End Function

    Public Function Having(predicate As String) As HavingSelectSqlExpression(Of T1, T2)
      Me.Builder.AddHaving(predicate)
      Return New HavingSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    Public Function Having() As HavingSelectSqlExpression(Of T1, T2)
      Return New HavingSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of T1, T2)
      Me.Builder.AddHaving(predicate, entityIndexHints)
      Return New HavingSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2), TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2), TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2))
      Return New SelectedSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
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

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace