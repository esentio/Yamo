Imports System.Data
Imports System.Data.Common
Imports System.Linq.Expressions
Imports System.Reflection

Namespace Infrastructure

  ''' <summary>
  ''' Value type reader factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTypeReaderFactory
    Inherits ReaderFactoryBase

    ' static methods are used instead of code generation if possible

    ''' <summary>
    ''' Creates reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Overridable Function CreateReader(dataReaderType As Type, type As Type) As Object
      Select Case type
        Case GetType(String)
          Return DirectCast(AddressOf ReadString, Func(Of DbDataReader, Int32, String))
        Case GetType(Int16)
          Return DirectCast(AddressOf ReadInt16, Func(Of DbDataReader, Int32, Int16))
        Case GetType(Int16?)
          Return DirectCast(AddressOf ReadNullableInt16, Func(Of DbDataReader, Int32, Int16?))
        Case GetType(Int32)
          Return DirectCast(AddressOf ReadInt32, Func(Of DbDataReader, Int32, Int32))
        Case GetType(Int32?)
          Return DirectCast(AddressOf ReadNullableInt32, Func(Of DbDataReader, Int32, Int32?))
        Case GetType(Int64)
          Return DirectCast(AddressOf ReadInt64, Func(Of DbDataReader, Int32, Int64))
        Case GetType(Int64?)
          Return DirectCast(AddressOf ReadNullableInt64, Func(Of DbDataReader, Int32, Int64?))
        Case GetType(Boolean)
          Return DirectCast(AddressOf ReadBoolean, Func(Of DbDataReader, Int32, Boolean))
        Case GetType(Boolean?)
          Return DirectCast(AddressOf ReadNullableBoolean, Func(Of DbDataReader, Int32, Boolean?))
        Case GetType(Guid)
          Return DirectCast(AddressOf ReadGuid, Func(Of DbDataReader, Int32, Guid))
        Case GetType(Guid?)
          Return DirectCast(AddressOf ReadNullableGuid, Func(Of DbDataReader, Int32, Guid?))
        Case GetType(DateTime)
          Return DirectCast(AddressOf ReadDateTime, Func(Of DbDataReader, Int32, DateTime))
        Case GetType(DateTime?)
          Return DirectCast(AddressOf ReadNullableDateTime, Func(Of DbDataReader, Int32, DateTime?))
        Case GetType(TimeSpan)
          Return CreateTimeSpanReader(dataReaderType)
        Case GetType(TimeSpan?)
          Return CreateNullableTimeSpanReader(dataReaderType)
        Case GetType(DateTimeOffset)
          Return CreateDateTimeOffsetReader(dataReaderType)
        Case GetType(DateTimeOffset?)
          Return CreateNullableDateTimeOffsetReader(dataReaderType)
        Case GetType(Decimal)
          Return DirectCast(AddressOf ReadDecimal, Func(Of DbDataReader, Int32, Decimal))
        Case GetType(Decimal?)
          Return DirectCast(AddressOf ReadNullableDecimal, Func(Of DbDataReader, Int32, Decimal?))
        Case GetType(Double)
          Return DirectCast(AddressOf ReadDouble, Func(Of DbDataReader, Int32, Double))
        Case GetType(Double?)
          Return DirectCast(AddressOf ReadNullableDouble, Func(Of DbDataReader, Int32, Double?))
        Case GetType(Single)
          Return DirectCast(AddressOf ReadSingle, Func(Of DbDataReader, Int32, Single))
        Case GetType(Single?)
          Return DirectCast(AddressOf ReadNullableSingle, Func(Of DbDataReader, Int32, Single?))
        Case GetType(Byte())
          Return DirectCast(AddressOf ReadByteArray, Func(Of DbDataReader, Int32, Byte()))
        Case GetType(Byte)
          Return DirectCast(AddressOf ReadByte, Func(Of DbDataReader, Int32, Byte))
        Case GetType(Byte?)
          Return DirectCast(AddressOf ReadNullableByte, Func(Of DbDataReader, Int32, Byte?))
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
    Protected Shared Function ReadString(reader As DbDataReader, index As Int32) As String
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
    Protected Shared Function ReadInt16(reader As DbDataReader, index As Int32) As Int16
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
    Protected Shared Function ReadNullableInt16(reader As DbDataReader, index As Int32) As Int16?
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
    Protected Shared Function ReadInt32(reader As DbDataReader, index As Int32) As Int32
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
    Protected Shared Function ReadNullableInt32(reader As DbDataReader, index As Int32) As Int32?
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
    Protected Shared Function ReadInt64(reader As DbDataReader, index As Int32) As Int64
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
    Protected Shared Function ReadNullableInt64(reader As DbDataReader, index As Int32) As Int64?
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
    Protected Shared Function ReadBoolean(reader As DbDataReader, index As Int32) As Boolean
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
    Protected Shared Function ReadNullableBoolean(reader As DbDataReader, index As Int32) As Boolean?
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
    Protected Shared Function ReadGuid(reader As DbDataReader, index As Int32) As Guid
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
    Protected Shared Function ReadNullableGuid(reader As DbDataReader, index As Int32) As Guid?
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
    Protected Shared Function ReadDateTime(reader As DbDataReader, index As Int32) As DateTime
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
    Protected Shared Function ReadNullableDateTime(reader As DbDataReader, index As Int32) As DateTime?
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
    Protected Shared Function ReadDecimal(reader As DbDataReader, index As Int32) As Decimal
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
    Protected Shared Function ReadNullableDecimal(reader As DbDataReader, index As Int32) As Decimal?
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
    Protected Shared Function ReadDouble(reader As DbDataReader, index As Int32) As Double
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
    Protected Shared Function ReadNullableDouble(reader As DbDataReader, index As Int32) As Double?
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
    Protected Shared Function ReadSingle(reader As DbDataReader, index As Int32) As Single
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
    Protected Shared Function ReadNullableSingle(reader As DbDataReader, index As Int32) As Single?
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
    Protected Shared Function ReadByteArray(reader As DbDataReader, index As Int32) As Byte()
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
    Protected Shared Function ReadByte(reader As DbDataReader, index As Int32) As Byte
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
    Protected Shared Function ReadNullableByte(reader As DbDataReader, index As Int32) As Byte?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetByte(index)
      End If
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="TimeSpan"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateTimeSpanReader(dataReaderType As Type) As Func(Of DbDataReader, Int32, TimeSpan)
      Return CreateReader(Of TimeSpan)(dataReaderType, "GetTimeSpan")
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="Nullable(Of TimeSpan)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateNullableTimeSpanReader(dataReaderType As Type) As Func(Of DbDataReader, Int32, TimeSpan?)
      Return CreateReader(Of TimeSpan?)(dataReaderType, "GetTimeSpan")
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="DateTimeOffset"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateDateTimeOffsetReader(dataReaderType As Type) As Func(Of DbDataReader, Int32, DateTimeOffset)
      Return CreateReader(Of DateTimeOffset)(dataReaderType, "GetDateTimeOffset")
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="Nullable(Of DateTimeOffset)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateNullableDateTimeOffsetReader(dataReaderType As Type) As Func(Of DbDataReader, Int32, DateTimeOffset?)
      Return CreateReader(Of DateTimeOffset?)(dataReaderType, "GetDateTimeOffset")
    End Function

    ''' <summary>
    ''' Creates reader function for generic value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="getMethod"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateReader(Of T)(dataReaderType As Type, getMethod As String) As Func(Of DbDataReader, Int32, T)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim parameters = {readerParam, indexParam}

      Dim readerVariable = Expression.Variable(dataReaderType, "r")
      Dim valueVariable = Expression.Variable(GetType(T), "value")

      Dim expressions = New List(Of Expression)(3)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))

      Dim valueAssignNull = Expression.Assign(valueVariable, Expression.Default(GetType(T)))

      Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, indexParam)
      Dim getValueCall = Expression.Call(readerVariable, getMethod, Nothing, indexParam)

      Dim underlyingNullableType = Nullable.GetUnderlyingType(GetType(T))

      If underlyingNullableType Is Nothing Then
        Dim valueAssign = Expression.Assign(valueVariable, getValueCall)
        Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, valueAssignNull, valueAssign)
        expressions.Add(isDBNullCond)
      Else
        Dim nullableConstructor = GetType(T).GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, Array.Empty(Of ParameterModifier)())
        Dim valueAssign = Expression.Assign(valueVariable, Expression.[New](nullableConstructor, getValueCall))
        Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, valueAssignNull, valueAssign)
        expressions.Add(isDBNullCond)
      End If

      expressions.Add(valueVariable)

      Dim body = Expression.Block({readerVariable, valueVariable}, expressions)

      Dim reader = Expression.Lambda(Of Func(Of DbDataReader, Int32, T))(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace