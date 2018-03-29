Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class UpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    Private m_SetAutoFields As Boolean

    Friend Sub New(context As DbContext, setAutoFields As Boolean)
      MyBase.New(context, New UpdateSqlExpressionBuilder(context, setAutoFields), New QueryExecutor(context))
      Me.Builder.SetMainTable(Of T)()
      m_SetAutoFields = setAutoFields
    End Sub

    Public Function [Set](action As Expression(Of Action(Of T))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(action)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function [Set](predicate As Expression(Of Func(Of T, FormattableString))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Public Function [Set](predicate As String) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    Friend Function Update(obj As T) As Int32
      If SkipUpdate(obj) Then
        Return 0
      End If

      If m_SetAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnUpdateSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj)
      Dim affectedRows = Me.Executor.ExecuteNonQuery(query)
      ResetPropertyModifiedTracking(obj)
      Return affectedRows
    End Function

    Private Function SkipUpdate(obj As Object) As Boolean
      If TypeOf obj Is IHasPropertyModifiedTracking Then
        Return Not DirectCast(obj, IHasPropertyModifiedTracking).IsAnyPropertyModified()
      End If

      Return False
    End Function

  End Class
End Namespace
