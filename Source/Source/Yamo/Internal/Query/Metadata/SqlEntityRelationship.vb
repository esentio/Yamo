Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ' TODO: SIP - add documentation to this class.
  Public Class SqlEntityRelationship
    ' TODO: SIP - structure instead?

    Public ReadOnly Property DeclaringEntity As SqlEntity

    Public ReadOnly Property RelationshipNavigation As RelationshipNavigation

    Private m_IsReferenceNavigation As Boolean

    Public ReadOnly Property IsReferenceNavigation As Boolean
      Get
        Return m_IsReferenceNavigation
      End Get
    End Property

    Public ReadOnly Property IsCollectionNavigation As Boolean
      Get
        Return Not m_IsReferenceNavigation
      End Get
    End Property

    Sub New(declaringEntity As SqlEntity, relationshipNavigation As RelationshipNavigation)
      Me.DeclaringEntity = declaringEntity
      Me.RelationshipNavigation = relationshipNavigation

      Select Case relationshipNavigation.GetType()
        Case GetType(ReferenceNavigation)
          m_IsReferenceNavigation = True
        Case GetType(CollectionNavigation)
          m_IsReferenceNavigation = False
        Case Else
          Throw New NotSupportedException($"Relationship of type '{relationshipNavigation.GetType()}' is not supported.")
      End Select
    End Sub

  End Class
End Namespace