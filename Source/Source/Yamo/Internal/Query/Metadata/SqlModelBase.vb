Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Base class for SQL related model data.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlModelBase

    ''' <summary>
    ''' Gets main SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MainEntity As EntityBasedSqlEntity

    ''' <summary>
    ''' Gets SQL entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected ReadOnly Property Entities As List(Of SqlEntityBase)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlModelBase"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="mainEntity"></param>
    Public Sub New(<DisallowNull> mainEntity As Entity)
      Me.Entities = New List(Of SqlEntityBase)

      Dim tableAlias = "T0"

      Me.MainEntity = New EntityBasedSqlEntity(mainEntity, tableAlias, 0)
      Me.Entities.Add(Me.MainEntity)
    End Sub

    ''' <summary>
    ''' Adds SQL entity used in the query.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    ''' <returns></returns>
    Protected Function AddEntity(<DisallowNull> entity As Entity, relationship As SqlEntityRelationship, isIgnored As Boolean) As EntityBasedSqlEntity
      Dim index = Me.Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)
      Dim sqlEntity = New EntityBasedSqlEntity(entity, tableAlias, index, relationship, isIgnored)

      Me.Entities.Add(sqlEntity)

      Return sqlEntity
    End Function

    ''' <summary>
    ''' Adds SQL entity used in the query.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    ''' <param name="creationBehavior"></param>
    ''' <returns></returns>
    Protected Function AddEntity(<DisallowNull> entity As NonModelEntity, relationship As SqlEntityRelationship, isIgnored As Boolean, creationBehavior As NonModelEntityCreationBehavior) As NonModelEntityBasedSqlEntity
      Dim index = Me.Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)
      Dim sqlEntity = New NonModelEntityBasedSqlEntity(entity, tableAlias, index, relationship, isIgnored, creationBehavior)

      Me.Entities.Add(sqlEntity)

      Return sqlEntity
    End Function

    ''' <summary>
    ''' Gets entity by its index.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function GetEntity(index As Int32) As SqlEntityBase
      Return Me.Entities(index)
    End Function

    ''' <summary>
    ''' Gets last entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastEntity() As SqlEntityBase
      Return Me.Entities.Last()
    End Function

    ''' <summary>
    ''' Gets all entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntities() As SqlEntityBase()
      Return Me.Entities.ToArray()
    End Function

    ''' <summary>
    ''' Gets entities count.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntityCount() As Int32
      Return Me.Entities.Count
    End Function

  End Class
End Namespace