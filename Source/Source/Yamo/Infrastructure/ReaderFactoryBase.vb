Namespace Infrastructure

  ''' <summary>
  ''' Base class for reader factories.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class ReaderFactoryBase

    ''' <summary>
    ''' Gets reader method for specified type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Protected Shared Function GetReadMethodForType(type As Type) As (Method As String, Convert As Boolean)
      Select Case type
        Case GetType(String)
          Return ("GetString", False)
        Case GetType(Int16), GetType(Int16?)
          Return ("GetInt16", False)
        Case GetType(Int32), GetType(Int32?)
          Return ("GetInt32", False)
        Case GetType(Int64), GetType(Int64?)
          Return ("GetInt64", False)
        Case GetType(Boolean), GetType(Boolean?)
          Return ("GetBoolean", False)
        Case GetType(Guid), GetType(Guid?)
          Return ("GetGuid", False)
        Case GetType(DateTime), GetType(DateTime?)
          Return ("GetDateTime", False)
        Case GetType(Decimal), GetType(Decimal?)
          Return ("GetDecimal", False)
        Case GetType(Double), GetType(Double?)
          Return ("GetDouble", False)
        Case GetType(Single), GetType(Single?)
          Return ("GetFloat", False)
        Case GetType(Byte())
          Return ("GetValue", True)
        Case GetType(Byte), GetType(Byte?)
          Return ("GetByte", False)
        Case Else
          Throw New NotSupportedException($"Reading value of type '{type}' is not supported.")
      End Select
    End Function

  End Class
End Namespace