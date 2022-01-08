Namespace Helpers

  Public Class Calendar

    Private Sub New()
    End Sub

    Public Shared Function Now() As DateTime
      Return TruncateMilliseconds(DateTime.Now)
    End Function

    Public Shared Function OffsetNow() As DateTimeOffset
      Return DateTimeOffset.Now
    End Function

    Public Shared Function TruncateMilliseconds(value As DateTime) As DateTime
      Return New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, 0, value.Kind)
    End Function

    Public Shared Function GetSqlServerMinDate() As DateTime
      Return DateTime.MinValue.Date
    End Function

    Public Shared Function GetSqlServerMaxDate() As DateTime
      Return DateTime.MaxValue.Date
    End Function

    Public Shared Function GetSqlServerMinTime() As TimeSpan
      Return TimeSpan.Zero
    End Function

    Public Shared Function GetSqlServerMaxTime() As TimeSpan
      Return TimeSpan.FromHours(24) - TimeSpan.FromMilliseconds(1)
    End Function

    Public Shared Function GetSqlServerMinDateTime() As DateTime
      Return New DateTime(1753, 1, 1, 0, 0, 0)
    End Function

    Public Shared Function GetSqlServerMaxDateTime() As DateTime
      Return New DateTime(9999, 12, 31, 23, 59, 59, 997)
    End Function

    Public Shared Function GetSqlServerMinDateTime2() As DateTime
      Return DateTime.MinValue
    End Function

    Public Shared Function GetSqlServerMaxDateTime2() As DateTime
      Return DateTime.MaxValue
    End Function

    Public Shared Function GetSqlServerMinDateTimeOffset() As DateTimeOffset
      Return DateTimeOffset.MinValue
    End Function

    Public Shared Function GetSqlServerMaxDateTimeOffset() As DateTimeOffset
      Return DateTimeOffset.MaxValue
    End Function

  End Class
End Namespace
