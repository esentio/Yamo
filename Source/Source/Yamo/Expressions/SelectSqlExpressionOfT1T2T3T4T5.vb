Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  Public Class SelectSqlExpression(Of T1, T2, T3, T4, T5)
    Inherits SelectSqlExpressionBase

    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {0, 5})
    End Function

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {1, 5})
    End Function

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {2, 5})
    End Function

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {3, 5})
    End Function

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T5, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {4, 5})
    End Function

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {0, 5})
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {1, 5})
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {2, 5})
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {3, 5})
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T5, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {4, 5})
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {0, 5})
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {1, 5})
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {2, 5})
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {3, 5})
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T5, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {4, 5})
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {0, 5})
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {1, 5})
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {2, 5})
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {3, 5})
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T5, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {4, 5})
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)
    End Function

    Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, Nothing)
    End Function

    Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, TJoined)(Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T1, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {0})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T1, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {0})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T2, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {1})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T2, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {1})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T3, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {2})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T3, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {2})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T4, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {3})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T4, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {3})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T5, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {4})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of T5, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, {4})
    End Function

    Public Function Where(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, Nothing)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalWhere(predicate, Nothing)
    End Function

    Public Function Where(predicate As String) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddWhere(predicate)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, True)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, {4}, False)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ExecuteScalar(Of Int32)(query)
    End Function

  End Class
End Namespace