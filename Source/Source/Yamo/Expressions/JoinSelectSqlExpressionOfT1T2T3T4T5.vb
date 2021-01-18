Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 5 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  ''' <typeparam name="T5"></typeparam>
  Public Class JoinSelectSqlExpression(Of T1, T2, T3, T4, T5)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="JoinSelectSqlExpression(Of T1, T2, T3, T4, T5)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Adds table hint(s).
    ''' </summary>
    ''' <param name="tableHints"></param>
    ''' <returns></returns>
    Public Function WithHints(tableHints As String) As JoinWithHintsSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.SetLastJoinTableHints(tableHints)
      Return New JoinWithHintsSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of T1, T5, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOn(predicate, {0, 4})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of T2, T5, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOn(predicate, {1, 4})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of T3, T5, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOn(predicate, {2, 4})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of T4, T5, Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOn(predicate, {3, 4})
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [On](predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Return InternalOn(predicate, Nothing)
    End Function

    ''' <summary>
    ''' Adds ON clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalOn(predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)
      Me.Builder.AddOn(Of T5)(predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of JoinSelectSqlExpression(Of T1, T2, T3, T4, T5), TResult), Optional otherwise As Func(Of JoinSelectSqlExpression(Of T1, T2, T3, T4, T5), TResult) = Nothing) As TResult
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

      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,)) Then Return True

      Return False
    End Function

  End Class
End Namespace
