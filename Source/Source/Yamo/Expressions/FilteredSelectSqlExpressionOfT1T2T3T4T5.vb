Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function [And](predicate As Expression(Of Func(Of T1, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {0})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T1, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {0})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T2, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {1})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T2, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {1})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T3, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {2})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T3, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {2})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T4, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {3})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T4, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {3})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T5, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {4})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of T5, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {4})
    End Function

    Public Function [And](predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, Nothing)
    End Function

    Public Function [And](predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, Nothing)
    End Function

    Public Function [And](predicate As String) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddWhere(predicate)
      Return Me
    End Function

    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return Me
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ExecuteScalar(Of Int32)(query)
    End Function

  End Class
End Namespace