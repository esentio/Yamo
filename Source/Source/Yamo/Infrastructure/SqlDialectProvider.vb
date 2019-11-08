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
      Protected Set(ByVal value As SqlFormatter)
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
      Protected Set(ByVal value As EntitySqlStringProviderFactory)
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
      Protected Set(ByVal value As ValueTypeReaderFactory)
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
      Protected Set(ByVal value As EntityReaderFactory)
        m_EntityReaderFactory = value
      End Set
    End Property

    Private m_DbValueConversion As DbValueConversion
    ''' <summary>
    ''' Gets or sets database value conversion.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Property DbValueConversion() As DbValueConversion
      Get
        Return m_DbValueConversion
      End Get
      Protected Set(ByVal value As DbValueConversion)
        m_DbValueConversion = value
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
    ''' Stores internal SQL helpers.
    ''' </summary>
    Private m_InternalSqlHelpers As Dictionary(Of Type, IInternalSqlHelper) = New Dictionary(Of Type, IInternalSqlHelper)

    ''' <summary>
    ''' Registers internal SQL helper.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="helper"></param>
    Public Sub RegisterInternalSqlHelper(helper As IInternalSqlHelper)
      m_InternalSqlHelpers(helper.SqlHelperType) = helper
    End Sub

    ''' <summary>
    ''' Gets internal SQL helper.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="sqlHelperType"></param>
    ''' <returns></returns>
    Public Function GetInternalSqlHelper(sqlHelperType As Type) As IInternalSqlHelper
      If Not m_InternalSqlHelpers.ContainsKey(sqlHelperType) Then
        Throw New ArgumentException($"No internal SQL helper implementation registered for SQL helper type '{sqlHelperType}'.")
      End If

      Return m_InternalSqlHelpers(sqlHelperType)
    End Function

  End Class
End Namespace