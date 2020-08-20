Namespace Model

  Public Class ArticleSubstitution

    Public Property OriginalArticleId As Int32

    Public Property SubstitutionArticleId As Int32

    Public Property Original As Article

    Public Property Substitution As Article

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ArticleSubstitution Then
        Return False
      Else
        Dim o = DirectCast(obj, ArticleSubstitution)

        If Not Object.Equals(Me.OriginalArticleId, o.OriginalArticleId) Then Return False
        If Not Object.Equals(Me.SubstitutionArticleId, o.SubstitutionArticleId) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.OriginalArticleId, Me.SubstitutionArticleId)
    End Function

  End Class
End Namespace