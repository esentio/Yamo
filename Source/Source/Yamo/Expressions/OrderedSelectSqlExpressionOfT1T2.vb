Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class OrderedSelectSqlExpression(Of T1, T2)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of T1, T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {0, 1}, True)
    End Function

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2), TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, {0, 1}, False)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2), TKey))) As OrderedSelectSqlExpression(Of T1, T2)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2))
      Return New SelectedSelectSqlExpression(Of T1, T2)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ExecuteScalar(Of Int32)(query)
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