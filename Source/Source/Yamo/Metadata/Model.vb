Namespace Metadata

  Public Class Model

    Private m_Entities As Dictionary(Of Type, Entity)

    Sub New()
      m_Entities = New Dictionary(Of Type, Entity)
    End Sub

    Friend Function AddEntity(entityType As Type) As Entity
      Dim entity As Entity = Nothing

      If Not m_Entities.TryGetValue(entityType, entity) Then
        entity = New Entity(entityType)
        m_Entities.Add(entityType, entity)
      End If

      Return entity
    End Function

    Public Function GetEntity(entityType As Type) As Entity
      Dim entity As Entity = Nothing

      If m_Entities.TryGetValue(entityType, entity) Then
        Return entity
      Else
        Throw New Exception($"Entity '{entityType}' is not defined in the model.")
      End If
    End Function

    Public Function TryGetEntity(entityType As Type) As Entity
      Dim entity As Entity = Nothing
      m_Entities.TryGetValue(entityType, entity)
      Return entity
    End Function

  End Class
End Namespace