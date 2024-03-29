﻿Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Sql

Namespace Infrastructure

  ''' <summary>
  ''' Base class for SQL dialect providers.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class SqlDialectProvider

    Private m_Formatter As SqlFormatter
    ''' <summary>
    ''' Gets or sets formatter.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property Formatter() As SqlFormatter
      Get
        Return m_Formatter
      End Get
      Protected Set(<DisallowNull> ByVal value As SqlFormatter)
        m_Formatter = value
      End Set
    End Property

    Private m_EntitySqlStringProviderFactory As EntitySqlStringProviderFactory
    ''' <summary>
    ''' Gets or sets entity SQL striong provider factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property EntitySqlStringProviderFactory() As EntitySqlStringProviderFactory
      Get
        Return m_EntitySqlStringProviderFactory
      End Get
      Protected Set(<DisallowNull> ByVal value As EntitySqlStringProviderFactory)
        m_EntitySqlStringProviderFactory = value
      End Set
    End Property

    Private m_ValueTypeReaderFactory As ValueTypeReaderFactory
    ''' <summary>
    ''' Gets or sets value type reader factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property ValueTypeReaderFactory() As ValueTypeReaderFactory
      Get
        Return m_ValueTypeReaderFactory
      End Get
      Protected Set(<DisallowNull> ByVal value As ValueTypeReaderFactory)
        m_ValueTypeReaderFactory = value
      End Set
    End Property

    Private m_EntityReaderFactory As EntityReaderFactory
    ''' <summary>
    ''' Gets or sets entity reader factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property EntityReaderFactory() As EntityReaderFactory
      Get
        Return m_EntityReaderFactory
      End Get
      Protected Set(<DisallowNull> ByVal value As EntityReaderFactory)
        m_EntityReaderFactory = value
      End Set
    End Property

    Private m_SupportedLimitType As LimitType
    ''' <summary>
    ''' Gets or sets supported limit clause type
    ''' </summary>
    ''' <returns></returns>
    Public Property SupportedLimitType() As LimitType
      Get
        Return m_SupportedLimitType
      End Get
      Protected Set(ByVal value As LimitType)
        m_SupportedLimitType = value
      End Set
    End Property

    ''' <summary>
    ''' Stores dialect specific SQL helpers.
    ''' </summary>
    Private m_DialectSpecificSqlHelpers As Dictionary(Of Type, Type) = New Dictionary(Of Type, Type)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlDialectProvider"/>.
    ''' </summary>
    ''' <param name="formatter"></param>
    ''' <param name="entitySqlStringProviderFactory"></param>
    ''' <param name="valueTypeReaderFactory"></param>
    ''' <param name="entityReaderFactory"></param>
    ''' <param name="supportedLimitType"></param>
    Protected Sub New(<DisallowNull> formatter As SqlFormatter, <DisallowNull> entitySqlStringProviderFactory As EntitySqlStringProviderFactory, <DisallowNull> valueTypeReaderFactory As ValueTypeReaderFactory, <DisallowNull> entityReaderFactory As EntityReaderFactory, supportedLimitType As LimitType)
      Me.Formatter = formatter
      Me.EntitySqlStringProviderFactory = entitySqlStringProviderFactory
      Me.ValueTypeReaderFactory = valueTypeReaderFactory
      Me.EntityReaderFactory = entityReaderFactory
      Me.SupportedLimitType = supportedLimitType
    End Sub

    ''' <summary>
    ''' Registers dialect specific SQL helper.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <typeparam name="TSqlHelper"></typeparam>
    ''' <typeparam name="TDialectSqlHelper"></typeparam>
    Public Sub RegisterDialectSpecificSqlHelper(Of TSqlHelper, TDialectSqlHelper)()
      m_DialectSpecificSqlHelpers(GetType(TSqlHelper)) = GetType(TDialectSqlHelper)
    End Sub

    ''' <summary>
    ''' Gets dialect specific SQL helper if it is registered.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlHelperType"></param>
    ''' <returns></returns>
    Public Function GetDialectSpecificSqlHelper(<DisallowNull> sqlHelperType As Type) As Type
      If m_DialectSpecificSqlHelpers.ContainsKey(sqlHelperType) Then
        Return m_DialectSpecificSqlHelpers(sqlHelperType)
      Else
        Return Nothing
      End If
    End Function

  End Class
End Namespace