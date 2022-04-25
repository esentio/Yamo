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
  Public Class SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
    Inherits SelectSqlExpressionBase
    Implements ISubqueryableSelectSqlExpression(Of T1)

    ''' <summary>
    ''' Creates new instance of <see cref="SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)"/>.
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
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T1, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T2, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T3, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T4, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T5, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T6, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T7, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T8, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T9, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T10, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T11, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T12, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T13, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of T14, TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Public Function Exclude(Of TProperty)(<DisallowNull> propertyExpression As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TProperty))) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(propertyExpression)
    End Function

    ''' <summary>
    ''' Excludes all columns of 2nd table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT2() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(1)
    End Function

    ''' <summary>
    ''' Excludes all columns of 3rd table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT3() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(2)
    End Function

    ''' <summary>
    ''' Excludes all columns of 4th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT4() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(3)
    End Function

    ''' <summary>
    ''' Excludes all columns of 5th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT5() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(4)
    End Function

    ''' <summary>
    ''' Excludes all columns of 6th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT6() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(5)
    End Function

    ''' <summary>
    ''' Excludes all columns of 7th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT7() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(6)
    End Function

    ''' <summary>
    ''' Excludes all columns of 8th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT8() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(7)
    End Function

    ''' <summary>
    ''' Excludes all columns of 9th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT9() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(8)
    End Function

    ''' <summary>
    ''' Excludes all columns of 10th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT10() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(9)
    End Function

    ''' <summary>
    ''' Excludes all columns of 11th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT11() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(10)
    End Function

    ''' <summary>
    ''' Excludes all columns of 12th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT12() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(11)
    End Function

    ''' <summary>
    ''' Excludes all columns of 13th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT13() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(12)
    End Function

    ''' <summary>
    ''' Excludes all columns of 14th table (entity) from SELECT clause.
    ''' </summary>
    ''' <returns></returns>
    Public Function ExcludeT14() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalExclude(13)
    End Function

    ''' <summary>
    ''' Excludes &lt;column&gt; from SELECT clause.
    ''' </summary>
    ''' <param name="propertyExpression"></param>
    ''' <returns></returns>
    Private Function InternalExclude(propertyExpression As Expression) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Me.Builder.ExcludeSelected(propertyExpression)
      Return Me
    End Function

    ''' <summary>
    ''' Excludes all columns of the table (entity) from SELECT clause.
    ''' </summary>
    ''' <param name="entityIndex"></param>
    ''' <returns></returns>
    Private Function InternalExclude(entityIndex As Int32) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Me.Builder.ExcludeSelected(entityIndex)
      Return Me
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T1)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T2)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T3)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T4)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T5)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T6)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T7)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T8)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T9)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T10)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T11)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T12)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T13)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of T14)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(action, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T1, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {0}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T2, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {1}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T3, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {2}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T4, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {3}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T5, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {4}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T6, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {5}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T7, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {6}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T8, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {7}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T9, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {8}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T10, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {9}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T11, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {10}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T12, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {11}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T13, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {12}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T1, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {0}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T2, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {1}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T3, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {2}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T4, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {3}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T5, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {4}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T6, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {5}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T7, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {6}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T8, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {7}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T9, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {8}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T10, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {9}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T11, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {10}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T12, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {11}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T13, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {12}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T14, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T14, TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, {13}, {13}, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Public Function Include(<DisallowNull> action As Expression(Of Action(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14))), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function Include(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TProperty)), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
      Return InternalInclude(keySelector, valueSelector, Nothing, Nothing, behavior)
    End Function

    ''' <summary>
    ''' Includes &lt;column(s)&gt; to SELECT clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="entityIndexHints"></param>
    ''' <param name="behavior"></param>
    ''' <returns></returns>
    Private Function InternalInclude(action As Expression, entityIndexHints As Int32(), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Private Function InternalInclude(keySelector As Expression, valueSelector As Expression, keySelectorEntityIndexHints As Int32(), valueSelectorEntityIndexHints As Int32(), Optional behavior As NonModelEntityCreationBehavior = NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull) As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)
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
    Public Function [If](Of TResult)(condition As Boolean, <DisallowNull> [then] As Func(Of SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TResult), Optional otherwise As Func(Of SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TResult) = Nothing) As TResult
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

      If genericType Is GetType(SelectedSelectSqlExpression(Of ,,,,,,,,,,,,,)) Then Return True
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
