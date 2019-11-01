Namespace Metadata

  ' TODO: SIP - add documentation to this class.
  Public MustInherit Class RelationshipNavigation

    Public ReadOnly Property PropertyName As String

    Public ReadOnly Property RelatedEntityType As Type

    Sub New(propertyName As String, relatedEntityType As Type)
      Me.PropertyName = propertyName
      Me.RelatedEntityType = relatedEntityType
    End Sub

  End Class
End Namespace