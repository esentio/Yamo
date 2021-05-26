Imports System.Data
Imports System.Linq.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' SQL result reader cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlResultReaderCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of Model, SqlResultReaderCache)

    ''' <summary>
    ''' Stores cached reader instances.<br/>
    ''' Instance type is actually Func(Of IDataReader, ReaderDataBase, T).
    ''' </summary>
    Private m_Readers As Dictionary(Of Type, Object)

    ''' <summary>
    ''' Stores cached reader instances that are wrapped as Func(Of IDataReader, ReaderDataBase, Object).<br/>
    ''' Instance type is actually Func(Of IDataReader, ReaderDataBase, Object).
    ''' </summary>
    Private m_ValueTypeWrappedReaders As Dictionary(Of Type, Func(Of IDataReader, ReaderDataBase, Object))

    ''' <summary>
    ''' Initializes <see cref="SqlResultReaderCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of Model, SqlResultReaderCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlResultReaderCache"/>.
    ''' </summary>
    Private Sub New()
      m_Readers = New Dictionary(Of Type, Object)
      m_ValueTypeWrappedReaders = New Dictionary(Of Type, Func(Of IDataReader, ReaderDataBase, Object))
    End Sub

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(model As Model, sqlResult As SqlResultBase) As Func(Of IDataReader, ReaderDataBase, Object)
      If sqlResult.ResultType.IsValueType Then
        Return GetInstance(model).GetOrCreateValueTypeToObjectWrappedReader(model, sqlResult)
      Else
        Return DirectCast(GetInstance(model).GetOrCreateReader(model, sqlResult), Func(Of IDataReader, ReaderDataBase, Object))
      End If
    End Function

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(Of T)(model As Model, sqlResult As SqlResultBase) As Func(Of IDataReader, ReaderDataBase, T)
      Return DirectCast(GetInstance(model).GetOrCreateReader(model, sqlResult), Func(Of IDataReader, ReaderDataBase, T))
    End Function

    ''' <summary>
    ''' Gets <see cref="SqlResultReaderCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(model As Model) As SqlResultReaderCache
      Dim instance As SqlResultReaderCache = Nothing

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(model, instance) Then
          instance = New SqlResultReaderCache
          m_Instances.Add(model, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates result factory.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Function GetOrCreateReader(model As Model, sqlResult As SqlResultBase) As Object
      Dim reader As Object = Nothing
      Dim resultType = sqlResult.ResultType

      SyncLock m_Readers
        Dim value As Object = Nothing

        If m_Readers.TryGetValue(resultType, value) Then
          reader = value
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = SqlResultReaderFactory.CreateResultFactory(sqlResult)
      Else
        Return reader
      End If

      SyncLock m_Readers
        m_Readers(resultType) = reader
      End SyncLock

      Return reader
    End Function

    ''' <summary>
    ''' Gets or creates result factory that return value type as an object.
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Private Function GetOrCreateValueTypeToObjectWrappedReader(model As Model, sqlResult As SqlResultBase) As Func(Of IDataReader, ReaderDataBase, Object)
      Dim reader As Func(Of IDataReader, ReaderDataBase, Object) = Nothing
      Dim resultType = sqlResult.ResultType

      SyncLock m_ValueTypeWrappedReaders
        Dim value As Func(Of IDataReader, ReaderDataBase, Object) = Nothing

        If m_ValueTypeWrappedReaders.TryGetValue(resultType, value) Then
          reader = value
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = SqlResultReaderFactory.CreateValueTypeToObjectResultFactoryWrapper(sqlResult.ResultType, GetOrCreateReader(model, sqlResult))
      Else
        Return reader
      End If

      SyncLock m_ValueTypeWrappedReaders
        m_ValueTypeWrappedReaders(resultType) = reader
      End SyncLock

      Return reader
    End Function

  End Class
End Namespace