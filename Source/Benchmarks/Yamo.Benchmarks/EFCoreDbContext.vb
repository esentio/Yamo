Imports Yamo.Benchmarks.Model
Imports Microsoft.EntityFrameworkCore
Imports EF = Microsoft.EntityFrameworkCore
Imports Microsoft.Data.SqlClient

Public Class EFCoreDbContext
  Inherits EF.DbContext

  Private m_Connection As SqlConnection

  Public Property Blogs As DbSet(Of Blog)

  Sub New(connection As SqlConnection)
    m_Connection = connection
  End Sub

  Protected Overrides Sub OnModelCreating(modelBuilder As EF.ModelBuilder)
    CreateBlogModel(modelBuilder)
    CreateUserModel(modelBuilder)
  End Sub

  Private Sub CreateBlogModel(modelBuilder As EF.ModelBuilder)
    modelBuilder.Entity(Of Blog)().ToTable("Blog")

    modelBuilder.Entity(Of Blog).Property(Function(x) x.Id)
    modelBuilder.Entity(Of Blog).Property(Function(x) x.Title).IsRequired()
    modelBuilder.Entity(Of Blog).Property(Function(x) x.Content).IsRequired()
    modelBuilder.Entity(Of Blog).Property(Function(x) x.Created).IsRequired()
    modelBuilder.Entity(Of Blog).Property(Function(x) x.CreatedUserId).IsRequired()
    modelBuilder.Entity(Of Blog).Property(Function(x) x.Modified)
    modelBuilder.Entity(Of Blog).Property(Function(x) x.ModifiedUserId)
    modelBuilder.Entity(Of Blog).Property(Function(x) x.Deleted)
    modelBuilder.Entity(Of Blog).Property(Function(x) x.DeletedUserId)

    modelBuilder.Entity(Of Blog).HasKey(Function(x) x.Id)

    modelBuilder.Entity(Of Blog).HasOne(Function(x) x.Author).WithMany().HasForeignKey(Function(x) x.CreatedUserId)
    modelBuilder.Entity(Of Blog).HasMany(Function(x) x.Comments).WithOne().HasForeignKey(Function(x) x.BlogId)
  End Sub

  Private Sub CreateCommentModel(modelBuilder As EF.ModelBuilder)
    modelBuilder.Entity(Of Comment)().ToTable("Comment")

    modelBuilder.Entity(Of Comment).Property(Function(x) x.Id)
    modelBuilder.Entity(Of Comment).Property(Function(x) x.BlogId)
    modelBuilder.Entity(Of Comment).Property(Function(x) x.Content).IsRequired()
    modelBuilder.Entity(Of Comment).Property(Function(x) x.Created).IsRequired()
    modelBuilder.Entity(Of Comment).Property(Function(x) x.CreatedUserId).IsRequired()

    modelBuilder.Entity(Of Comment).HasKey(Function(x) x.Id)
  End Sub

  Private Sub CreateUserModel(modelBuilder As EF.ModelBuilder)
    modelBuilder.Entity(Of User)().ToTable("User")

    modelBuilder.Entity(Of User).Property(Function(x) x.Id)
    modelBuilder.Entity(Of User).Property(Function(x) x.Login).IsRequired()
    modelBuilder.Entity(Of User).Property(Function(x) x.FirstName).IsRequired()
    modelBuilder.Entity(Of User).Property(Function(x) x.LastName).IsRequired()
    modelBuilder.Entity(Of User).Property(Function(x) x.Email).IsRequired()

    modelBuilder.Entity(Of Blog).HasKey(Function(x) x.Id)
  End Sub

  Protected Overrides Sub OnConfiguring(optionsBuilder As EF.DbContextOptionsBuilder)
    optionsBuilder.UseSqlServer(m_Connection)
  End Sub

End Class
