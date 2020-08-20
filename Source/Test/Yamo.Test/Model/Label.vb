Namespace Model

  Public Class Label

    Public Property TableId As String

    Public Property Id As Int32

    Public Property Language As String

    Public Property Description As String

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot Label Then
        Return False
      Else
        Dim o = DirectCast(obj, Label)

        If Not Object.Equals(Me.TableId, o.TableId) Then Return False
        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Language, o.Language) Then Return False
        If Not Object.Equals(Me.Description, o.Description) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.TableId, Me.Id, Me.Language, Me.Description)
    End Function

  End Class
End Namespace