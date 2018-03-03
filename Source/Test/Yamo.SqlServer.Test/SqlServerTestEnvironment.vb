Imports System.Data.SqlClient
Imports Yamo.Test

Public Class SqlServerTestEnvironment
  Implements ITestEnvironment

  Private m_ConnectionString As String

  Private m_Connection As SqlConnection

  Private Sub New()
    m_ConnectionString = Configuration.ConfigurationManager.ConnectionStrings("TestDb").ConnectionString

    m_Connection = New SqlConnection(m_ConnectionString)
    m_Connection.Open()
  End Sub

  Public Shared Function Create() As SqlServerTestEnvironment
    Return New SqlServerTestEnvironment
  End Function

  Public Function CreateDbContext() As BaseTestDbContext Implements ITestEnvironment.CreateDbContext
    Return New SqlServerTestDbContext(m_Connection)
  End Function

End Class
