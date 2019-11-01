Namespace Metadata

  ' TODO: SIP - add documentation to this class.
  Public Class ReferenceNavigation
    Inherits RelationshipNavigation

    Sub New(propertyName As String, relatedEntityType As Type)
      MyBase.New(propertyName, relatedEntityType)
    End Sub

  End Class
End Namespace