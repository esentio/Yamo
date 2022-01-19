Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Entity auto fields setter cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityAutoFieldsSetterCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Model, EntityAutoFieldsSetterCache)

    ''' <summary>
    ''' Stores cached oninsert setter instances.
    ''' </summary>
    Private m_OnInsertSetters As Dictionary(Of Type, Action(Of Object, DbContext))

    ''' <summary>
    ''' Stores cached on update setter instances.
    ''' </summary>
    Private m_OnUpdateSetters As Dictionary(Of Type, Action(Of Object, DbContext))

    ''' <summary>
    ''' Stores cached on soft delete setter instances.
    ''' </summary>
    Private m_OnDeleteSetters As Dictionary(Of Type, Action(Of Object, DbContext))

    ''' <summary>
    ''' Initializes <see cref="EntityAutoFieldsSetterCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Model, EntityAutoFieldsSetterCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityAutoFieldsSetterCache"/>.
    ''' </summary>
    Private Sub New()
      m_OnInsertSetters = New Dictionary(Of Type, Action(Of Object, DbContext))
      m_OnUpdateSetters = New Dictionary(Of Type, Action(Of Object, DbContext))
      m_OnDeleteSetters = New Dictionary(Of Type, Action(Of Object, DbContext))
    End Sub

    ''' <summary>
    ''' Gets on insert setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function GetOnInsertSetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type) As Action(Of Object, DbContext)
      Return GetInstance(model).GetOrCreateOnInsertSetter(model, entityType)
    End Function

    ''' <summary>
    ''' Gets on update setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function GetOnUpdateSetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type) As Action(Of Object, DbContext)
      Return GetInstance(model).GetOrCreateOnUpdateSetter(model, entityType)
    End Function

    ''' <summary>
    ''' Gets on soft delete setter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function GetOnDeleteSetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type) As Action(Of Object, DbContext)
      Return GetInstance(model).GetOrCreateOnDeleteSetter(model, entityType)
    End Function

    ''' <summary>
    ''' Gets <see cref="EntityAutoFieldsSetterCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(model As Model) As EntityAutoFieldsSetterCache
      Dim instance As EntityAutoFieldsSetterCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New EntityAutoFieldsSetterCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates on insert setter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateOnInsertSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim setter As Action(Of Object, DbContext) = Nothing

      SyncLock m_OnInsertSetters
        m_OnInsertSetters.TryGetValue(entityType, setter)
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

    ''' <summary>
    ''' Gets or creates on update setter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateOnUpdateSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim setter As Action(Of Object, DbContext) = Nothing

      SyncLock m_OnUpdateSetters
        m_OnUpdateSetters.TryGetValue(entityType, setter)
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

    ''' <summary>
    ''' Gets or creates on delete setter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateOnDeleteSetter(model As Model, entityType As Type) As Action(Of Object, DbContext)
      Dim setter As Action(Of Object, DbContext) = Nothing

      SyncLock m_OnDeleteSetters
        m_OnDeleteSetters.TryGetValue(entityType, setter)
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