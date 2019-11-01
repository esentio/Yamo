Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Date related SQL helper methods.
  ''' </summary>
  Public Class DateDiff
    Inherits SqlHelper

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameYear(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year and quarter.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameQuarter(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year and month.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameMonth(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month and day.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameDay(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day and hour.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameHour(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day, hour and minute.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameMinute(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day, hour, minute and second.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameSecond(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day, hour, minute, second and millisecond.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    Public Shared Function SameMillisecond(date1 As DateTime, date2 As DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(method As MethodInfo, dialectProvider As SqlDialectProvider) As String
      Return dialectProvider.GetInternalSqlHelper(GetType(DateDiff)).GetSqlFormat(method)
    End Function

  End Class
End Namespace
