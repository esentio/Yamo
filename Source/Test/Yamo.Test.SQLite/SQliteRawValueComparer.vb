Public Class SQliteRawValueComparer
  Inherits RawValueComparer

  Public Overrides Sub AreRawValuesEqual(expected As Object, actual As Object)
    If actual IsNot DBNull.Value Then
      If TypeOf expected Is Guid Then
        actual = FromRawValue(Of Guid)(actual)
      ElseIf TypeOf expected Is Guid? Then
        actual = FromRawValue(Of Guid?)(actual)

      ElseIf TypeOf expected Is Boolean Then
        actual = FromRawValue(Of Boolean)(actual)
      ElseIf TypeOf expected Is Boolean? Then
        actual = FromRawValue(Of Boolean?)(actual)

      ElseIf TypeOf expected Is Int16 Then
        actual = FromRawValue(Of Int16)(actual)
      ElseIf TypeOf expected Is Int16? Then
        actual = FromRawValue(Of Int16?)(actual)

      ElseIf TypeOf expected Is Int32 Then
        actual = FromRawValue(Of Int32)(actual)
      ElseIf TypeOf expected Is Int32? Then
        actual = FromRawValue(Of Int32?)(actual)

      ElseIf TypeOf expected Is Single Then
        actual = FromRawValue(Of Single)(actual)
      ElseIf TypeOf expected Is Single? Then
        actual = FromRawValue(Of Single?)(actual)

      ElseIf TypeOf expected Is Decimal Then
        actual = FromRawValue(Of Decimal)(actual)
      ElseIf TypeOf expected Is Decimal? Then
        actual = FromRawValue(Of Decimal?)(actual)

      ElseIf TypeOf expected Is DateTime Then
        actual = FromRawValue(Of DateTime)(actual)
      ElseIf TypeOf expected Is DateTime? Then
        actual = FromRawValue(Of DateTime?)(actual)

      ElseIf TypeOf expected Is TimeSpan Then
        actual = FromRawValue(Of TimeSpan)(actual)
      ElseIf TypeOf expected Is TimeSpan? Then
        actual = FromRawValue(Of TimeSpan?)(actual)

      ElseIf TypeOf expected Is DateTimeOffset Then
        actual = FromRawValue(Of DateTimeOffset)(actual)
      ElseIf TypeOf expected Is DateTimeOffset? Then
        actual = FromRawValue(Of DateTimeOffset?)(actual)

      ElseIf TypeOf expected Is DateOnly Then
        actual = FromRawValue(Of DateOnly)(actual)
      ElseIf TypeOf expected Is DateOnly? Then
        actual = FromRawValue(Of DateOnly?)(actual)

      ElseIf TypeOf expected Is TimeOnly Then
        actual = FromRawValue(Of TimeOnly)(actual)
      ElseIf TypeOf expected Is TimeOnly? Then
        actual = FromRawValue(Of TimeOnly?)(actual)
      End If
    End If

    MyBase.AreRawValuesEqual(expected, actual)
  End Sub

  Private Function FromRawValue(Of T)(value As Object) As T
    If value Is DBNull.Value Then
      Return Nothing
    End If

    If GetType(T) Is GetType(Guid) Then
      If TypeOf value Is String Then
        value = New Guid(DirectCast(value, String))
      ElseIf TypeOf value Is Byte() Then
        value = New Guid(DirectCast(value, Byte()))
      End If
    ElseIf GetType(T) Is GetType(Guid?) Then
      If TypeOf value Is String Then
        value = New Guid?(New Guid(DirectCast(value, String)))
      ElseIf TypeOf value Is Byte() Then
        value = New Guid?(New Guid(DirectCast(value, Byte())))
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

    ElseIf GetType(T) Is GetType(TimeSpan) Then
      If TypeOf value Is String Then
        value = TimeSpan.Parse(DirectCast(value, String))
      End If
    ElseIf GetType(T) Is GetType(TimeSpan?) Then
      If TypeOf value Is String Then
        value = New TimeSpan?(TimeSpan.Parse(DirectCast(value, String)))
      End If

    ElseIf GetType(T) Is GetType(DateTimeOffset) Then
      If TypeOf value Is String Then
        value = DateTimeOffset.Parse(DirectCast(value, String))
      End If
    ElseIf GetType(T) Is GetType(DateTimeOffset?) Then
      If TypeOf value Is String Then
        value = New DateTimeOffset?(DateTimeOffset.Parse(DirectCast(value, String)))
      End If

    ElseIf GetType(T) Is GetType(DateOnly) Then
      If TypeOf value Is String Then
        value = DateOnly.Parse(DirectCast(value, String))
      End If
    ElseIf GetType(T) Is GetType(DateOnly?) Then
      If TypeOf value Is String Then
        value = New DateOnly?(DateOnly.Parse(DirectCast(value, String)))
      End If

    ElseIf GetType(T) Is GetType(TimeOnly) Then
      If TypeOf value Is String Then
        value = TimeOnly.Parse(DirectCast(value, String))
      End If
    ElseIf GetType(T) Is GetType(TimeOnly?) Then
      If TypeOf value Is String Then
        value = New TimeOnly?(TimeOnly.Parse(DirectCast(value, String)))
      End If
    End If

    Return DirectCast(value, T)
  End Function

End Class
