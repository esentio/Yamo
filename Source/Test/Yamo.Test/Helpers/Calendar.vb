Namespace Helpers

  Public Class Calendar

    Private Sub New()
    End Sub

    Public Shared Function Now() As DateTime
      Return TruncateMilliseconds(DateTime.Now)
    End Function

    Public Shared Function TruncateMilliseconds(value As DateTime) As DateTime
      Return New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, 0, value.Kind)
    End Function

    Public Shared Function GetSqlServerMinDate() As DateTime
      Return New DateTime(1753, 1, 1, 0, 0, 0)
    End Function

    Public Shared Function GetSqlServerMaxDate() As DateTime
      Return New DateTime(9999, 12, 31, 0, 0, 0)
    End Function

  End Class
End Namespace
