Imports System.IO
Imports System.Text.RegularExpressions
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

  Public Sub InitializeDatabase() Implements ITestEnvironment.InitializeDatabase
    Using db = CreateDbContext()
      Dim sql = File.ReadAllText("Sql\SQLiteDbInitialize.sql")
      ExecuteSql(db, sql)
    End Using
  End Sub

  Public Sub UninitializeDatabase() Implements ITestEnvironment.UninitializeDatabase
    Using db = CreateDbContext()
      Dim sql = File.ReadAllText("Sql\SQLiteDbUninitialize.sql")
      ExecuteSql(db, sql)
    End Using
  End Sub

  Private Sub ExecuteSql(db As BaseTestDbContext, sql As String)
    ' this regex doesn't cover all valid GO use cases, but it's good enough for now
    Dim sqls = Regex.Split(sql, "\r\nGO\r\n", RegexOptions.Multiline)

    For Each s In sqls
      db.Execute(s)
    Next
  End Sub

  Public Function CreateRawValueComparer() As RawValueComparer Implements ITestEnvironment.CreateRawValueComparer
    Return New SQliteRawValueComparer()
  End Function

End Class
