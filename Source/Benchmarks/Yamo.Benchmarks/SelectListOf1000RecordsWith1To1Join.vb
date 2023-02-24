Imports BenchmarkDotNet.Attributes
Imports Yamo.Benchmarks.Model
Imports Dapper
Imports Microsoft.EntityFrameworkCore
Imports System.Data.Common

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
    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      Return db.Blogs.Include(Function(x) x.Author).ToList()
    End Using
  End Function

  <Benchmark(Description:="EF Core (no tracking)")>
  Public Function EFCoreNoTracking() As List(Of Blog)
    Using db = New EFCoreDbContext(CurrentMode, Me.Connection)
      db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking
      Return db.Blogs.Include(Function(x) x.Author).ToList()
    End Using
  End Function

  <Benchmark(Description:="Yamo", Baseline:=True)>
  Public Function Yamo() As List(Of Blog)
    Using db = New YamoDbContext(CurrentMode, Me.Connection)
      Return db.From(Of Blog).
                Join(Of User)(Function(b, u) b.CreatedUserId = u.Id).
                SelectAll().ToList()
    End Using
  End Function

  <Benchmark(Description:="Handcoded")>
  Public Function Handcoded() As List(Of Blog)
    Dim list = New List(Of Blog)

    Using command = CreateCommand()
      Using dataReader = command.ExecuteReader()
        While dataReader.Read()
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

          Dim user = New User

          user.Id = dataReader.GetInt32(9)
          user.Login = dataReader.GetString(10)
          user.FirstName = dataReader.GetString(11)
          user.LastName = dataReader.GetString(12)
          user.Email = dataReader.GetString(13)

          blog.Author = user

          list.Add(blog)
        End While
      End Using
    End Using

    Return list
  End Function

  Private Function CreateCommand() As DbCommand
    Dim command = Me.Connection.CreateCommand()
    command.CommandText = "SELECT b.Id, b.Title, b.Content, b.Created, b.CreatedUserId, b.Modified, b.ModifiedUserId, b.Deleted, b.DeletedUserId, u.Id, u.Login, u.FirstName, u.LastName, u.Email FROM Blog b INNER JOIN [User] u ON b.CreatedUserId = u.Id"
    Return command
  End Function

End Class
