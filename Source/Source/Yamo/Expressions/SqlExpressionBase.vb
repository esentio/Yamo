Namespace Expressions

  ''' <summary>
  ''' Base class for SQL expressions.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlExpressionBase

    ' TODO: SIP - move here from children?
    'Protected Property Executor As QueryExecutor

    ''' <summary>
    ''' Reset database property modified tracking on an entity that implements <see cref="IHasDbPropertyModifiedTracking"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="obj"></param>
    Protected Sub ResetDbPropertyModifiedTracking(obj As Object)
      If TypeOf obj Is IHasDbPropertyModifiedTracking Then
        DirectCast(obj, IHasDbPropertyModifiedTracking).ResetDbPropertyModifiedTracking()
      End If
    End Sub

    ''' <summary>
    ''' Gets entity type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Protected Function GetEntityType(Of T)(obj As T) As Type
      If GetType(T) Is GetType(Object) Then
        Return obj.GetType()
      Else
        Return GetType(T)
      End If
    End Function

  End Class
End Namespace
