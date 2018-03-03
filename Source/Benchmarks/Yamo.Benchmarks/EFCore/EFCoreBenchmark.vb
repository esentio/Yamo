Imports BenchmarkDotNet.Attributes
Imports Microsoft.EntityFrameworkCore
Imports Yamo.Benchmarks.Model

Namespace EFCore

  Public Class EFCoreBenchmark
    Inherits BenchmarkBase

    <GlobalSetup>
    Public Overrides Sub Setup()
      MyBase.Setup()
    End Sub

    <Benchmark(Description:="Select list of 1000 records")>
    <BenchmarkCategory("SelectListOf1000Records")>
    Public Function SelectListOf1000Records() As List(Of Blog)
      Using db = New EFCoreDbContext(Me.Connection)
        Return db.Blogs.ToList()
      End Using
    End Function

    <Benchmark(Description:="Select list of 1000 records with 1:1 join")>
    <BenchmarkCategory("SelectListOf1000RecordsWith1To1Join")>
    Public Function SelectListOf1000RecordsWith1To1Join() As List(Of Blog)
      Using db = New EFCoreDbContext(Me.Connection)
        Return db.Blogs.Include(Function(x) x.Author).ToList()
      End Using
    End Function

    <Benchmark(Description:="Select list of 1000 records with 1:N join")>
    <BenchmarkCategory("SelectListOf1000RecordsWith1ToNJoin")>
    Public Function SelectListOf1000RecordsWith1ToNJoin() As List(Of Blog)
      Using db = New EFCoreDbContext(Me.Connection)
        Return db.Blogs.Include(Function(x) x.Comments).ToList()
      End Using
    End Function

    <Benchmark(Description:="Select 1 record")>
    <BenchmarkCategory("Select1Record")>
    Public Function Select1Record() As Blog
      Using db = New EFCoreDbContext(Me.Connection)
        Return db.Blogs.Single(Function(x) x.Id = 42)
      End Using
    End Function

    <Benchmark(Description:="Select 500 records one by one")>
    <BenchmarkCategory("Select500RecordsOneByOne")>
    Public Function Select500RecordsOneByOne() As Blog()
      Dim records = New Blog(499) {}

      Using db = New EFCoreDbContext(Me.Connection)
        For i = 1 To records.Length
          Dim j = i
          records(i - 1) = db.Blogs.Single(Function(x) x.Id = j)
        Next
      End Using

      Return records
    End Function

  End Class
End Namespace
