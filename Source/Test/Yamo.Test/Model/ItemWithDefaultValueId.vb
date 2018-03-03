Namespace Model

  Public Class ItemWithDefaultValueId

    Public Property Id As Guid

    Public Property Description As String

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithDefaultValueId Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithDefaultValueId)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return Helpers.Compare.GetHashCode(Me.Id, Me.Description)
    End Function

  End Class
End Namespace
