Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeColumn()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        SelectAll().
                        Exclude(Function(x) x.IntColumn).
                        FirstOrDefault()

        Assert.AreEqual(0, result.IntColumn)

        item.IntColumn = 0

        ' check if remaining fields are loaded
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeIsRequiredColumns()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        SelectAll().
                        Exclude(Function(x) x.Nvarchar50Column).
                        Exclude(Function(x) x.Nvarchar50ColumnNull).
                        Exclude(Function(x) x.IntColumn).
                        Exclude(Function(x) x.IntColumnNull).
                        Exclude(Function(x) x.Varbinary50Column).
                        Exclude(Function(x) x.Varbinary50ColumnNull).
                        FirstOrDefault()

        Assert.AreEqual("", result.Nvarchar50Column)
        Assert.AreEqual(Nothing, result.Nvarchar50ColumnNull)
        Assert.AreEqual(0, result.IntColumn)
        Assert.AreEqual(New Int32?, result.IntColumnNull)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual({}, result.Varbinary50Column))
        Assert.AreEqual(Nothing, result.Varbinary50ColumnNull)

        item.Nvarchar50Column = ""
        item.Nvarchar50ColumnNull = Nothing
        item.IntColumn = 0
        item.IntColumnNull = Nothing
        item.Varbinary50Column = {}
        item.Varbinary50ColumnNull = Nothing

        ' check if remaining fields are loaded
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludePrimaryKeyColumn()
      Dim item = Me.ModelFactory.CreateLabel("table", 10, English, "lorem ipsum")

      InsertItems(item)

      Assert.ThrowsException(Of ArgumentException)(
        Sub()
          Using db = CreateDbContext()
            Dim result = db.From(Of Label).
                                SelectAll().
                                Exclude(Function(x) x.TableId).
                                FirstOrDefault()
          End Using
        End Sub
      )
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeJoinedTableInMToNRelationship()
      Dim article1 = Me.ModelFactory.CreateArticle(1)

      Dim category1 = Me.ModelFactory.CreateCategory(1)
      Dim category2 = Me.ModelFactory.CreateCategory(2)

      Dim article1Category1 = Me.ModelFactory.CreateArticleCategory(1, 1)
      Dim article1Category2 = Me.ModelFactory.CreateArticleCategory(1, 2)

      InsertItems(article1)
      InsertItems(category1, category2)
      InsertItems(article1Category1, article1Category2)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticleCategory)(Function(a As Article, ac As ArticleCategory) a.Id = ac.ArticleId).
                        LeftJoin(Of Category)(Function(ac As ArticleCategory, c As Category) ac.CategoryId = c.Id).As(Function(a) a.Categories).
                        SelectAll().
                        ExcludeT2().
                        ToList()

        Assert.AreEqual(1, result.Count)

        Dim articleResult = result.First()
        Assert.AreEqual(2, articleResult.Categories.Count)
        Assert.IsNotNull(articleResult.Categories.SingleOrDefault(Function(c) c.Id = 1))
        Assert.IsNotNull(articleResult.Categories.SingleOrDefault(Function(c) c.Id = 2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeJoinedTable()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, 1)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, 2)
      Dim linkedItem4 = Me.ModelFactory.CreateLinkedItem(4, 3)

      InsertItems(linkedItem1, linkedItem2, linkedItem3, linkedItem4)

      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItem).
                        Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.PreviousId.Value).As(Function(x) x.NextItem).
                        Join(Of LinkedItem)(Function(j) j.T2.Id = j.T3.PreviousId.Value).As(Function(j) j.T2.NextItem).
                        Join(Of LinkedItem)(Function(j) j.T3.Id = j.T4.PreviousId.Value).As(Function(j) j.T3.NextItem).
                        SelectAll().
                        ExcludeT3().
                        ToList()

        Assert.AreEqual(1, result.Count)

        Dim linkedItem1Result = result.First()
        Assert.AreEqual(linkedItem1, linkedItem1Result)
        Assert.AreEqual(linkedItem2, linkedItem1Result.NextItem)
        Assert.IsNull(linkedItem1Result.NextItem.NextItem)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeColumnInJoinedTable()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, 1)
      linkedItem2.Description = "item 2"

      InsertItems(linkedItem1, linkedItem2)

      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItem).
                        Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.PreviousId.Value).As(Function(x) x.NextItem).
                        SelectAll().
                        Exclude(Function(j) j.T2.Description).
                        ToList()

        Assert.AreEqual(1, result.Count)

        Dim linkedItem1Result = result.First()

        Assert.AreEqual("", linkedItem1Result.NextItem.Description)

        ' check if remaining fields are loaded
        linkedItem2.Description = ""

        Assert.AreEqual(linkedItem2, linkedItem1Result.NextItem)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeColumnsWithShuffledProperties()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItemWithShuffledProperties(1, Nothing)
      linkedItem1.Description = "item 1"
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItemWithShuffledProperties(2, 1)
      linkedItem2.Description = "item 2"
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItemWithShuffledProperties(3, 2)
      linkedItem3.Description = "item 3"

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItemWithShuffledProperties).
                        Join(Of LinkedItemWithShuffledProperties)(Function(j) j.T1.Id = j.T2.PreviousId.Value).As(Function(x) x.NextItem).
                        Join(Of LinkedItemWithShuffledProperties)(Function(j) j.T2.Id = j.T3.PreviousId.Value).As(Function(j) j.T2.NextItem).
                        SelectAll().
                        Exclude(Function(j) j.T1.Description).
                        Exclude(Function(j) j.T3.Description).
                        ToList()

        Assert.AreEqual(1, result.Count)

        Dim linkedItem1Result = result.First()

        Assert.AreEqual("", linkedItem1Result.Description)
        Assert.AreEqual("", linkedItem1Result.NextItem.NextItem.Description)

        ' check if remaining fields are loaded
        linkedItem1.Description = ""
        linkedItem3.Description = ""

        Assert.AreEqual(linkedItem1, linkedItem1Result)
        Assert.AreEqual(linkedItem2, linkedItem1Result.NextItem)
        Assert.AreEqual(linkedItem3, linkedItem1Result.NextItem.NextItem)
      End Using
    End Sub

  End Class
End Namespace
