Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports System.Reflection

Namespace Infrastructure

  ''' <summary>
  ''' Value type reader factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTypeReaderFactory
    Inherits ReaderFactoryBase

    ' NOTE: static methods are used instead of code generation if possible

    ' NOTE: specialized GetX methods are much faster than GetFieldValue(Of T) method

    ''' <summary>
    ''' Creates reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Overridable Function CreateReader(<DisallowNull> dataReaderType As Type, <DisallowNull> type As Type) As Object
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
#If NET6_0_OR_GREATER Then
        Case GetType(DateOnly)
          Return DirectCast(AddressOf ReadDateOnly, Func(Of DbDataReader, Int32, DateOnly))
        Case GetType(DateOnly?)
          Return DirectCast(AddressOf ReadNullableDateOnly, Func(Of DbDataReader, Int32, DateOnly?))
        Case GetType(TimeOnly)
          Return DirectCast(AddressOf ReadTimeOnly, Func(Of DbDataReader, Int32, TimeOnly))
        Case GetType(TimeOnly?)
          Return DirectCast(AddressOf ReadNullableTimeOnly, Func(Of DbDataReader, Int32, TimeOnly?))
#End If
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
        Case GetType(Char)
          Return DirectCast(AddressOf ReadChar, Func(Of DbDataReader, Int32, Char))
        Case GetType(Char?)
          Return DirectCast(AddressOf ReadNullableChar, Func(Of DbDataReader, Int32, Char?))
        Case Else
          Return CreateGenericReader(dataReaderType, type)
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
    Protected Shared Function ReadString(<DisallowNull> reader As DbDataReader, index As Int32) As String
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
    Protected Shared Function ReadInt16(<DisallowNull> reader As DbDataReader, index As Int32) As Int16
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
    Protected Shared Function ReadNullableInt16(<DisallowNull> reader As DbDataReader, index As Int32) As Int16?
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
    Protected Shared Function ReadInt32(<DisallowNull> reader As DbDataReader, index As Int32) As Int32
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
    Protected Shared Function ReadNullableInt32(<DisallowNull> reader As DbDataReader, index As Int32) As Int32?
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
    Protected Shared Function ReadInt64(<DisallowNull> reader As DbDataReader, index As Int32) As Int64
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
    Protected Shared Function ReadNullableInt64(<DisallowNull> reader As DbDataReader, index As Int32) As Int64?
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
    Protected Shared Function ReadBoolean(<DisallowNull> reader As DbDataReader, index As Int32) As Boolean
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
    Protected Shared Function ReadNullableBoolean(<DisallowNull> reader As DbDataReader, index As Int32) As Boolean?
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
    Protected Shared Function ReadGuid(<DisallowNull> reader As DbDataReader, index As Int32) As Guid
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
    Protected Shared Function ReadNullableGuid(<DisallowNull> reader As DbDataReader, index As Int32) As Guid?
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
    Protected Shared Function ReadDateTime(<DisallowNull> reader As DbDataReader, index As Int32) As DateTime
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
    Protected Shared Function ReadNullableDateTime(<DisallowNull> reader As DbDataReader, index As Int32) As DateTime?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetDateTime(index)
      End If
    End Function

#If NET6_0_OR_GREATER Then
    ''' <summary>
    ''' Reads <see cref="DateOnly"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadDateOnly(<DisallowNull> reader As DbDataReader, index As Int32) As DateOnly
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetFieldValue(Of DateOnly)(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of DateOnly)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableDateOnly(<DisallowNull> reader As DbDataReader, index As Int32) As DateOnly?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetFieldValue(Of DateOnly)(index)
      End If
    End Function
    
    ''' <summary>
    ''' Reads <see cref="TimeOnly"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadTimeOnly(<DisallowNull> reader As DbDataReader, index As Int32) As TimeOnly
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetFieldValue(Of TimeOnly)(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of TimeOnly)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableTimeOnly(<DisallowNull> reader As DbDataReader, index As Int32) As TimeOnly?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetFieldValue(Of TimeOnly)(index)
      End If
    End Function
#End If

    ''' <summary>
    ''' Reads <see cref="Decimal"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadDecimal(<DisallowNull> reader As DbDataReader, index As Int32) As Decimal
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
    Protected Shared Function ReadNullableDecimal(<DisallowNull> reader As DbDataReader, index As Int32) As Decimal?
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
    Protected Shared Function ReadDouble(<DisallowNull> reader As DbDataReader, index As Int32) As Double
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
    Protected Shared Function ReadNullableDouble(<DisallowNull> reader As DbDataReader, index As Int32) As Double?
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
    Protected Shared Function ReadSingle(<DisallowNull> reader As DbDataReader, index As Int32) As Single
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
    Protected Shared Function ReadNullableSingle(<DisallowNull> reader As DbDataReader, index As Int32) As Single?
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
    Protected Shared Function ReadByteArray(<DisallowNull> reader As DbDataReader, index As Int32) As Byte()
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
    Protected Shared Function ReadByte(<DisallowNull> reader As DbDataReader, index As Int32) As Byte
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
    Protected Shared Function ReadNullableByte(<DisallowNull> reader As DbDataReader, index As Int32) As Byte?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetByte(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Char"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadChar(<DisallowNull> reader As DbDataReader, index As Int32) As Char
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetChar(index)
      End If
    End Function

    ''' <summary>
    ''' Reads <see cref="Nullable(Of Char)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Protected Shared Function ReadNullableChar(<DisallowNull> reader As DbDataReader, index As Int32) As Char?
      If reader.IsDBNull(index) Then
        Return Nothing
      Else
        Return reader.GetChar(index)
      End If
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="TimeSpan"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateTimeSpanReader(<DisallowNull> dataReaderType As Type) As Func(Of DbDataReader, Int32, TimeSpan)
      Return CreateReader(Of TimeSpan)(dataReaderType, "GetTimeSpan")
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="Nullable(Of TimeSpan)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateNullableTimeSpanReader(<DisallowNull> dataReaderType As Type) As Func(Of DbDataReader, Int32, TimeSpan?)
      Return CreateReader(Of TimeSpan?)(dataReaderType, "GetTimeSpan")
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="DateTimeOffset"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateDateTimeOffsetReader(<DisallowNull> dataReaderType As Type) As Func(Of DbDataReader, Int32, DateTimeOffset)
      Return CreateReader(Of DateTimeOffset)(dataReaderType, "GetDateTimeOffset")
    End Function

    ''' <summary>
    ''' Creates reader function for <see cref="Nullable(Of DateTimeOffset)"/> value.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateNullableDateTimeOffsetReader(<DisallowNull> dataReaderType As Type) As Func(Of DbDataReader, Int32, DateTimeOffset?)
      Return CreateReader(Of DateTimeOffset?)(dataReaderType, "GetDateTimeOffset")
    End Function

    ''' <summary>
    ''' Creates reader function for value using Get* method.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="getMethod"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateReader(Of T)(<DisallowNull> dataReaderType As Type, <DisallowNull> getMethod As String) As Func(Of DbDataReader, Int32, T)
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim parameters = {readerParam, indexParam}

      Dim readerVariable = Expression.Variable(dataReaderType, "r")
      Dim valueVariable = Expression.Variable(GetType(T), "value")

      Dim expressions = New List(Of Expression)(3)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))

      Dim valueAssignNull = Expression.Assign(valueVariable, Expression.Default(GetType(T)))

      Dim underlyingNullableType = Nullable.GetUnderlyingType(GetType(T))

      Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, indexParam)
      Dim getValueCall = Expression.Call(readerVariable, getMethod, Nothing, indexParam)

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

    ''' <summary>
    ''' Creates reader function for value using <see cref="DbDataReader.GetFieldValue(Of T)"/> method.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Protected Overridable Function CreateGenericReader(<DisallowNull> dataReaderType As Type, <DisallowNull> type As Type) As Object
      Dim readerParam = Expression.Parameter(GetType(DbDataReader), "reader")
      Dim indexParam = Expression.Parameter(GetType(Int32), "index")
      Dim parameters = {readerParam, indexParam}

      Dim readerVariable = Expression.Variable(dataReaderType, "r")
      Dim valueVariable = Expression.Variable(type, "value")

      Dim expressions = New List(Of Expression)(3)

      expressions.Add(Expression.Assign(readerVariable, Expression.Convert(readerParam, dataReaderType)))

      Dim valueAssignNull = Expression.Assign(valueVariable, Expression.Default(type))

      Dim underlyingNullableType = Nullable.GetUnderlyingType(type)

      Dim isDBNullCall = Expression.Call(readerVariable, "IsDBNull", Nothing, indexParam)

      Dim genericType = If(underlyingNullableType Is Nothing, type, underlyingNullableType)
      Dim getValueCall = Expression.Call(readerVariable, "GetFieldValue", {genericType}, indexParam)

      If underlyingNullableType Is Nothing Then
        Dim valueAssign = Expression.Assign(valueVariable, getValueCall)
        Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, valueAssignNull, valueAssign)
        expressions.Add(isDBNullCond)
      Else
        Dim nullableConstructor = type.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, CallingConventions.HasThis, {underlyingNullableType}, Array.Empty(Of ParameterModifier)())
        Dim valueAssign = Expression.Assign(valueVariable, Expression.[New](nullableConstructor, getValueCall))
        Dim isDBNullCond = Expression.IfThenElse(isDBNullCall, valueAssignNull, valueAssign)
        expressions.Add(isDBNullCond)
      End If

      expressions.Add(valueVariable)

      Dim body = Expression.Block({readerVariable, valueVariable}, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace