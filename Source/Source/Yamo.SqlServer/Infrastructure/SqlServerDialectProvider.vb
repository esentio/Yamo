﻿Imports Yamo.Infrastructure

Namespace Infrastructure

  ''' <summary>
  ''' MS SQL Server dialect provider.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlServerDialectProvider
    Inherits SqlDialectProvider

    ''' <summary>
    ''' Gets instance of <see cref="SqlServerDialectProvider"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Shadows ReadOnly Property Instance As SqlServerDialectProvider = New SqlServerDialectProvider

    ''' <summary>
    ''' Creates new instance of <see cref="SqlServerDialectProvider"/>.
    ''' </summary>
    Private Sub New()
      Me.Formatter = New SqlFormatter
      Me.EntitySqlStringProviderFactory = New EntitySqlStringProviderFactory
      Me.ValueTypeReaderFactory = New ValueTypeReaderFactory
      Me.EntityReaderFactory = New EntityReaderFactory
      Me.SupportedLimitType = LimitType.Top Or LimitType.OffsetFetch
      RegisterDialectSpecificSqlHelper(Of Yamo.Sql.Exp, Yamo.SqlServer.Sql.Exp)()
      RegisterDialectSpecificSqlHelper(Of Yamo.Sql.DateTime, Yamo.SqlServer.Sql.DateTime)()
    End Sub

  End Class
End Namespace