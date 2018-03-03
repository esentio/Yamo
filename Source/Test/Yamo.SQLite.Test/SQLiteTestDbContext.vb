Imports Microsoft.Data.Sqlite
Imports Yamo.Test

Public Class SQLiteTestDbContext
  Inherits BaseTestDbContext

  Private m_Connection As SqliteConnection

  Sub New(connection As SqliteConnection)
    m_Connection = connection
  End Sub

  Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    optionsBuilder.UseSQLite(m_Connection)
  End Sub

End Class
