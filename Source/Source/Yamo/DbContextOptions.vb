Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Infrastructure

''' <summary>
''' Configuration options used by a <see cref="DbContext"/>.
''' </summary>
Public Class DbContextOptions

  ''' <summary>
  ''' Current SQL dialect provider.
  ''' </summary>
  ''' <returns></returns>
  Friend Property DialectProvider As SqlDialectProvider

  ''' <summary>
  ''' Externally provided database connection.
  ''' </summary>
  ''' <returns></returns>
  Public Property Connection As <MaybeNull> DbConnection

  ''' <summary>
  ''' Database connection factory (used when no database connection is provided).
  ''' </summary>
  ''' <returns></returns>
  Public Property ConnectionFactory As <MaybeNull> Func(Of DbConnection)

  ''' <summary>
  ''' Database command timeout.
  ''' </summary>
  ''' <returns></returns>
  Public Property CommandTimeout As Int32?

End Class
