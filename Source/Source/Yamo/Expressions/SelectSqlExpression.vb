Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    Friend Sub New(context As DbContext)
      MyBase.New(New SelectSqlExpressionBuilder(context), New QueryExecutor(context))
      Me.Builder.SetMainTable(Of T)()
    End Sub

    ''' <summary>
    ''' Adds INNER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {0, 1})
    End Function

    ''' <summary>
    ''' Adds INNER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {0, 1})
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {0, 1})
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {0, 1})
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of T, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, {0, 1})
    End Function

    ''' <summary>
    ''' Adds JOIN statement.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T, Boolean))) As FilteredSelectSqlExpression(Of T)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T, FormattableString))) As FilteredSelectSqlExpression(Of T)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As String) As FilteredSelectSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <returns></returns>
    Public Function Where() As FilteredSelectSqlExpression(Of T)
      Return New FilteredSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return New FilteredSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds GROUP BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As GroupedSelectSqlExpression(Of T)
      Return InternalGroupBy(Of TKey)(keySelector, {0})
    End Function

    ''' <summary>
    ''' Adds GROUP BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of T)
      Me.Builder.AddGroupBy(keySelector, entityIndexHints)
      Return New GroupedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T, TKey))) As OrderedSelectSqlExpression(Of T)
      Return InternalOrderBy(keySelector, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY statement.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression(Of Func(Of T, TKey)), ascending As Boolean) As OrderedSelectSqlExpression(Of T)
      Me.Builder.AddOrderBy(keySelector, {0}, ascending)
      Return New OrderedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT statement with all columns of the table (entity).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T)
      Me.Builder.AddSelectAll(GetType(T))
      Return New SelectedSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
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