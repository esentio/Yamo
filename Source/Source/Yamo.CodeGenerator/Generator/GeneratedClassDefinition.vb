Namespace Generator

  Public Class GeneratedClassDefinition

    Public ReadOnly Property Type As GeneratedClass

    Public ReadOnly Property MinEntityCount As Int32

    Public ReadOnly Property MaxEntityCount As Int32

    Sub New(type As GeneratedClass, maxEntityCount As Int32)
      Me.New(type, 1, maxEntityCount)
    End Sub

    Sub New(type As GeneratedClass, minEntityCount As Int32, maxEntityCount As Int32)
      Me.Type = type
      Me.MinEntityCount = minEntityCount
      Me.MaxEntityCount = maxEntityCount
    End Sub

  End Class
End Namespace