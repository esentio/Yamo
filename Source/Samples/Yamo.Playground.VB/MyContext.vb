Imports Yamo

Public Class MyContext
  Inherits DbContext

  Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Blog)() '.ToTable("tbl_Blog")

    modelBuilder.Entity(Of Blog).Property(Function(b) b.Id).IsKey()
    modelBuilder.Entity(Of Blog).Property(Function(b) b.Title)
    modelBuilder.Entity(Of Blog).Property(Function(b) b.Content) '.HasColumnName("sContent")
    modelBuilder.Entity(Of Blog).Property(Function(b) b.PublishDate)
    modelBuilder.Entity(Of Blog).Property(Function(b) b.Rating)
    modelBuilder.Entity(Of Blog).Property(Function(b) b.ExtraRating)

    modelBuilder.Entity(Of Blog).HasMany(Function(b) b.Comments)

    modelBuilder.Entity(Of Comment)(Sub(cb)
                                      cb.Property(Function(c) c.Id).IsKey()
                                      cb.Property(Function(c) c.BlogId)
                                      cb.Property(Function(c) c.UserId)
                                      cb.Property(Function(c) c.Content)
                                      cb.Property(Function(c) c.PublishDate)
                                    End Sub)

    modelBuilder.Entity(Of Comment).HasOne(Function(b) b.Blog)

    '--------

    modelBuilder.Entity(Of Article)()
    modelBuilder.Entity(Of Article).Property(Function(a) a.Id).IsKey()
    modelBuilder.Entity(Of Article).Property(Function(a) a.Price)

    modelBuilder.Entity(Of Article).HasOne(Function(b) b.Label)
    modelBuilder.Entity(Of Article).HasMany(Function(b) b.Parts)


    modelBuilder.Entity(Of ArticlePart)()
    modelBuilder.Entity(Of ArticlePart).Property(Function(a) a.Id).IsKey()
    modelBuilder.Entity(Of ArticlePart).Property(Function(a) a.ArticleId)
    modelBuilder.Entity(Of ArticlePart).Property(Function(a) a.Price)

    modelBuilder.Entity(Of Article).HasOne(Function(b) b.Label)


    modelBuilder.Entity(Of Label)()
    modelBuilder.Entity(Of Label).Property(Function(a) a.TableId).IsKey()
    modelBuilder.Entity(Of Label).Property(Function(a) a.Id).IsKey()
    modelBuilder.Entity(Of Label).Property(Function(a) a.Language)
    modelBuilder.Entity(Of Label).Property(Function(a) a.Description)

  End Sub

  Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    optionsBuilder.UseSqlServer(Program.Connection)
  End Sub

End Class
