Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents ORDER BY clause in SQL SELECT statement.
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
  ''' <typeparam name="T10"></typeparam>
  ''' <typeparam name="T11"></typeparam>
  Public Class OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
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
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7), GetType(T8), GetType(T9), GetType(T10), GetType(T11))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11), TResult))) As CustomSelectSqlExpression(Of TResult)
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