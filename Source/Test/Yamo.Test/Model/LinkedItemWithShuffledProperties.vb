Namespace Model

  Public Class LinkedItemWithShuffledProperties

    ' same as LinkedItem, but properties order in model definition is changed

    Public Property Id As Int32

    Public Property PreviousId As Int32?

    Public Property Description As String

    Public Property NextItem As LinkedItemWithShuffledProperties

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot LinkedItemWithShuffledProperties Then
        Return False
      Else
        Dim o = DirectCast(obj, LinkedItemWithShuffledProperties)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.PreviousId, o.PreviousId) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return Helpers.Compare.GetHashCode(Me.Id, Me.PreviousId, Me.Description)
    End Function

  End Class
End Namespace