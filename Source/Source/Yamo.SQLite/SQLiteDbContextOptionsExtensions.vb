Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.CompilerServices
Imports Yamo.SQLite.Infrastructure

Namespace Global.Yamo

  ''' <summary>
  ''' Configuration options specific for SQLite.
  ''' </summary>
  Public Module SQLiteDbContextOptionsExtensions

    ''' <summary>
    ''' Configures the context to connect to SQLite database.
    ''' </summary>
    ''' <param name="optionsBuilder"></param>
    ''' <param name="connection"></param>
    ''' <returns></returns>
    <Extension>
    Public Function UseSQLite(<DisallowNull> optionsBuilder As DbContextOptionsBuilder, <DisallowNull> connection As DbConnection) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(SQLiteDialectProvider.Instance)
      internalBuilder.UseConnection(connection)
      Return optionsBuilder
    End Function

    ''' <summary>
    ''' Configures the context to connect to SQLite database.
    ''' </summary>
    ''' <param name="optionsBuilder"></param>
    ''' <param name="connectionFactory"></param>
    ''' <returns></returns>
    <Extension>
    Public Function UseSQLite(<DisallowNull> optionsBuilder As DbContextOptionsBuilder, <DisallowNull> connectionFactory As Func(Of DbConnection)) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(SQLiteDialectProvider.Instance)
      internalBuilder.UseConnection(connectionFactory)
      Return optionsBuilder
    End Function

  End Module
End Namespace