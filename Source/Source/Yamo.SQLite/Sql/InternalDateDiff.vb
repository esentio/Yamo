Imports Yamo.Sql

Namespace Sql

  Public Class InternalDateDiff
    Implements IInternalSqlHelper

    Public ReadOnly Property SqlHelperType As Type Implements IInternalSqlHelper.SqlHelperType
      Get
        Return GetType(DateDiff)
      End Get
    End Property

    Public Function GetSqlFormat(methodName As String) As String Implements IInternalSqlHelper.GetSqlFormat
      Select Case methodName
        Case NameOf(DateDiff.SameYear)
          Return "(strftime('%Y', {0}) = strftime('%Y', {1}))"
        Case NameOf(DateDiff.SameQuarter)
          ' quarter: (cast(strftime('%m', {0}) as integer) + 2) / 3
          Return "(strftime('%Y', {0}) = strftime('%Y', {1}) AND ((cast(strftime('%m', {0}) as integer) + 2) / 3) = ((cast(strftime('%m', {1}) as integer) + 2) / 3))"
        Case NameOf(DateDiff.SameMonth)
          Return "(strftime('%Y-%m', {0}) = strftime('%Y-%m', {1}))"
        Case NameOf(DateDiff.SameDay)
          Return "(strftime('%Y-%m-%d', {0}) = strftime('%Y-%m-%d', {1}))"
        Case NameOf(DateDiff.SameHour)
          Return "(strftime('%Y-%m-%d %H', {0}) = strftime('%Y-%m-%d %H', {1}))"
        Case NameOf(DateDiff.SameMinute)
          Return "(strftime('%Y-%m-%d %H:%M', {0}) = strftime('%Y-%m-%d %H:%M', {1}))"
        Case NameOf(DateDiff.SameSecond)
          Return "(strftime('%Y-%m-%d %H:%M:%S', {0}) = strftime('%Y-%m-%d %H:%M:%S', {1}))"
        Case NameOf(DateDiff.SameMillisecond)
          Return "(strftime('%Y-%m-%d %H:%M:%f', {0}) = strftime('%Y-%m-%d %H:%M:%f', {1}))"
        Case Else
          Throw New NotSupportedException($"Method '{methodName}' is not supported.")
      End Select
    End Function
  End Class
End Namespace
