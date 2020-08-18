Imports Microsoft.Data.SqlClient
Imports Yamo.Benchmarks.Model

Namespace Yamo

  Public Class YamoDbContext
    Inherits DbContext

    Private m_Connection As SqlConnection

    Sub New(connection As SqlConnection)
      m_Connection = connection
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
      CreateBlogModel(modelBuilder)
      CreateCommentModel(modelBuilder)
      CreateUserModel(modelBuilder)
    End Sub

    Private Sub CreateBlogModel(modelBuilder As ModelBuilder)
      modelBuilder.Entity(Of Blog)()

      modelBuilder.Entity(Of Blog).Property(Function(x) x.Id).IsKey().IsIdentity()
      modelBuilder.Entity(Of Blog).Property(Function(x) x.Title).IsRequired()
      modelBuilder.Entity(Of Blog).Property(Function(x) x.Content).IsRequired()
      modelBuilder.Entity(Of Blog).Property(Function(x) x.Created)
      modelBuilder.Entity(Of Blog).Property(Function(x) x.CreatedUserId)
      modelBuilder.Entity(Of Blog).Property(Function(x) x.Modified)
      modelBuilder.Entity(Of Blog).Property(Function(x) x.ModifiedUserId)
      modelBuilder.Entity(Of Blog).Property(Function(x) x.Deleted)
      modelBuilder.Entity(Of Blog).Property(Function(x) x.DeletedUserId)

      modelBuilder.Entity(Of Blog).HasOne(Function(x) x.Author)
      modelBuilder.Entity(Of Blog).HasMany(Function(x) x.Comments)
    End Sub

    Private Sub CreateCommentModel(modelBuilder As ModelBuilder)
      modelBuilder.Entity(Of Comment)()

      modelBuilder.Entity(Of Comment).Property(Function(x) x.Id).IsKey().IsIdentity()
      modelBuilder.Entity(Of Comment).Property(Function(x) x.BlogId)
      modelBuilder.Entity(Of Comment).Property(Function(x) x.Content).IsRequired()
      modelBuilder.Entity(Of Comment).Property(Function(x) x.Created)
      modelBuilder.Entity(Of Comment).Property(Function(x) x.CreatedUserId)
    End Sub

    Private Sub CreateUserModel(modelBuilder As ModelBuilder)
      modelBuilder.Entity(Of User)()

      modelBuilder.Entity(Of User).Property(Function(x) x.Id).IsKey().IsIdentity()
      modelBuilder.Entity(Of User).Property(Function(x) x.Login).IsRequired()
      modelBuilder.Entity(Of User).Property(Function(x) x.FirstName).IsRequired()
      modelBuilder.Entity(Of User).Property(Function(x) x.LastName).IsRequired()
      modelBuilder.Entity(Of User).Property(Function(x) x.Email).IsRequired()
    End Sub

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
      optionsBuilder.UseSqlServer(m_Connection)
    End Sub

  End Class
End Namespace
