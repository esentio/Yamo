Namespace Model

  Public Class ArticlePart

    Public Property Id As Int32

    Public Property ArticleId As Int32

    Public Property Price As Decimal

    Public Property Label As Label

    Public Property PriceWithDiscount As Decimal

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot ArticlePart Then
        Return False
      Else
        Dim o = DirectCast(obj, ArticlePart)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.ArticleId, o.ArticleId) Then Return False
        If Not Object.Equals(Me.Price, o.Price) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.ArticleId, Me.Price)
    End Function

  End Class
End Namespace