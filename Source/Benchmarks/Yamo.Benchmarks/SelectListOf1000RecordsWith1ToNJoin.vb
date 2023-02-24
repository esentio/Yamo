Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore
Imports System.Data.Common

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
    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      Return db.Blogs.Include(Function(x) x.Comments).ToList()
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As List(Of Blog)
    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.Include(Function(x) x.Comments).ToList()
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As List(Of Blog)
    Using db = New YamoDbContext(CurrentMode, Me.Connection)
      Return db.From(Of Blog).
                Join(Of Comment)(Function(b, c) b.Id = c.BlogId).
                SelectAll().ToList()
    End Using
  End Function

  <Benchmark(Description:="Handcoded")>
  Public Function Handcoded() As List(Of Blog)
    Dim list = New List(Of Blog)

    Dim blogDict = New Dictionary(Of Int32, Blog)

    Using command = CreateCommand()
      Using dataReader = command.ExecuteReader()
        While dataReader.Read()
          Dim blogId = dataReader.GetInt32(0)

          Dim blog As Blog = Nothing

          If Not blogDict.TryGetValue(blogId, blog) Then
            blog = New Blog

            blog.Id = blogId
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

            blog.Comments = New List(Of Comment)

            blogDict.Add(blogId, blog)

            list.Add(blog)
          End If

          Dim comment = New Comment

          comment.Id = dataReader.GetInt32(9)
          comment.BlogId = dataReader.GetInt32(10)
          comment.Content = dataReader.GetString(11)
          comment.Created = dataReader.GetDateTime(12)
          comment.CreatedUserId = dataReader.GetInt32(13)

          blog.Comments.Add(comment)
        End While
      End Using
    End Using

    Return list
  End Function

  Private Function CreateCommand() As DbCommand
    Dim command = Me.Connection.CreateCommand()
    command.CommandText = "SELECT b.Id, b.Title, b.Content, b.Created, b.CreatedUserId, b.Modified, b.ModifiedUserId, b.Deleted, b.DeletedUserId, c.Id, c.BlogId, c.Content, c.Created, c.CreatedUserId FROM Blog b INNER JOIN Comment c ON b.Id = c.BlogId"
    Return command
  End Function

End Class
