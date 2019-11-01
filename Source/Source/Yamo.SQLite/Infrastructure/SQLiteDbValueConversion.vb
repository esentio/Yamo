Imports Yamo.Infrastructure

Namespace Infrastructure

  ' TODO: SIP - add documentation to this class.
  Public Class SQLiteDbValueConversion
    Inherits DbValueConversion

    Public Overrides Function FromDbValue(Of T)(value As Object) As T
      If value Is DBNull.Value Then
        Return Nothing
      End If

      If GetType(T) Is GetType(Guid) Then
        If TypeOf value Is Byte() Then
          value = New Guid(DirectCast(value, Byte()))
        End If
      ElseIf GetType(T) Is GetType(Guid?) Then
        If TypeOf value Is Byte() Then
          value = New Guid?(New Guid((DirectCast(value, Byte()))))
        End If

      ElseIf GetType(T) Is GetType(Boolean) Then
        ' value is Int64
        Return CType(value, T)
      ElseIf GetType(T) Is GetType(Boolean?) Then
        ' value is Int64
        value = New Boolean?(CType(value, Boolean))

      ElseIf GetType(T) Is GetType(Int16) Then
        ' value is Int64
        Return CType(value, T)
      ElseIf GetType(T) Is GetType(Int16?) Then
        ' value is Int64
        value = New Int16?(CType(value, Int16))

      ElseIf GetType(T) Is GetType(Int32) Then
        ' value is Int64
        Return CType(value, T)
      ElseIf GetType(T) Is GetType(Int32?) Then
        ' value is Int64
        value = New Int32?(CType(value, Int32))

      ElseIf GetType(T) Is GetType(Single) Then
        ' value is Double
        Return CType(value, T)
      ElseIf GetType(T) Is GetType(Single?) Then
        ' value is Double
        value = New Single?(CType(value, Single))

      ElseIf GetType(T) Is GetType(Decimal) Then
        ' value is Int64 or Double
        Return CType(value, T)
      ElseIf GetType(T) Is GetType(Decimal?) Then
        ' value is Int64 or Double
        value = New Decimal?(CType(value, Decimal))

      ElseIf GetType(T) Is GetType(DateTime) Then
        ' value is String
        Return CType(value, T)
      ElseIf GetType(T) Is GetType(DateTime?) Then
        ' value is String
        value = New DateTime?(CType(value, DateTime))
      End If

      Return DirectCast(value, T)
    End Function

  End Class
End Namespace