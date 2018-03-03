Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper

Namespace Dapper

  Public Class DapperBenchmark
    Inherits BenchmarkBase

    <GlobalSetup>
    Public Overrides Sub Setup()
      MyBase.Setup()
    End Sub

    <Benchmark(Description:="Select list of 1000 records", Baseline:=True)>
    <BenchmarkCategory("SelectListOf1000Records")>
    Public Function SelectListOf1000Records() As List(Of Blog)
      Return Me.Connection.Query(Of Blog)("select * from blog").ToList()
    End Function

    <Benchmark(Description:="Select list of 1000 records with 1:1 join", Baseline:=True)>
    <BenchmarkCategory("SelectListOf1000RecordsWith1To1Join")>
    Public Function SelectListOf1000RecordsWith1To1Join() As List(Of Blog)
      Dim sql = "select * from blog inner join [user] on blog.createduserid = [user].id"
      Return Me.Connection.Query(Of Blog, User, Blog)(sql, Function(b, u)
                                                             b.Author = u
                                                             Return b
                                                           End Function).ToList()
    End Function

    <Benchmark(Description:="Select list of 1000 records with 1:N join", Baseline:=True)>
    <BenchmarkCategory("SelectListOf1000RecordsWith1ToNJoin")>
    Public Function SelectListOf1000RecordsWith1ToNJoin() As List(Of Blog)
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

    <Benchmark(Description:="Select 1 record", Baseline:=True)>
    <BenchmarkCategory("Select1Record")>
    Public Function Select1Record() As Blog
      Return Me.Connection.QuerySingle(Of Blog)("select * from blog where id = @Id", New With {.Id = 42})
    End Function

    <Benchmark(Description:="Select 500 records one by one", Baseline:=True)>
    <BenchmarkCategory("Select500RecordsOneByOne")>
    Public Function Select500RecordsOneByOne() As Blog()
      Dim records = New Blog(499) {}

      For i = 1 To records.Length
        records(i - 1) = Me.Connection.QuerySingle(Of Blog)("select * from blog where id = @Id", New With {.Id = i})
      Next

      Return records
    End Function

  End Class
End Namespace
