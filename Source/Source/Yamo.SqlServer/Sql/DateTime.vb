Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure
Imports Yamo.Sql

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
    Public Overloads Shared Function GetSqlFormat(method As MethodCallExpression, dialectProvider As SqlDialectProvider) As SqlFormat
      Select Case method.Method.Name
        Case NameOf(DateTime.SameYear)
          Return New SqlFormat("(DATEDIFF(year, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameQuarter)
          Return New SqlFormat("(DATEDIFF(quarter, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameMonth)
          Return New SqlFormat("(DATEDIFF(month, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameDay)
          Return New SqlFormat("(DATEDIFF(day, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameHour)
          Return New SqlFormat("(DATEDIFF(hour, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameMinute)
          Return New SqlFormat("(DATEDIFF(minute, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameSecond)
          Return New SqlFormat("(DATEDIFF(second, {0}, {1}) = 0)", method.Arguments)
        Case NameOf(DateTime.SameMillisecond)
          Return New SqlFormat("(DATEDIFF(millisecond, {0}, {1}) = 0)", method.Arguments)
        Case Else
          Throw New NotSupportedException($"Method '{method.Method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
