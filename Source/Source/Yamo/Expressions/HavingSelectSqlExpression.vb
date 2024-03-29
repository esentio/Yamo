﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
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
    Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of T, Boolean))) As HavingSelectSqlExpression(Of T)
      Return InternalHaving(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds AND condition to HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [And](<DisallowNull> predicate As Expression(Of Func(Of T, FormattableString))) As HavingSelectSqlExpression(Of T)
      Return InternalHaving(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds AND condition to HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function [And](<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As HavingSelectSqlExpression(Of T)
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
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T, FormattableString))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(predicate, True, parameters)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="provider"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> provider As ISelectSortProvider) As OrderedSelectSqlExpression(Of T)
      provider.AddOrderBy(Of T)(Me.Builder)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T, FormattableString))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(predicate, False, parameters)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
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
      Me.Builder.AddSelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns)
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
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T, TResult)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0}, behavior)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32(), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints, behavior)
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
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of HavingSelectSqlExpression(Of T), TResult), Optional otherwise As Func(Of HavingSelectSqlExpression(Of T), TResult) = Nothing) As TResult
      If condition Then
        Return [then].Invoke(Me)
      ElseIf otherwise Is Nothing Then
        Return CreateResultForCondition(Of TResult)()
      Else
        Return otherwise.Invoke(Me)
      End If
    End Function

    ''' <summary>
    ''' Creates result for condition if condition is not met.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <returns></returns>
    Private Function CreateResultForCondition(Of TResult)() As TResult
      Dim thisType = Me.GetType()
      Dim resultType = GetType(TResult)

      If thisType Is resultType Then
        Return DirectCast(DirectCast(Me, Object), TResult)
      End If

      If Not CanCreateResultForCondition(resultType) Then
        Throw New InvalidOperationException($"Parameter 'otherwise' in If() method is required for return type '{resultType}'.")
      End If

      Return DirectCast(Activator.CreateInstance(resultType, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance, Nothing, {Me.Builder, Me.Executor}, Nothing), TResult)
    End Function

    ''' <summary>
    ''' Checks if result can be created if condition is not met.
    ''' </summary>
    ''' <param name="resultType"></param>
    ''' <returns></returns>
    Private Function CanCreateResultForCondition(resultType As Type) As Boolean
      If Not GetType(SelectSqlExpressionBase).IsAssignableFrom(resultType) Then
        Return False
      End If

      If Not resultType.IsGenericType Then
        Return False
      End If

      Dim genericType = resultType.GetGenericTypeDefinition()

      If genericType Is GetType(HavingSelectSqlExpression(Of )) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of )) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of )) Then Return True

      Return False
    End Function

  End Class
End Namespace
