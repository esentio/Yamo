Imports System.Data
Imports System.Data.Common
Imports Yamo.Infrastructure

Public Class DbContextOptions

  Friend Property DialectProvider As SqlDialectProvider

  Public Property Connection As DbConnection

  Public Property ConnectionFactory As Func(Of DbConnection)

  Public Property CommandTimeout As Int32?

End Class
