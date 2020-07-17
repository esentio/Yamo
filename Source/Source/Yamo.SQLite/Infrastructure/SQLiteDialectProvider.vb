Imports Yamo.Infrastructure

Namespace Infrastructure

  ''' <summary>
  ''' SQLite dialect provider.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SQLiteDialectProvider
    Inherits SqlDialectProvider

    ''' <summary>
    ''' Gets instance of <see cref="SQLiteDialectProvider"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Shadows ReadOnly Property Instance As SQLiteDialectProvider = New SQLiteDialectProvider

    ''' <summary>
    ''' Creates new instance of <see cref="SQLiteDialectProvider"/>.
    ''' </summary>
    Private Sub New()
      Me.Formatter = New SQLiteFormatter
      Me.EntitySqlStringProviderFactory = New SQLiteEntitySqlStringProviderFactory
      Me.ValueTypeReaderFactory = New ValueTypeReaderFactory
      Me.EntityReaderFactory = New EntityReaderFactory
      Me.DbValueConversion = New SQLiteDbValueConversion
      Me.SupportedLimitType = LimitType.Limit
      RegisterDialectSpecificSqlHelper(Of Yamo.Sql.Exp, Yamo.SQLite.Sql.Exp)()
      RegisterDialectSpecificSqlHelper(Of Yamo.Sql.DateTime, Yamo.SQLite.Sql.DateTime)()
    End Sub

  End Class
End Namespace