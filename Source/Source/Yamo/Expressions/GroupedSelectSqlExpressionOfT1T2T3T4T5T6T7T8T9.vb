Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
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

    Public Function Having(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), Boolean))) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalHaving(predicate, Nothing)
    End Function

    Public Function Having(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), FormattableString))) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalHaving(predicate, Nothing)
    End Function

    Public Function Having(predicate As String) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddHaving(predicate)
      Return New HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    Public Function Having() As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return New HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    Private Function InternalHaving(predicate As Expression, entityIndexHints As Int32()) As HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddHaving(predicate, entityIndexHints)
      Return New HavingSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7), GetType(T8), GetType(T9))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace
