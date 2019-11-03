Namespace Metadata

  ''' <summary>
  ''' Represent a 1:N relationship navigation between entities.
  ''' </summary>
  Public Class CollectionNavigation
    Inherits RelationshipNavigation

    ''' <summary>
    ''' Gets type of the collection stored in the navigation property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CollectionType As Type

    ''' <summary>
    ''' Creates new instance of <see cref="CollectionNavigation"/>.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    ''' <param name="collectionType"></param>
    Sub New(propertyName As String, relatedEntityType As Type, collectionType As Type)
      MyBase.New(propertyName, relatedEntityType)
      Me.CollectionType = collectionType
    End Sub

  End Class
End Namespace