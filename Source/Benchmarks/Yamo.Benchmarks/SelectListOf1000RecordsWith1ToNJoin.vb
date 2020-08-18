Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore

Public Class SelectListOf1000RecordsWith1ToNJoin
  Inherits BenchmarkBase

  <Benchmark(Description:="Dapper")>
  Public Function Dapper() As List(Of Blog)
    Dim sql = "select * from blog inner join comment on blog.id = comment.blogid"
    Dim blogDict = New Dictionary(Of Int32, Blog)
    Return Me.Connection.Query(Of Blog, Comment, Blog)(sql, Function(b, c)
                                                              Dim blog As Blog = Nothing

                                                              If Not blogDict.TryGetValue(b.Id, blog) Then
                                                                blog = b
                                                                blog.Comments = New List(Of Comment)
                                                                blogDict.Add(blog.Id, blog)
                                                              End If

                                                              blog.Comments.Add(c)
                                                              Return blog
                                                            End Function).Distinct().ToList()
  End Function

  <Benchmark(Description:="EF Core")>
  Public Function EFCore() As List(Of Blog)
    Using db = New EFCoreDbContext(Me.Connection)
      Return db.Blogs.Include(Function(x) x.Comments).ToList()
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As List(Of Blog)
    Using db = New EFCoreDbContext(Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.Include(Function(x) x.Comments).ToList()
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As List(Of Blog)
    Using db = New YamoDbContext(Me.Connection)
      Return db.From(Of Blog).
                Join(Of Comment)(Function(b, c) b.Id = c.BlogId).
                SelectAll().ToList()
    End Using
  End Function

End Class
