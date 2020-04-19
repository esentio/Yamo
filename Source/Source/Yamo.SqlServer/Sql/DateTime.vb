Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Date &amp; time related SQL helper methods.<br/>
  ''' Specific for MS SQL Server.
  ''' </summary>
  Public Class DateTime
    Inherits Yamo.Sql.DateTime

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodInfo, dialectProvider As SqlDialectProvider) As String
      Select Case method.Name
        Case NameOf(DateTime.SameYear)
          Return "(DATEDIFF(year, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameQuarter)
          Return "(DATEDIFF(quarter, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameMonth)
          Return "(DATEDIFF(month, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameDay)
          Return "(DATEDIFF(day, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameHour)
          Return "(DATEDIFF(hour, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameMinute)
          Return "(DATEDIFF(minute, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameSecond)
          Return "(DATEDIFF(second, {0}, {1}) = 0)"
        Case NameOf(DateTime.SameMillisecond)
          Return "(DATEDIFF(millisecond, {0}, {1}) = 0)"
        Case Else
          Throw New NotSupportedException($"Method '{method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
