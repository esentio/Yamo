Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  Public Class CustomSqlEntity
    ' TODO: SIP - structure instead?

    Public ReadOnly Property Index As Int32

    Public ReadOnly Property IsEntity As Boolean

    Public ReadOnly Property EntityIndex As Int32

    Public ReadOnly Property Type As Type

    Public ReadOnly Property ColumnAlias As String

    Public ReadOnly Property ColumnAliases As String()

    Public ReadOnly Property Name As String

    ' simple value
    Public Sub New(index As Int32, type As Type, columnAlias As String, name As String)
      Me.Index = index
      Me.IsEntity = False
      Me.EntityIndex = -1
      Me.Type = type
      Me.ColumnAlias = columnAlias
      Me.ColumnAliases = Nothing
      Me.Name = name
    End Sub

    ' entity
    Public Sub New(index As Int32, entityIndex As Int32, type As Type, columnAliases As String(), name As String)
      Me.Index = index
      Me.IsEntity = True
      Me.EntityIndex = entityIndex
      Me.Type = type
      Me.ColumnAlias = Nothing
      Me.ColumnAliases = columnAliases
      Me.Name = name
    End Sub

  End Class
End Namespace