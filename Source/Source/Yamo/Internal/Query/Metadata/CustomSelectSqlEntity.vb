Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  Public Class CustomSelectSqlEntity
    ' TODO: SIP - structure instead?

    Public ReadOnly Property Index As Int32

    Public ReadOnly Property IsEntity As Boolean

    Public ReadOnly Property EntityIndex As Int32

    Public ReadOnly Property Type As Type

    Public Sub New(index As Int32, isEntity As Boolean, entityIndex As Int32, type As Type)
      Me.Index = index
      Me.IsEntity = isEntity
      Me.EntityIndex = entityIndex
      Me.Type = type
    End Sub

  End Class
End Namespace