Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Post processor cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class PostProcessorCache

    ''' <summary>
    ''' Stores cache instances.<br/>
    ''' <br/>
    ''' Key: <see cref="Model"/> instance.<br/>
    ''' Value: <see cref="PostProcessorCache"/> instance.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Model, PostProcessorCache)

    ''' <summary>
    ''' Stores cached post processor instances.<br/>
    ''' <br/>
    ''' Key: type corresponding to <see cref="SqlResultBase.ResultType"/>.<br/>
    ''' Value: <see cref="Action(Of Object)"/> delegate, where parameter is object instance.
    ''' </summary>
    Private m_PostProcessors As Dictionary(Of Type, Action(Of Object))

    ''' <summary>
    ''' Stores cached post processor instances for ad hoc types.<br/>
    ''' <br/>
    ''' Key: <see cref="AdHocTypeSqlResultReaderKey"/> value.<br/>
    ''' Value: <see cref="Action(Of Object)"/> delegate, where parameter is object instance.
    ''' </summary>
    Private m_AdHocTypePostProcessors As Dictionary(Of AdHocTypeSqlResultReaderKey, Action(Of Object))

    ''' <summary>
    ''' Initializes <see cref="PostProcessorCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Model, PostProcessorCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="PostProcessorCache"/>.
    ''' </summary>
    Private Sub New()
      m_PostProcessors = New Dictionary(Of Type, Action(Of Object))
      m_AdHocTypePostProcessors = New Dictionary(Of AdHocTypeSqlResultReaderKey, Action(Of Object))
    End Sub

    ''' <summary>
    ''' Gets post processor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function GetPostProcessor(<DisallowNull> model As Model, <DisallowNull> sqlResult As SqlResultBase) As Action(Of Object)
      Return GetInstance(model).GetOrCreatePostProcessor(sqlResult)
    End Function

    ''' <summary>
    ''' Gets <see cref="PostProcessorCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(model As Model) As PostProcessorCache
      Dim instance As PostProcessorCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New PostProcessorCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates post processor.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Function GetOrCreatePostProcessor(sqlResult As SqlResultBase) As Action(Of Object)
      If TypeOf sqlResult Is AdHocTypeSqlResult Then
        Return GetOrCreatePostProcessorForAdHocTypeSqlResult(DirectCast(sqlResult, AdHocTypeSqlResult))
      Else
        Return GetOrCreatePostProcessorForSqlResult(sqlResult)
      End If
    End Function

    ''' <summary>
    ''' Gets or creates post processor.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Function GetOrCreatePostProcessorForAdHocTypeSqlResult(sqlResult As AdHocTypeSqlResult) As Action(Of Object)
      Dim postProcessor As Action(Of Object) = Nothing
      Dim key = sqlResult.GetKey()
      Dim cached = False

      SyncLock m_AdHocTypePostProcessors
        If m_AdHocTypePostProcessors.TryGetValue(key, postProcessor) Then
          cached = True
        End If
      End SyncLock

      If cached Then
        Return postProcessor
      Else
        postProcessor = PostProcessorFactory.TryCreatePostProcessor(sqlResult)
      End If

      SyncLock m_AdHocTypePostProcessors
        m_AdHocTypePostProcessors(key) = postProcessor
      End SyncLock

      Return postProcessor
    End Function

    ''' <summary>
    ''' Gets or creates post processor.
    ''' </summary>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Function GetOrCreatePostProcessorForSqlResult(sqlResult As SqlResultBase) As Action(Of Object)
      Dim postProcessor As Action(Of Object) = Nothing
      Dim resultType = sqlResult.ResultType
      Dim cached = False

      SyncLock m_PostProcessors
        If m_PostProcessors.TryGetValue(resultType, postProcessor) Then
          cached = True
        End If
      End SyncLock

      If cached Then
        Return postProcessor
      Else
        postProcessor = PostProcessorFactory.TryCreatePostProcessor(sqlResult)
      End If

      SyncLock m_PostProcessors
        m_PostProcessors(resultType) = postProcessor
      End SyncLock

      Return postProcessor
    End Function

  End Class
End Namespace