﻿Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ''' <summary>
  ''' Represents SELECT clause in SQL SELECT statement.
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  Public Class SelectedSelectSqlExpression(Of T1, T2, T3)
    Inherits SelectSqlExpressionBase
    Implements ISubqueryableSelectSqlExpression(Of T1)

    ''' <summary>
    ''' Creates new instance of <see cref="SelectedSelectSqlExpression(Of T1, T2, T3)"/>.
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
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T1, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T2, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T3, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of Join(Of T1, T2, T3), TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes all columns of 2nd table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT2() As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalExclude(1)
    End Function

    ''' <summary>
    ''' Excludes all columns of 3rd table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT3() As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalExclude(2)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    ''' <summary>
    ''' Excludes all columns of the table (entity) from SELECT clause.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <returns></returns>
    Private Function InternalExclude(entityIndex As Int32) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.ExcludeSelected(entityIndex)
      Return Me
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T1)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(action, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T2)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(action, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T3)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(action, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {0}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {0}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {0}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {1}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {1}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {1}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {2}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {2}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, {2}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of Join(Of T1, T2, T3))), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(action, Nothing, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3), TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of Join(Of T1, T2, T3), TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Return InternalInclude(keySelector, valueSelector, Nothing, Nothing, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function InternalInclude(action As Expression, entityIndexHints As Int32(), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.IncludeToSelected(action, entityIndexHints, behavior)
      Return Me
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="keySelectorEntityIndexHints"></param>
    ''' <param name="valueSelectorEntityIndexHints"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function InternalInclude(keySelector As Expression, valueSelector As Expression, keySelectorEntityIndexHints As Int32(), valueSelectorEntityIndexHints As Int32(), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3)
      Me.Builder.IncludeToSelected(keySelector, valueSelector, keySelectorEntityIndexHints, valueSelectorEntityIndexHints, behavior)
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
    ''' Adds UNION operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function Union(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T1))) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Union, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds UNION operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function Union(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Union, queryExpression)
    End Function

    ''' <summary>
    ''' Adds UNION operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Union(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Union, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds UNION ALL operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function UnionAll(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T1))) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.UnionAll, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds UNION ALL operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function UnionAll(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.UnionAll, queryExpression)
    End Function

    ''' <summary>
    ''' Adds UNION ALL operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function UnionAll(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.UnionAll, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds EXCEPT operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function Except(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T1))) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Except, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds EXCEPT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function Except(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Except, queryExpression)
    End Function

    ''' <summary>
    ''' Adds EXCEPT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Except(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Except, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds INTERSECT operator.
    ''' </summary>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Public Function Intersect(<DisallowNull> queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T1))) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Intersect, queryExpressionFactory)
    End Function

    ''' <summary>
    ''' Adds INTERSECT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Public Function Intersect(<DisallowNull> queryExpression As FormattableString) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Intersect, queryExpression)
    End Function

    ''' <summary>
    ''' Adds INTERSECT operator.
    ''' </summary>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function Intersect(<DisallowNull> queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T1)
      Return InternalSet(SetOperator.Intersect, queryExpression, parameters)
    End Function

    ''' <summary>
    ''' Adds set operator.
    ''' </summary>
    ''' <param name="setOperator"></param>
    ''' <param name="queryExpressionFactory"></param>
    ''' <returns></returns>
    Private Function InternalSet(setOperator As SetOperator, queryExpressionFactory As Func(Of SubqueryContext, ISubqueryableSelectSqlExpression(Of T1))) As SetSelectSqlExpression(Of T1)
      Me.Builder.AddSet(Of T1)(Me.Executor, setOperator, queryExpressionFactory)
      Return New SetSelectSqlExpression(Of T1)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds set operator.
    ''' </summary>
    ''' <param name="setOperator"></param>
    ''' <param name="queryExpression"></param>
    ''' <returns></returns>
    Private Function InternalSet(setOperator As SetOperator, queryExpression As FormattableString) As SetSelectSqlExpression(Of T1)
      Me.Builder.AddSet(Of T1)(setOperator, queryExpression)
      Return New SetSelectSqlExpression(Of T1)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds set operator.
    ''' </summary>
    ''' <param name="setOperator"></param>
    ''' <param name="queryExpression"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Private Function InternalSet(setOperator As SetOperator, queryExpression As RawSqlString, <DisallowNull> ParamArray parameters() As Object) As SetSelectSqlExpression(Of T1)
      Me.Builder.AddSet(Of T1)(setOperator, queryExpression, parameters)
      Return New SetSelectSqlExpression(Of T1)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of SelectedSelectSqlExpression(Of T1, T2, T3), TResult), Optional otherwise As Func(Of SelectedSelectSqlExpression(Of T1, T2, T3), TResult) = Nothing) As TResult
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

      If genericType Is GetType(SelectedSelectSqlExpression(Of ,,)) Then Return True
      If genericType Is GetType(DistinctSelectSqlExpression(Of )) Then Return True
      If genericType Is GetType(SetSelectSqlExpression(Of )) Then Return True

      Return False
    End Function

    ''' <summary>
    ''' Creates SQL subquery.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToSubquery() As Subquery(Of T1) Implements ISubqueryableSelectSqlExpression(Of T1).ToSubquery
      Return Me.Builder.CreateSubquery(Of T1)()
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
    Public Function FirstOrDefault(Optional behavior As CollectionNavigationFillBehavior = CollectionNavigationFillBehavior.ProcessOnlyFirstRow) As <MaybeNull> T1
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ReadFirstOrDefault(Of T1)(query, behavior)
    End Function

  End Class
End Namespace
