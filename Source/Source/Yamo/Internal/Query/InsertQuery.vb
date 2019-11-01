Namespace Internal.Query

  ' TODO: SIP - add documentation to this class.
  Public Class InsertQuery
    Inherits Query

    Public ReadOnly Property ReadDbGeneratedValues As Boolean

    Public ReadOnly Property Entity As Object

    Sub New(sql As SqlString, readDbGeneratedValues As Boolean, entity As Object)
      MyBase.New(sql)
      Me.ReadDbGeneratedValues = readDbGeneratedValues
      Me.Entity = entity
    End Sub

    Sub New(sql As String, parameters As List(Of SqlParameter), readDbGeneratedValues As Boolean, entity As Object)
      MyBase.New(sql, parameters)
      Me.ReadDbGeneratedValues = readDbGeneratedValues
      Me.Entity = entity
    End Sub

  End Class
End Namespace