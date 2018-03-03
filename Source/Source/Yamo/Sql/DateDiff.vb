Imports Yamo.Infrastructure

Namespace Sql

  Public Class DateDiff
    Inherits SqlHelper

    Public Shared Function SameYear(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameQuarter(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameMonth(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameDay(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameHour(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameMinute(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameSecond(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SameMillisecond(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Overloads Shared Function GetSqlFormat(methodName As String, dialectProvider As SqlDialectProvider) As String
      Return dialectProvider.GetInternalSqlHelper(GetType(DateDiff)).GetSqlFormat(methodName)
    End Function

  End Class
End Namespace
