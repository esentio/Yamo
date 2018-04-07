Imports System.Reflection
Imports Yamo.Infrastructure

Namespace Sql

  Public Class Aggregate
    Inherits SqlHelper

    ' TODO: SIP - probably rewrite to IInternalSqlHelper in the future:
    ' - STDEV in SQLite only works with extension-functions.c
    ' - add platform specific functions (we need better API first!)

    Public Shared Function Count() As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function Count(Of T)(value As T) As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function CountDistinct(Of T)(value As T) As Int32
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function Sum(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function SumDistinct(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function Avg(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function AvgDistinct(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function Stdev(Of T)(value As T) As Double
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function StdevDistinct(Of T)(value As T) As Double
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function Min(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

    Public Shared Function Max(Of T)(value As T) As T
      Throw New Exception("This method is not intended to be called directly.")
    End Function

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
