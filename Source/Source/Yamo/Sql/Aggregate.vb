Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Aggregate SQL helper methods.
  ''' </summary>
  Public Class Aggregate
    Inherits SqlHelper

    ' TODO: SIP - probably rewrite to IInternalSqlHelper in the future:
    ' - STDEV in SQLite only works with extension-functions.c
    ' - add platform specific functions (we need better API first!)

    ''' <summary>
    ''' Translates to COUNT(*) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Count() As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to COUNT(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function Count(Of T)(value As T) As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to COUNT(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function CountDistinct(Of T)(value As T) As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SUM(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function Sum(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SUM(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function SumDistinct(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to AVG(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function Avg(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to AVG(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function AvgDistinct(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to STDEV(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function Stdev(Of T)(value As T) As Double
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to STDEV(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function StdevDistinct(Of T)(value As T) As Double
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to MIN(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function Min(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to MAX(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function Max(Of T)(value As T) As T
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
      Select Case method.Name
        Case NameOf(Aggregate.Count)
          If method.IsGenericMethod Then
            Return "COUNT({0})"
          Else
            Return "COUNT(*)"
          End If

        Case NameOf(Aggregate.CountDistinct)
          Return "COUNT(DISTINCT {0})"

        Case NameOf(Aggregate.Sum)
          Return "SUM({0})"

        Case NameOf(Aggregate.SumDistinct)
          Return "SUM(DISTINCT {0})"

        Case NameOf(Aggregate.Avg)
          Return "AVG({0})"

        Case NameOf(Aggregate.AvgDistinct)
          Return "AVG(DISTINCT {0})"

        Case NameOf(Aggregate.Stdev)
          Return "STDEV({0})"

        Case NameOf(Aggregate.StdevDistinct)
          Return "STDEV(DISTINCT {0})"

        Case NameOf(Aggregate.Min)
          Return "MIN({0})"

        Case NameOf(Aggregate.Max)
          Return "MAX({0})"

        Case Else
          Throw New NotSupportedException($"Method '{method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
