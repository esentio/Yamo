Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Date &amp; time related SQL helper methods.
  ''' </summary>
  Public Class DateTime
    Inherits SqlHelper

    ''' <summary>
    ''' Translates to SQL expression that returns current database datetime.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetCurrentDateTime() As System.DateTime
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that returns current database date (without time).<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetCurrentDate() As System.DateTime
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that returns date part of a datetime value.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function GetDate(value As System.DateTime) As System.DateTime
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that returns date part of a datetime value.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function GetDate(value As System.DateTime?) As System.DateTime?
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameYear(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year and quarter.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameQuarter(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year and month.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameMonth(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month and day.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameDay(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day and hour.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameHour(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day, hour and minute.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameMinute(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day, hour, minute and second.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameSecond(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SQL expression that compares 2 datetime expressions and checks whether the values have the same year, month, day, hour, minute, second and millisecond.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    Public Shared Function SameMillisecond(value1 As System.DateTime, value2 As System.DateTime) As Boolean
      Throw New Exception("This method is not intended to be called directly.")
    End Function

  End Class
End Namespace
