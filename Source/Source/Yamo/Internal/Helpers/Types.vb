Imports System.Data
Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace Internal.Helpers

  ''' <summary>
  ''' Types related helpers.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class Types

    ''' <summary>
    ''' Creates new instance of <see cref="Types"/>.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Gets whether type is <see cref="Nullable(Of T)"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function IsNullable(type As Type) As Boolean
      ' this might be on hot path; checking IsValueType before calling Nullable.GetUnderlyingType seems to be faster for most use cases
      Return type.IsValueType AndAlso Nullable.GetUnderlyingType(type) IsNot Nothing
    End Function

    ''' <summary>
    ''' Gets whether type is ValueTuple or nullable ValueTuple.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function IsValueTupleOrNullableValueTuple(type As Type) As Boolean
      ' via https://www.tabsoverspaces.com/233605-checking-whether-the-type-is-a-tuple-valuetuple

      Dim underlyingNullableType = Nullable.GetUnderlyingType(type)

      If underlyingNullableType IsNot Nothing Then
        type = underlyingNullableType
      End If

      Return IsValueTuple(type)
    End Function

    ''' <summary>
    ''' Gets whether type is ValueTuple.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function IsValueTuple(type As Type) As Boolean
      ' via https://www.tabsoverspaces.com/233605-checking-whether-the-type-is-a-tuple-valuetuple

      If Not type.IsGenericType Then
        Return False
      End If

      Dim genericType = type.GetGenericTypeDefinition()

      Return genericType Is GetType(ValueTuple(Of )) OrElse
             genericType Is GetType(ValueTuple(Of ,)) OrElse
             genericType Is GetType(ValueTuple(Of ,)) OrElse
             genericType Is GetType(ValueTuple(Of ,,)) OrElse
             genericType Is GetType(ValueTuple(Of ,,,)) OrElse
             genericType Is GetType(ValueTuple(Of ,,,,)) OrElse
             genericType Is GetType(ValueTuple(Of ,,,,,)) OrElse
             genericType Is GetType(ValueTuple(Of ,,,,,,)) OrElse
             genericType Is GetType(ValueTuple(Of ,,,,,,,)) AndAlso IsValueTuple(type.GetGenericArguments(7))
    End Function

    ''' <summary>
    ''' Checks whether type is an anonymous type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function IsAnonymousType(type As Type) As Boolean
      ' via https://stackoverflow.com/questions/2483023/how-to-test-if-a-type-is-anonymous
      ' and https://elegantcode.com/2011/06/24/detecting-anonymous-types-on-mono/
      Return Attribute.IsDefined(type, GetType(CompilerGeneratedAttribute), False) AndAlso
             type.IsGenericType AndAlso
             (type.Name.Contains("AnonymousType") OrElse type.Name.Contains("AnonType")) AndAlso
             (type.Name.StartsWith("<>") OrElse type.Name.StartsWith("VB$")) AndAlso
             (type.Attributes And TypeAttributes.NotPublic) = TypeAttributes.NotPublic
    End Function

    ''' <summary>
    ''' Checks whether member represents <see cref="Nullable(Of T).Value"/> call.<br/>
    ''' Note: type is not checked!<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="member"></param>
    ''' <returns></returns>
    Public Shared Function IsNullableValueAccess(member As MemberInfo) As Boolean
      Return member.Name = "Value"
    End Function

    ''' <summary>
    ''' Checks whether member represents <see cref="Nullable(Of T).HasValue"/> call.<br/>
    ''' Note: type is not checked!<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="member"></param>
    ''' <returns></returns>
    Public Shared Function IsNullableHasValueAccess(member As MemberInfo) As Boolean
      Return member.Name = "HasValue"
    End Function

    ''' <summary>
    ''' Gets flattened ValueTuple generic arguments.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetFlattenedValueTupleGenericArguments(type As Type) As List(Of Type)
      Dim args = New List(Of Type)
      AddValueTupleGenericArguments(type, args)
      Return args
    End Function

    ''' <summary>
    ''' Recursively adds ValueTuple generic arguments to the list.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="allArgs"></param>
    Private Shared Sub AddValueTupleGenericArguments(type As Type, allArgs As List(Of Type))
      Dim args = type.GetGenericArguments()
      Dim count = args.Length

      If 0 < count Then
        For i = 0 To count - 2
          allArgs.Add(args(i))
        Next

        Dim lastArg = args(count - 1)

        If IsValueTuple(lastArg) Then
          AddValueTupleGenericArguments(lastArg, allArgs)
        Else
          allArgs.Add(lastArg)
        End If
      End If
    End Sub

    ''' <summary>
    ''' Gets <see cref="ValueTupleTypeInfo"/> for value tuple type or nullable value type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetValueTupleTypeInfo(type As Type) As ValueTupleTypeInfo
      Dim args = New List(Of Type)
      Dim cis = New List(Of CtorInfo)

      AddValueTupleArgumentsAndCtorInfos(type, args, cis)

      Return New ValueTupleTypeInfo(type, args, cis)
    End Function

    ''' <summary>
    ''' Recursively adds ValueTuple generic arguments and constructor infos to the lists.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="allArgs"></param>
    ''' <param name="allCis"></param>
    Private Shared Sub AddValueTupleArgumentsAndCtorInfos(type As Type, allArgs As List(Of Type), allCis As List(Of CtorInfo))
      Dim args = type.GetGenericArguments()
      Dim count = args.Length

      allCis.Add(New CtorInfo(type.GetConstructor(args), count))

      If 0 < count Then
        For i = 0 To count - 2
          allArgs.Add(args(i))
        Next

        Dim lastArg = args(count - 1)

        If IsValueTuple(lastArg) Then
          AddValueTupleArgumentsAndCtorInfos(lastArg, allArgs, allCis)
        Else
          allArgs.Add(lastArg)
        End If
      End If
    End Sub

    ''' <summary>
    ''' Fast way to determine whether type is a model entity or not.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function IsProbablyModel(type As Type) As Boolean
      ' TODO: SIP - this won't work if model entity is a structure!
      If type.IsValueType Then Return False
      If type Is GetType(String) Then Return False
      If type Is GetType(Byte()) Then Return False
      If type Is GetType(Object()) Then Return False
      Return True
    End Function

    ''' <summary>
    ''' Gets <see cref="DbType"/> for CLR type.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetDbTypeForClrType(type As Type) As DbType?
      ' See also:
      ' https://referencesource.microsoft.com/#System.Data.Entity/System/Data/Metadata/TypeHelpers.cs,6da27ae495b4be68
      ' https://referencesource.microsoft.com/#System.Data/fx/src/data/System/Data/SqlClient/SqlEnums.cs,b80e3c28640e1801
      ' https://stackoverflow.com/questions/7952142/how-to-resolve-system-type-to-system-data-dbtype
      ' https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings

      Select Case type
        Case GetType(String)
          Return DbType.String
        Case GetType(Int16), GetType(Int16?)
          Return DbType.Int16
        Case GetType(Int32), GetType(Int32?)
          Return DbType.Int32
        Case GetType(Int64), GetType(Int64?)
          Return DbType.Int64
        Case GetType(Boolean), GetType(Boolean?)
          Return DbType.Boolean
        Case GetType(Guid), GetType(Guid?)
          Return DbType.Guid
        Case GetType(DateTime), GetType(DateTime?)
          Return DbType.DateTime
        Case GetType(TimeSpan), GetType(TimeSpan?)
          Return DbType.Time
        Case GetType(DateTimeOffset), GetType(DateTimeOffset?)
          Return DbType.DateTimeOffset
        Case GetType(Decimal), GetType(Decimal?)
          Return DbType.Decimal
        Case GetType(Double), GetType(Double?)
          Return DbType.Double
        Case GetType(Single), GetType(Single?)
          Return DbType.Single
        Case GetType(Byte())
          Return DbType.Binary
        Case GetType(Byte), GetType(Byte?)
          Return DbType.Byte
        Case GetType(Char())
          Return DbType.String
        Case GetType(DBNull)
          Return DbType.Object
        Case GetType(SByte), GetType(SByte?)
          Return DbType.SByte
        Case GetType(Char), GetType(Byte?)
          Return DbType.String
        Case GetType(UInt16), GetType(UInt16?)
          Return DbType.UInt16
        Case GetType(UInt32), GetType(UInt32?)
          Return DbType.UInt32
        Case GetType(UInt64), GetType(UInt64?)
          Return DbType.UInt64
        Case Else
          If Type.GetTypeCode(type) = TypeCode.Object Then
            Return DbType.Object
          End If
      End Select

      Return Nothing
    End Function

  End Class
End Namespace
