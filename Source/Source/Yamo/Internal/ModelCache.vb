Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Model cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ModelCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Type, ModelCache)

    ''' <summary>
    ''' Stores cached model instances.
    ''' </summary>
    Private m_Models As Dictionary(Of Type, Model)

    ''' <summary>
    ''' Initializes <see cref="ModelCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Type, ModelCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="ModelCache"/>.
    ''' </summary>
    Private Sub New()
      m_Models = New Dictionary(Of Type, Model)
    End Sub

    ''' <summary>
    ''' Gets model.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Public Shared Function GetModel(dialectProvider As SqlDialectProvider, context As DbContext) As Model
      Return GetInstance(dialectProvider).GetOrCreateModel(context)
    End Function

    ''' <summary>
    ''' Gets <see cref="ModelCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Gets or creates model.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Private Function GetOrCreateModel(context As DbContext) As Model
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