﻿Imports Yamo.Expressions.Builders
Imports Yamo.Internal
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class UpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    Friend Sub New(context As DbContext)
      MyBase.New(context, New UpdateSqlExpressionBuilder(context), New QueryExecutor(context))
    End Sub

    Friend Function Update(obj As T, Optional setAutoFields As Boolean = True) As Int32
      If SkipUpdate(obj) Then
        Return 0
      End If

      If setAutoFields Then
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
