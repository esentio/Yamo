Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 4 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  Public Class SelectSqlExpression(Of T1, T2, T3, T4)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpression(Of T1, T2, T3, T4)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {0, 4})
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {1, 4})
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {2, 4})
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, {3, 4})
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function Join(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner)
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, tableSource)
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {0, 4})
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {1, 4})
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {2, 4})
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, {3, 4})
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSource)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {0, 4})
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {1, 4})
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {2, 4})
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, {3, 4})
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSource)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T1, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {0, 4})
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T2, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {1, 4})
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T3, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {2, 4})
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of T4, TJoined, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, {3, 4})
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSource)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, {0, 1})
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)(tableSource As FormattableString) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Me.Builder.AddJoin(Of TJoined)(JoinType.CrossJoin, tableSource)
      Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)(tableSource As RawSqlString, ParamArray parameters() As Object) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Me.Builder.AddJoin(Of TJoined)(JoinType.CrossJoin, tableSource, parameters)
      Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType)
      Return New JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, tableSource)
      Return New JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, tableSource, parameters)
      Return New JoinSelectSqlExpression(Of T1, T2, T3, T4, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T1, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T1, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T2, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {1})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T2, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {1})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T3, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {2})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T3, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {2})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T4, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {3})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T4, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, {3})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4), Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4), FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalWhere(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Where(predicate As String, ParamArray parameters() As Object) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.AddWhere(predicate, parameters)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function Where() As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalGroupBy(Of TKey)(keySelector, {0})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalGroupBy(Of TKey)(keySelector, {1})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalGroupBy(Of TKey)(keySelector, {2})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalGroupBy(Of TKey)(keySelector, {3})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of T1, T2, T3, T4, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalGroupBy(Of TKey)(keySelector, {0, 1, 2, 3})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4), TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalGroupBy(Of TKey)(keySelector, Nothing)
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.AddGroupBy(keySelector, entityIndexHints)
      Return New GroupedSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalOrderBy(Of TKey)(keySelector, {3}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
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
    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalLimit(Nothing, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4)
      Return InternalLimit(offset, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.AddLimit(offset, count)
      Return New LimitedSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of T1, T2, T3, T4, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1, 2, 3})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4), TResult))) As CustomSelectSqlExpression(Of TResult)
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
    Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of SelectSqlExpression(Of T1, T2, T3, T4), TResult), Optional otherwise As Func(Of SelectSqlExpression(Of T1, T2, T3, T4), TResult) = Nothing) As TResult
      Dim result As TResult

      If condition Then
        result = [then].Invoke(Me)
      ElseIf otherwise Is Nothing Then
        Me.Builder.StartConditionalIgnoreMode()
        result = [then].Invoke(Me)
        Me.Builder.EndConditionalIgnoreMode()
      Else
        result = otherwise.Invoke(Me)
      End If

      Return result
    End Function

  End Class
End Namespace
