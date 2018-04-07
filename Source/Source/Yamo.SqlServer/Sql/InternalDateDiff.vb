Imports System.Reflection
Imports Yamo.Sql

Namespace Sql

  Public Class InternalDateDiff
    Implements IInternalSqlHelper

    Public ReadOnly Property SqlHelperType As Type Implements IInternalSqlHelper.SqlHelperType
      Get
        Return GetType(DateDiff)
      End Get
    End Property

    Public Function GetSqlFormat(method As MethodInfo) As String Implements IInternalSqlHelper.GetSqlFormat
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
