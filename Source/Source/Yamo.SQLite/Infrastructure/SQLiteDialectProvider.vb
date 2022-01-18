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
    Public Shared ReadOnly Property Instance As SQLiteDialectProvider = New SQLiteDialectProvider

    ''' <summary>
    ''' Creates new instance of <see cref="SQLiteDialectProvider"/>.
    ''' </summary>
    Private Sub New()
      MyBase.New(New SQLiteFormatter, New SQLiteEntitySqlStringProviderFactory, New ValueTypeReaderFactory, New EntityReaderFactory, LimitType.Limit)
      RegisterDialectSpecificSqlHelper(Of Yamo.Sql.Exp, Yamo.SQLite.Sql.Exp)()
      RegisterDialectSpecificSqlHelper(Of Yamo.Sql.DateTime, Yamo.SQLite.Sql.DateTime)()
    End Sub

  End Class
End Namespace