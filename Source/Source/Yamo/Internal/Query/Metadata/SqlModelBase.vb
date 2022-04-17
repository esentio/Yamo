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
    Public ReadOnly Property MainEntity As <MaybeNull> SqlEntityBase

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
    Sub New()
      Me.MainEntity = Nothing
      Me.Entities = New List(Of SqlEntityBase)
    End Sub

    ''' <summary>
    ''' Sets main SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="mainEntity"></param>
    ''' <param name="tableSourceIsSubquery"></param>
    ''' <returns></returns>
    Public Function SetMainEntity(<DisallowNull> mainEntity As Entity, tableSourceIsSubquery As Boolean) As EntityBasedSqlEntity
      If Me.MainEntity IsNot Nothing Then
        Throw New InvalidOperationException("Main entity is already set.")
      End If

      Dim tableAlias = "T0"
      Dim sqlEntity = New EntityBasedSqlEntity(mainEntity, tableSourceIsSubquery, tableAlias, 0)

      _MainEntity = sqlEntity
      Me.Entities.Add(Me.MainEntity)

      Return sqlEntity
    End Function

    ''' <summary>
    ''' Sets main SQL entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="mainEntity"></param>
    ''' <returns></returns>
    Public Function SetMainEntity(<DisallowNull> mainEntity As NonModelEntity) As NonModelEntityBasedSqlEntity
      If Me.MainEntity IsNot Nothing Then
        Throw New InvalidOperationException("Main entity is already set.")
      End If

      Dim tableAlias = "T0"
      Dim sqlEntity = New NonModelEntityBasedSqlEntity(mainEntity, tableAlias, 0, Nothing, False)

      _MainEntity = sqlEntity
      Me.Entities.Add(Me.MainEntity)

      Return sqlEntity
    End Function

    ''' <summary>
    ''' Adds SQL entity used in the query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableSourceIsSubquery"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    ''' <returns></returns>
    Protected Function AddEntity(<DisallowNull> entity As Entity, tableSourceIsSubquery As Boolean, relationship As SqlEntityRelationship, isIgnored As Boolean) As EntityBasedSqlEntity
      Dim index = Me.Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)
      Dim sqlEntity = New EntityBasedSqlEntity(entity, tableSourceIsSubquery, tableAlias, index, relationship, isIgnored)

      Me.Entities.Add(sqlEntity)

      Return sqlEntity
    End Function

    ''' <summary>
    ''' Adds SQL entity used in the query.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    ''' <returns></returns>
    Protected Function AddEntity(<DisallowNull> entity As NonModelEntity, relationship As SqlEntityRelationship, isIgnored As Boolean) As NonModelEntityBasedSqlEntity
      Dim index = Me.Entities.Count
      Dim tableAlias = "T" & index.ToString(Globalization.CultureInfo.InvariantCulture)
      Dim sqlEntity = New NonModelEntityBasedSqlEntity(entity, tableAlias, index, relationship, isIgnored)

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