Imports System.Data
Imports System.Linq.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Custom result reader cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class CustomResultReaderCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Model, CustomResultReaderCache)

    ''' <summary>
    ''' Stores result factory instances.<br/>
    ''' Instance type is actually Func(Of IDataReader, CustomEntityReadInfo(), T).
    ''' </summary>
    Private m_ResultFactories As Dictionary(Of Type, Object)

    ''' <summary>
    ''' Initializes <see cref="CustomResultReaderCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Model, CustomResultReaderCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="CustomResultReaderCache"/>.
    ''' </summary>
    Private Sub New()
      m_ResultFactories = New Dictionary(Of Type, Object)
    End Sub

    ''' <summary>
    ''' Get result factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="model"></param>
    ''' <param name="resultType"></param>
    ''' <returns></returns>
    Public Shared Function GetResultFactory(Of T)(model As Model, resultType As Type) As Func(Of IDataReader, CustomEntityReadInfo(), T)
      Return GetInstance(model).GetOrCreateResultFactory(Of T)(model, resultType)
    End Function

    ''' <summary>
    ''' Create result factory if it does not exist.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="node"></param>
    ''' <param name="customEntities"></param>
    Public Shared Sub CreateResultFactoryIfNotExists(model As Model, node As Expression, customEntities As CustomSqlEntity())
      GetInstance(model).CreateResultFactory(model, node, customEntities)
    End Sub

    ''' <summary>
    ''' Gets <see cref="CustomResultReaderCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(model As Model) As CustomResultReaderCache
      Dim instance As CustomResultReaderCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New CustomResultReaderCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates result factory.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="model"></param>
    ''' <param name="resultType"></param>
    ''' <returns></returns>
    Private Function GetOrCreateResultFactory(Of T)(model As Model, resultType As Type) As Func(Of IDataReader, CustomEntityReadInfo(), T)
      Dim resultFactory As Func(Of IDataReader, CustomEntityReadInfo(), T) = Nothing

      SyncLock m_ResultFactories
        Dim value As Object = Nothing

        If m_ResultFactories.TryGetValue(resultType, value) Then
          resultFactory = DirectCast(value, Func(Of IDataReader, CustomEntityReadInfo(), T))
        End If
      End SyncLock

      If resultFactory Is Nothing Then
        resultFactory = DirectCast(CustomResultReaderFactory.CreateResultFactory(resultType), Func(Of IDataReader, CustomEntityReadInfo(), T))
      Else
        Return resultFactory
      End If

      SyncLock m_ResultFactories
        m_ResultFactories(resultType) = resultFactory
      End SyncLock

      Return resultFactory
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="node"></param>
    ''' <param name="customEntities"></param>
    Private Sub CreateResultFactory(model As Model, node As Expression, customEntities As CustomSqlEntity())
      Dim resultType = node.Type

      SyncLock m_ResultFactories
        If m_ResultFactories.ContainsKey(resultType) Then
          Exit Sub
        End If
      End SyncLock

      Dim resultFactory = CustomResultReaderFactory.CreateResultFactory(node, customEntities)

      SyncLock m_ResultFactories
        m_ResultFactories(resultType) = resultFactory
      End SyncLock
    End Sub

  End Class
End Namespace