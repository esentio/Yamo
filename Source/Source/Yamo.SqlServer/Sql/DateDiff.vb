Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Date related SQL helper methods.<br/>
  ''' Specific for MS SQL Server.
  ''' </summary>
  Public Class DateDiff
    Inherits Yamo.Sql.DateDiff

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodInfo, dialectProvider As SqlDialectProvider) As String
      Select Case method.Name
        Case NameOf(DateDiff.SameYear)
          Return "(DATEDIFF(year, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameQuarter)
          Return "(DATEDIFF(quarter, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameMonth)
          Return "(DATEDIFF(month, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameDay)
          Return "(DATEDIFF(day, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameHour)
          Return "(DATEDIFF(hour, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameMinute)
          Return "(DATEDIFF(minute, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameSecond)
          Return "(DATEDIFF(second, {0}, {1}) = 0)"
        Case NameOf(DateDiff.SameMillisecond)
          Return "(DATEDIFF(millisecond, {0}, {1}) = 0)"
        Case Else
          Throw New NotSupportedException($"Method '{method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
