Imports Yamo.Infrastructure

Public Class UnitTestDialectProvider
  Inherits SqlDialectProvider

  Public Shared Shadows ReadOnly Property Instance As UnitTestDialectProvider = New UnitTestDialectProvider

  Private Sub New()
    Me.Formatter = New SqlFormatter
    Me.EntitySqlStringProviderFactory = New EntitySqlStringProviderFactory
    Me.ValueTypeReaderFactory = New ValueTypeReaderFactory
    Me.EntityReaderFactory = New EntityReaderFactory
    Me.SupportedLimitType = LimitType.Top Or LimitType.OffsetFetch
  End Sub

End Class
