Imports System.Data.Common
Imports Microsoft.Data.Sqlite
Imports Yamo.Test
Imports Yamo.Test.SQLite.Model

Public Class SQLiteTestDbContext
  Inherits BaseTestDbContext

  Private m_Connection As SqliteConnection

  Sub New(connection As SqliteConnection)
    m_Connection = connection
  End Sub

  Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
    MyBase.OnModelCreating(modelBuilder)
    CreateItemWithDateAndTimeOnlyFieldsModel(modelBuilder)
    CreateItemWithDateAndTimeOnlyFieldsArchiveModel(modelBuilder)
  End Sub

  Private Sub CreateItemWithDateAndTimeOnlyFieldsModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFields)()

    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFields).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFields).Property(Function(x) x.DateOnlyColumn)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFields).Property(Function(x) x.DateOnlyColumnNull)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFields).Property(Function(x) x.TimeOnlyColumn)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFields).Property(Function(x) x.TimeOnlyColumnNull)
  End Sub

  Private Sub CreateItemWithDateAndTimeOnlyFieldsArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFieldsArchive)()

    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFieldsArchive).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFieldsArchive).Property(Function(x) x.DateOnlyColumn)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFieldsArchive).Property(Function(x) x.DateOnlyColumnNull)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFieldsArchive).Property(Function(x) x.TimeOnlyColumn)
    modelBuilder.Entity(Of ItemWithDateAndTimeOnlyFieldsArchive).Property(Function(x) x.TimeOnlyColumnNull)
  End Sub

  Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    optionsBuilder.UseSQLite(m_Connection)
  End Sub

  Protected Overrides Sub OnCommandExecuting(command As DbCommand)
    MyBase.OnCommandExecuting(command)
  End Sub

End Class
