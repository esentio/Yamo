﻿Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents HAVING clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class HavingSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="HavingSelectSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds AND condition to HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T, Boolean))) As HavingSelectSqlExpression(Of T)
      Return InternalHaving(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds AND condition to HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](predicate As Expression(Of Func(Of T, FormattableString))) As HavingSelectSqlExpression(Of T)
      Return InternalHaving(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds AND condition to HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function [And](predicate As String, ParamArray parameters() As Object) As HavingSelectSqlExpression(Of T)
      Me.Builder.AddHaving(predicate, parameters)
      Return Me
    End Function

    ''' <summary>
    ''' Adds HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of T)
      Me.Builder.AddHaving(predicate, entityIndexHints)
      Return Me
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(Of TKey)(keySelector, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(Of TKey)(keySelector, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey)), ascending As Boolean) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(keySelector, {0}, ascending)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of T)
      Return InternalLimit(Nothing, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of T)
      Return InternalLimit(offset, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of T)
      Me.Builder.AddLimit(offset, count)
      Return New LimitedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of the table (entity).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T)
      Me.Builder.AddSelectAll(GetType(T))
      Return New SelectedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
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

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of HavingSelectSqlExpression(Of T), TResult), Optional otherwise As Func(Of HavingSelectSqlExpression(Of T), TResult) = Nothing) As TResult
      Dim result As TResult

      If condition Then
        result = [then].Invoke(Me)
      ElseIf otherwise Is Nothing Then
        Me.Builder.StartConditionalIgnoreMode()
        result = [then].Invoke(Me)
        Me.Builder.EndConditionalIgnoreMode()
      Else
        result = otherwise.Invoke(Me)
      End If

      Return result
    End Function

  End Class
End Namespace
