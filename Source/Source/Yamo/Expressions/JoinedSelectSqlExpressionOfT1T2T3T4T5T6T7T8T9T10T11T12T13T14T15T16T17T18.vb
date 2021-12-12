Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 18 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  ''' <typeparam name="T5"></typeparam>
  ''' <typeparam name="T6"></typeparam>
  ''' <typeparam name="T7"></typeparam>
  ''' <typeparam name="T8"></typeparam>
  ''' <typeparam name="T9"></typeparam>
  ''' <typeparam name="T10"></typeparam>
  ''' <typeparam name="T11"></typeparam>
  ''' <typeparam name="T12"></typeparam>
  ''' <typeparam name="T13"></typeparam>
  ''' <typeparam name="T14"></typeparam>
  ''' <typeparam name="T15"></typeparam>
  ''' <typeparam name="T16"></typeparam>
  ''' <typeparam name="T17"></typeparam>
  ''' <typeparam name="T18"></typeparam>
  Public Class JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)
    Inherits SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)

    ''' <summary>
    ''' Creates new instance of <see cref="JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17), TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Private Function InternalAs(relationship As Expression) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)
      Me.Builder.SetLastJoinRelationship(relationship)
      Return New SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Overloads Function [If](Of TResult)(condition As Boolean, [then] As Func(Of JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18), TResult), Optional otherwise As Func(Of JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18), TResult) = Nothing) As TResult
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

      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(SelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinWithHintsSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(JoinedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(FilteredSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(GroupedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(HavingSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
      If genericType Is GetType(OrderedSelectSqlExpression(Of ,,,,,,,,,,,,,,,,,,,,,,,,)) Then Return True
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
