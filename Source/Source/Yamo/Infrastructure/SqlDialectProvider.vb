Imports Yamo.Sql

Namespace Infrastructure

  ' TODO: SIP - add documentation to this class.
  Public MustInherit Class SqlDialectProvider

    Private m_Formatter As SqlFormatter
    Public Property Formatter() As SqlFormatter
      Get
        Return m_Formatter
      End Get
      Protected Set(ByVal value As SqlFormatter)
        m_Formatter = value
      End Set
    End Property

    Private m_EntitySqlStringProviderFactory As EntitySqlStringProviderFactory
    Public Property EntitySqlStringProviderFactory() As EntitySqlStringProviderFactory
      Get
        Return m_EntitySqlStringProviderFactory
      End Get
      Protected Set(ByVal value As EntitySqlStringProviderFactory)
        m_EntitySqlStringProviderFactory = value
      End Set
    End Property

    Private m_ValueTypeReaderFactory As ValueTypeReaderFactory
    Public Property ValueTypeReaderFactory() As ValueTypeReaderFactory
      Get
        Return m_ValueTypeReaderFactory
      End Get
      Protected Set(ByVal value As ValueTypeReaderFactory)
        m_ValueTypeReaderFactory = value
      End Set
    End Property

    Private m_EntityReaderFactory As EntityReaderFactory
    Public Property EntityReaderFactory() As EntityReaderFactory
      Get
        Return m_EntityReaderFactory
      End Get
      Protected Set(ByVal value As EntityReaderFactory)
        m_EntityReaderFactory = value
      End Set
    End Property

    Private m_DbValueConversion As DbValueConversion
    Public Property DbValueConversion() As DbValueConversion
      Get
        Return m_DbValueConversion
      End Get
      Protected Set(ByVal value As DbValueConversion)
        m_DbValueConversion = value
      End Set
    End Property

    Private m_InternalSqlHelpers As Dictionary(Of Type, IInternalSqlHelper) = New Dictionary(Of Type, IInternalSqlHelper)

    Public Sub RegisterInternalSqlHelper(helper As IInternalSqlHelper)
      m_InternalSqlHelpers(helper.SqlHelperType) = helper
    End Sub

    Public Function GetInternalSqlHelper(sqlHelperType As Type) As IInternalSqlHelper
      If Not m_InternalSqlHelpers.ContainsKey(sqlHelperType) Then
        Throw New ArgumentException($"No internal SQL helper implementation registered for SQL helper type '{sqlHelperType}'.")
      End If

      Return m_InternalSqlHelpers(sqlHelperType)
    End Function

  End Class
End Namespace