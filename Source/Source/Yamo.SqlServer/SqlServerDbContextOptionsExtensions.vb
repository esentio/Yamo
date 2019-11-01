Imports System.Data.Common
Imports System.Runtime.CompilerServices
Imports Yamo.SqlServer.Infrastructure

Namespace Global.Yamo

  ' TODO: SIP - add documentation to this class.
  Public Module SqlServerDbContextOptionsExtensions

    <Extension>
    Public Function UseSqlServer(optionsBuilder As DbContextOptionsBuilder, connection As DbConnection) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(SqlServerDialectProvider.Instance)
      internalBuilder.UseConnection(connection)
      Return optionsBuilder
    End Function

    <Extension>
    Public Function UseSqlServer(optionsBuilder As DbContextOptionsBuilder, connectionFactory As Func(Of DbConnection)) As DbContextOptionsBuilder
      Dim internalBuilder = optionsBuilder.GetInternalBuilder()
      internalBuilder.UseDialectProvider(SqlServerDialectProvider.Instance)
      internalBuilder.UseConnection(connectionFactory)
      Return optionsBuilder
    End Function

  End Module
End Namespace