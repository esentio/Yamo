Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Entity auto fields getter cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityAutoFieldsGetterCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Model, EntityAutoFieldsGetterCache)

    ''' <summary>
    ''' Stores cached on update getter instances.<br/>
    ''' </summary>
    Private m_OnUpdateGetters As Dictionary(Of Type, Func(Of DbContext, Object()))

    ''' <summary>
    ''' Stores cached on soft delete getter instances.<br/>
    ''' </summary>
    Private m_OnDeleteGetters As Dictionary(Of Type, Func(Of DbContext, Object()))

    ''' <summary>
    ''' Initializes <see cref="EntityAutoFieldsGetterCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Model, EntityAutoFieldsGetterCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityAutoFieldsGetterCache"/>.
    ''' </summary>
    Private Sub New()
      m_OnUpdateGetters = New Dictionary(Of Type, Func(Of DbContext, Object()))
      m_OnDeleteGetters = New Dictionary(Of Type, Func(Of DbContext, Object()))
    End Sub

    ''' <summary>
    ''' Gets on update getter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function GetOnUpdateGetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type) As Func(Of DbContext, Object())
      Return GetInstance(model).GetOrCreateOnUpdateGetter(model, entityType)
    End Function

    ''' <summary>
    ''' Gets on soft delete getter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
    Public Shared Function GetOnDeleteGetter(<DisallowNull> model As Model, <DisallowNull> entityType As Type) As Func(Of DbContext, Object())
      Return GetInstance(model).GetOrCreateOnDeleteGetter(model, entityType)
    End Function

    ''' <summary>
    ''' Gets <see cref="EntityAutoFieldsGetterCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates on update getter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates on soft delete getter.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="entityType"></param>
    ''' <returns></returns>
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