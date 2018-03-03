Namespace Expressions

  Public MustInherit Class SqlExpressionBase

    ' TODO: SIP - move here from children?
    'Protected Property Executor As QueryExecutor

    Protected Sub ResetPropertyModifiedTracking(obj As Object)
      If TypeOf obj Is IHasPropertyModifiedTracking Then
        DirectCast(obj, IHasPropertyModifiedTracking).ResetPropertyModifiedTracking()
      End If
    End Sub

    Protected Function GetEntityType(Of T)(obj As T) As Type
      If GetType(T) Is GetType(Object) Then
        Return obj.GetType()
      Else
        Return GetType(T)
      End If
    End Function

  End Class
End Namespace
