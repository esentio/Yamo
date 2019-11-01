Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    Friend Sub New(context As DbContext)
      MyBase.New(New SelectSqlExpressionBuilder(context), New QueryExecutor(context))
      Me.Builder.SetMainTable(Of T)()
    End Sub

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {0, 1})
    End Function

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {0, 1})
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {0, 1})
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {0, 1})
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)
    End Function

    Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, {0, 1})
    End Function

    Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T, TJoined)(Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T, Boolean))) As FilteredSelectSqlExpression(Of T)
      Return InternalWhere(predicate, {0})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T, FormattableString))) As FilteredSelectSqlExpression(Of T)
      Return InternalWhere(predicate, {0})
    End Function

    Public Function Where(predicate As String) As FilteredSelectSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function Where() As FilteredSelectSqlExpression(Of T)
      Return New FilteredSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return New FilteredSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As GroupedSelectSqlExpression(Of T)
      Return InternalGroupBy(Of TKey)(keySelector, {0})
    End Function

    Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of T)
      Me.Builder.AddGroupBy(keySelector, entityIndexHints)
      Return New GroupedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey)), ascending As Boolean) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(keySelector, {0}, ascending)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T)
      Me.Builder.AddSelectAll(GetType(T))
      Return New SelectedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace