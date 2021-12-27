Namespace Model

  Public Class ModelFactory

    Public Overridable Function CreateArticle(id As Int32) As Article
      Return New Article With {
        .Id = id,
        .Price = Helpers.Data.CreateRandomPositiveDecimal(5, 2)
      }
    End Function

    Public Overridable Function CreateArticle(id As Int32, price As Decimal) As Article
      Return New Article With {
        .Id = id,
        .Price = price
      }
    End Function

    Public Overridable Function CreateArticleCategory(articleId As Int32, categoryId As Int32) As ArticleCategory
      Return New ArticleCategory With {
        .ArticleId = articleId,
        .CategoryId = categoryId
      }
    End Function

    Public Overridable Function CreateArticlePart(id As Int32, articleId As Int32) As ArticlePart
      Return New ArticlePart With {
        .Id = id,
        .ArticleId = articleId,
        .Price = Helpers.Data.CreateRandomPositiveDecimal(5, 2)
      }
    End Function

    Public Overridable Function CreateArticlePart(id As Int32, articleId As Int32, price As Decimal) As ArticlePart
      Return New ArticlePart With {
        .Id = id,
        .ArticleId = articleId,
        .Price = price
      }
    End Function

    Public Overridable Function CreateArticleSubstitution(originalArticleId As Int32, substitutionArticleId As Int32) As ArticleSubstitution
      Return New ArticleSubstitution With {
        .OriginalArticleId = originalArticleId,
        .SubstitutionArticleId = substitutionArticleId
      }
    End Function

    Public Overridable Function CreateCategory(id As Int32) As Category
      Return New Category With {
        .Id = id
      }
    End Function

    Public Overridable Function CreateItemWithAllSupportedValuesWithEmptyValues() As ItemWithAllSupportedValues
      Return New ItemWithAllSupportedValues With {
        .Id = Guid.NewGuid(),
        .UniqueidentifierColumn = Guid.Empty,
        .UniqueidentifierColumnNull = Nothing,
        .Nvarchar50Column = "",
        .Nvarchar50ColumnNull = Nothing,
        .NvarcharMaxColumn = "",
        .NvarcharMaxColumnNull = Nothing,
        .BitColumn = False,
        .BitColumnNull = Nothing,
        .SmallintColumn = 0,
        .SmallintColumnNull = Nothing,
        .IntColumn = 0,
        .IntColumnNull = Nothing,
        .BigintColumn = 0,
        .BigintColumnNull = Nothing,
        .RealColumn = 0,
        .RealColumnNull = Nothing,
        .FloatColumn = 0,
        .FloatColumnNull = Nothing,
        .Numeric10and3Column = 0,
        .Numeric10and3ColumnNull = Nothing,
        .Numeric15and0Column = 0,
        .Numeric15and0ColumnNull = Nothing,
        .DateColumn = Helpers.Calendar.Now().Date,
        .DateColumnNull = Nothing,
        .DatetimeColumn = Helpers.Calendar.Now(),
        .DatetimeColumnNull = Nothing,
        .Varbinary50Column = New Byte() {},
        .Varbinary50ColumnNull = Nothing,
        .VarbinaryMaxColumn = New Byte() {},
        .VarbinaryMaxColumnNull = Nothing
      }
    End Function

    Public Overridable Function CreateItemWithAllSupportedValuesWithMinValues() As ItemWithAllSupportedValues
      Return New ItemWithAllSupportedValues With {
        .Id = Guid.NewGuid(),
        .UniqueidentifierColumn = Guid.NewGuid(),
        .UniqueidentifierColumnNull = Guid.NewGuid(),
        .Nvarchar50Column = "",
        .Nvarchar50ColumnNull = "",
        .NvarcharMaxColumn = "",
        .NvarcharMaxColumnNull = "",
        .BitColumn = False,
        .BitColumnNull = False,
        .SmallintColumn = Int16.MinValue,
        .SmallintColumnNull = Int16.MinValue,
        .IntColumn = Int32.MinValue,
        .IntColumnNull = Int32.MinValue,
        .BigintColumn = Int32.MinValue,
        .BigintColumnNull = Int32.MinValue,
        .RealColumn = Single.MinValue,
        .RealColumnNull = Single.MinValue,
        .FloatColumn = Double.MinValue,
        .FloatColumnNull = Double.MinValue,
        .Numeric10and3Column = -9999999.999D,
        .Numeric10and3ColumnNull = -9999999.999D,
        .Numeric15and0Column = -999999999999999D,
        .Numeric15and0ColumnNull = -999999999999999D,
        .DateColumn = DateTime.MinValue.Date,
        .DateColumnNull = DateTime.MinValue.Date,
        .DatetimeColumn = Helpers.Calendar.GetSqlServerMinDate(),
        .DatetimeColumnNull = Helpers.Calendar.GetSqlServerMinDate(),
        .Varbinary50Column = New Byte() {},
        .Varbinary50ColumnNull = New Byte() {},
        .VarbinaryMaxColumn = New Byte() {},
        .VarbinaryMaxColumnNull = New Byte() {}
      }
    End Function

    Public Overridable Function CreateItemWithAllSupportedValuesWithMaxValues() As ItemWithAllSupportedValues
      Return New ItemWithAllSupportedValues With {
        .Id = Guid.NewGuid(),
        .UniqueidentifierColumn = Guid.NewGuid(),
        .UniqueidentifierColumnNull = Guid.NewGuid(),
        .Nvarchar50Column = "01234567890123456789012345678901234567890123456789",
        .Nvarchar50ColumnNull = "01234567890123456789012345678901234567890123456789",
        .NvarcharMaxColumn = Helpers.Data.CreateLargeRandomString(),
        .NvarcharMaxColumnNull = Helpers.Data.CreateLargeRandomString(),
        .BitColumn = True,
        .BitColumnNull = True,
        .SmallintColumn = Int16.MaxValue,
        .SmallintColumnNull = Int16.MaxValue,
        .IntColumn = Int32.MaxValue,
        .IntColumnNull = Int32.MaxValue,
        .BigintColumn = Int32.MaxValue,
        .BigintColumnNull = Int32.MaxValue,
        .RealColumn = Single.MaxValue,
        .RealColumnNull = Single.MaxValue,
        .FloatColumn = Double.MaxValue,
        .FloatColumnNull = Double.MaxValue,
        .Numeric10and3Column = 9999999.999D,
        .Numeric10and3ColumnNull = 9999999.999D,
        .Numeric15and0Column = 999999999999999D,
        .Numeric15and0ColumnNull = 999999999999999D,
        .DateColumn = DateTime.MaxValue.Date,
        .DateColumnNull = DateTime.MaxValue.Date,
        .DatetimeColumn = Helpers.Calendar.GetSqlServerMaxDate(),
        .DatetimeColumnNull = Helpers.Calendar.GetSqlServerMaxDate(),
        .Varbinary50Column = Helpers.Data.CreateRandomByteArray(50),
        .Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(50),
        .VarbinaryMaxColumn = Helpers.Data.CreateRandomByteArray(100000),
        .VarbinaryMaxColumnNull = Helpers.Data.CreateRandomByteArray(100000)
      }
    End Function

    Public Overridable Function CreateItemWithAuditFields() As ItemWithAuditFields
      Return New ItemWithAuditFields With {
        .Description = Helpers.Data.CreateRandomString(50)
      }
    End Function

    Public Overridable Function CreateItemWithDefaultValueId() As ItemWithDefaultValueId
      Return New ItemWithDefaultValueId With {
        .Description = Helpers.Data.CreateRandomString(50)
      }
    End Function

    Public Overridable Function CreateItemWithDefaultValueId(id As Guid) As ItemWithDefaultValueId
      Return New ItemWithDefaultValueId With {
        .Id = id,
        .Description = Helpers.Data.CreateRandomString(50)
      }
    End Function

    Public Overridable Function CreateItemWithIdentityId() As ItemWithIdentityId
      Return New ItemWithIdentityId With {
        .Description = Helpers.Data.CreateRandomString(50)
      }
    End Function

    Public Overridable Function CreateItemWithIdentityId(id As Int32) As ItemWithIdentityId
      Return New ItemWithIdentityId With {
        .Id = id,
        .Description = Helpers.Data.CreateRandomString(50)
      }
    End Function

    Public Overridable Function CreateItemWithIdentityIdAndDefaultValues() As ItemWithIdentityIdAndDefaultValues
      Return New ItemWithIdentityIdAndDefaultValues With {
        .Description = Helpers.Data.CreateRandomString(50)
      }
    End Function

    Public Overridable Function CreateItemWithIdentityIdAndDefaultValues(id As Int32, uniqueidentifierValue As Guid, intValue As Int32) As ItemWithIdentityIdAndDefaultValues
      Return New ItemWithIdentityIdAndDefaultValues With {
        .Id = id,
        .Description = Helpers.Data.CreateRandomString(50),
        .UniqueidentifierValue = uniqueidentifierValue,
        .IntValue = intValue
      }
    End Function

    Public Overridable Function CreateItemWithPropertyModifiedTracking() As ItemWithPropertyModifiedTracking
      Return New ItemWithPropertyModifiedTracking With {
        .Description = Helpers.Data.CreateRandomString(50),
        .IntValue = Helpers.Data.CreateRandomInt32()
      }
    End Function

    Public Overridable Function CreateLabel(tableId As String, id As Int32, language As String) As Label
      Return New Label With {
        .TableId = tableId,
        .Id = id,
        .Language = language,
        .Description = $"{tableId}-{id}-{language}"
      }
    End Function

    Public Overridable Function CreateLabel(tableId As String, id As Int32, language As String, description As String) As Label
      Return New Label With {
        .TableId = tableId,
        .Id = id,
        .Language = language,
        .Description = description
      }
    End Function

    Public Overridable Function CreateLinkedItem(id As Int32, previousId As Int32?) As LinkedItem
      Return New LinkedItem With {
        .Id = id,
        .PreviousId = previousId,
        .Description = $"{id}-{previousId}"
      }
    End Function

    Public Overridable Function CreateLinkedItemWithShuffledProperties(id As Int32, previousId As Int32?) As LinkedItemWithShuffledProperties
      Return New LinkedItemWithShuffledProperties With {
        .Id = id,
        .PreviousId = previousId,
        .Description = $"{id}-{previousId}"
      }
    End Function

    Public Overridable Function CreateLinkedItemChild(id As Int32, linkedItemId As Int32) As LinkedItemChild
      Return New LinkedItemChild With {
        .Id = id,
        .LinkedItemId = linkedItemId,
        .Description = $"{id}-{linkedItemId}"
      }
    End Function

    Public Overridable Function CreateItemInSchema(id As Int32) As ItemInSchema
      Return New ItemInSchema With {
        .Id = id,
        .Description = Helpers.Data.CreateRandomString(50),
        .RelatedItemId = Nothing
      }
    End Function

  End Class
End Namespace
