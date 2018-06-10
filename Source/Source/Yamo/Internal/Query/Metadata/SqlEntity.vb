Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  Public Class SqlEntity
    ' TODO: SIP - structure instead?

    Public ReadOnly Property Entity As Entity

    Public ReadOnly Property TableAlias As String

    Public ReadOnly Property Index As Int32

    Public ReadOnly Property Relationship As SqlEntityRelationship

    Public ReadOnly Property IsExcluded As Boolean

    Public ReadOnly Property IncludedColumns As Boolean()

    Sub New(entity As Entity, tableAlias As String, index As Int32)
      Me.Entity = entity
      Me.TableAlias = tableAlias
      Me.Index = index
      Me.Relationship = Nothing
      Me.IsExcluded = False

      Dim lastIndex = Me.Entity.GetPropertiesCount() - 1
      Dim includedColumns = New Boolean(lastIndex) {}

      For i = 0 To lastIndex
        includedColumns(i) = True
      Next

      Me.IncludedColumns = includedColumns
    End Sub

    Sub New(entity As Entity, tableAlias As String, index As Int32, relationship As SqlEntityRelationship)
      Me.New(entity, tableAlias, index)
      Me.Relationship = relationship
    End Sub

    Public Sub SetRelationship(relationship As SqlEntityRelationship)
      Me._Relationship = relationship
    End Sub

    Public Sub Exclude()
      Me._IsExcluded = True
    End Sub

    Public Function GetColumnCount() As Int32
      If Me.IsExcluded Then
        Return 0
      End If

      Dim count = 0

      For i = 0 To Me.IncludedColumns.Length - 1
        If Me.IncludedColumns(i) Then
          count += 1
        End If
      Next

      Return count
    End Function

  End Class
End Namespace