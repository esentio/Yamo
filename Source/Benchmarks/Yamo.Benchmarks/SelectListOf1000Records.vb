Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore

Public Class SelectListOf1000Records
  Inherits BenchmarkBase

  <Benchmark(Description:="Dapper")>
  Public Function Dapper() As List(Of Blog)
    Return Me.Connection.Query(Of Blog)("select * from blog").ToList()
  End Function

  <Benchmark(Description:="EF Core")>
  Public Function EFCore() As List(Of Blog)
    Using db = New EFCoreDbContext(Me.Connection)
      Return db.Blogs.ToList()
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As List(Of Blog)
    Using db = New EFCoreDbContext(Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.ToList()
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As List(Of Blog)
    Using db = New YamoDbContext(Me.Connection)
      Return db.From(Of Blog).SelectAll().ToList()
    End Using
  End Function

  <Benchmark(Description:="Yamo (using query)")>
  Public Function YamoQuery() As List(Of Blog)
    Using db = New YamoDbContext(Me.Connection)
      Return db.Query(Of Blog)($"SELECT {Sql.Model.Columns(Of Blog)} FROM Blog")
    End Using
  End Function

End Class
