Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function ThenBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function ThenByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return Me
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7), GetType(T8))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace