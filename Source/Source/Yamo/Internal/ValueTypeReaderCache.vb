Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

' TODO: SIP - will this still be needed in the future. Delete if not.

Namespace Internal

  Public Class ValueTypeReaderCache

    Private Shared m_Instances As Dictionary(Of Int32, ValueTypeReaderCache)

    ' Func(Of IDataReader, Int32, T)
    Private m_Readers As Dictionary(Of Type, Object)

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, ValueTypeReaderCache)
    End Sub

    Private Sub New()
      m_Readers = New Dictionary(Of Type, Object)
    End Sub

    Public Shared Function GetReader(Of T)(dialectProvider As SqlDialectProvider, model As Model) As Func(Of IDataReader, Int32, T)
      Return GetInstance(dialectProvider, model).GetOrCreateReader(Of T)(dialectProvider)
    End Function

    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider, model As Model) As ValueTypeReaderCache
      Dim instance As ValueTypeReaderCache

      ' TODO: use System.HashCode instead (when available in .NET)
      Dim key = (dialectProvider, model).GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New ValueTypeReaderCache
          m_Instances = New Dictionary(Of Int32, ValueTypeReaderCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New ValueTypeReaderCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetOrCreateReader(Of T)(dialectProvider As SqlDialectProvider) As Func(Of IDataReader, Int32, T)
      Dim type = GetType(T)
      Dim reader As Func(Of IDataReader, Int32, T) = Nothing

      SyncLock m_Readers
        If m_Readers.ContainsKey(type) Then
          reader = DirectCast(m_Readers(type), Func(Of IDataReader, Int32, T))
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.ValueTypeReaderFactory.CreateReader(Of T)
      Else
        Return reader
      End If

      SyncLock m_Readers
        m_Readers(type) = reader
      End SyncLock

      Return reader
    End Function

  End Class
End Namespace