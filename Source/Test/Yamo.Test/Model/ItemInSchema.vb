Namespace Model

  Public Class ItemInSchema

    Public Property Id As Int32

    Public Property Description As String

    Public Property RelatedItemId As Int32?

    Public Property Deleted As DateTime?

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemInSchema Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemInSchema)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False
        If Not Object.Equals(Me.RelatedItemId, o.RelatedItemId) Then Return False
        If Not Object.Equals(Me.Deleted, o.Deleted) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return Helpers.Compare.GetHashCode(Me.Id, Me.Description, Me.RelatedItemId, Me.Deleted)
    End Function

  End Class
End Namespace