Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Value type reader cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class ValueTypeReaderCache

    ''' <summary>
    ''' Stores cache instances.<br/>
    ''' <br/>
    ''' Key: actual <see cref="DbDataReader"/> type, <see cref="SqlDialectProvider"/> instance, <see cref="Model"/> instance.<br/>
    ''' Value: <see cref="ValueTypeReaderCache"/> instance.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of (Type, SqlDialectProvider, Model), ValueTypeReaderCache)

    ''' <summary>
    ''' Stores cached reader instances.<br/>
    ''' <br/>
    ''' Key: type of scalar result.<br/>
    ''' Value: <see cref="Func(Of DbDataReader, Int32, T)"/> delegate, where first parameter is <see cref="DbDataReader"/> instance, second parameter is starting reader index and return value is actual result.
    ''' </summary>
    Private m_Readers As Dictionary(Of Type, Object)

    ''' <summary>
    ''' Initializes <see cref="ValueTypeReaderCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of (Type, SqlDialectProvider, Model), ValueTypeReaderCache)
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
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(Of T)(<DisallowNull> dataReaderType As Type, <DisallowNull> dialectProvider As SqlDialectProvider, <DisallowNull> model As Model) As Func(Of DbDataReader, Int32, T)
      Return DirectCast(GetInstance(dataReaderType, dialectProvider, model).GetOrCreateReader(dataReaderType, dialectProvider, GetType(T)), Func(Of DbDataReader, Int32, T))
    End Function

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(<DisallowNull> dataReaderType As Type, <DisallowNull> dialectProvider As SqlDialectProvider, <DisallowNull> model As Model, <DisallowNull> type As Type) As Object
      Return GetInstance(dataReaderType, dialectProvider, model).GetOrCreateReader(dataReaderType, dialectProvider, type)
    End Function

    ''' <summary>
    ''' Gets <see cref="ValueTypeReaderCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(dataReaderType As Type, dialectProvider As SqlDialectProvider, model As Model) As ValueTypeReaderCache
      Dim instance As ValueTypeReaderCache = Nothing

      Dim key = (dataReaderType, dialectProvider, model)

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
    ''' <param name="dataReaderType"></param>
    ''' <param name="dialectProvider"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function GetOrCreateReader(dataReaderType As Type, dialectProvider As SqlDialectProvider, type As Type) As Object
      Dim reader As Object = Nothing

      SyncLock m_Readers
        m_Readers.TryGetValue(type, reader)
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.ValueTypeReaderFactory.CreateReader(dataReaderType, type)
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