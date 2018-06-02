Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class EntityReaderCache

    Private Shared m_Instances As Dictionary(Of Int32, EntityReaderCache)

    Private m_Readers As Dictionary(Of Type, Func(Of IDataReader, Int32, BitArray, Object))

    Private m_ContainsPKReaders As Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Boolean))

    Private m_PKReaders As Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Object))

    Private m_DbGeneratedValuesReaders As Dictionary(Of Type, Action(Of IDataReader, Int32, Object))

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, EntityReaderCache)
    End Sub

    Private Sub New()
      m_Readers = New Dictionary(Of Type, Func(Of IDataReader, Int32, BitArray, Object))
      m_ContainsPKReaders = New Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Boolean))
      m_PKReaders = New Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Object))
      m_DbGeneratedValuesReaders = New Dictionary(Of Type, Action(Of IDataReader, Int32, Object))
    End Sub

    Public Shared Function GetReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, BitArray, Object)
      Return GetInstance(dialectProvider, model).GetOrCreateReader(dialectProvider, model, type)
    End Function

    Public Shared Function GetContainsPKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Boolean)
      Return GetInstance(dialectProvider, model).GetOrCreateContainsPKReader(dialectProvider, model, type)
    End Function

    Public Shared Function GetPKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Object)
      Return GetInstance(dialectProvider, model).GetOrCreatePKReader(dialectProvider, model, type)
    End Function

    Public Shared Function GetDbGeneratedValuesReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Action(Of IDataReader, Int32, Object)
      Return GetInstance(dialectProvider, model).GetOrCreateDbGeneratedValuesReader(dialectProvider, model, type)
    End Function

    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider, model As Model) As EntityReaderCache
      Dim instance As EntityReaderCache

      ' TODO: use System.HashCode instead (when available in .NET)
      Dim key = (dialectProvider, model).GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New EntityReaderCache
          m_Instances = New Dictionary(Of Int32, EntityReaderCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New EntityReaderCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetOrCreateReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, BitArray, Object)
      Dim reader As Func(Of IDataReader, Int32, BitArray, Object) = Nothing

      SyncLock m_Readers
        If m_Readers.ContainsKey(type) Then
          reader = m_Readers(type)
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.EntityReaderFactory.CreateReader(model, type)
      Else
        Return reader
      End If

      SyncLock m_Readers
        m_Readers(type) = reader
      End SyncLock

      Return reader
    End Function

    Private Function GetOrCreateContainsPKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Boolean)
      Dim reader As Func(Of IDataReader, Int32, Int32(), Boolean) = Nothing

      SyncLock m_ContainsPKReaders
        If m_ContainsPKReaders.ContainsKey(type) Then
          reader = m_ContainsPKReaders(type)
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.EntityReaderFactory.CreateContainsPKReader(model, type)
      Else
        Return reader
      End If

      SyncLock m_ContainsPKReaders
        m_ContainsPKReaders(type) = reader
      End SyncLock

      Return reader
    End Function

    Private Function GetOrCreatePKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Object)
      Dim reader As Func(Of IDataReader, Int32, Int32(), Object) = Nothing

      SyncLock m_PKReaders
        If m_PKReaders.ContainsKey(type) Then
          reader = m_PKReaders(type)
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.EntityReaderFactory.CreatePKReader(model, type)
      Else
        Return reader
      End If

      SyncLock m_PKReaders
        m_PKReaders(type) = reader
      End SyncLock

      Return reader
    End Function

    Private Function GetOrCreateDbGeneratedValuesReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Action(Of IDataReader, Int32, Object)
      Dim reader As Action(Of IDataReader, Int32, Object) = Nothing

      SyncLock m_DbGeneratedValuesReaders
        If m_DbGeneratedValuesReaders.ContainsKey(type) Then
          reader = m_DbGeneratedValuesReaders(type)
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.EntityReaderFactory.CreateDbGeneratedValuesReader(model, type)
      Else
        Return reader
      End If

      SyncLock m_DbGeneratedValuesReaders
        m_DbGeneratedValuesReaders(type) = reader
      End SyncLock

      Return reader
    End Function

  End Class
End Namespace