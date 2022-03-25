Namespace Model

  Public Class NonModelObject

    Public Property GuidValue As Guid

    Public Property BooleanValue As Boolean

    Public Property StringValue As String

    Public Property IntValue As Int32

    Public Property NullableDecimalValue As Decimal?

    Public Property ItemWithAllSupportedValues As ItemWithAllSupportedValues

    Public Property Article As Article

    Public Property Label As Label

    Public Sub New()
    End Sub

    Public Sub New(guidValue As Guid, booleanValue As Boolean)
      Me.GuidValue = guidValue
      Me.BooleanValue = booleanValue
    End Sub

    Public Sub New(guidValue As Guid, stringValue As String, nullableDecimalValue As Decimal?)
      Me.GuidValue = guidValue
      Me.StringValue = stringValue
      Me.NullableDecimalValue = nullableDecimalValue
    End Sub

    Public Sub New(guidValue As Guid, itemWithAllSupportedValues As ItemWithAllSupportedValues)
      Me.GuidValue = guidValue
      Me.ItemWithAllSupportedValues = itemWithAllSupportedValues
    End Sub

    Public Sub New(article As Article, label As Label)
      Me.Article = article
      Me.Label = label
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelObject Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelObject)

        If Not Object.Equals(Me.GuidValue, o.GuidValue) Then Return False
        If Not Object.Equals(Me.BooleanValue, o.BooleanValue) Then Return False
        If Not Object.Equals(Me.StringValue, o.StringValue) Then Return False
        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False
        If Not Object.Equals(Me.NullableDecimalValue, o.NullableDecimalValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.GuidValue, Me.BooleanValue, Me.StringValue, Me.IntValue, Me.NullableDecimalValue)
    End Function

  End Class
End Namespace