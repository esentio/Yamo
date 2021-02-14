Namespace Model

  Public Class Article

    Public Property Id As Int32

    Public Property Price As Decimal

    Public Property Label As Label

    Public Property Parts As List(Of ArticlePart)

    Public Property Categories As List(Of Category)

    Public Property AlternativeLabel1 As Label ' do not define this in model

    Public Property AlternativeLabel2 As Label ' do not define this in model

    Public Property PriceWithDiscount As Decimal

    Public Property LabelDescription As String

    Public Property Tag As Object

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot Article Then
        Return False
      Else
        Dim o = DirectCast(obj, Article)

        If Not Object.Equals(Me.Id, o.Id) Then Return False
        If Not Object.Equals(Me.Price, o.Price) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.Id, Me.Price)
    End Function

  End Class
End Namespace