Imports Yamo.Infrastructure

Namespace Infrastructure

  ' TODO: SIP - add documentation to this class.
  Public Class SQLiteDialectProvider
    Inherits SqlDialectProvider

    Public Shared Shadows ReadOnly Property Instance As SQLiteDialectProvider = New SQLiteDialectProvider

    Private Sub New()
      Me.Formatter = New SQLiteFormatter
      Me.EntitySqlStringProviderFactory = New SQLiteEntitySqlStringProviderFactory
      Me.ValueTypeReaderFactory = New ValueTypeReaderFactory
      Me.EntityReaderFactory = New EntityReaderFactory
      Me.DbValueConversion = New SQLiteDbValueConversion
      RegisterInternalSqlHelper(New Sql.InternalDateDiff)
    End Sub

  End Class
End Namespace