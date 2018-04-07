Imports System.Data
Imports System.Linq.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal

  Public Class CustomResultReaderCache

    Private Shared m_Instances As Dictionary(Of Int32, CustomResultReaderCache)

    ' Func(Of IDataReader, CustomEntityReadInfo(), T)
    Private m_ResultFactories As Dictionary(Of Type, Object)

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, CustomResultReaderCache)
    End Sub

    Private Sub New()
      m_ResultFactories = New Dictionary(Of Type, Object)
    End Sub

    Public Shared Function GetResultFactory(Of T)(model As Model, resultType As Type) As Func(Of IDataReader, CustomEntityReadInfo(), T)
      Return GetInstance(model).GetResultFactoryOrThrow(Of T)(model, resultType)
    End Function

    Public Shared Sub CreateResultFactoryIfNotExists(model As Model, node As Expression, customEntities As CustomSqlEntity())
      GetInstance(model).CreateResultFactory(model, node, customEntities)
    End Sub

    Private Shared Function GetInstance(model As Model) As CustomResultReaderCache
      Dim instance As CustomResultReaderCache
      Dim key = model.GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New CustomResultReaderCache
          m_Instances = New Dictionary(Of Int32, CustomResultReaderCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New CustomResultReaderCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetResultFactoryOrThrow(Of T)(model As Model, resultType As Type) As Func(Of IDataReader, CustomEntityReadInfo(), T)
      Dim resultFactory As Func(Of IDataReader, CustomEntityReadInfo(), T) = Nothing

      SyncLock m_ResultFactories
        If m_ResultFactories.ContainsKey(resultType) Then
          resultFactory = DirectCast(m_ResultFactories(resultType), Func(Of IDataReader, CustomEntityReadInfo(), T))
        End If
      End SyncLock

      If resultFactory Is Nothing Then
        Throw New Exception($"Missing result factory method for type '{resultType}'.")
      End If

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