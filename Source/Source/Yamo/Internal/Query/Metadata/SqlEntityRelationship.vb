Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related entity relationship data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlEntityRelationship

    ' TODO: SIP - structure instead?

    ''' <summary>
    ''' Gets declaring entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DeclaringEntity As SqlEntityBase

    ''' <summary>
    ''' Gets relationship navigation.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RelationshipNavigation As RelationshipNavigation

    ''' <summary>
    ''' Stores whether reference navigation is used.
    ''' </summary>
    Private m_IsReferenceNavigation As Boolean

    ''' <summary>
    ''' Gets whether reference navigation is used.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsReferenceNavigation As Boolean
      Get
        Return m_IsReferenceNavigation
      End Get
    End Property

    ''' <summary>
    ''' Gets whether collection navigation is used.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsCollectionNavigation As Boolean
      Get
        Return Not m_IsReferenceNavigation
      End Get
    End Property

    ''' <summary>
    ''' Creates new instance of <see cref="SqlEntityRelationship"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="declaringEntity"></param>
    ''' <param name="relationshipNavigation"></param>
    Sub New(<DisallowNull> declaringEntity As SqlEntityBase, <DisallowNull> relationshipNavigation As RelationshipNavigation)
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