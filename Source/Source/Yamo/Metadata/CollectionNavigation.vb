Namespace Metadata

  ' TODO: SIP - add documentation to this class.
  Public Class CollectionNavigation
    Inherits RelationshipNavigation

    Public ReadOnly Property CollectionType As Type

    Sub New(propertyName As String, relatedEntityType As Type, collectionType As Type)
      MyBase.New(propertyName, relatedEntityType)
      Me.CollectionType = collectionType
    End Sub

  End Class
End Namespace