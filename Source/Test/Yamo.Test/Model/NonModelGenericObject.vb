Namespace Model

  Public Class NonModelGenericObject(Of T1, T2)

    Public Property Value1 As T1

    Public Property Value2 As T2

    Public Sub New(value1 As T1, value2 As T2)
      Me.Value1 = value1
      Me.Value2 = value2
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelGenericObject(Of T1, T2) Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelGenericObject(Of T1, T2))

        If Not Object.Equals(Me.Value1, o.Value1) Then Return False
        If Not Object.Equals(Me.Value2, o.Value2) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Value1, Me.Value2)
    End Function

  End Class
End Namespace