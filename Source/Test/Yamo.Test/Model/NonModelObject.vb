Namespace Model

  Public Class NonModelObject

    Public Property GuidValue As Guid

    Public Property BooleanValue As Boolean

    Public Property StringValue1 As String

    Public Property StringValue2 As String

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

    Public Sub New(guidValue As Guid, stringValue1 As String, nullableDecimalValue As Decimal?)
      Me.GuidValue = guidValue
      Me.StringValue1 = stringValue1
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

    Public Sub New(intValue As Int32, stringValue1 As String, stringValue2 As String)
      Me.IntValue = intValue
      Me.StringValue1 = stringValue1
      Me.StringValue2 = stringValue2
    End Sub

    Public Sub New(stringValue2 As String)
      Me.StringValue2 = stringValue2
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing OrElse TypeOf obj IsNot NonModelObject Then
        Return False
      Else
        Dim o = DirectCast(obj, NonModelObject)

        If Not Object.Equals(Me.GuidValue, o.GuidValue) Then Return False
        If Not Object.Equals(Me.BooleanValue, o.BooleanValue) Then Return False
        If Not Object.Equals(Me.StringValue1, o.StringValue1) Then Return False
        If Not Object.Equals(Me.StringValue2, o.StringValue2) Then Return False
        If Not Object.Equals(Me.IntValue, o.IntValue) Then Return False
        If Not Object.Equals(Me.NullableDecimalValue, o.NullableDecimalValue) Then Return False

        Return True
      End If
    End Function

    Public Overrides Function GetHashCode() As Int32
      Return HashCode.Combine(Me.GuidValue, Me.BooleanValue, Me.StringValue1, Me.StringValue2, Me.IntValue, Me.NullableDecimalValue)
    End Function

  End Class
End Namespace