Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore

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

    Using db = New EFCoreDbContext(Me.Connection)
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

    Using db = New EFCoreDbContext(Me.Connection)
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

    Using db = New YamoDbContext(Me.Connection)
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

    Using db = New YamoDbContext(Me.Connection)
      For i = 1 To records.Length
        Dim j = i
        records(i - 1) = db.QueryFirstOrDefault(Of Blog)($"SELECT {Sql.Model.Columns(Of Blog)} FROM Blog WHERE Id = {j}")
      Next
    End Using

    Return records
  End Function

End Class
