Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {5}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T7, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {6}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {5}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T7, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, {6}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
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

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T4, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {3})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T5, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {4})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T6, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {5})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T7, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {6})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1, 2, 3, 4, 5, 6})
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace