Namespace Metadata

  ''' <summary>
  ''' Metadata about entities, relationships between them and their mapping to the database.
  ''' </summary>
  Public Class Model

    ''' <summary>
    ''' Stores entities by their type.
    ''' </summary>
    Private m_Entities As Dictionary(Of Type, Entity)

    ''' <summary>
    ''' Creates new instance of <see cref="Model"/>.
    ''' </summary>
    Sub New()
      m_Entities = New Dictionary(Of Type, Entity)
    End Sub

    ''' <summary>
    ''' Adds entity to the model.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Friend Function AddEntity(entityType As Type) As Entity
      Dim entity As Entity = Nothing

      If Not m_Entities.TryGetValue(entityType, entity) Then
        entity = New Entity(entityType)
        m_Entities.Add(entityType, entity)
      End If

      Return entity
    End Function

    ''' <summary>
    ''' Returns an entity of defined type.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Function GetEntity(entityType As Type) As Entity
      Dim entity As Entity = Nothing

      If m_Entities.TryGetValue(entityType, entity) Then
        Return entity
      Else
        Throw New Exception($"Entity '{entityType}' is not defined in the model.")
      End If
    End Function

    ''' <summary>
    ''' Returns an entity of defined type or <see langword="Nothing"/>.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Function TryGetEntity(entityType As Type) As Entity
      Dim entity As Entity = Nothing
      m_Entities.TryGetValue(entityType, entity)
      Return entity
    End Function

  End Class
End Namespace