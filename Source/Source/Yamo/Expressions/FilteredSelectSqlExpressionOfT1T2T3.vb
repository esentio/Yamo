﻿Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents WHERE clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  Public Class FilteredSelectSqlExpression(Of T1, T2, T3)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="FilteredSelectSqlExpression(Of T1, T2, T3)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T1, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T1, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T2, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {1})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T2, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {1})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T3, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {2})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T3, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {2})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T1, T2, T3, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {0, 1, 2})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T1, T2, T3, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, {0, 1, 2})
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of Join(Of T1, T2, T3), Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of Join(Of T1, T2, T3), FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Return InternalWhere(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds AND condition to WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As String) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddWhere(predicate)
      Return Me
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return Me
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3)
      Return InternalGroupBy(Of TKey)(keySelector, {0})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3)
      Return InternalGroupBy(Of TKey)(keySelector, {1})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3)
      Return InternalGroupBy(Of TKey)(keySelector, {2})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T1, T2, T3, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3)
      Return InternalGroupBy(Of TKey)(keySelector, {0, 1, 2})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3), TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3)
      Return InternalGroupBy(Of TKey)(keySelector, Nothing)
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddGroupBy(keySelector, entityIndexHints)
      Return New GroupedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3)
      Return InternalLimit(Nothing, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3)
      Return InternalLimit(offset, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddLimit(offset, count)
      Return New LimitedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT COUNT(*) clause, executes SQL query and returns the result.
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {1})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T3, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {2})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, T2, T3, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1, 2})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace
