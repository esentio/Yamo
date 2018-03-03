Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class InsertSqlExpression(Of T)
    Inherits SqlExpressionBase

    Protected ReadOnly DbContext As DbContext

    Protected Property Builder As InsertSqlExpressionBuilder

    Protected Property Executor As QueryExecutor

    Friend Sub New(context As DbContext)
      Me.DbContext = context
      Me.Builder = New InsertSqlExpressionBuilder(context)
      Me.Executor = New QueryExecutor(context)
    End Sub

    Public Function Insert(obj As T, Optional useDbIdentityAndDefaults As Boolean = True, Optional setAutoFields As Boolean = True) As Int32
      If setAutoFields Then
        Dim setter = EntityAutoFieldsSetterCache.GetOnInsertSetter(Me.DbContext.Model, GetEntityType(obj))
        setter(obj, Me.DbContext)
      End If

      Dim query = Me.Builder.CreateQuery(obj, useDbIdentityAndDefaults)
      Dim affectedRows = Me.Executor.ExecuteInsert(query)
      ResetPropertyModifiedTracking(obj)
      Return affectedRows
    End Function

  End Class
End Namespace