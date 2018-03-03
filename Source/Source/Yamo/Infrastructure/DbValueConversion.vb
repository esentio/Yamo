Namespace Infrastructure

  Public Class DbValueConversion

    Public Overridable Function FromDbValue(Of T)(value As Object) As T
      If value Is DBNull.Value Then
        Return Nothing
      Else
        Return DirectCast(value, T)
      End If
    End Function

  End Class
End Namespace