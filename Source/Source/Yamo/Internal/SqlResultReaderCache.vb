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
  ''' SQL result reader cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlResultReaderCache

    ''' <summary>
    ''' Stores cache instances.<br/>
    ''' <br/>
    ''' Key: actual <see cref="DbDataReader"/> type, <see cref="Model"/> instance.<br/>
    ''' Value: <see cref="SqlResultReaderCache"/> instance.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of (Type, Model), SqlResultReaderCache)

    ''' <summary>
    ''' Stores cached reader instances.<br/>
    ''' <br/>
    ''' Key: type corresponding to <see cref="SqlResultBase.ResultType"/>.<br/>
    ''' Value: <see cref="Func(Of DbDataReader, ReaderDataBase, T)"/> delegate, where first parameter is <see cref="DbDataReader"/> instance, second parameter is <see cref="ReaderDataBase"/> instance and return value is actual result.
    ''' </summary>
    Private m_Readers As Dictionary(Of Type, Object)

    ''' <summary>
    ''' Stores cached reader instances that are wrapped as Func(Of DbDataReader, ReaderDataBase, Object).<br/>
    ''' <br/>
    ''' Key: type corresponding to <see cref="SqlResultBase.ResultType"/>.<br/>
    ''' Value: <see cref="Func(Of DbDataReader, ReaderDataBase, Object)"/> delegate, where first parameter is <see cref="DbDataReader"/> instance, second parameter is <see cref="ReaderDataBase"/> instance and return value is actual result casted as an <see cref="Object"/>.
    ''' </summary>
    Private m_ValueTypeWrappedReaders As Dictionary(Of Type, Func(Of DbDataReader, ReaderDataBase, Object))

    ''' <summary>
    ''' Initializes <see cref="SqlResultReaderCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of (Type, Model), SqlResultReaderCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="SqlResultReaderCache"/>.
    ''' </summary>
    Private Sub New()
      m_Readers = New Dictionary(Of Type, Object)
      m_ValueTypeWrappedReaders = New Dictionary(Of Type, Func(Of DbDataReader, ReaderDataBase, Object))
    End Sub

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(<DisallowNull> dataReaderType As Type, <DisallowNull> model As Model, <DisallowNull> sqlResult As SqlResultBase) As Func(Of DbDataReader, ReaderDataBase, Object)
      If sqlResult.ResultType.IsValueType Then
        Return GetInstance(dataReaderType, model).GetOrCreateValueTypeToObjectWrappedReader(model, sqlResult)
      Else
        Return DirectCast(GetInstance(dataReaderType, model).GetOrCreateReader(model, sqlResult), Func(Of DbDataReader, ReaderDataBase, Object))
      End If
    End Function

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <param name="sqlResult"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(Of T)(<DisallowNull> dataReaderType As Type, <DisallowNull> model As Model, <DisallowNull> sqlResult As SqlResultBase) As Func(Of DbDataReader, ReaderDataBase, T)
      Return DirectCast(GetInstance(dataReaderType, model).GetOrCreateReader(model, sqlResult), Func(Of DbDataReader, ReaderDataBase, T))
    End Function

    ''' <summary>
    ''' Gets <see cref="SqlResultReaderCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="dataReaderType"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(dataReaderType As Type, model As Model) As SqlResultReaderCache
      Dim instance As SqlResultReaderCache = Nothing

      Dim key = (dataReaderType, model)

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(key, instance) Then
          instance = New SqlResultReaderCache
          m_Instances.Add(key, instance)
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
    Private Function GetOrCreateValueTypeToObjectWrappedReader(model As Model, sqlResult As SqlResultBase) As Func(Of DbDataReader, ReaderDataBase, Object)
      Dim reader As Func(Of DbDataReader, ReaderDataBase, Object) = Nothing
      Dim resultType = sqlResult.ResultType

      SyncLock m_ValueTypeWrappedReaders
        Dim value As Func(Of DbDataReader, ReaderDataBase, Object) = Nothing

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