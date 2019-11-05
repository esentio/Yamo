Namespace Infrastructure

  ''' <summary>
  ''' Database value conversion.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class DbValueConversion

    ''' <summary>
    ''' Converts value from database value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Overridable Function FromDbValue(Of T)(value As Object) As T
      If value Is DBNull.Value OrElse value Is Nothing Then
        Return Nothing
      Else
        Return DirectCast(value, T)
      End If
    End Function

  End Class
End Namespace