Namespace Metadata

  ''' <summary>
  ''' Represent a 1:1 relationship navigation between entities.
  ''' </summary>
  Public Class ReferenceNavigation
    Inherits RelationshipNavigation

    ''' <summary>
    ''' Creates new instance of <see cref="ReferenceNavigation"/>.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    Sub New(propertyName As String, relatedEntityType As Type)
      MyBase.New(propertyName, relatedEntityType)
    End Sub

  End Class
End Namespace