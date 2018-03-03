Namespace Model

  Public Class ItemWithIdentityIdAndDefaultValues

    Public Property Id As Int32

    Public Property Description As String

    Public Property UniqueidentifierValue As Guid

    Public Property IntValue As Int32

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ItemWithIdentityIdAndDefaultValues Then
        Return False
      Else
        Dim o = DirectCast(obj, ItemWithIdentityIdAndDefaultValues)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False
        If Not Object.Equals(Me.UniqueidentifierValue, o.UniqueidentifierValue) Then Return False
        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return Helpers.Compare.GetHashCode(Me.Id, Me.Description, Me.UniqueidentifierValue, Me.IntValue)
    End Function

  End Class
End Namespace
