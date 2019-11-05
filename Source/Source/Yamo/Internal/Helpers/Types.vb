Imports System.Reflection

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
             genericType Is GetType(ValueTuple(Of ,,,,,,,)) AndAlso IsValueTupleOrNullableValueTuple(type.GetGenericArguments(7))
    End Function

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
      Return True
    End Function

  End Class
End Namespace
