Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class EntityRelationshipSetterCache

    Private Shared m_Instances As Dictionary(Of Model, EntityRelationshipSetterCache)

    Private m_Setters As Dictionary(Of (Type, String), Action(Of Object, Object))

    Private m_CollectionInitSetters As Dictionary(Of (Type, String), Action(Of Object))

    Shared Sub New()
      m_Instances = New Dictionary(Of Model, EntityRelationshipSetterCache)
    End Sub

    Private Sub New()
      m_Setters = New Dictionary(Of (Type, String), Action(Of Object, Object))
      m_CollectionInitSetters = New Dictionary(Of (Type, String), Action(Of Object))
    End Sub

    Public Shared Function GetSetter(model As Model, entityType As Type, relationshipNavigation As RelationshipNavigation) As Action(Of Object, Object)
      Return GetInstance(model).GetOrCreateSetter(model, entityType, relationshipNavigation)
    End Function

    Public Shared Function GetCollectionInitSetter(model As Model, entityType As Type, collectionNavigation As CollectionNavigation) As Action(Of Object)
      Return GetInstance(model).GetOrCreateCollectionInitSetter(model, entityType, collectionNavigation)
    End Function

    Private Shared Function GetInstance(model As Model) As EntityRelationshipSetterCache
      Dim instance As EntityRelationshipSetterCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New EntityRelationshipSetterCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    Private Function GetOrCreateSetter(model As Model, entityType As Type, relationshipNavigation As RelationshipNavigation) As Action(Of Object, Object)
      Dim setter As Action(Of Object, Object) = Nothing

      Dim key = (entityType, relationshipNavigation.PropertyName)

      SyncLock m_Setters
        m_Setters.TryGetValue(key, setter)
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

      Dim key = (entityType, collectionNavigation.PropertyName)

      SyncLock m_CollectionInitSetters
        m_CollectionInitSetters.TryGetValue(key, setter)
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