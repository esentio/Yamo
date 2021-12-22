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
  Public Class OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)"/>.
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
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T1, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T2, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T3, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {3}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T4, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {3}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {4}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T5, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {4}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {5}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T6, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {5}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T7, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {6}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T7, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {6}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T8, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {7}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T8, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {7}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T9, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {8}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T9, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {8}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T10, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {9}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of T10, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {9}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function ThenBy(predicate As String, ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Me.Builder.AddOrderBy(predicate, True, parameters)
      Return Me
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T1, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T2, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T3, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {3}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T4, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {3}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {4}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T5, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {4}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {5}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T6, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {5}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T7, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {6}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T7, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {6}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T8, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {7}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T8, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {7}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T9, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {8}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T9, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {8}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T10, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {9}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of T10, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, {9}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalOrderBy(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(predicate As String, ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Me.Builder.AddOrderBy(predicate, False, parameters)
      Return Me
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalLimit(Nothing, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Return InternalLimit(offset, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Me.Builder.AddLimit(offset, count)
      Return New LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7), GetType(T8), GetType(T9), GetType(T10))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T4, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {3})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T5, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {4})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T6, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {5})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T7, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {6})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T8, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {7})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T9, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {8})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T10, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {9})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TResult))) As CustomSelectSqlExpression(Of TResult)
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

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TResult), Optional otherwise As Func(Of OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TResult) = Nothing) As TResult
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

      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,)) Then Return True

      Return False
    End Function

  End Class
End Namespace
