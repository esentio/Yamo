Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related entity data that are based on entity model.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityBasedSqlEntity
    Inherits SqlEntityBase

    ''' <summary>
    ''' Gets entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As Entity

    ''' <summary>
    ''' Creates new instance of <see cref="EntityBasedSqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    Sub New(<DisallowNull> entity As Entity)
      Me.New(entity, "", -1)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityBasedSqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    Sub New(<DisallowNull> entity As Entity, <DisallowNull> tableAlias As String, index As Int32)
      MyBase.New(entity.EntityType, tableAlias, index, entity.GetPropertiesCount())
      Me.Entity = entity
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityBasedSqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    Sub New(<DisallowNull> entity As Entity, <DisallowNull> tableAlias As String, index As Int32, relationship As SqlEntityRelationship, isIgnored As Boolean)
      MyBase.New(entity.EntityType, tableAlias, index, entity.GetPropertiesCount(), relationship, isIgnored)
      Me.Entity = entity
    End Sub

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    Public Overrides Function GetColumnName(<DisallowNull> propertyName As String) As String
      Return Me.Entity.GetProperty(propertyName).ColumnName
    End Function

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Overrides Function GetColumnName(index As Int32) As String
      Return Me.Entity.GetProperty(index).ColumnName
    End Function

  End Class
End Namespace