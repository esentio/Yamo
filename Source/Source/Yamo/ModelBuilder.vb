Imports Yamo.Metadata
Imports Yamo.Metadata.Builders

' NOTE: not in Metadata.Builders namespace, so we don't need to use imports in custom DbContext classes

''' <summary>
''' Provides an API for configuring <see cref="Metadata.Model"/>.
''' </summary>
Public Class ModelBuilder

  ''' <summary>
  ''' Gets model that is being configured.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property Model As Model

  ''' <summary>
  ''' Creates new instance of <see cref="ModelBuilder"/>.
  ''' </summary>
  Sub New()
    Me.Model = New Model
  End Sub

  ''' <summary>
  ''' Returns an object that can be used to configure an entity of a given type in the model.
  ''' If entity of this type doesn't already exist in the model definition, it will be added.
  ''' </summary>
  ''' <typeparam name="TEntity"></typeparam>
  ''' <returns></returns>
  Public Function Entity(Of TEntity As Class)() As EntityBuilder(Of TEntity)
    Return New EntityBuilder(Of TEntity)(Me.Model)
  End Function

  ''' <summary>
  ''' Configures entity of a given type in the model.
  ''' If entity of this type doesn't already exist in the model definition, it will be added.
  ''' </summary>
  ''' <typeparam name="TEntity"></typeparam>
  ''' <param name="buildAction"></param>
  ''' <returns></returns>
  Public Function Entity(Of TEntity As Class)(buildAction As Action(Of EntityBuilder(Of TEntity))) As ModelBuilder
    buildAction(Entity(Of TEntity)())
    Return Me
  End Function

End Class
