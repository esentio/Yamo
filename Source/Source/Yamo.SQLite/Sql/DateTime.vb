Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Date &amp; time related SQL helper methods.<br/>
  ''' Specific for SQLite.
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
          Return "(strftime('%Y', {0}) = strftime('%Y', {1}))"
        Case NameOf(DateTime.SameQuarter)
          ' quarter: (cast(strftime('%m', {0}) as integer) + 2) / 3
          Return "(strftime('%Y', {0}) = strftime('%Y', {1}) AND ((cast(strftime('%m', {0}) as integer) + 2) / 3) = ((cast(strftime('%m', {1}) as integer) + 2) / 3))"
        Case NameOf(DateTime.SameMonth)
          Return "(strftime('%Y-%m', {0}) = strftime('%Y-%m', {1}))"
        Case NameOf(DateTime.SameDay)
          Return "(strftime('%Y-%m-%d', {0}) = strftime('%Y-%m-%d', {1}))"
        Case NameOf(DateTime.SameHour)
          Return "(strftime('%Y-%m-%d %H', {0}) = strftime('%Y-%m-%d %H', {1}))"
        Case NameOf(DateTime.SameMinute)
          Return "(strftime('%Y-%m-%d %H:%M', {0}) = strftime('%Y-%m-%d %H:%M', {1}))"
        Case NameOf(DateTime.SameSecond)
          Return "(strftime('%Y-%m-%d %H:%M:%S', {0}) = strftime('%Y-%m-%d %H:%M:%S', {1}))"
        Case NameOf(DateTime.SameMillisecond)
          Return "(strftime('%Y-%m-%d %H:%M:%f', {0}) = strftime('%Y-%m-%d %H:%M:%f', {1}))"
        Case Else
          Throw New NotSupportedException($"Method '{method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
