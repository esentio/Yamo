Imports System.Data.Common
Imports Microsoft.Data.SqlClient
Imports Yamo.Test

Public Class SqlServerTestDbContext
  Inherits BaseTestDbContext

  Private m_Connection As SqlConnection

  Sub New(connection As SqlConnection)
    m_Connection = connection
  End Sub

  Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    optionsBuilder.UseSqlServer(m_Connection)
  End Sub

  Protected Overrides Sub OnCommandExecuting(command As DbCommand)
    MyBase.OnCommandExecuting(command)
  End Sub

End Class
