Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection

Namespace Infrastructure

  ''' <summary>
  ''' Value type reader factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTypeReaderFactory
    Inherits ReaderFactoryBase

    ''' <summary>
    ''' Creates reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Overridable Function CreateReader(type As Type) As Object
      ' code generation was replaced by static methods.

      Select Case type
        Case GetType(String)
          Return DirectCast(AddressOf ReadString, Func(Of IDataRecord, Int32, String))
        Case GetType(Int16)
          Return DirectCast(AddressOf ReadInt16, Func(Of IDataRecord, Int32, Int16))
        Case GetType(Int16?)
          Return DirectCast(AddressOf ReadNullableInt16, Func(Of IDataRecord, Int32, Int16?))
        Case GetType(Int32)
          Return DirectCast(AddressOf ReadInt32, Func(Of IDataRecord, Int32, Int32))
        Case GetType(Int32?)
          Return DirectCast(AddressOf ReadNullableInt32, Func(Of IDataRecord, Int32, Int32?))
        Case GetType(Int64)
          Return DirectCast(AddressOf ReadInt64, Func(Of IDataRecord, Int32, Int64))
        Case GetType(Int64?)
          Return DirectCast(AddressOf ReadNullableInt64, Func(Of IDataRecord, Int32, Int64?))
        Case GetType(Boolean)
          Return DirectCast(AddressOf ReadBoolean, Func(Of IDataRecord, Int32, Boolean))
        Case GetType(Boolean?)
          Return DirectCast(AddressOf ReadNullableBoolean, Func(Of IDataRecord, Int32, Boolean?))
        Case GetType(Guid)
          Return DirectCast(AddressOf ReadGuid, Func(Of IDataRecord, Int32, Guid))
        Case GetType(Guid?)
          Return DirectCast(AddressOf ReadNullableGuid, Func(Of IDataRecord, Int32, Guid?))
        Case GetType(DateTime)
          Return DirectCast(AddressOf ReadDateTime, Func(Of IDataRecord, Int32, DateTime))
        Case GetType(DateTime?)
          Return DirectCast(AddressOf ReadNullableDateTime, Func(Of IDataRecord, Int32, DateTime?))
        Case GetType(Decimal)
          Return DirectCast(AddressOf ReadDecimal, Func(Of IDataRecord, Int32, Decimal))
        Case GetType(Decimal?)
          Return DirectCast(AddressOf ReadNullableDecimal, Func(Of IDataRecord, Int32, Decimal?))
        Case GetType(Double)
          Return DirectCast(AddressOf ReadDouble, Func(Of IDataRecord, Int32, Double))
        Case GetType(Double?)
          Return DirectCast(AddressOf ReadNullableDouble, Func(Of IDataRecord, Int32, Double?))
        Case GetType(Single)
          Return DirectCast(AddressOf ReadSingle, Func(Of IDataRecord, Int32, Single))
        Case GetType(Single?)
          Return DirectCast(AddressOf ReadNullableSingle, Func(Of IDataRecord, Int32, Single?))
        Case GetType(Byte())
          Return DirectCast(AddressOf ReadByteArray, Func(Of IDataRecord, Int32, Byte()))
        Case GetType(Byte)
          Return DirectCast(AddressOf ReadByte, Func(Of IDataRecord, Int32, Byte))
        Case GetType(Byte?)
          Return DirectCast(AddressOf ReadNullableByte, Func(Of IDataRecord, Int32, Byte?))
        Case Else
          Throw New NotSupportedException($"Reading value of type '{type}' is not supported.")
      End Select
    End Function

    ' NOTE: we perform IsDBNull check on non-nullable types anyway and return default value. This behavior is
    ' probably more convenient in custom selects than throwing an exception, especially when called from FirstOrDefault.
    ' Also, QueryFirstOrDefault behaves the same way. If this behavior should change/be optional in the future (probably
    ' with introducing First/QueryFirst methods), make sure it is constistent across all use cases.

    ''' <summary>
    ''' Reads <see cref="String"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadString(reader As IDataRecord, index As Int32) As String
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetString(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Int16"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadInt16(reader As IDataRecord, index As Int32) As Int16
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetInt16(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Int16)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableInt16(reader As IDataRecord, index As Int32) As Int16?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetInt16(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Int32"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadInt32(reader As IDataRecord, index As Int32) As Int32
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetInt32(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Int32)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableInt32(reader As IDataRecord, index As Int32) As Int32?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetInt32(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Int64"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadInt64(reader As IDataRecord, index As Int32) As Int64
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetInt64(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Int64)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableInt64(reader As IDataRecord, index As Int32) As Int64?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetInt64(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Boolean"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadBoolean(reader As IDataRecord, index As Int32) As Boolean
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetBoolean(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Boolean)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableBoolean(reader As IDataRecord, index As Int32) As Boolean?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetBoolean(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Guid"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadGuid(reader As IDataRecord, index As Int32) As Guid
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetGuid(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Guid)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableGuid(reader As IDataRecord, index As Int32) As Guid?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetGuid(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="DateTime"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadDateTime(reader As IDataRecord, index As Int32) As DateTime
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDateTime(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of DateTime)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableDateTime(reader As IDataRecord, index As Int32) As DateTime?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDateTime(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Decimal"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadDecimal(reader As IDataRecord, index As Int32) As Decimal
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDecimal(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Decimal)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableDecimal(reader As IDataRecord, index As Int32) As Decimal?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDecimal(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Double"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadDouble(reader As IDataRecord, index As Int32) As Double
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDouble(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Double)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableDouble(reader As IDataRecord, index As Int32) As Double?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDouble(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Single"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadSingle(reader As IDataRecord, index As Int32) As Single
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetFloat(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Single)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableSingle(reader As IDataRecord, index As Int32) As Single?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetFloat(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Byte()"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadByteArray(reader As IDataRecord, index As Int32) As Byte()
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return DirectCast(reader.GetValue(index), Byte())
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Byte"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadByte(reader As IDataRecord, index As Int32) As Byte
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetByte(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Byte)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableByte(reader As IDataRecord, index As Int32) As Byte?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetByte(index)
      End If
    End Function

  End Class
End Namespace