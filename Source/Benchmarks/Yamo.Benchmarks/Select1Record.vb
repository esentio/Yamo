Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore

Public Class Select1Record
  Inherits BenchmarkBase

  <Benchmark(Description:="Dapper")>
  Public Function Dapper() As Blog
    Return Me.Connection.QuerySingle(Of Blog)("select * from blog where id = @Id", New With {.Id = 42})
  End Function

  <Benchmark(Description:="EF Core")>
  Public Function EFCore() As Blog
    Using db = New EFCoreDbContext(Me.Connection)
      Return db.Blogs.Single(Function(x) x.Id = 42)
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As Blog
    Using db = New EFCoreDbContext(Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.Single(Function(x) x.Id = 42)
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As Blog
    Using db = New YamoDbContext(Me.Connection)
      Return db.From(Of Blog).Where(Function(x) x.Id = 42).SelectAll().FirstOrDefault()
    End Using
  End Function

  <Benchmark(Description:="Yamo (using query)")>
  Public Function YamoQuery() As Blog
    Using db = New YamoDbContext(Me.Connection)
      Return db.QueryFirstOrDefault(Of Blog)($"SELECT {Sql.Model.Columns(Of Blog)} FROM Blog WHERE Id = {42}")
    End Using
  End Function

End Class
