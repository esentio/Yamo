﻿Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  ''' <summary>
  ''' Entity reader cache.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class EntityReaderCache

    ''' <summary>
    ''' Stores cache instances.
    ''' </summary>
    Private Shared m_Instances As Dictionary(Of (SqlDialectProvider, Model), EntityReaderCache)

    ''' <summary>
    ''' Stores cached reader instances.
    ''' </summary>
    Private m_Readers As Dictionary(Of Type, Func(Of IDataReader, Int32, Boolean(), Object))

    ''' <summary>
    ''' Stores cached contains primary key reader instances.
    ''' </summary>
    Private m_ContainsPKReaders As Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Boolean))

    ''' <summary>
    ''' Stores cached primary key reader instances.
    ''' </summary>
    Private m_PKReaders As Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Object))

    ''' <summary>
    ''' Stores cached database generated values reader instances.
    ''' </summary>
    Private m_DbGeneratedValuesReaders As Dictionary(Of Type, Action(Of IDataReader, Int32, Object))

    ''' <summary>
    ''' Initializes <see cref="EntityReaderCache"/> related static data.
    ''' </summary>
    Shared Sub New()
      m_Instances = New Dictionary(Of (SqlDialectProvider, Model), EntityReaderCache)
    End Sub

    ''' <summary>
    ''' Creates new instance of <see cref="EntityReaderCache"/>.
    ''' </summary>
    Private Sub New()
      m_Readers = New Dictionary(Of Type, Func(Of IDataReader, Int32, Boolean(), Object))
      m_ContainsPKReaders = New Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Boolean))
      m_PKReaders = New Dictionary(Of Type, Func(Of IDataReader, Int32, Int32(), Object))
      m_DbGeneratedValuesReaders = New Dictionary(Of Type, Action(Of IDataReader, Int32, Object))
    End Sub

    ''' <summary>
    ''' Gets reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Boolean(), Object)
      Return GetInstance(dialectProvider, model).GetOrCreateReader(dialectProvider, model, type)
    End Function

    ''' <summary>
    ''' Gets contains primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetContainsPKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Boolean)
      Return GetInstance(dialectProvider, model).GetOrCreateContainsPKReader(dialectProvider, model, type)
    End Function

    ''' <summary>
    ''' Gets primary key reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetPKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Object)
      Return GetInstance(dialectProvider, model).GetOrCreatePKReader(dialectProvider, model, type)
    End Function

    ''' <summary>
    ''' Gets database generated values reader.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetDbGeneratedValuesReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Action(Of IDataReader, Int32, Object)
      Return GetInstance(dialectProvider, model).GetOrCreateDbGeneratedValuesReader(dialectProvider, model, type)
    End Function

    ''' <summary>
    ''' Gets <see cref="EntityReaderCache"/> cache instance. If it doesn't exist, it is created.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider, model As Model) As EntityReaderCache
      Dim instance As EntityReaderCache = Nothing

      Dim key = (dialectProvider, model)

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(key, instance) Then
          instance = New EntityReaderCache
          m_Instances.Add(key, instance)
        End If
      End SyncLock

      Return instance
    End Function

    ''' <summary>
    ''' Gets or creates reader.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function GetOrCreateReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Boolean(), Object)
      Dim reader As Func(Of IDataReader, Int32, Boolean(), Object) = Nothing

      SyncLock m_Readers
        m_Readers.TryGetValue(type, reader)
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

    ''' <summary>
    ''' Gets or creates contains primary key reader.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function GetOrCreateContainsPKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Boolean)
      Dim reader As Func(Of IDataReader, Int32, Int32(), Boolean) = Nothing

      SyncLock m_ContainsPKReaders
        m_ContainsPKReaders.TryGetValue(type, reader)
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

    ''' <summary>
    ''' Gets or creates primary key reader.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function GetOrCreatePKReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Func(Of IDataReader, Int32, Int32(), Object)
      Dim reader As Func(Of IDataReader, Int32, Int32(), Object) = Nothing

      SyncLock m_PKReaders
        m_PKReaders.TryGetValue(type, reader)
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

    ''' <summary>
    ''' Gets or creates database generated values reader.
    ''' </summary>
    ''' <param name="dialectProvider"></param>
    ''' <param name="model"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Private Function GetOrCreateDbGeneratedValuesReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Action(Of IDataReader, Int32, Object)
      Dim reader As Action(Of IDataReader, Int32, Object) = Nothing

      SyncLock m_DbGeneratedValuesReaders
        m_DbGeneratedValuesReaders.TryGetValue(type, reader)
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