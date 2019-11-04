Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {5}, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, {5}, False)
    End Function

    ''' <summary>
    ''' Adds &lt;column&gt; DESC to ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    ''' <summary>
    ''' Adds SELECT statement with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT COUNT(*) statement, executes SQL query and returns the result.
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {1})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T3, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {2})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T4, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {3})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T5, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {4})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T6, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {5})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1, 2, 3, 4, 5})
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    ''' <summary>
    ''' Adds SELECT statement with custom columns selection.
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