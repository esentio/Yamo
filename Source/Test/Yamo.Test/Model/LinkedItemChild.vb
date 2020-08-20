Namespace Model

  Public Class LinkedItemChild

    Public Property Id As Int32

    Public Property LinkedItemId As Int32

    Public Property Description As String

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot LinkedItemChild Then
        Return False
      Else
        Dim o = DirectCast(obj, LinkedItemChild)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.LinkedItemId, o.LinkedItemId) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.LinkedItemId, Me.Description)
    End Function

  End Class
End Namespace