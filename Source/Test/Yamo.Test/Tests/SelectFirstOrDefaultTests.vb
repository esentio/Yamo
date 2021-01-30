Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectFirstOrDefaultTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultUsingProcessOnlyFirstRowBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      ' using default
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(x) x.Id).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article1, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(x) x.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessOnlyFirstRow)

        Assert.AreEqual(article1, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultUsingProcessUntilMainEntityChangeBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(x) x.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultUsingProcessAllRowsBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(x) x.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWith1To1RelationshipUsingProcessOnlyFirstRowBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1 = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2 = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3 = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1, label2, label3)

      ' using default
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article1, result)
        Assert.AreEqual(label1, result.Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessOnlyFirstRow)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(label1, result.Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWith1To1RelationshipUsingProcessUntilMainEntityChangeBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1 = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2 = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3 = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1, label2, label3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(label1, result.Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWith1To1RelationshipUsingProcessAllRowsBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1 = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2 = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3 = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1, label2, label3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(label1, result.Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWith1ToNRelationshipUsingProcessOnlyFirstRowBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 7D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 2D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 1D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 3D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 4D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 5D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 6D)

      InsertItems(article1, article2, article3, article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)

      ' using default
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T2.ArticleId).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T2.Price).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article1, result)
        CollectionAssert.AreEqual({article1Part3}, result.Parts)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T2.ArticleId).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T2.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessOnlyFirstRow)

        Assert.AreEqual(article1, result)
        CollectionAssert.AreEqual({article1Part3}, result.Parts)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWith1ToNRelationshipUsingProcessUntilMainEntityChangeBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 7D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 2D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 1D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 3D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 4D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 5D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 6D)

      InsertItems(article1, article2, article3, article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T2.ArticleId).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T2.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T2.ArticleId).
                        OrderBy(Function(j) j.T2.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        CollectionAssert.AreEqual({article1Part3, article1Part2}, result.Parts)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWith1ToNRelationshipUsingProcessAllRowsBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 7D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 2D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 1D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 3D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 4D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 5D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 6D)

      InsertItems(article1, article2, article3, article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T2.ArticleId).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T2.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T2.ArticleId).
                        OrderBy(Function(j) j.T2.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWithMultiple1To1And1ToNAndMToNRelationshipUsingProcessOnlyFirstRowBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Label = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article2Label = Me.ModelFactory.CreateLabel(NameOf(Article), 2, English)
      Dim article3Label = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 7D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 2D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 1D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 3D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 4D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 5D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 6D)

      Dim article1Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1001, English)
      Dim article1Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1002, English)
      Dim article1Part3Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1003, English)
      Dim article2Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 2001, English)
      Dim article2Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 2002, English)
      Dim article3Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3001, English)
      Dim article3Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3002, English)

      Dim category1 = Me.ModelFactory.CreateCategory(1)
      Dim category2 = Me.ModelFactory.CreateCategory(2)
      Dim category3 = Me.ModelFactory.CreateCategory(3)

      Dim category1Label = Me.ModelFactory.CreateLabel(NameOf(Category), 1, English)
      Dim category2Label = Me.ModelFactory.CreateLabel(NameOf(Category), 2, English)
      Dim category3Label = Me.ModelFactory.CreateLabel(NameOf(Category), 3, English)

      Dim article1Category1 = Me.ModelFactory.CreateArticleCategory(1, 1)
      Dim article1Category2 = Me.ModelFactory.CreateArticleCategory(1, 2)
      Dim article1Category3 = Me.ModelFactory.CreateArticleCategory(1, 3)
      Dim article2Category1 = Me.ModelFactory.CreateArticleCategory(2, 1)
      Dim article2Category2 = Me.ModelFactory.CreateArticleCategory(2, 2)
      Dim article3Category1 = Me.ModelFactory.CreateArticleCategory(3, 1)
      Dim article3Category3 = Me.ModelFactory.CreateArticleCategory(3, 3)

      InsertItems(article1, article2, article3)
      InsertItems(article1Label, article2Label, article3Label)
      InsertItems(article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)
      InsertItems(article1Part1Label, article1Part2Label, article1Part3Label, article2Part1Label, article2Part2Label, article3Part1Label, article3Part2Label)
      InsertItems(category1, category2, category3)
      InsertItems(category1Label, category2Label, category3Label)
      InsertItems(article1Category1, article1Category2, article1Category3, article2Category1, article2Category2, article3Category1, article3Category3)

      ' using default
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).ThenByDescending(Function(j) j.T6.Id).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        CollectionAssert.AreEqual({category3}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).ThenByDescending(Function(j) j.T6.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessOnlyFirstRow)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        CollectionAssert.AreEqual({category3}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWithMultiple1To1And1ToNAndMToNRelationshipUsingProcessUntilMainEntityChangeBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Label = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article2Label = Me.ModelFactory.CreateLabel(NameOf(Article), 2, English)
      Dim article3Label = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 7D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 2D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 1D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 3D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 4D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 5D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 6D)

      Dim article1Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1001, English)
      Dim article1Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1002, English)
      Dim article1Part3Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1003, English)
      Dim article2Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 2001, English)
      Dim article2Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 2002, English)
      Dim article3Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3001, English)
      Dim article3Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3002, English)

      Dim category1 = Me.ModelFactory.CreateCategory(1)
      Dim category2 = Me.ModelFactory.CreateCategory(2)
      Dim category3 = Me.ModelFactory.CreateCategory(3)

      Dim category1Label = Me.ModelFactory.CreateLabel(NameOf(Category), 1, English)
      Dim category2Label = Me.ModelFactory.CreateLabel(NameOf(Category), 2, English)
      Dim category3Label = Me.ModelFactory.CreateLabel(NameOf(Category), 3, English)

      Dim article1Category1 = Me.ModelFactory.CreateArticleCategory(1, 1)
      Dim article1Category2 = Me.ModelFactory.CreateArticleCategory(1, 2)
      Dim article1Category3 = Me.ModelFactory.CreateArticleCategory(1, 3)
      Dim article2Category1 = Me.ModelFactory.CreateArticleCategory(2, 1)
      Dim article2Category2 = Me.ModelFactory.CreateArticleCategory(2, 2)
      Dim article3Category1 = Me.ModelFactory.CreateArticleCategory(3, 1)
      Dim article3Category3 = Me.ModelFactory.CreateArticleCategory(3, 3)

      InsertItems(article1, article2, article3)
      InsertItems(article1Label, article2Label, article3Label)
      InsertItems(article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)
      InsertItems(article1Part1Label, article1Part2Label, article1Part3Label, article2Part1Label, article2Part2Label, article3Part1Label, article3Part2Label)
      InsertItems(category1, category2, category3)
      InsertItems(category1Label, category2Label, category3Label)
      InsertItems(article1Category1, article1Category2, article1Category3, article2Category1, article2Category2, article3Category1, article3Category3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).ThenByDescending(Function(j) j.T6.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        Assert.AreEqual(article1Part1Label, result.Parts(2).Label)
        CollectionAssert.AreEqual({category3, category2, category1}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
        Assert.AreEqual(category2Label, result.Categories(1).Label)
        Assert.AreEqual(category1Label, result.Categories(2).Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderBy(Function(j) j.T3.Price).ThenByDescending(Function(j) j.T6.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        CollectionAssert.AreEqual({category3, category2, category1}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
        Assert.AreEqual(category2Label, result.Categories(1).Label)
        Assert.AreEqual(category1Label, result.Categories(2).Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderByDescending(Function(j) j.T6.Id).ThenBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        Assert.AreEqual(article1Part1Label, result.Parts(2).Label)
        CollectionAssert.AreEqual({category3}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderByDescending(Function(j) j.T6.Id).ThenBy(Function(j) j.T3.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        CollectionAssert.AreEqual({category3}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectFirstOrDefaultWithMultiple1To1And1ToNAndMToNRelationshipUsingProcessAllRowsBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Label = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article2Label = Me.ModelFactory.CreateLabel(NameOf(Article), 2, English)
      Dim article3Label = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 7D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 2D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 1D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 3D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 4D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 5D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 6D)

      Dim article1Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1001, English)
      Dim article1Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1002, English)
      Dim article1Part3Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1003, English)
      Dim article2Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 2001, English)
      Dim article2Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 2002, English)
      Dim article3Part1Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3001, English)
      Dim article3Part2Label = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3002, English)

      Dim category1 = Me.ModelFactory.CreateCategory(1)
      Dim category2 = Me.ModelFactory.CreateCategory(2)
      Dim category3 = Me.ModelFactory.CreateCategory(3)

      Dim category1Label = Me.ModelFactory.CreateLabel(NameOf(Category), 1, English)
      Dim category2Label = Me.ModelFactory.CreateLabel(NameOf(Category), 2, English)
      Dim category3Label = Me.ModelFactory.CreateLabel(NameOf(Category), 3, English)

      Dim article1Category1 = Me.ModelFactory.CreateArticleCategory(1, 1)
      Dim article1Category2 = Me.ModelFactory.CreateArticleCategory(1, 2)
      Dim article1Category3 = Me.ModelFactory.CreateArticleCategory(1, 3)
      Dim article2Category1 = Me.ModelFactory.CreateArticleCategory(2, 1)
      Dim article2Category2 = Me.ModelFactory.CreateArticleCategory(2, 2)
      Dim article3Category1 = Me.ModelFactory.CreateArticleCategory(3, 1)
      Dim article3Category3 = Me.ModelFactory.CreateArticleCategory(3, 3)

      InsertItems(article1, article2, article3)
      InsertItems(article1Label, article2Label, article3Label)
      InsertItems(article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)
      InsertItems(article1Part1Label, article1Part2Label, article1Part3Label, article2Part1Label, article2Part2Label, article3Part1Label, article3Part2Label)
      InsertItems(category1, category2, category3)
      InsertItems(category1Label, category2Label, category3Label)
      InsertItems(article1Category1, article1Category2, article1Category3, article2Category1, article2Category2, article3Category1, article3Category3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).ThenByDescending(Function(j) j.T6.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        Assert.AreEqual(article1Part1Label, result.Parts(2).Label)
        CollectionAssert.AreEqual({category3, category2, category1}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
        Assert.AreEqual(category2Label, result.Categories(1).Label)
        Assert.AreEqual(category1Label, result.Categories(2).Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderBy(Function(j) j.T3.Price).ThenByDescending(Function(j) j.T6.Id).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        Assert.AreEqual(article1Part1Label, result.Parts(2).Label)
        CollectionAssert.AreEqual({category3, category2, category1}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
        Assert.AreEqual(category2Label, result.Categories(1).Label)
        Assert.AreEqual(category1Label, result.Categories(2).Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T2.TableId = NameOf(Article) AndAlso j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T4.TableId = NameOf(ArticlePart) AndAlso j.T3.Id = j.T4.Id).
                        LeftJoin(Of ArticleCategory)(Function(j) j.T1.Id = j.T5.ArticleId).
                        LeftJoin(Of Category)(Function(j) j.T5.CategoryId = j.T6.Id).As(Function(j) j.T1.Categories).
                        LeftJoin(Of Label)(Function(j) j.T7.TableId = NameOf(Category) AndAlso j.T6.Id = j.T7.Id).
                        OrderByDescending(Function(j) j.T6.Id).ThenBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article1, result)
        Assert.AreEqual(article1Label, result.Label)
        CollectionAssert.AreEqual({article1Part3, article1Part2, article1Part1}, result.Parts)
        Assert.AreEqual(article1Part3Label, result.Parts(0).Label)
        Assert.AreEqual(article1Part2Label, result.Parts(1).Label)
        Assert.AreEqual(article1Part1Label, result.Parts(2).Label)
        CollectionAssert.AreEqual({category3, category2, category1}, result.Categories)
        Assert.AreEqual(category3Label, result.Categories(0).Label)
        Assert.AreEqual(category2Label, result.Categories(1).Label)
        Assert.AreEqual(category1Label, result.Categories(2).Label)
      End Using
    End Sub

  End Class
End Namespace
