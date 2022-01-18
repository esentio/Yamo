Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure
Imports Yamo.Sql

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
    Public Overloads Shared Function GetSqlFormat(<DisallowNull> method As MethodCallExpression, <DisallowNull> dialectProvider As SqlDialectProvider) As SqlFormat
      Select Case method.Method.Name
        Case NameOf(DateTime.GetCurrentDateTime)
          Return New SqlFormat("datetime('now', 'localtime')", method.Arguments)
        Case NameOf(DateTime.GetCurrentDate)
          Return New SqlFormat("date()", method.Arguments)
        Case NameOf(DateTime.GetDate)
          Return New SqlFormat("date({0})", method.Arguments)
        Case NameOf(DateTime.SameYear)
          Return New SqlFormat("(strftime('%Y', {0}) = strftime('%Y', {1}))", method.Arguments)
        Case NameOf(DateTime.SameQuarter)
          ' quarter: (cast(strftime('%m', {0}) as integer) + 2) / 3
          Return New SqlFormat("(strftime('%Y', {0}) = strftime('%Y', {1}) AND ((cast(strftime('%m', {0}) as integer) + 2) / 3) = ((cast(strftime('%m', {1}) as integer) + 2) / 3))", method.Arguments)
        Case NameOf(DateTime.SameMonth)
          Return New SqlFormat("(strftime('%Y-%m', {0}) = strftime('%Y-%m', {1}))", method.Arguments)
        Case NameOf(DateTime.SameDay)
          Return New SqlFormat("(strftime('%Y-%m-%d', {0}) = strftime('%Y-%m-%d', {1}))", method.Arguments)
        Case NameOf(DateTime.SameHour)
          Return New SqlFormat("(strftime('%Y-%m-%d %H', {0}) = strftime('%Y-%m-%d %H', {1}))", method.Arguments)
        Case NameOf(DateTime.SameMinute)
          Return New SqlFormat("(strftime('%Y-%m-%d %H:%M', {0}) = strftime('%Y-%m-%d %H:%M', {1}))", method.Arguments)
        Case NameOf(DateTime.SameSecond)
          Return New SqlFormat("(strftime('%Y-%m-%d %H:%M:%S', {0}) = strftime('%Y-%m-%d %H:%M:%S', {1}))", method.Arguments)
        Case NameOf(DateTime.SameMillisecond)
          Return New SqlFormat("(strftime('%Y-%m-%d %H:%M:%f', {0}) = strftime('%Y-%m-%d %H:%M:%f', {1}))", method.Arguments)
        Case Else
          Throw New NotSupportedException($"Method '{method.Method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
