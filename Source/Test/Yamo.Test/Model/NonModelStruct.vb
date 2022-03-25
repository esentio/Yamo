Namespace Model

  Public Structure NonModelStruct

    Public Property GuidValue As Guid

    Public Property BooleanValue As Boolean

    Public Property StringValue As String

    Public Property IntValue As Int32

    Public Property NullableDecimalValue As Decimal?

    Public Property ItemWithAllSupportedValues As ItemWithAllSupportedValues

    Public Sub New(guidValue As Guid, stringValue As String, nullableDecimalValue As Decimal?)
      Me.New()
      Me.GuidValue = guidValue
      Me.StringValue = stringValue
      Me.NullableDecimalValue = nullableDecimalValue
    End Sub

    Public Sub New(guidValue As Guid, itemWithAllSupportedValues As ItemWithAllSupportedValues)
      Me.New()
      Me.GuidValue = guidValue
      Me.ItemWithAllSupportedValues = itemWithAllSupportedValues
    End Sub

  End Structure
End Namespace