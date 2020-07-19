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
    ''' Stores whether auto fields should be set.
    ''' </summary>
    Private m_SetAutoFields As Boolean

    ''' <summary>
    ''' Creates new instance of <see cref="UpdateSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="setAutoFields"></param>
    Friend Sub New(context As DbContext, setAutoFields As Boolean)
      MyBase.New(context, New UpdateSqlExpressionBuilder(context, setAutoFields), New QueryExecutor(context))
      Me.Builder.SetMainTable(Of T)()
      m_SetAutoFields = setAutoFields
    End Sub

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
    ''' <returns></returns>
    Friend Function Update(obj As T) As Int32
      If SkipUpdate(obj) Then
        Return 0
      End If

      If m_SetAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnUpdateSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj)
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
