Imports System.Data
Imports Microsoft.Data.Sqlite
Imports Yamo.Test

Namespace Tests

  <TestClass()>
  Public Class DbContextTests
    Inherits Yamo.Test.Tests.DbContextTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overrides Sub CreateContextWithConnection()
      Dim cs = Configuration.ConfigurationManager.ConnectionStrings("TestDb").ConnectionString

      Using conn = New SqliteConnection(cs)
        Using db = New DbContextWithProvidedConnection(conn)
          Assert.AreEqual(conn, db.Database.Connection)

          Dim result = db.QueryFirstOrDefault(Of Int32)("SELECT 1")
          Assert.AreEqual(1, result)
        End Using

        Assert.AreEqual(ConnectionState.Open, conn.State)

        Using db = New DbContextWithProvidedConnection(conn)
          Assert.AreEqual(conn, db.Database.Connection)

          Dim result = db.QueryFirstOrDefault(Of Int32)("SELECT 1")
          Assert.AreEqual(1, result)
        End Using
      End Using
    End Sub

    <TestMethod()>
    Public Overrides Sub CreateContextWithConnectionFactory()
      Dim cs = Configuration.ConfigurationManager.ConnectionStrings("TestDb").ConnectionString

      Using db = New DbContextWithProvidedConnectionFactory(cs)
        Dim result = db.QueryFirstOrDefault(Of Int32)("SELECT 1")
        Assert.AreEqual(1, result)
      End Using
    End Sub

    Public Class DbContextWithProvidedConnection
      Inherits DbContext

      Private m_Connection As SqliteConnection

      Sub New(connection As SqliteConnection)
        m_Connection = connection
      End Sub

      Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSQLite(m_Connection)
      End Sub
    End Class

    Public Class DbContextWithProvidedConnectionFactory
      Inherits DbContext

      Private m_ConnectionString As String

      Sub New(connectionString As String)
        m_ConnectionString = connectionString
      End Sub

      Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSQLite(Function() New SqliteConnection(m_ConnectionString))
      End Sub
    End Class

  End Class
End Namespace