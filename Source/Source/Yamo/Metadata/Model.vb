Namespace Metadata

  Public Class Model

    Private m_Entities As Dictionary(Of Type, Entity)

    Sub New()
      m_Entities = New Dictionary(Of Type, Entity)
    End Sub

    Friend Function AddEntity(entityType As Type) As Entity
      If m_Entities.ContainsKey(entityType) Then
        Return m_Entities(entityType)
      Else
        Dim entity = New Entity(entityType)
        m_Entities.Add(entityType, entity)
        Return entity
      End If
    End Function

    Public Function GetEntity(entityType As Type) As Entity
      If m_Entities.ContainsKey(entityType) Then
        Return m_Entities(entityType)
      Else
        Throw New Exception($"Entity '{entityType}' is not defined in the model.")
      End If
    End Function

  End Class
End Namespace