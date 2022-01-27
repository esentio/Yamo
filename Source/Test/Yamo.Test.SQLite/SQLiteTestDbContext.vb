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
    CreateItemWithOnlySQLiteSupportedFieldsModel(modelBuilder)
    CreateItemWithOnlySQLiteSupportedFieldsArchiveModel(modelBuilder)
  End Sub

  Private Sub CreateItemWithOnlySQLiteSupportedFieldsModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields)()

    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.DateOnlyColumn)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.DateOnlyColumnNull)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.TimeOnlyColumn)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.TimeOnlyColumnNull)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.Nchar1Column)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFields).Property(Function(x) x.Nchar1ColumnNull)
  End Sub

  Private Sub CreateItemWithOnlySQLiteSupportedFieldsArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive)()

    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.DateOnlyColumn)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.DateOnlyColumnNull)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.TimeOnlyColumn)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.TimeOnlyColumnNull)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.Nchar1Column)
    modelBuilder.Entity(Of ItemWithOnlySQLiteSupportedFieldsArchive).Property(Function(x) x.Nchar1ColumnNull)
  End Sub

  Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    optionsBuilder.UseSQLite(m_Connection)
  End Sub

  Protected Overrides Sub OnCommandExecuting(command As DbCommand)
    MyBase.OnCommandExecuting(command)
  End Sub

End Class
