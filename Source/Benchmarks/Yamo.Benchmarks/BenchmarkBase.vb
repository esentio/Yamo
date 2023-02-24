Imports System.Data.Common
Imports System.IO
Imports System.Text.RegularExpressions
Imports BenchmarkDotNet.Attributes
Imports BenchmarkDotNet.Configs
Imports BenchmarkDotNet.Engines
Imports Microsoft.Data.SqlClient
Imports Microsoft.Data.Sqlite
Imports Microsoft.EntityFrameworkCore.DbLoggerCategory.Database
Imports Microsoft.Extensions.Options

'<SimpleJob(launchCount:=1, warmupCount:=3, targetCount:=5, invocationCount:=100, id:="QuickJob")>
'<SimpleJob(1, 1, 1, 1)>
<Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)>
<RankColumn()>
<MemoryDiagnoser()>
Public MustInherit Class BenchmarkBase

  Protected Const CurrentMode As Mode = Mode.SQLite

  Protected Property Connection As DbConnection
  <GlobalSetup>
  Public Sub Setup()
    If CurrentMode = Mode.SqlServer Then
      Me.Connection = New SqlConnection("Server=WIN10DEV01;Database=YamoBenchmark;User Id=dbuser;Password=dbpassword;TrustServerCertificate=True;")
      Me.Connection.Open()
      InitializeDatabase("Sql\SqlServerDbInitialize.sql")
    ElseIf CurrentMode = Mode.SQLite Then
      Me.Connection = New SqliteConnection("Data Source=:memory:;")
      Me.Connection.Open()
      InitializeDatabase("Sql\SQLiteDbInitialize.sql")
    Else
      Throw New NotSupportedException()
    End If
  End Sub

  Private Sub InitializeDatabase(path As String)
    Dim sql = File.ReadAllText(path)
    Dim sqls = Regex.Split(sql, "\r\nGO\r\n", RegexOptions.Multiline)

    For Each s In sqls
      ExecuteCommand(Me.Connection, s)
    Next
  End Sub

  Private Sub ExecuteCommand(connection As DbConnection, sql As String)
    Using command = CreateCommand(connection, sql)
      command.ExecuteNonQuery()
    End Using
  End Sub

  Private Function CreateCommand(connection As DbConnection, sql As String) As DbCommand
    Dim command = connection.CreateCommand()
    command.CommandText = sql
    Return command
  End Function

End Class
