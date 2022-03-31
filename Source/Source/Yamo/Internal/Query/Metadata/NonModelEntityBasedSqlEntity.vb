Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related entity data that are based on non model entity.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class NonModelEntityBasedSqlEntity
    Inherits SqlEntityBase

    ''' <summary>
    ''' Gets entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Entity As NonModelEntity

    ''' <summary>
    ''' Gets entity creation behavior.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CreationBehavior As NonModelEntityCreationBehavior

    ''' <summary>
    ''' Creates new instance of <see cref="EntityBasedSqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    ''' <param name="relationship"></param>
    ''' <param name="isIgnored"></param>
    ''' <param name="creationBehavior"></param>
    Sub New(<DisallowNull> entity As NonModelEntity, <DisallowNull> tableAlias As String, index As Int32, relationship As SqlEntityRelationship, isIgnored As Boolean, creationBehavior As NonModelEntityCreationBehavior)
      MyBase.New(entity.EntityType, tableAlias, index, entity.GetColumnsCount(), relationship, isIgnored)
      Me.Entity = entity
      Me.CreationBehavior = creationBehavior
    End Sub

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    Public Overrides Function GetColumnName(<DisallowNull> propertyName As String) As String
      Return Me.Entity.GetColumnName(propertyName)
    End Function

    ''' <summary>
    ''' Gets column name.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Overrides Function GetColumnName(index As Int32) As String
      Return Me.Entity.GetColumnName(index)
    End Function

  End Class
End Namespace