Imports Microsoft.Data.Sqlite
Imports Yamo.Test

Public Class SQLiteTestEnvironment
  Implements ITestEnvironment

  Private m_ConnectionString As String

  Private m_Connection As SqliteConnection

  Private Sub New()
    m_ConnectionString = Configuration.ConfigurationManager.ConnectionStrings("TestDb").ConnectionString

    m_Connection = New SqliteConnection(m_ConnectionString)
    m_Connection.Open()
  End Sub

  Public Shared Function Create() As SQLiteTestEnvironment
    Return New SQLiteTestEnvironment
  End Function

  Public Function CreateDbContext() As BaseTestDbContext Implements ITestEnvironment.CreateDbContext
    Return New SQLiteTestDbContext(m_Connection)
  End Function

End Class
