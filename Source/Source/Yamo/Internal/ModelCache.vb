Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class ModelCache

    Private Shared m_Instances As Dictionary(Of Type, ModelCache)

    Private m_Models As Dictionary(Of Type, Model)

    Shared Sub New()
      m_Instances = New Dictionary(Of Type, ModelCache)
    End Sub

    Private Sub New()
      m_Models = New Dictionary(Of Type, Model)
    End Sub

    Public Shared Function GetModel(dialectProvider As SqlDialectProvider, context As DbContext) As Model
      Return GetInstance(dialectProvider).GetOrCreateReader(context)
    End Function

    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider) As ModelCache
      Dim instance As ModelCache = Nothing

      Dim key = dialectProvider.GetType()

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(key, instance) Then
          instance = New ModelCache
          m_Instances.Add(key, instance)
        End If
      End SyncLock

      Return instance
    End Function

    Private Function GetOrCreateReader(context As DbContext) As Model
      Dim model As Model = Nothing
      Dim key = context.GetType()

      SyncLock m_Models
        m_Models.TryGetValue(key, model)
      End SyncLock

      If model Is Nothing Then
        model = context.CreateModel()
      Else
        Return model
      End If

      SyncLock m_Models
        m_Models(key) = model
      End SyncLock

      Return model
    End Function

  End Class
End Namespace