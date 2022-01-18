Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 6 tables (entities) and (at least) last one has defined table hints.
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  ''' <typeparam name="T5"></typeparam>
  ''' <typeparam name="T6"></typeparam>
  Public Class JoinWithHintsSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="JoinWithHintsSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of T1, T6, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOn(predicate, {0, 5})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of T2, T6, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOn(predicate, {1, 5})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of T3, T6, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOn(predicate, {2, 5})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of T4, T6, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOn(predicate, {3, 5})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of T5, T6, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOn(predicate, {4, 5})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](<DisallowNull> predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Return InternalOn(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalOn(predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)
      Me.Builder.AddOn(Of T6)(predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of JoinWithHintsSelectSqlExpression(Of T1, T2, T3, T4, T5, T6), TResult), <DisallowNull> otherwise As Func(Of JoinWithHintsSelectSqlExpression(Of T1, T2, T3, T4, T5, T6), TResult)) As TResult
      If condition Then
        Return [then].Invoke(Me)
      Else
        Return otherwise.Invoke(Me)
      End If
    End Function

  End Class
End Namespace
