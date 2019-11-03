Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Value type reader cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTypeReaderCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of (SqlDialectProvider, Model), ValueTypeReaderCache)

    ''' <summary>
    ''' Stores cached reader instances.<br/>
    ''' Instance type is actually Func(Of IDataReader, Int32, T).
    ''' </summary>
    Private m_Readers As Dictionary(Of Type, Object)

    ''' <summary>
    ''' Initializes <see cref="ValueTypeReaderCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of (SqlDialectProvider, Model), ValueTypeReaderCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="ValueTypeReaderCache"/>.
    ''' </summary>
    Private Sub New()
      m_Readers = New Dictionary(Of Type, Object)
    End Sub

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(Of T)(dialectProvider As SqlDialectProvider, model As Model) As Func(Of IDataReader, Int32, T)
      Return DirectCast(GetInstance(dialectProvider, model).GetOrCreateReader(dialectProvider, GetType(T)), Func(Of IDataReader, Integer, T))
    End Function

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Object
      Return GetInstance(dialectProvider, model).GetOrCreateReader(dialectProvider, type)
    End Function

    ''' <summary>
    ''' Gets <see cref="ValueTypeReaderCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider, model As Model) As ValueTypeReaderCache
      Dim instance As ValueTypeReaderCache = Nothing

      Dim key = (dialectProvider, model)

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(key, instance) Then
          instance = New ValueTypeReaderCache
          m_Instances.Add(key, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates reader.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function GetOrCreateReader(dialectProvider As SqlDialectProvider, type As Type) As Object
      Dim reader As Object = Nothing

      SyncLock m_Readers
        m_Readers.TryGetValue(type, reader)
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.ValueTypeReaderFactory.CreateReader(type)
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