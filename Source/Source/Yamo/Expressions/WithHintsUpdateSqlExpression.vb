Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL UPDATE statement with defined table hints.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class WithHintsUpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="WithHintsUpdateSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(context As DbContext, builder As UpdateSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <returns></returns>
    Public Function [Set](<DisallowNull> action As Expression(Of Action(Of T))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(action)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function [Set](Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T, TProperty)), value As TProperty) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, value)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <returns></returns>
    Public Function [Set](Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T, TProperty)), <DisallowNull> valueSelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, valueSelector)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [Set](<DisallowNull> predicate As Expression(Of Func(Of T, FormattableString))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function [Set](<DisallowNull> predicate As String, <DisallowNull> ParamArray parameters() As Object) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate, parameters)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause where value is set to null.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function SetNull(Of TProperty)(<DisallowNull> keySelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, DirectCast(Nothing, Object))
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Executes UPDATE statement and returns the number of affected rows.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="setAutoFields"></param>
    ''' <param name="forceUpdateAllFields"></param>
    ''' <returns></returns>
    Public Function Execute(<DisallowNull> obj As T, Optional setAutoFields As Boolean = True, Optional forceUpdateAllFields As Boolean = False) As Int32
      If Not forceUpdateAllFields And SkipUpdate(obj) Then
        Return 0
      End If

      If setAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnUpdateSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj, forceUpdateAllFields)
      Dim affectedRows = Me.Executor.Execute(query)
      ResetDbPropertyModifiedTracking(obj)
      Return affectedRows
    End Function

    ''' <summary>
    ''' Gets whether update could be skipped, because there has been no change in the entity.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Private Function SkipUpdate(obj As Object) As Boolean
      If TypeOf obj Is IHasDbPropertyModifiedTracking Then
        Return Not DirectCast(obj, IHasDbPropertyModifiedTracking).IsAnyDbPropertyModified()
      End If

      Return False
    End Function

  End Class
End Namespace
