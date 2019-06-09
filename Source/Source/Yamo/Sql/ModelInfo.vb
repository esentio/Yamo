Namespace Sql

  Public Structure ModelInfo

    Public ReadOnly Property Model As Type

    Public ReadOnly Property TableAlias As String

    Public Sub New(model As Type, tableAlias As String)
      Me.Model = model
      Me.TableAlias = tableAlias
    End Sub

  End Structure
End Namespace
