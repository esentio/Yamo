Imports System.Data
Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Entity member setter cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityMemberSetterCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Model, EntityMemberSetterCache)

    ''' <summary>
    ''' Stores cached setter instances.
    ''' </summary>
    Private m_Setters As Dictionary(Of (Type, String), Action(Of Object, Object))

    ''' <summary>
    ''' Stores cached collection add setter instances.
    ''' </summary>
    Private m_CollectionAddSetters As Dictionary(Of (Type, String), Action(Of Object, Object))

    ''' <summary>
    ''' Stores cached collection init setter instances.
    ''' </summary>
    Private m_CollectionInitSetters As Dictionary(Of (Type, String), Action(Of Object))

    ''' <summary>
    ''' Initializes <see cref="EntityMemberSetterCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Model, EntityMemberSetterCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityMemberSetterCache"/>.
    ''' </summary>
    Private Sub New()
      m_Setters = New Dictionary(Of (Type, String), Action(Of Object, Object))
      m_CollectionAddSetters = New Dictionary(Of (Type, String), Action(Of Object, Object))
      m_CollectionInitSetters = New Dictionary(Of (Type, String), Action(Of Object))
    End Sub

    ''' <summary>
    ''' Gets setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="relationshipNavigation"></param>
    ''' <returns></returns>
    Public Shared Function GetSetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type, <DisallowNull> relationshipNavigation As RelationshipNavigation) As Action(Of Object, Object)
      If TypeOf relationshipNavigation Is ReferenceNavigation Then
        Return GetInstance(model).GetOrCreateSetter(entityType, relationshipNavigation.PropertyName, relationshipNavigation.RelatedEntityType)
      ElseIf TypeOf relationshipNavigation Is CollectionNavigation Then
        Return GetInstance(model).GetOrCreateCollectionAddSetter(entityType, relationshipNavigation.PropertyName, relationshipNavigation.RelatedEntityType)
      Else
        Throw New NotSupportedException($"Relationship of type '{relationshipNavigation.GetType()}' is not supported.")
      End If
    End Function

    ''' <summary>
    ''' Gets setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="valueType"></param>
    ''' <returns></returns>
    Public Shared Function GetSetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type, <DisallowNull> propertyOrFieldName As String, <DisallowNull> valueType As Type) As Action(Of Object, Object)
      Return GetInstance(model).GetOrCreateSetter(entityType, propertyOrFieldName, valueType)
    End Function

    ''' <summary>
    ''' Gets collection init setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <param name="collectionNavigation"></param>
    ''' <returns></returns>
    Public Shared Function GetCollectionInitSetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type, <DisallowNull> collectionNavigation As CollectionNavigation) As Action(Of Object)
      Return GetInstance(model).GetOrCreateCollectionInitSetter(entityType, collectionNavigation.PropertyName, collectionNavigation.CollectionType, collectionNavigation.RelatedEntityType)
    End Function

    ''' <summary>
    ''' Gets <see cref="EntityMemberSetterCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(model As Model) As EntityMemberSetterCache
      Dim instance As EntityMemberSetterCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New EntityMemberSetterCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates setter.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="valueType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateSetter(entityType As Type, propertyOrFieldName As String, valueType As Type) As Action(Of Object, Object)
      Dim setter As Action(Of Object, Object) = Nothing

      Dim key = (entityType, propertyOrFieldName)

      SyncLock m_Setters
        m_Setters.TryGetValue(key, setter)
      End SyncLock

      If setter Is Nothing Then
        setter = EntityMemberSetterFactory.CreateSetter(entityType, propertyOrFieldName, valueType)
      Else
        Return setter
      End If

      SyncLock m_Setters
        m_Setters(key) = setter
      End SyncLock

      Return setter
    End Function

    ''' <summary>
    ''' Gets or creates collection add setter.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateCollectionAddSetter(entityType As Type, propertyOrFieldName As String, itemType As Type) As Action(Of Object, Object)
      Dim setter As Action(Of Object, Object) = Nothing

      Dim key = (entityType, propertyOrFieldName)

      SyncLock m_CollectionAddSetters
        m_CollectionAddSetters.TryGetValue(key, setter)
      End SyncLock

      If setter Is Nothing Then
        setter = EntityMemberSetterFactory.CreateCollectionAddSetter(entityType, propertyOrFieldName, itemType)
      Else
        Return setter
      End If

      SyncLock m_CollectionAddSetters
        m_CollectionAddSetters(key) = setter
      End SyncLock

      Return setter
    End Function

    ''' <summary>
    ''' Gets or creates collection init setter.
    ''' </summary>
    ''' <param name="entityType"></param>
    ''' <param name="propertyOrFieldName"></param>
    ''' <param name="collectionType"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateCollectionInitSetter(entityType As Type, propertyOrFieldName As String, collectionType As Type, itemType As Type) As Action(Of Object)
      Dim setter As Action(Of Object) = Nothing

      Dim key = (entityType, propertyOrFieldName)

      SyncLock m_CollectionInitSetters
        m_CollectionInitSetters.TryGetValue(key, setter)
      End SyncLock

      If setter Is Nothing Then
        setter = EntityMemberSetterFactory.CreateCollectionInitSetter(entityType, propertyOrFieldName, collectionType, itemType)
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