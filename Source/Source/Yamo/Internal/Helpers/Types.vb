Imports System.Reflection

Namespace Internal.Helpers

  ' TODO: SIP - add documentation to this class.
  Public Class Types

    Private Sub New()
    End Sub

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

    Public Shared Function IsProbablyModel(type As Type) As Boolean
      ' used as a fast way to determine whether type is a model entity or not
      ' TODO: SIP - this won't work if model entity is a structure!
      If type.IsValueType Then Return False
      If type Is GetType(String) Then Return False
      If type Is GetType(Byte()) Then Return False
      Return True
    End Function

  End Class
End Namespace
