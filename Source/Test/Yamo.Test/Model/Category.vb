Namespace Model

  Public Class Category

    Public Property Id As Int32

    Public Property Label As Label

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot Category Then
        Return False
      Else
        Dim o = DirectCast(obj, Category)

        If Not Object.Equals(Me.Id, o.Id) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return Me.Id.GetHashCode()
    End Function

  End Class
End Namespace