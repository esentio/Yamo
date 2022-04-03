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
    ''' Creates new instance of <see cref="EntityBasedSqlEntity"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="tableAlias"></param>
    ''' <param name="index"></param>
    Sub New(<DisallowNull> entity As NonModelEntity, <DisallowNull> tableAlias As String, index As Int32)
      MyBase.New(entity.EntityType, tableAlias, index, entity.GetColumnsCount())
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
    Sub New(<DisallowNull> entity As NonModelEntity, <DisallowNull> tableAlias As String, index As Int32, relationship As SqlEntityRelationship, isIgnored As Boolean)
      MyBase.New(entity.EntityType, tableAlias, index, entity.GetColumnsCount(), relationship, isIgnored)
      Me.Entity = entity
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
    ''' Gets column names.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function GetColumnNames() As List(Of String)
      Return Me.Entity.GetColumnNames()
    End Function

  End Class
End Namespace