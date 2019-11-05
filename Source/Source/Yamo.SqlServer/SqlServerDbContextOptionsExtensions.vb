Imports System.Data.Common
Imports System.Runtime.CompilerServices
Imports Yamo.SqlServer.Infrastructure

Namespace Global.Yamo

  ''' <summary>
  ''' Configuration options specific for MS SQL Server.
  ''' </summary>
  Public Module SqlServerDbContextOptionsExtensions

    ''' <summary>
    ''' Configures the context to connect to MS SQL Server database.
    ''' </summary>
    ''' <param name="optionsBuilder"></param>
    ''' <param name="connection"></param>
    ''' <returns></returns>
    <Extension>
    Public Function UseSqlServer(optionsBuilder As DbContextOptionsBuilder, connection As DbConnection) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(SqlServerDialectProvider.Instance)
      internalBuilder.UseConnection(connection)
      Return optionsBuilder
    End Function

    ''' <summary>
    ''' Configures the context to connect to MS SQL Server database.
    ''' </summary>
    ''' <param name="optionsBuilder"></param>
    ''' <param name="connectionFactory"></param>
    ''' <returns></returns>
    <Extension>
    Public Function UseSqlServer(optionsBuilder As DbContextOptionsBuilder, connectionFactory As Func(Of DbConnection)) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(SqlServerDialectProvider.Instance)
      internalBuilder.UseConnection(connectionFactory)
      Return optionsBuilder
    End Function

  End Module
End Namespace