Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class EntityAutoFieldsGetterCache

    Private Shared m_Instances As Dictionary(Of Model, EntityAutoFieldsGetterCache)

    Private m_OnUpdateGetters As Dictionary(Of Type, Func(Of DbContext, Object()))

    Private m_OnDeleteGetters As Dictionary(Of Type, Func(Of DbContext, Object()))

    Shared Sub New()
      m_Instances = New Dictionary(Of Model, EntityAutoFieldsGetterCache)
    End Sub

    Private Sub New()
      m_OnUpdateGetters = New Dictionary(Of Type, Func(Of DbContext, Object()))
      m_OnDeleteGetters = New Dictionary(Of Type, Func(Of DbContext, Object()))
    End Sub

    Public Shared Function GetOnUpdateGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Return GetInstance(model).GetOrCreateOnUpdateGetter(model, entityType)
    End Function

    Public Shared Function GetOnDeleteGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Return GetInstance(model).GetOrCreateOnDeleteGetter(model, entityType)
    End Function

    Private Shared Function GetInstance(model As Model) As EntityAutoFieldsGetterCache
      Dim instance As EntityAutoFieldsGetterCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New EntityAutoFieldsGetterCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    Private Function GetOrCreateOnUpdateGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Dim getter As Func(Of DbContext, Object()) = Nothing

      SyncLock m_OnUpdateGetters
        m_OnUpdateGetters.TryGetValue(entityType, getter)
      End SyncLock

      If getter Is Nothing Then
        getter = EntityAutoFieldsGetterFactory.CreateOnUpdateGetter(model, entityType)
      Else
        Return getter
      End If

      SyncLock m_OnUpdateGetters
        m_OnUpdateGetters(entityType) = getter
      End SyncLock

      Return getter
    End Function

    Private Function GetOrCreateOnDeleteGetter(model As Model, entityType As Type) As Func(Of DbContext, Object())
      Dim getter As Func(Of DbContext, Object()) = Nothing

      SyncLock m_OnDeleteGetters
        m_OnDeleteGetters.TryGetValue(entityType, getter)
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