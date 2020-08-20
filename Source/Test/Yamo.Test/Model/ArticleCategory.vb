Namespace Model

  Public Class ArticleCategory

    Public Property ArticleId As Int32

    Public Property CategoryId As Int32

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ArticleCategory Then
        Return False
      Else
        Dim o = DirectCast(obj, ArticleCategory)

        If Not Object.Equals(Me.ArticleId, o.ArticleId) Then Return False
        If Not Object.Equals(Me.CategoryId, o.CategoryId) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.ArticleId, Me.CategoryId)
    End Function

  End Class
End Namespace