Imports System.Data
Imports System.Data.Common
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Data.SqlClient
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

  Public Sub InitializeDatabase() Implements ITestEnvironment.InitializeDatabase
    Using db = CreateDbContext()
      Dim sql = File.ReadAllText("Sql\SqlServerDbInitialize.sql")
      ExecuteSql(db, sql)
    End Using
  End Sub

  Public Sub UninitializeDatabase() Implements ITestEnvironment.UninitializeDatabase
    Using db = CreateDbContext()
      Dim sql = File.ReadAllText("Sql\SqlServerDbUninitialize.sql")
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

  Public Function CreateDbParameter(value As Object, dbType As DbType) As DbParameter Implements ITestEnvironment.CreateDbParameter
    Return New SqlParameter() With {
      .Value = value,
      .DbType = dbType
    }
  End Function

  Public Function CreateRawValueComparer() As RawValueComparer Implements ITestEnvironment.CreateRawValueComparer
    Return New RawValueComparer()
  End Function

End Class
