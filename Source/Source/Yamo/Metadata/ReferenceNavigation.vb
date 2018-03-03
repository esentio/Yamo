Namespace Metadata

  Public Class ReferenceNavigation
    Inherits RelationshipNavigation

    Sub New(propertyName As String, relatedEntityType As Type)
      MyBase.New(propertyName, relatedEntityType)
    End Sub

  End Class
End Namespace