Namespace Model

  Public Class LinkedItem

    Public Property Id As Int32

    Public Property PreviousId As Int32?

    Public Property Description As String

    Public Property NextItem As LinkedItem

    Public Property Children As List(Of LinkedItemChild)

    Public Property RelatedItem As Object

    Public Property RelatedItems As List(Of Object)

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot LinkedItem Then
        Return False
      Else
        Dim o = DirectCast(obj, LinkedItem)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.PreviousId, o.PreviousId) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.PreviousId, Me.Description)
    End Function

  End Class
End Namespace