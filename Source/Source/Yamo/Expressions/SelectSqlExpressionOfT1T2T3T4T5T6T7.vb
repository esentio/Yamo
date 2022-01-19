Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 7 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  ''' <typeparam name="T5"></typeparam>
  ''' <typeparam name="T6"></typeparam>
  ''' <typeparam name="T7"></typeparam>
  Public Class SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)"/>.
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
    Public Function Join(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function Join(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner)
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, tableSource)
    End Function

    ''' <summary>
    ''' Adds INNER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Join(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSource)
    End Function

    ''' <summary>
    ''' Adds LEFT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function LeftJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSource)
    End Function

    ''' <summary>
    ''' Adds RIGHT OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function RightJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)() As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSource)
    End Function

    ''' <summary>
    ''' Adds FULL OUTER JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function FullJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, tableSource, parameters)
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, {0, 1})
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)(<DisallowNull> tableSource As FormattableString) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Me.Builder.AddJoin(Of TJoined)(JoinType.CrossJoin, tableSource)
      Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds CROSS JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function CrossJoin(Of TJoined)(<DisallowNull> tableSource As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Me.Builder.AddJoin(Of TJoined)(JoinType.CrossJoin, tableSource, parameters)
      Me.Builder.AddOn(Of TJoined)(Nothing, {0, 1})
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType)
      Return New JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSource As FormattableString) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, tableSource)
      Return New JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds JOIN clause.
    ''' </summary>
    ''' <typeparam name="TJoined"></typeparam>
    ''' <param name="joinType"></param>
    ''' <param name="tableSource"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Private Function InternalJoin(Of TJoined)(joinType As JoinType, tableSource As RawSqlString, ParamArray parameters() As Object) As JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, tableSource, parameters)
      Return New JoinSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, TJoined)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T1, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T1, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {0})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T2, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {1})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T2, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {1})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T3, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {2})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T3, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {2})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T4, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {3})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T4, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {3})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T5, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {4})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T5, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {4})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T6, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {5})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T6, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {5})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T7, Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {6})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of T7, FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, {6})
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalWhere(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Where(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddWhere(predicate, parameters)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function Where() As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {0})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {1})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {2})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {3})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {4})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {5})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {6})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, {0, 1, 2, 3, 4, 5, 6})
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function GroupBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalGroupBy(Of TKey)(keySelector, Nothing)
    End Function

    ''' <summary>
    ''' Adds GROUP BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddGroupBy(keySelector, entityIndexHints)
      Return New GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T1, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {0}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T2, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {1}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T3, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {2}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {3}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T4, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {3}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {4}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T5, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {4}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {5}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T6, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {5}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {6}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of T7, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {6}, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function OrderBy(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddOrderBy(predicate, True, parameters)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T1, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {0}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T2, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {1}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T3, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {2}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {3}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T4, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {3}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {4}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T5, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {4}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {5}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T6, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {5}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {6}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of T7, FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, {6}, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(Of TKey)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), FormattableString))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalOrderBy(keySelector, Nothing, False)
    End Function

    ''' <summary>
    ''' Adds ORDER BY DESC clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function OrderByDescending(<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddOrderBy(predicate, False, parameters)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ORDER BY clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="ascending"></param>
    ''' <returns></returns>
    Private Function InternalOrderBy(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalLimit(Nothing, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Function Limit(offset As Int32, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalLimit(offset, count)
    End Function

    ''' <summary>
    ''' Adds clause to limit rows returned by the query. Depending on the database, LIMIT, TOP or OFFSET FETCH clause is used.
    ''' </summary>
    ''' <param name="offset"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Private Function InternalLimit(offset As Int32?, count As Int32) As LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddLimit(offset, count)
      Return New LimitedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SELECT clause with all columns of all tables (entities).
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
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
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T1, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T2, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {1})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T3, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {2})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T4, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {3})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T5, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {4})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T6, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {5})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T7, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {6})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, {0, 1, 2, 3, 4, 5, 6})
    End Function

    ''' <summary>
    ''' Adds SELECT clause with custom columns selection.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Public Function [Select](Of TResult)(<DisallowNull> selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7), TResult))) As CustomSelectSqlExpression(Of TResult)
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
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7), TResult), Optional otherwise As Func(Of SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7), TResult) = Nothing) As TResult
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

      Dim thisTypeGenericArgumentsCount = thisType.GenericTypeArguments.Length
      Dim resultTypeGenericArguments = resultType.GenericTypeArguments
      Dim resultTypeGenericArgumentsCount = resultTypeGenericArguments.Length

      If thisTypeGenericArgumentsCount < resultTypeGenericArgumentsCount Then
        For i = thisTypeGenericArgumentsCount To resultTypeGenericArgumentsCount - 1
          Me.Builder.AddIgnoredJoin(resultTypeGenericArguments(i))
        Next
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

      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(LimitedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True

      Return False
    End Function

  End Class
End Namespace
