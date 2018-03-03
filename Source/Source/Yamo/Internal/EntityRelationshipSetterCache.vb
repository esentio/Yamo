Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class EntityRelationshipSetterCache

    Private Shared m_Instances As Dictionary(Of Int32, EntityRelationshipSetterCache)

    Private m_Setters As Dictionary(Of Int32, Action(Of Object, Object))

    Private m_CollectionInitSetters As Dictionary(Of Int32, Action(Of Object))

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, EntityRelationshipSetterCache)
    End Sub

    Private Sub New()
      m_Setters = New Dictionary(Of Int32, Action(Of Object, Object))
      m_CollectionInitSetters = New Dictionary(Of Int32, Action(Of Object))
    End Sub

    Public Shared Function GetSetter(model As Model, entityType As Type, relationshipNavigation As RelationshipNavigation) As Action(Of Object, Object)
      Return GetInstance(model).GetOrCreateSetter(model, entityType, relationshipNavigation)
    End Function

    Public Shared Function GetCollectionInitSetter(model As Model, entityType As Type, collectionNavigation As CollectionNavigation) As Action(Of Object)
      Return GetInstance(model).GetOrCreateCollectionInitSetter(model, entityType, collectionNavigation)
    End Function

    Private Shared Function GetInstance(model As Model) As EntityRelationshipSetterCache
      Dim instance As EntityRelationshipSetterCache
      Dim key = model.GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New EntityRelationshipSetterCache
          m_Instances = New Dictionary(Of Int32, EntityRelationshipSetterCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New EntityRelationshipSetterCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetOrCreateSetter(model As Model, entityType As Type, relationshipNavigation As RelationshipNavigation) As Action(Of Object, Object)
      Dim setter As Action(Of Object, Object) = Nothing

      ' TODO: use System.HashCode instead (when available in .NET)
      Dim key = (entityType, relationshipNavigation.PropertyName).GetHashCode()

      SyncLock m_Setters
        If m_Setters.ContainsKey(key) Then
          setter = m_Setters(key)
        End If
      End SyncLock

      If setter Is Nothing Then
        setter = EntityRelationshipSetterFactory.CreateSetter(model, entityType, relationshipNavigation)
      Else
        Return setter
      End If

      SyncLock m_Setters
        m_Setters(key) = setter
      End SyncLock

      Return setter
    End Function

    Private Function GetOrCreateCollectionInitSetter(model As Model, entityType As Type, collectionNavigation As CollectionNavigation) As Action(Of Object)
      Dim setter As Action(Of Object) = Nothing

      ' TODO: use System.HashCode instead (when available in .NET)
      Dim key = (entityType, collectionNavigation.PropertyName).GetHashCode()

      SyncLock m_CollectionInitSetters
        If m_CollectionInitSetters.ContainsKey(key) Then
          setter = m_CollectionInitSetters(key)
        End If
      End SyncLock

      If setter Is Nothing Then
        setter = EntityRelationshipSetterFactory.CreateCollectionInitSetter(model, entityType, collectionNavigation)
      Else
        Return setter
      End If

      SyncLock m_CollectionInitSetters
        m_CollectionInitSetters(key) = setter
      End SyncLock

      Return setter
    End Function

  End Class
End Namespace