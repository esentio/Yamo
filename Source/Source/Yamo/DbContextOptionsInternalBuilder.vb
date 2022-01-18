Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports Yamo.Infrastructure

''' <summary>
''' Provides an API for configuring <see cref="DbContextOptions"/>.<br/>
''' This class is supposed to be used in particular database dialect extension methods to do the configuration.
''' </summary>
Public Class DbContextOptionsInternalBuilder

  ''' <summary>
  ''' Configuration options.
  ''' </summary>
  ''' <returns></returns>
  Private ReadOnly Property Options As DbContextOptions

  ''' <summary>
  ''' Creates new instance of <see cref="DbContextOptionsInternalBuilder"/>.
  ''' </summary>
  ''' <param name="options"></param>
  Sub New(<DisallowNull> options As DbContextOptions)
    Me.Options = options
  End Sub

  ''' <summary>
  ''' Sets SQL dialect provider.
  ''' </summary>
  ''' <param name="dialectProvider"></param>
  Public Sub UseDialectProvider(<DisallowNull> dialectProvider As SqlDialectProvider)
    Me.Options.DialectProvider = dialectProvider
  End Sub

  ''' <summary>
  ''' Sets externally created database connection.
  ''' </summary>
  ''' <param name="connection"></param>
  Public Sub UseConnection(<DisallowNull> connection As DbConnection)
    Me.Options.Connection = connection
  End Sub

  ''' <summary>
  ''' Sets database connection factory method.
  ''' </summary>
  ''' <param name="connectionFactory"></param>
  Public Sub UseConnection(<DisallowNull> connectionFactory As Func(Of DbConnection))
    Me.Options.ConnectionFactory = connectionFactory
  End Sub

End Class
