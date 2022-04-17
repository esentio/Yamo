Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ''' <summary>
  ''' Represents SELECT clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class SelectedSelectSqlExpression(Of T)
    Inherits SelectSqlExpressionBase
    Implements ISubqueryableSelectSqlExpression(Of T)

    ''' <summary>
    ''' Creates new instance of <see cref="SelectedSelectSqlExpression(Of T)"/>.
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
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T, TProperty))) As SelectedSelectSqlExpression(Of T)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T))) As SelectedSelectSqlExpression(Of T)
      Return InternalInclude(action, {0})
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T, TProperty))) As SelectedSelectSqlExpression(Of T)
      Return InternalInclude(keySelector, valueSelector, {0}, {0})
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalInclude(action As Expression, entityIndexHints As Int32()) As SelectedSelectSqlExpression(Of T)
      Me.Builder.IncludeToSelected(action, entityIndexHints)
      Return Me
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="keySelectorEntityIndexHints"></param>
    ''' <param name="valueSelectorEntityIndexHints"></param>
    ''' <returns></returns>
    Private Function InternalInclude(keySelector As Expression, valueSelector As Expression, keySelectorEntityIndexHints As Int32(), valueSelectorEntityIndexHints As Int32()) As SelectedSelectSqlExpression(Of T)
      Me.Builder.IncludeToSelected(keySelector, valueSelector, keySelectorEntityIndexHints, valueSelectorEntityIndexHints)
      Return Me
    End Function

    ''' <summary>
    ''' Adds DISTINCT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function Distinct() As DistinctSelectSqlExpression(Of T)
      Me.Builder.AddDistinct()
      Return New DistinctSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds UNION operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function Union(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T))) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Union, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds UNION operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function Union(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Union, queryExpression)
    End Function

    ''' <summary>
    ''' Adds UNION operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Union(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Union, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds UNION ALL operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function UnionAll(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T))) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.UnionAll, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds UNION ALL operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function UnionAll(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.UnionAll, queryExpression)
    End Function

    ''' <summary>
    ''' Adds UNION ALL operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function UnionAll(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.UnionAll, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds EXCEPT operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function Except(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T))) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Except, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds EXCEPT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function Except(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Except, queryExpression)
    End Function

    ''' <summary>
    ''' Adds EXCEPT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Except(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Except, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds INTERSECT operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function Intersect(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T))) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Intersect, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds INTERSECT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function Intersect(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Intersect, queryExpression)
    End Function

    ''' <summary>
    ''' Adds INTERSECT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Intersect(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T)
      Return InternalSet(SetOperator.Intersect, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds set operator.
    ''' </summary>
    ''' <param name="setOperator"></param>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Private Function InternalSet(setOperator As SetOperator, queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T))) As SetSelectSqlExpression(Of T)
      Me.Builder.AddSet(Of T)(Me.Executor, setOperator, queryExpressionFactory)
      Return New SetSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds set operator.
    ''' </summary>
    ''' <param name="setOperator"></param>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Private Function InternalSet(setOperator As SetOperator, queryExpression As FormattableString) As SetSelectSqlExpression(Of T)
      Me.Builder.AddSet(Of T)(setOperator, queryExpression)
      Return New SetSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds set operator.
    ''' </summary>
    ''' <param name="setOperator"></param>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Private Function InternalSet(setOperator As SetOperator, queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T)
      Me.Builder.AddSet(Of T)(setOperator, queryExpression, parameters)
      Return New SetSelectSqlExpression(Of T)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of SelectedSelectSqlExpression(Of T), TResult), Optional otherwise As Func(Of SelectedSelectSqlExpression(Of T), TResult) = Nothing) As TResult
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

      If genericType Is GetType(SelectedSelectSqlExpression(Of )) Then Return True
      If genericType Is GetType(DistinctSelectSqlExpression(Of )) Then Return True
      If genericType Is GetType(SetSelectSqlExpression(Of )) Then Return True

      Return False
    End Function

    ''' <summary>
    ''' Creates SQL subquery.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToSubquery() As Subquery(Of T) Implements ISubqueryableSelectSqlExpression(Of T).ToSubquery
      Return Me.Builder.CreateSubquery(Of T)()
    End Function

    ''' <summary>
    ''' Executes SQL query and returns list of records.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToList() As List(Of T)
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadList(Of T)(query)
    End Function

    ''' <summary>
    ''' Executes SQL query and returns first record or a default value.
    ''' </summary>
    ''' <param name="behavior">Defines how collection navigation properties are filled. This setting has no effect if no collection navigation properties are used.</param>
    ''' <returns></returns>
    Public Function FirstOrDefault(Optional behavior As CollectionNavigationFillBehavior = CollectionNavigationFillBehavior.ProcessOnlyFirstRow) As <MaybeNull> T
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T)(query, behavior)
    End Function

  End Class
End Namespace
