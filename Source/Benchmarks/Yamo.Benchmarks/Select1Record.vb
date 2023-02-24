Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore
Imports System.Data.Common

Public Class Select1Record
  Inherits BenchmarkBase

  <Benchmark(Description:="Dapper")>
  Public Function Dapper() As Blog
    Return Me.Connection.QuerySingle(Of Blog)("select * from blog where id = @Id", New With {.Id = 42})
  End Function

  <Benchmark(Description:="EF Core")>
  Public Function EFCore() As Blog
    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      Return db.Blogs.Single(Function(x) x.Id = 42)
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As Blog
    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.Single(Function(x) x.Id = 42)
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As Blog
    Using db = New YamoDbContext(CurrentMode, Me.Connection)
      Return db.From(Of Blog).Where(Function(x) x.Id = 42).SelectAll().FirstOrDefault()
    End Using
  End Function

  <Benchmark(Description:="Yamo (using query)")>
  Public Function YamoQuery() As Blog
    Using db = New YamoDbContext(CurrentMode, Me.Connection)
      Return db.QueryFirstOrDefault(Of Blog)($"SELECT {Sql.Model.Columns(Of Blog)} FROM Blog WHERE Id = {42}")
    End Using
  End Function

  <Benchmark(Description:="Handcoded")>
  Public Function Handcoded() As Blog
    Using command = CreateCommand()
      Using dataReader = command.ExecuteReader()
        If dataReader.Read() Then
          Dim blog = New Blog

          blog.Id = dataReader.GetInt32(0)
          blog.Title = dataReader.GetString(1)
          blog.Content = dataReader.GetString(2)
          blog.Created = dataReader.GetDateTime(3)
          blog.CreatedUserId = dataReader.GetInt32(4)

          If dataReader.IsDBNull(5) Then
            blog.Modified = Nothing
          Else
            blog.Modified = dataReader.GetDateTime(5)
          End If

          If dataReader.IsDBNull(6) Then
            blog.ModifiedUserId = Nothing
          Else
            blog.ModifiedUserId = dataReader.GetInt32(6)
          End If

          If dataReader.IsDBNull(7) Then
            blog.Deleted = Nothing
          Else
            blog.Deleted = dataReader.GetDateTime(7)
          End If

          If dataReader.IsDBNull(8) Then
            blog.DeletedUserId = Nothing
          Else
            blog.DeletedUserId = dataReader.GetInt32(8)
          End If

          Return blog
        Else
          Return Nothing
        End If
      End Using
    End Using
  End Function

  Private Function CreateCommand() As DbCommand
    Dim command = Me.Connection.CreateCommand()
    command.CommandText = "SELECT Id, Title, Content, Created, CreatedUserId, Modified, ModifiedUserId, Deleted, DeletedUserId FROM Blog WHERE Id = @id"

    Dim parameter = command.CreateParameter()
    parameter.ParameterName = "@id"
    parameter.Value = 42
    command.Parameters.Add(parameter)

    Return command
  End Function

End Class
