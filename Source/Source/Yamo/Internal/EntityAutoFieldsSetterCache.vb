Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class EntityAutoFieldsSetterCache

    Private Shared m_Instances As Dictionary(Of Int32, EntityAutoFieldsSetterCache)

    Private m_OnInsertSetters As Dictionary(Of Type, Action(Of Object, DbContext))

    Private m_OnUpdateSetters As Dictionary(Of Type, Action(Of Object, DbContext))

    Private m_OnDeleteSetters As Dictionary(Of Type, Action(Of Object, DbContext))

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, EntityAutoFieldsSetterCache)
    End Sub

    Private Sub New()
      m_OnInsertSetters = New Dictionary(Of Type, Action(Of Object, DbContext))
      m_OnUpdateSetters = New Dictionary(Of Type, Action(Of Object, DbContext))
      m_OnDeleteSetters = New Dictionary(Of Type, Action(Of Object, DbContext))
    End Sub

    Public Shared Function GetOnInsertSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Return GetInstance(model).GetOrCreateOnInsertSetter(model, entityType)
    End Function

    Public Shared Function GetOnUpdateSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Return GetInstance(model).GetOrCreateOnUpdateSetter(model, entityType)
    End Function

    Public Shared Function GetOnDeleteSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Return GetInstance(model).GetOrCreateOnDeleteSetter(model, entityType)
    End Function

    Private Shared Function GetInstance(model As Model) As EntityAutoFieldsSetterCache
      Dim instance As EntityAutoFieldsSetterCache
      Dim key = model.GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New EntityAutoFieldsSetterCache
          m_Instances = New Dictionary(Of Int32, EntityAutoFieldsSetterCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New EntityAutoFieldsSetterCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetOrCreateOnInsertSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim setter As Action(Of Object, DbContext) = Nothing

      SyncLock m_OnInsertSetters
        If m_OnInsertSetters.ContainsKey(entityType) Then
          setter = m_OnInsertSetters(entityType)
        End If
      End SyncLock

      If setter Is Nothing Then
        setter = EntityAutoFieldsSetterFactory.CreateOnInsertSetter(model, entityType)
      Else
        Return setter
      End If

      SyncLock m_OnInsertSetters
        m_OnInsertSetters(entityType) = setter
      End SyncLock

      Return setter
    End Function

    Private Function GetOrCreateOnUpdateSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim setter As Action(Of Object, DbContext) = Nothing

      SyncLock m_OnUpdateSetters
        If m_OnUpdateSetters.ContainsKey(entityType) Then
          setter = m_OnUpdateSetters(entityType)
        End If
      End SyncLock

      If setter Is Nothing Then
        setter = EntityAutoFieldsSetterFactory.CreateOnUpdateSetter(model, entityType)
      Else
        Return setter
      End If

      SyncLock m_OnUpdateSetters
        m_OnUpdateSetters(entityType) = setter
      End SyncLock

      Return setter
    End Function

    Private Function GetOrCreateOnDeleteSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim setter As Action(Of Object, DbContext) = Nothing

      SyncLock m_OnDeleteSetters
        If m_OnDeleteSetters.ContainsKey(entityType) Then
          setter = m_OnDeleteSetters(entityType)
        End If
      End SyncLock

      If setter Is Nothing Then
        setter = EntityAutoFieldsSetterFactory.CreateOnDeleteSetter(model, entityType)
      Else
        Return setter
      End If

      SyncLock m_OnDeleteSetters
        m_OnDeleteSetters(entityType) = setter
      End SyncLock

      Return setter
    End Function

  End Class
End Namespace