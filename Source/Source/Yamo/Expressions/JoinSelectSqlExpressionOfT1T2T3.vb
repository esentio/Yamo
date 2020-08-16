Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 3 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  Public Class JoinSelectSqlExpression(Of T1, T2, T3)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="JoinSelectSqlExpression(Of T1, T2, T3)"/>.
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
    Public Function [On](predicate As Expression(Of Func(Of T1, T3, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOn(predicate, {0, 2})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of T2, T3, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOn(predicate, {1, 2})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of T1, T2, T3, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOn(predicate, {0, 1, 2})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of Join(Of T1, T2, T3), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3)
      Return InternalOn(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalOn(predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.AddOn(Of T3)(predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of JoinSelectSqlExpression(Of T1, T2, T3), TResult), otherwise As Func(Of JoinSelectSqlExpression(Of T1, T2, T3), TResult)) As TResult
      If condition Then
        Return [then].Invoke(Me)
      Else
        Return otherwise.Invoke(Me)
      End If
    End Function

  End Class
End Namespace
