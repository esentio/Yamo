Imports System.Data
Imports System.Linq.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal

  Public Class CustomResultReaderCache

    Private Shared m_Instances As Dictionary(Of Model, CustomResultReaderCache)

    ' Func(Of IDataReader, CustomEntityReadInfo(), T)
    Private m_ResultFactories As Dictionary(Of Type, Object)

    Shared Sub New()
      m_Instances = New Dictionary(Of Model, CustomResultReaderCache)
    End Sub

    Private Sub New()
      m_ResultFactories = New Dictionary(Of Type, Object)
    End Sub

    Public Shared Function GetResultFactory(Of T)(model As Model, resultType As Type) As Func(Of IDataReader, CustomEntityReadInfo(), T)
      Return GetInstance(model).GetOrCreateResultFactory(Of T)(model, resultType)
    End Function

    Public Shared Sub CreateResultFactoryIfNotExists(model As Model, node As Expression, customEntities As CustomSqlEntity())
      GetInstance(model).CreateResultFactory(model, node, customEntities)
    End Sub

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