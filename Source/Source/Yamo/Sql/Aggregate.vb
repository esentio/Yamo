Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  ''' <summary>
  ''' Aggregate SQL helper methods.
  ''' </summary>
  Public Class Aggregate
    Inherits SqlHelper

    ' - STDEV in SQLite only works with extension-functions.c
    ' - maybe add platform specific functions

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
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Count(Of T)(<DisallowNull> expression As T) As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to COUNT(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function CountDistinct(Of T)(<DisallowNull> expression As T) As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SUM(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Sum(Of T)(<DisallowNull> expression As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to SUM(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function SumDistinct(Of T)(<DisallowNull> expression As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to AVG(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Avg(Of T)(<DisallowNull> expression As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to AVG(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function AvgDistinct(Of T)(<DisallowNull> expression As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to STDEV(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Stdev(Of T)(<DisallowNull> expression As T) As Double
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to STDEV(DISTINCT &lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function StdevDistinct(Of T)(<DisallowNull> expression As T) As Double
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to MIN(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Min(Of T)(<DisallowNull> expression As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Translates to MAX(&lt;expression&gt;) function call.<br/>
    ''' This method is not intended to be called directly. Use it only as a part of the query expression.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Shared Function Max(Of T)(<DisallowNull> expression As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    ''' <summary>
    ''' Returns SQL format string that is appended to final SQL statement.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="method"></param>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
    Public Overloads Shared Function GetSqlFormat(<DisallowNull> method As MethodCallExpression, <DisallowNull> dialectProvider As SqlDialectProvider) As SqlFormat
      Select Case method.Method.Name
        Case NameOf(Aggregate.Count)
          If method.Method.IsGenericMethod Then
            Return New SqlFormat("COUNT({0})", method.Arguments)
          Else
            Return New SqlFormat("COUNT(*)", method.Arguments)
          End If

        Case NameOf(Aggregate.CountDistinct)
          Return New SqlFormat("COUNT(DISTINCT {0})", method.Arguments)

        Case NameOf(Aggregate.Sum)
          Return New SqlFormat("SUM({0})", method.Arguments)

        Case NameOf(Aggregate.SumDistinct)
          Return New SqlFormat("SUM(DISTINCT {0})", method.Arguments)

        Case NameOf(Aggregate.Avg)
          Return New SqlFormat("AVG({0})", method.Arguments)

        Case NameOf(Aggregate.AvgDistinct)
          Return New SqlFormat("AVG(DISTINCT {0})", method.Arguments)

        Case NameOf(Aggregate.Stdev)
          Return New SqlFormat("STDEV({0})", method.Arguments)

        Case NameOf(Aggregate.StdevDistinct)
          Return New SqlFormat("STDEV(DISTINCT {0})", method.Arguments)

        Case NameOf(Aggregate.Min)
          Return New SqlFormat("MIN({0})", method.Arguments)

        Case NameOf(Aggregate.Max)
          Return New SqlFormat("MAX({0})", method.Arguments)

        Case Else
          Throw New NotSupportedException($"Method '{method.Method.Name}' is not supported.")
      End Select
    End Function

  End Class
End Namespace
