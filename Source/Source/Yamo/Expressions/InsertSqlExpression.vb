Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class InsertSqlExpression(Of T)
    Inherits SqlExpressionBase

    Protected ReadOnly DbContext As DbContext

    Protected Property Builder As InsertSqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Private m_SetAutoFields As Boolean

    Friend Sub New(context As DbContext, useDbIdentityAndDefaults As Boolean, setAutoFields As Boolean)
      Me.DbContext = context
      Me.Builder = New InsertSqlExpressionBuilder(context, useDbIdentityAndDefaults)
      Me.Executor = New QueryExecutor(context)
      m_SetAutoFields = setAutoFields
    End Sub

    Public Function Insert(obj As T) As Int32
      If m_SetAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnInsertSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj)
      Dim affectedRows = Me.Executor.ExecuteInsert(query)
      ResetPropertyModifiedTracking(obj)
      Return affectedRows
    End Function

  End Class
End Namespace