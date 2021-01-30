Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SELECT clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  Public Class SelectedSelectSqlExpression(Of T1, T2)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectedSelectSqlExpression(Of T1, T2)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T1, TProperty))) As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of T2, TProperty))) As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(propertyExpression As Expression(Of Func(Of Join(Of T1, T2), TProperty))) As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes all columns of 2nd table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT2() As SelectedSelectSqlExpression(Of T1, T2)
      Return InternalExclude(1)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    ''' <summary>
    ''' Excludes all columns of the table (entity) from SELECT clause.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <returns></returns>
    Private Function InternalExclude(entityIndex As Int32) As SelectedSelectSqlExpression(Of T1, T2)
      Me.Builder.ExcludeSelected(entityIndex)
      Return Me
    End Function

    ''' <summary>
    ''' Adds DISTINCT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function Distinct() As DistinctSelectSqlExpression(Of T1)
      Me.Builder.AddDistinct()
      Return New DistinctSelectSqlExpression(Of T1)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, [then] As Func(Of SelectedSelectSqlExpression(Of T1, T2), TResult), Optional otherwise As Func(Of SelectedSelectSqlExpression(Of T1, T2), TResult) = Nothing) As TResult
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

      If genericType Is GetType(SelectedSelectSqlExpression(Of ,)) Then Return True
      If genericType Is GetType(DistinctSelectSqlExpression(Of )) Then Return True

      Return False
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToList() As List(Of T1)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T1)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or a default value.
    ''' </summary>
    ''' <param name="behavior">Defines how collection navigation properties are filled. This setting has no effect if no collection navigation properties are used.</param>
    ''' <returns></returns>
    Public Function FirstOrDefault(Optional behavior As CollectionNavigationFillBehavior = CollectionNavigationFillBehavior.ProcessOnlyFirstRow) As T1
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T1)(query, behavior)
    End Function

  End Class
End Namespace
