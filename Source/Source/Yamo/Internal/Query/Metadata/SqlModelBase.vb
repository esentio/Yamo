Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Base class for SQL related model data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlModelBase

    ''' <summary>
    ''' Gets model.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Model As Model

    ''' <summary>
    ''' Gets main SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MainEntity As SqlEntity
      Get
        Return Me.Entities(0)
      End Get
    End Property

    ''' <summary>
    ''' Gets SQL entities.
    ''' </summary>
    ''' <returns></returns>
    Protected ReadOnly Property Entities As List(Of SqlEntity)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlModelBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="mainEntityType"></param>
    Public Sub New(model As Model, mainEntityType As Type)
      Me.Model = model
      Me.Entities = New List(Of SqlEntity)

      Dim entity = Me.Model.GetEntity(mainEntityType)
      Dim tableAlias = "T0"

      Me.Entities.Add(New SqlEntity(entity, tableAlias, 0))
    End Sub

    ''' <summary>
    ''' Adds SQL entity used in the query.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    ''' <returns></returns>
    Protected Function AddEntity(entityType As Type, relationship As SqlEntityRelationship, isIgnored As Boolean) As SqlEntity
      Dim entity = Me.Model.GetEntity(entityType)
      Dim index = Me.Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)
      Dim sqlEntity = New SqlEntity(entity, tableAlias, index, relationship, isIgnored)

      Me.Entities.Add(sqlEntity)

      Return sqlEntity
    End Function

    ''' <summary>
    ''' Gets entity by its index.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function GetEntity(index As Int32) As SqlEntity
      Return Me.Entities(index)
    End Function

    ''' <summary>
    ''' Gets last entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastEntity() As SqlEntity
      Return Me.Entities.Last()
    End Function

    ''' <summary>
    ''' Gets all entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntities() As SqlEntity()
      Return Me.Entities.ToArray()
    End Function

    ''' <summary>
    ''' Get entities count.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntityCount() As Int32
      Return Me.Entities.Count
    End Function

  End Class
End Namespace