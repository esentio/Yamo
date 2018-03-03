Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class EntityAutoFieldsGetterCache

    Private Shared m_Instances As Dictionary(Of Int32, EntityAutoFieldsGetterCache)

    Private m_OnDeleteGetters As Dictionary(Of Type, Func(Of DbContext, Object()))

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, EntityAutoFieldsGetterCache)
    End Sub

    Private Sub New()
      m_OnDeleteGetters = New Dictionary(Of Type, Func(Of DbContext, Object()))
    End Sub

    Public Shared Function GetOnDeleteGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Return GetInstance(model).GetOrCreateOnDeleteGetter(model, entityType)
    End Function

    Private Shared Function GetInstance(model As Model) As EntityAutoFieldsGetterCache
      Dim instance As EntityAutoFieldsGetterCache
      Dim key = model.GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New EntityAutoFieldsGetterCache
          m_Instances = New Dictionary(Of Int32, EntityAutoFieldsGetterCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New EntityAutoFieldsGetterCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetOrCreateOnDeleteGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Dim getter As Func(Of DbContext, Object()) = Nothing

      SyncLock m_OnDeleteGetters
        If m_OnDeleteGetters.ContainsKey(entityType) Then
          getter = m_OnDeleteGetters(entityType)
        End If
      End SyncLock

      If getter Is Nothing Then
        getter = EntityAutoFieldsGetterFactory.CreateOnDeleteGetter(model, entityType)
      Else
        Return getter
      End If

      SyncLock m_OnDeleteGetters
        m_OnDeleteGetters(entityType) = getter
      End SyncLock

      Return getter
    End Function

  End Class
End Namespace