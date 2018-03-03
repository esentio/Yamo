Imports Yamo.Infrastructure

Namespace Infrastructure

  Public Class SqlServerDialectProvider
    Inherits SqlDialectProvider

    Public Shared Shadows ReadOnly Property Instance As SqlServerDialectProvider = New SqlServerDialectProvider

    Private Sub New()
      Me.Formatter = New SqlFormatter
      Me.EntitySqlStringProviderFactory = New EntitySqlStringProviderFactory
      Me.ValueTypeReaderFactory = New ValueTypeReaderFactory
      Me.EntityReaderFactory = New EntityReaderFactory
      Me.DbValueConversion = New DbValueConversion
      RegisterInternalSqlHelper(New Sql.InternalDateDiff)
    End Sub

  End Class
End Namespace