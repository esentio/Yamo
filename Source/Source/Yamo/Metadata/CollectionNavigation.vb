Namespace Metadata

  Public Class CollectionNavigation
    Inherits RelationshipNavigation

    Public ReadOnly Property CollectionType As Type

    Sub New(propertyName As String, relatedEntityType As Type, collectionType As Type)
      MyBase.New(propertyName, relatedEntityType)
      Me.CollectionType = collectionType
    End Sub

  End Class
End Namespace