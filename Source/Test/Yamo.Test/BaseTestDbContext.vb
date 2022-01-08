Imports System.Data
Imports System.Data.Common
Imports Yamo.Test.Model

Public Class BaseTestDbContext
  Inherits DbContext

  Public Property UserId As Int32

  Private m_LastCommandText As String

  Public Function GetLastCommandText() As String
    Return m_LastCommandText
  End Function

  Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
    CreateArticleModel(modelBuilder)
    CreateArticleArchiveModel(modelBuilder)
    CreateArticleCategoryModel(modelBuilder)
    CreateArticlePartModel(modelBuilder)
    CreateArticleSubstitutionModel(modelBuilder)
    CreateCategoryModel(modelBuilder)
    CreateItemWithAllSupportedValuesModel(modelBuilder)
    CreateItemWithAllSupportedValuesArchiveModel(modelBuilder)
    CreateItemWithAuditFieldsModel(modelBuilder)
    CreateItemWithAuditFieldsArchiveModel(modelBuilder)
    CreateItemWithDefaultValueIdModel(modelBuilder)
    CreateItemWithDefaultValueIdArchiveModel(modelBuilder)
    CreateItemWithIdentityIdModel(modelBuilder)
    CreateItemWithIdentityIdArchiveModel(modelBuilder)
    CreateItemWithIdentityIdAndDefaultValuesModel(modelBuilder)
    CreateItemWithIdentityIdAndDefaultValuesArchiveModel(modelBuilder)
    CreateItemWithPropertyModifiedTrackingModel(modelBuilder)
    CreateItemWithPropertyModifiedTrackingArchiveModel(modelBuilder)
    CreateLabelModel(modelBuilder)
    CreateLabelArchiveModel(modelBuilder)
    CreateLinkedItemModel(modelBuilder)
    CreateLinkedItemArchiveModel(modelBuilder)
    CreateLinkedItemWithShuffledPropertiesModel(modelBuilder)
    CreateLinkedItemChildModel(modelBuilder)
    CreateItemInSchemaModel(modelBuilder)
  End Sub

  Private Sub CreateArticleModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Article)()

    modelBuilder.Entity(Of Article).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of Article).Property(Function(x) x.Price)

    modelBuilder.Entity(Of Article).HasOne(Function(x) x.Label)
    modelBuilder.Entity(Of Article).HasMany(Function(x) x.Parts)
    modelBuilder.Entity(Of Article).HasMany(Function(x) x.Categories)
  End Sub

  Private Sub CreateArticleArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticleArchive)()

    modelBuilder.Entity(Of ArticleArchive).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ArticleArchive).Property(Function(x) x.Price)

    modelBuilder.Entity(Of Article).HasOne(Function(x) x.Label)
    modelBuilder.Entity(Of Article).HasMany(Function(x) x.Parts)
    modelBuilder.Entity(Of Article).HasMany(Function(x) x.Categories)
  End Sub

  Private Sub CreateArticleCategoryModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticleCategory)()

    modelBuilder.Entity(Of ArticleCategory).Property(Function(x) x.ArticleId).IsKey()
    modelBuilder.Entity(Of ArticleCategory).Property(Function(x) x.CategoryId).IsKey()
  End Sub

  Private Sub CreateArticlePartModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticlePart)()

    modelBuilder.Entity(Of ArticlePart).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ArticlePart).Property(Function(x) x.ArticleId)
    modelBuilder.Entity(Of ArticlePart).Property(Function(x) x.Price)

    modelBuilder.Entity(Of ArticlePart).HasOne(Function(x) x.Label)
  End Sub

  Private Sub CreateArticleSubstitutionModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticleSubstitution)()

    modelBuilder.Entity(Of ArticleSubstitution).Property(Function(x) x.OriginalArticleId).IsKey()
    modelBuilder.Entity(Of ArticleSubstitution).Property(Function(x) x.SubstitutionArticleId).IsKey()

    modelBuilder.Entity(Of ArticleSubstitution).HasOne(Function(x) x.Original)
    modelBuilder.Entity(Of ArticleSubstitution).HasOne(Function(x) x.Substitution)
  End Sub

  Private Sub CreateCategoryModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Category)()

    modelBuilder.Entity(Of Category).Property(Function(x) x.Id).IsKey()

    modelBuilder.Entity(Of Category).HasOne(Function(x) x.Label)
  End Sub

  Private Sub CreateItemWithAllSupportedValuesModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithAllSupportedValues)()

    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.UniqueidentifierColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.UniqueidentifierColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Nvarchar50Column).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Nvarchar50ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.NvarcharMaxColumn).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.NvarcharMaxColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BitColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BitColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.SmallintColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.SmallintColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.IntColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.IntColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BigintColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BigintColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.RealColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.RealColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.FloatColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.FloatColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric10and3Column)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric10and3ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric15and0Column)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric15and0ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.DateColumn).UseDbType(DbType.Date)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.DateColumnNull).UseDbType(DbType.Date)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.TimeColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.TimeColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.DatetimeColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.DatetimeColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Datetime2Column).UseDbType(DbType.DateTime2)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Datetime2ColumnNull).UseDbType(DbType.DateTime2)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Varbinary50Column).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Varbinary50ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.VarbinaryMaxColumn).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.VarbinaryMaxColumnNull)
  End Sub

  Private Sub CreateItemWithAllSupportedValuesArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive)()

    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.UniqueidentifierColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.UniqueidentifierColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Nvarchar50Column).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Nvarchar50ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.NvarcharMaxColumn).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.NvarcharMaxColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.BitColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.BitColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.SmallintColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.SmallintColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.IntColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.IntColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.BigintColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.BigintColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.RealColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.RealColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.FloatColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.FloatColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Numeric10and3Column)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Numeric10and3ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Numeric15and0Column)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Numeric15and0ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.DateColumn).UseDbType(DbType.Date)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.DateColumnNull).UseDbType(DbType.Date)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.TimeColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.TimeColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.DatetimeColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.DatetimeColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Datetime2Column).UseDbType(DbType.DateTime2)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Datetime2ColumnNull).UseDbType(DbType.DateTime2)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Varbinary50Column).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.Varbinary50ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.VarbinaryMaxColumn).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValuesArchive).Property(Function(x) x.VarbinaryMaxColumnNull)
  End Sub

  Private Sub CreateItemWithAuditFieldsModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithAuditFields)()

    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Created).SetOnInsertTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.CreatedUserId).SetOnInsertTo(Function(c As BaseTestDbContext) c.UserId)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Modified).SetOnUpdateTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.ModifiedUserId).SetOnUpdateTo(Function(c As BaseTestDbContext) c.UserId)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Deleted).SetOnDeleteTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.DeletedUserId).SetOnDeleteTo(Function(c As BaseTestDbContext) c.UserId)
  End Sub

  Private Sub CreateItemWithAuditFieldsArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive)()

    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.Created).SetOnInsertTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.CreatedUserId).SetOnInsertTo(Function(c As BaseTestDbContext) c.UserId)
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.Modified).SetOnUpdateTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.ModifiedUserId).SetOnUpdateTo(Function(c As BaseTestDbContext) c.UserId)
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.Deleted).SetOnDeleteTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFieldsArchive).Property(Function(x) x.DeletedUserId).SetOnDeleteTo(Function(c As BaseTestDbContext) c.UserId)
  End Sub

  Private Sub CreateItemWithDefaultValueIdModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithDefaultValueId)()

    modelBuilder.Entity(Of ItemWithDefaultValueId).Property(Function(x) x.Id).IsKey().HasDefaultValue()
    modelBuilder.Entity(Of ItemWithDefaultValueId).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemWithDefaultValueIdArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithDefaultValueIdArchive)()

    modelBuilder.Entity(Of ItemWithDefaultValueIdArchive).Property(Function(x) x.Id).IsKey().HasDefaultValue()
    modelBuilder.Entity(Of ItemWithDefaultValueIdArchive).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemWithIdentityIdModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithIdentityId)()

    modelBuilder.Entity(Of ItemWithIdentityId).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithIdentityId).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemWithIdentityIdArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithIdentityIdArchive)()

    modelBuilder.Entity(Of ItemWithIdentityIdArchive).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithIdentityIdArchive).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemWithIdentityIdAndDefaultValuesModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues)()

    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.UniqueidentifierValue).HasDefaultValue()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.IntValue).HasDefaultValue()
  End Sub

  Private Sub CreateItemWithIdentityIdAndDefaultValuesArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValuesArchive)()

    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValuesArchive).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValuesArchive).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValuesArchive).Property(Function(x) x.UniqueidentifierValue).HasDefaultValue()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValuesArchive).Property(Function(x) x.IntValue).HasDefaultValue()
  End Sub

  Private Sub CreateItemWithPropertyModifiedTrackingModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking)()

    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking).Property(Function(x) x.IntValue)
  End Sub

  Private Sub CreateItemWithPropertyModifiedTrackingArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithPropertyModifiedTrackingArchive)()

    modelBuilder.Entity(Of ItemWithPropertyModifiedTrackingArchive).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithPropertyModifiedTrackingArchive).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithPropertyModifiedTrackingArchive).Property(Function(x) x.IntValue)
  End Sub

  Private Sub CreateLabelModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Label)()

    modelBuilder.Entity(Of Label).Property(Function(x) x.TableId).IsKey().IsRequired()
    modelBuilder.Entity(Of Label).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of Label).Property(Function(x) x.Language).IsKey().IsRequired()
    modelBuilder.Entity(Of Label).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateLabelArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LabelArchive)()

    modelBuilder.Entity(Of LabelArchive).Property(Function(x) x.TableId).IsKey().IsRequired()
    modelBuilder.Entity(Of LabelArchive).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of LabelArchive).Property(Function(x) x.Language).IsKey().IsRequired()
    modelBuilder.Entity(Of LabelArchive).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateLinkedItemModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItem)()

    modelBuilder.Entity(Of LinkedItem).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of LinkedItem).Property(Function(x) x.PreviousId)
    modelBuilder.Entity(Of LinkedItem).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateLinkedItemArchiveModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItemArchive)()

    modelBuilder.Entity(Of LinkedItemArchive).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of LinkedItemArchive).Property(Function(x) x.PreviousId)
    modelBuilder.Entity(Of LinkedItemArchive).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateLinkedItemWithShuffledPropertiesModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).ToTable("LinkedItem")

    ' properties are shuffled and PK property is defined last
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).Property(Function(x) x.PreviousId)
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).Property(Function(x) x.Id).IsKey()
  End Sub

  Private Sub CreateLinkedItemChildModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItemChild)()

    modelBuilder.Entity(Of LinkedItemChild).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of LinkedItemChild).Property(Function(x) x.LinkedItemId)
    modelBuilder.Entity(Of LinkedItemChild).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemInSchemaModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemInSchema)().ToTable(NameOf(ItemInSchema), "test_schema")

    modelBuilder.Entity(Of ItemInSchema).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ItemInSchema).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemInSchema).Property(Function(x) x.RelatedItemId)
    modelBuilder.Entity(Of ItemInSchema).Property(Function(x) x.Deleted).SetOnDeleteTo(Function() Helpers.Calendar.Now)
  End Sub

  Protected Overrides Sub OnCommandExecuting(command As DbCommand)
    m_LastCommandText = command.CommandText
    MyBase.OnCommandExecuting(command)
  End Sub

End Class
