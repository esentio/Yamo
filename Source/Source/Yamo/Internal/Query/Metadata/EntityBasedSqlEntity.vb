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
    ''' Gets column aliases.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Returns <see langword="Nothing"/> if aliases are not used.</returns>
    Public ReadOnly Property ColumnAliases As <MaybeNull> String()

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
    ''' Sets column aliases if they are used.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="aliases"></param>
    Public Sub SetColumnAliases(<DisallowNull> aliases As String())
      _ColumnAliases = aliases
    End Sub

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    Public Overrides Function GetColumnName(<DisallowNull> propertyName As String) As String
      If Me.ColumnAliases Is Nothing Then
        Return Me.Entity.GetProperty(propertyName).ColumnName
      Else
        Return Me.ColumnAliases(Me.Entity.GetProperty(propertyName).Index)
      End If
    End Function

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Overrides Function GetColumnName(index As Int32) As String
      If Me.ColumnAliases Is Nothing Then
        Return Me.Entity.GetProperty(index).ColumnName
      Else
        Return Me.ColumnAliases(index)
      End If
    End Function

  End Class
End Namespace