Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related model data for select statement.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SelectSqlModel
    Inherits SqlModelBase

    Private m_SqlResult As SqlResultBase
    ''' <summary>
    ''' Gets or sets SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property SqlResult() As <MaybeNull> SqlResultBase
      Get
        Return m_SqlResult
      End Get
      Set(<DisallowNull> ByVal value As SqlResultBase)
        m_SqlResult = value
      End Set
    End Property

    Private m_NonModelEntity As NonModelEntity
    ''' <summary>
    ''' Gets or sets ad hoc non model entity.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Non model entity or <see langword="Nothing"/> if the select is not custom or this is not a subquery.</returns>
    Public Property NonModelEntity() As <MaybeNull> NonModelEntity
      Get
        Return m_NonModelEntity
      End Get
      Set(ByVal value As NonModelEntity)
        m_NonModelEntity = value
      End Set
    End Property

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="mainEntity"></param>
    Public Sub New(<DisallowNull> model As Model, <DisallowNull> mainEntity As Entity)
      MyBase.New(model, mainEntity)
    End Sub

    ''' <summary>
    ''' Adds joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function AddJoin(<DisallowNull> entity As Entity, Optional relationship As SqlEntityRelationship = Nothing) As SqlEntityBase
      Return AddEntity(entity, relationship, False)
    End Function

    ''' <summary>
    ''' Adds joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function AddJoin(<DisallowNull> entity As NonModelEntity, Optional relationship As SqlEntityRelationship = Nothing) As SqlEntityBase
      Return AddEntity(entity, relationship, False)
    End Function

    ''' <summary>
    ''' Adds ignored joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    Public Function AddIgnoredJoin(<DisallowNull> entity As Entity) As SqlEntityBase
      Return AddEntity(entity, Nothing, True)
    End Function

    ''' <summary>
    ''' Adds ignored joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    Public Function AddIgnoredJoin(<DisallowNull> entity As NonModelEntity) As SqlEntityBase
      Return AddEntity(entity, Nothing, True)
    End Function

    ''' <summary>
    ''' Checks whether join is used.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Function ContainsJoins() As Boolean
      Return 1 < Me.Entities.Count
    End Function

  End Class
End Namespace