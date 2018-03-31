Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  Public Class CustomSelectSqlEntity
    ' TODO: SIP - structure instead?

    Public ReadOnly Property Index As Int32

    Public ReadOnly Property Type As Type

    Public Sub New(index As Int32, type As Type)
      Me.Index = index
      Me.Type = type
    End Sub

  End Class
End Namespace