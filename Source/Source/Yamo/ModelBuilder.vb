Imports Yamo.Metadata
Imports Yamo.Metadata.Builders

' not in Metadata.Builders namespace, so we don't need to use imports

Public Class ModelBuilder

  Public ReadOnly Property Model As Model

  Sub New()
    Me.Model = New Model
  End Sub

  Public Function Entity(Of TEntity As Class)() As EntityBuilder(Of TEntity)
    Return New EntityBuilder(Of TEntity)(Me.Model)
  End Function

  Public Function Entity(Of TEntity As Class)(buildAction As Action(Of EntityBuilder(Of TEntity))) As ModelBuilder
    buildAction(Entity(Of TEntity)())
    Return Me
  End Function

End Class
