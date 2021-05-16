Imports Yamo.Metadata

Namespace Internal.Query.Metadata

  ''' <summary>
  ''' Represents SQL related model data for select statement.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SelectSqlModel
    Inherits SqlModelBase

    ''' <summary>
    ''' Gets or sets custom SQL result.<br/>
    ''' Contains <see langword="Nothing"/> if the select is not custom.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property CustomSqlResult As SqlResultBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlModel"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="mainEntityType"></param>
    Public Sub New(model As Model, mainEntityType As Type)
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
    Public Function AddIgnoredJoin(entityType As Type) As SqlEntity
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