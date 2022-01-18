Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related model data for select statement.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SelectSqlModel
    Inherits SqlModelBase

    Private m_CustomSqlResult As SqlResultBase
    ''' <summary>
    ''' Gets or sets custom SQL result.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns>Custom SQL result or <see langword="Nothing"/> if the select is not custom.</returns>
    Public Property CustomSqlResult() As <MaybeNull> SqlResultBase
      Get
        Return m_CustomSqlResult
      End Get
      Set(<DisallowNull> ByVal value As SqlResultBase)
        m_CustomSqlResult = value
      End Set
    End Property

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="mainEntityType"></param>
    Public Sub New(<DisallowNull> model As Model, <DisallowNull> mainEntityType As Type)
      MyBase.New(model, mainEntityType)
    End Sub

    ''' <summary>
    ''' Adds joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function AddJoin(Of T)(Optional relationship As SqlEntityRelationship = Nothing) As SqlEntity
      Return AddEntity(GetType(T), relationship, False)
    End Function

    ''' <summary>
    ''' Adds ignored joined table.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Function AddIgnoredJoin(<DisallowNull> entityType As Type) As SqlEntity
      Return AddEntity(entityType, Nothing, True)
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