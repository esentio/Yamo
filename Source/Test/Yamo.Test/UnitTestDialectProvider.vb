Imports Yamo.Infrastructure

Public Class UnitTestDialectProvider
  Inherits SqlDialectProvider

  Public Shared ReadOnly Property Instance As UnitTestDialectProvider = New UnitTestDialectProvider

  Private Sub New()
    MyBase.New(New SqlFormatter, New EntitySqlStringProviderFactory, New ValueTypeReaderFactory, New EntityReaderFactory, LimitType.Top Or LimitType.OffsetFetch)
  End Sub

End Class
