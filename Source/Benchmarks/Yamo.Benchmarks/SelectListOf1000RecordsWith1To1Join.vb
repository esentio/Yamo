Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore

Public Class SelectListOf1000RecordsWith1To1Join
  Inherits BenchmarkBase

  <Benchmark(Description:="Dapper")>
  Public Function Dapper() As List(Of Blog)
    Dim sql = "select * from blog inner join [user] on blog.createduserid = [user].id"
    Return Me.Connection.Query(Of Blog, User, Blog)(sql, Function(b, u)
                                                           b.Author = u
                                                           Return b
                                                         End Function).ToList()
  End Function

  <Benchmark(Description:="EF Core")>
  Public Function EFCore() As List(Of Blog)
    Using db = New EFCoreDbContext(Me.Connection)
      Return db.Blogs.Include(Function(x) x.Author).ToList()
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As List(Of Blog)
    Using db = New EFCoreDbContext(Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.Include(Function(x) x.Author).ToList()
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As List(Of Blog)
    Using db = New YamoDbContext(Me.Connection)
      Return db.From(Of Blog).
                Join(Of User)(Function(b, u) b.CreatedUserId = u.Id).
                SelectAll().ToList()
    End Using
  End Function

End Class
