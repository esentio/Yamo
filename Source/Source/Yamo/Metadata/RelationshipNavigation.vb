Namespace Metadata

  ''' <summary>
  ''' Base class to represent a relationship navigation between entities.
  ''' </summary>
  Public MustInherit Class RelationshipNavigation

    ''' <summary>
    ''' Gets name of the navigation property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PropertyName As String

    ''' <summary>
    ''' Gets type of related entity.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RelatedEntityType As Type

    ''' <summary>
    ''' Creates new instance of <see cref="RelationshipNavigation"/>.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <param name="relatedEntityType"></param>
    Sub New(propertyName As String, relatedEntityType As Type)
      Me.PropertyName = propertyName
      Me.RelatedEntityType = relatedEntityType
    End Sub

  End Class
End Namespace