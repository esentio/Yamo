Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore
Imports System.Data.Common

Public Class Select500RecordsOneByOne
  Inherits BenchmarkBase

  <Benchmark(Description:="Dapper")>
  Public Function Dapper() As Blog()
    Dim records = New Blog(499) {}

    For i = 1 To records.Length
      records(i - 1) = Me.Connection.QuerySingle(Of Blog)("select * from blog where id = @Id", New With {.Id = i})
    Next

    Return records
  End Function

  <Benchmark(Description:="EF Core")>
  Public Function EFCore() As Blog()
    Dim records = New Blog(499) {}

    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      For i = 1 To records.Length
        Dim j = i
        records(i - 1) = db.Blogs.Single(Function(x) x.Id = j)
      Next
    End Using

    Return records
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As Blog()
    Dim records = New Blog(499) {}

    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking

      For i = 1 To records.Length
        Dim j = i
        records(i - 1) = db.Blogs.Single(Function(x) x.Id = j)
      Next
    End Using

    Return records
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As Blog()
    Dim records = New Blog(499) {}

    Using db = New YamoDbContext(CurrentMode, Me.Connection)
      For i = 1 To records.Length
        Dim j = i
        records(i - 1) = db.From(Of Blog).Where(Function(x) x.Id = j).SelectAll().FirstOrDefault()
      Next
    End Using

    Return records
  End Function

  <Benchmark(Description:="Yamo (using query)")>
  Public Function YamoQuery() As Blog()
    Dim records = New Blog(499) {}

    Using db = New YamoDbContext(CurrentMode, Me.Connection)
      For i = 1 To records.Length
        Dim j = i
        records(i - 1) = db.QueryFirstOrDefault(Of Blog)($"SELECT {Sql.Model.Columns(Of Blog)} FROM Blog WHERE Id = {j}")
      Next
    End Using

    Return records
  End Function

  <Benchmark(Description:="Handcoded")>
  Public Function Handcoded() As Blog()
    Dim records = New Blog(499) {}

    For i = 1 To records.Length
      Dim j = i

      Using command = CreateCommand(j)
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

            records(i - 1) = blog
          Else
            records(i - 1) = Nothing
          End If
        End Using
      End Using
    Next

    Return records
  End Function

  Private Function CreateCommand(id As Int32) As DbCommand
    Dim command = Me.Connection.CreateCommand()
    command.CommandText = "SELECT Id, Title, Content, Created, CreatedUserId, Modified, ModifiedUserId, Deleted, DeletedUserId FROM Blog WHERE Id = @id"

    Dim parameter = command.CreateParameter()
    parameter.ParameterName = "@id"
    parameter.Value = id
    command.Parameters.Add(parameter)

    Return command
  End Function

End Class
