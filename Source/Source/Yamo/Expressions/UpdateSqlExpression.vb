Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL UPDATE statement.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class UpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="UpdateSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="tableNameOverride"></param>
    Friend Sub New(context As DbContext, Optional tableNameOverride As String = Nothing)
      MyBase.New(context, New UpdateSqlExpressionBuilder(context, tableNameOverride), New QueryExecutor(context))
      Me.Builder.SetMainTable(Of T)()
    End Sub

    ''' <summary>
    ''' Adds table hint(s).
    ''' </summary>
    ''' <param name="tableHints"></param>
    ''' <returns></returns>
    Public Function WithHints(tableHints As String) As WithHintsUpdateSqlExpression(Of T)
      Me.Builder.SetTableHints(tableHints)
      Return New WithHintsUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <returns></returns>
    Public Function [Set](action As Expression(Of Action(Of T))) As SetUpdateSqlExpression(Of T)
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
    Public Function [Set](Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty)), value As TProperty) As SetUpdateSqlExpression(Of T)
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
    Public Function [Set](Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty)), valueSelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, valueSelector)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [Set](predicate As Expression(Of Func(Of T, FormattableString))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function [Set](predicate As String, ParamArray parameters() As Object) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate, parameters)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET clause where value is set to null.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function SetNull(Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
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
    Public Function Execute(obj As T, Optional setAutoFields As Boolean = True, Optional forceUpdateAllFields As Boolean = False) As Int32
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
