Imports System.Data.Common
Imports Yamo.Infrastructure

Public Class DbContextOptionsInternalBuilder

  Private ReadOnly Property Options As DbContextOptions

  Sub New(options As DbContextOptions)
    Me.Options = options
  End Sub

  Public Sub UseDialectProvider(dialectProvider As SqlDialectProvider)
    Me.Options.DialectProvider = dialectProvider
  End Sub

  Public Sub UseConnection(connection As DbConnection)
    Me.Options.Connection = connection
  End Sub

  Public Sub UseConnection(connectionFactory As Func(Of DbConnection))
    Me.Options.ConnectionFactory = connectionFactory
  End Sub

End Class
