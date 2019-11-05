Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents GROUP BY clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  ''' <typeparam name="T5"></typeparam>
  ''' <typeparam name="T6"></typeparam>
  ''' <typeparam name="T7"></typeparam>
  ''' <typeparam name="T8"></typeparam>
  ''' <typeparam name="T9"></typeparam>
  Public Class GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Having(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalHaving(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Having(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalHaving(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Having(predicate As String) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddHaving(predicate)
      Return New HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds HAVING clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function Having() As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return New HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds HAVING clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddHaving(predicate, entityIndexHints)
      Return New HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
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
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7), GetType(T8), GetType(T9))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), TResult))) As CustomSelectSqlExpression(Of TResult)
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
