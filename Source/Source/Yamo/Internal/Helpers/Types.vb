Imports System.Reflection

Namespace Internal.Helpers

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

  End Class
End Namespace
