Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ' TODO: SIP - add documentation to this class.
  Public Class CustomSqlEntity
    ' TODO: SIP - structure instead?

    Public ReadOnly Property Index As Int32

    Public ReadOnly Property IsEntity As Boolean

    Public ReadOnly Property EntityIndex As Int32

    Public ReadOnly Property Type As Type

    ' simple value
    Public Sub New(index As Int32, type As Type)
      Me.Index = index
      Me.IsEntity = False
      Me.EntityIndex = -1
      Me.Type = type
    End Sub

    ' entity
    Public Sub New(index As Int32, entityIndex As Int32, type As Type)
      Me.Index = index
      Me.IsEntity = True
      Me.EntityIndex = entityIndex
      Me.Type = type
    End Sub

  End Class
End Namespace