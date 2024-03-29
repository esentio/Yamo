﻿Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithIncludeTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeUsingAction()
      ' same as test below, just use API allowed only in VB.NET

      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 300D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "Article 2")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Article 3")

      InsertItems(article1, article2, article3, label1En, label2En, label3En)

      ' no join
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(a) a.Id).
                        SelectAll().
                        Include(Sub(a) a.PriceWithDiscount = a.Price * 0.9D).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(a) a.Id).
                        SelectAll().
                        Include(Sub(a) a.PriceWithDiscount = a.Price * 0.9D).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
      End Using

      ' join
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(a) a.PriceWithDiscount = a.Price * 0.9D).
                        Include(Sub(a) a.LabelDescription = "foo").
                        Include(Sub(a) a.Tag = a.Price).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("foo", result.LabelDescription)
        Assert.AreEqual(article1.Price, result.Tag)
        Assert.IsNull(result.Label)
      End Using

      ' similar as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T1.Price * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        Include(Sub(j) j.T1.Tag = j.T1.Price).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article 1", result.LabelDescription)
        Assert.AreEqual(article1.Price, result.Tag)
        Assert.IsNull(result.Label)
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T1.Price * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        Include(Sub(j) j.T1.Tag = j.T1.Price).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article 1", result.LabelDescription)
        Assert.AreEqual(article1.Price, result.Tag)
        Assert.AreEqual(label1En, result.Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T1.Price * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        Include(Sub(j) j.T1.Tag = j.T1.Price).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article 1", "Article 2", "Article 3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        CollectionAssert.AreEqual({article1.Price, article2.Price, article3.Price}, result.Select(Function(x) x.Tag).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T1.Price * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        Include(Sub(j) j.T1.Tag = j.T1.Price).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article 1", "Article 2", "Article 3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        CollectionAssert.AreEqual({article1.Price, article2.Price, article3.Price}, result.Select(Function(x) x.Tag).ToArray())
        CollectionAssert.AreEqual({label1En, label2En, label3En}, result.Select(Function(x) x.Label).ToArray())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeUsingKeyValueSelectors()
      ' same as test above, just use different API

      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 300D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "Article 2")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Article 3")

      InsertItems(article1, article2, article3, label1En, label2En, label3En)

      ' no join
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(a) a.Id).
                        SelectAll().
                        Include(Function(a) a.PriceWithDiscount, Function(a) a.Price * 0.9D).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(a) a.Id).
                        SelectAll().
                        Include(Function(a) a.PriceWithDiscount, Function(a) a.Price * 0.9D).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(a) a.PriceWithDiscount, Function(a) a.Price * 0.9D).
                        Include(Function(a) a.LabelDescription, Function(l) l.Description).
                        Include(Function(a As Article) a.Tag, Function(a) a.Price).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article 1", result.LabelDescription)
        Assert.AreEqual(article1.Price, result.Tag)
        Assert.IsNull(result.Label)
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(j) j.T1.PriceWithDiscount, Function(j) j.T1.Price * 0.9D).
                        Include(Function(j) j.T1.LabelDescription, Function(j) j.T2.Description).
                        Include(Function(j) j.T1.Tag, Function(j) j.T1.Price).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article 1", result.LabelDescription)
        Assert.AreEqual(article1.Price, result.Tag)
        Assert.IsNull(result.Label)
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Function(j) j.T1.PriceWithDiscount, Function(j) j.T1.Price * 0.9D).
                        Include(Function(j) j.T1.LabelDescription, Function(j) j.T2.Description).
                        Include(Function(j) j.T1.Tag, Function(j) j.T1.Price).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article 1", result.LabelDescription)
        Assert.AreEqual(article1.Price, result.Tag)
        Assert.AreEqual(label1En, result.Label)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(j) j.T1.PriceWithDiscount, Function(j) j.T1.Price * 0.9D).
                        Include(Function(j) j.T1.LabelDescription, Function(j) j.T2.Description).
                        Include(Function(j) j.T1.Tag, Function(j) j.T1.Price).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article 1", "Article 2", "Article 3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        CollectionAssert.AreEqual({article1.Price, article2.Price, article3.Price}, result.Select(Function(x) x.Tag).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Function(j) j.T1.PriceWithDiscount, Function(j) j.T1.Price * 0.9D).
                        Include(Function(j) j.T1.LabelDescription, Function(j) j.T2.Description).
                        Include(Function(j) j.T1.Tag, Function(j) j.T1.Price).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article 1", "Article 2", "Article 3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        CollectionAssert.AreEqual({article1.Price, article2.Price, article3.Price}, result.Select(Function(x) x.Tag).ToArray())
        CollectionAssert.AreEqual({label1En, label2En, label3En}, result.Select(Function(x) x.Label).ToArray())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeInJoinedTables()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 300D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "Article 2")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Article 3")

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1, 10D)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(1002, 1, 11D)
      Dim article1Part3 = Me.ModelFactory.CreateArticlePart(1003, 1, 12D)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(2001, 2, 13D)
      Dim article2Part2 = Me.ModelFactory.CreateArticlePart(2002, 2, 14D)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3, 15D)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3, 16D)

      InsertItems(article1, article2, article3)
      InsertItems(label1En, label2En, label3En)
      InsertItems(article1Part1, article1Part2, article1Part3, article2Part1, article2Part2, article3Part1, article3Part2)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        Join(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        OrderBy(Function(j) j.T1.Id).ThenBy(Function(j) j.T3.Price).
                        SelectAll().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T1.Price * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        Include(Sub(j) j.T2.Tag = j.T2.Id).
                        Include(Sub(j) j.T3.PriceWithDiscount = j.T3.Price * 0.8D).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article 1", "Article 2", "Article 3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        CollectionAssert.AreEqual({label1En, label2En, label3En}, result.Select(Function(x) x.Label).ToArray())
        CollectionAssert.AreEqual({label1En.Id, label2En.Id, label3En.Id}, result.Select(Function(x) x.Label.Tag).ToArray())
        CollectionAssert.AreEqual({article1Part1, article1Part2, article1Part3}, result(0).Parts)
        CollectionAssert.AreEqual({article2Part1, article2Part2}, result(1).Parts)
        CollectionAssert.AreEqual({article3Part1, article3Part2}, result(2).Parts)
        CollectionAssert.AreEqual({article1Part1.Price * 0.8D, article1Part2.Price * 0.8D, article1Part3.Price * 0.8D}, result(0).Parts.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({article2Part1.Price * 0.8D, article2Part2.Price * 0.8D}, result(1).Parts.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({article3Part1.Price * 0.8D, article3Part2.Price * 0.8D}, result(2).Parts.Select(Function(x) x.PriceWithDiscount).ToArray())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeOfComplexType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 300D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "Article 2")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Article 3")

      InsertItems(article1, article2, article3, label1En, label2En, label3En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = (LabelId:=j.T2.Id, LabelDescription:=j.T2.Description)).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({label1En, label2En, label3En}, result.Select(Function(x) x.Label).ToArray())
        CollectionAssert.AreEqual({(label1En.Id, label1En.Description), (label2En.Id, label2En.Description), (label3En.Id, label3En.Description)}, result.Select(Function(x) x.Tag).ToArray())
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = New With {Key .LabelId = j.T2.Id, Key .LabelDescription = j.T2.Description}).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({label1En, label2En, label3En}, result.Select(Function(x) x.Label).ToArray())
        CollectionAssert.AreEqual({New With {Key .LabelId = label1En.Id, Key .LabelDescription = label1En.Description}, New With {Key .LabelId = label2En.Id, Key .LabelDescription = label2En.Description}, New With {Key .LabelId = label3En.Id, Key .LabelDescription = label3En.Description}}, result.Select(Function(x) x.Tag).ToArray())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeOfEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 300D)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 400D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "Article 2")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Article 3")

      InsertItems(article1, article2, article3, article4, label1En, label2En, label3En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3, article4}, result) ' this only checks "model" properties
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
        CollectionAssert.AreEqual({label1En, label2En, label3En, Nothing}, result.Select(Function(x) x.Tag).ToArray())
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3, article4}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({label1En, label2En, label3En, Nothing}, result.Select(Function(x) x.Label).ToArray())
        CollectionAssert.AreEqual({label1En, label2En, label3En, Nothing}, result.Select(Function(x) x.Tag).ToArray())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeScalarValueUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T2.Id * 0.9D, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        Include(Sub(j) j.T1.NullablePriceWithDiscount = j.T2.Id * 0.9D, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Id * 0.9D, result(0).PriceWithDiscount)
        Assert.AreEqual(New Decimal?(label1En.Id * 0.9D), result(0).NullablePriceWithDiscount)
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(0D, result(1).PriceWithDiscount)
        Assert.AreEqual(New Decimal?, result(1).NullablePriceWithDiscount)
        Assert.AreEqual(Nothing, result(1).LabelDescription)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T2.Id * 0.9D, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        Include(Sub(j) j.T1.NullablePriceWithDiscount = j.T2.Id * 0.9D, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Id * 0.9D, result(0).PriceWithDiscount)
        Assert.AreEqual(New Decimal?(label1En.Id * 0.9D), result(0).NullablePriceWithDiscount)
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(0D, result(1).PriceWithDiscount)
        Assert.AreEqual(New Decimal?, result(1).NullablePriceWithDiscount)
        Assert.AreEqual(Nothing, result(1).LabelDescription)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T2.Id * 0.9D).
                        Include(Sub(j) j.T1.NullablePriceWithDiscount = j.T2.Id * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Id * 0.9D, result(0).PriceWithDiscount)
        Assert.AreEqual(New Decimal?(label1En.Id * 0.9D), result(0).NullablePriceWithDiscount)
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(0D, result(1).PriceWithDiscount)
        Assert.AreEqual(New Decimal?, result(1).NullablePriceWithDiscount)
        Assert.AreEqual(Nothing, result(1).LabelDescription)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeScalarValueUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T2.Id * 0.9D, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        Include(Sub(j) j.T1.NullablePriceWithDiscount = j.T2.Id * 0.9D, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Id * 0.9D, result(0).PriceWithDiscount)
        Assert.AreEqual(New Decimal?(label1En.Id * 0.9D), result(0).NullablePriceWithDiscount)
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(0D, result(1).PriceWithDiscount)
        Assert.AreEqual(New Decimal?, result(1).NullablePriceWithDiscount)
        Assert.AreEqual(Nothing, result(1).LabelDescription)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeEntityValueUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(Nothing, result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(Nothing, result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(Nothing, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeEntityValueUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(Nothing, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeAnonymousTypeValueUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New With {Key .LabelId = j.T2.Id, Key .NullableLabelId = CType(j.T2.Id, Int32?), Key .LabelDescription = j.T2.Description, Key .Label = j.T2}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .LabelId = label1En.Id, Key .NullableLabelId = New Int32?(label1En.Id), Key .LabelDescription = label1En.Description, Key .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New With {Key .LabelId = j.T2.Id, Key .NullableLabelId = CType(j.T2.Id, Int32?), Key .LabelDescription = j.T2.Description, Key .Label = j.T2}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .LabelId = label1En.Id, Key .NullableLabelId = New Int32?(label1En.Id), Key .LabelDescription = label1En.Description, Key .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New With {Key .LabelId = j.T2.Id, Key .NullableLabelId = CType(j.T2.Id, Int32?), Key .LabelDescription = j.T2.Description, Key .Label = j.T2}).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .LabelId = label1En.Id, Key .NullableLabelId = New Int32?(label1En.Id), Key .LabelDescription = label1En.Description, Key .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeAnonymousTypeValueUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New With {Key .LabelId = j.T2.Id, Key .NullableLabelId = CType(j.T2.Id, Int32?), Key .LabelDescription = j.T2.Description, Key .Label = j.T2}, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .LabelId = label1En.Id, Key .NullableLabelId = New Int32?(label1En.Id), Key .LabelDescription = label1En.Description, Key .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .LabelId = 0, Key .NullableLabelId = New Int32?(), Key .LabelDescription = CType(Nothing, String), Key .Label = CType(Nothing, Label)}, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueTupleValueUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = (LabelId:=j.T2.Id, NullableLabelId:=CType(j.T2.Id, Int32?), LabelDescription:=j.T2.Description, Label:=j.T2), NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, New Int32?(label1En.Id), label1En.Description, label1En), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = (LabelId:=j.T2.Id, NullableLabelId:=CType(j.T2.Id, Int32?), LabelDescription:=j.T2.Description, Label:=j.T2), NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, New Int32?(label1En.Id), label1En.Description, label1En), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = (LabelId:=j.T2.Id, NullableLabelId:=CType(j.T2.Id, Int32?), LabelDescription:=j.T2.Description, Label:=j.T2)).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, New Int32?(label1En.Id), label1En.Description, label1En), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueTupleValueUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = (LabelId:=j.T2.Id, NullableLabelId:=CType(j.T2.Id, Int32?), LabelDescription:=j.T2.Description, Label:=j.T2), NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, New Int32?(label1En.Id), label1En.Description, label1En), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual((0, New Int32?(), CType(Nothing, String), CType(Nothing, Label)), result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeAdHocTypeValueUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New NonModelObject With {.IntValue = j.T2.Id, .NullableIntValue = CType(j.T2.Id, Int32?), .StringValue1 = j.T2.Description, .Label = j.T2}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject With {.IntValue = label1En.Id, .NullableIntValue = New Int32?(label1En.Id), .StringValue1 = label1En.Description, .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New NonModelObject With {.IntValue = j.T2.Id, .NullableIntValue = CType(j.T2.Id, Int32?), .StringValue1 = j.T2.Description, .Label = j.T2}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject With {.IntValue = label1En.Id, .NullableIntValue = New Int32?(label1En.Id), .StringValue1 = label1En.Description, .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New NonModelObject With {.IntValue = j.T2.Id, .NullableIntValue = CType(j.T2.Id, Int32?), .StringValue1 = j.T2.Description, .Label = j.T2}).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject With {.IntValue = label1En.Id, .NullableIntValue = New Int32?(label1En.Id), .StringValue1 = label1En.Description, .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeAdHocTypeValueUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = New NonModelObject With {.IntValue = j.T2.Id, .NullableIntValue = CType(j.T2.Id, Int32?), .StringValue1 = j.T2.Description, .Label = j.T2}, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject With {.IntValue = label1En.Id, .NullableIntValue = New Int32?(label1En.Id), .StringValue1 = label1En.Description, .Label = label1En}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject With {.IntValue = 0, .NullableIntValue = New Int32?(), .StringValue1 = Nothing, .Label = Nothing}, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeCasting()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' Int32 to Decimal, Int32 to object
      ' NOTE: There is a " * 1.5D / 1.5D" workaround to force returning decimal value.
      ' Otherwise, there is following exception in SqlDataReader.GetDecimal(Int32 i) method for SQL Server:
      ' System.InvalidCastException: Unable to cast object of type 'System.Int32' to type 'System.Decimal'.
      ' Other option would be to use CAST/CONVERT in raw SQL.
      ' This is "deeper" issue in Yamo and not the only use case when it could happen.
      ' For now, Yamo doesn't solve this automatically.
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T2.Id * 1.5D / 1.5D).
                        Include(Sub(j) j.T1.NullablePriceWithDiscount = j.T2.Id * 1.5D / 1.5D).
                        Include(Sub(j) j.T1.Tag = j.T2.Id).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(CType(label1En.Id, Decimal), result(0).PriceWithDiscount)
        Assert.AreEqual(CType(label1En.Id, Decimal?), result(0).NullablePriceWithDiscount)
        Assert.AreEqual(label1En.Id, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(0D, result(1).PriceWithDiscount)
        Assert.AreEqual(New Decimal?, result(1).NullablePriceWithDiscount)
        Assert.AreEqual(0, result(1).Tag)
      End Using

      ' same as above using key value selectors
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(j) j.T1.PriceWithDiscount, Function(j) j.T2.Id * 1.5D / 1.5D).
                        Include(Function(j) j.T1.NullablePriceWithDiscount, Function(j) j.T2.Id * 1.5D / 1.5D).
                        Include(Function(j) j.T1.Tag, Function(j) j.T2.Id).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(CType(label1En.Id, Decimal), result(0).PriceWithDiscount)
        Assert.AreEqual(CType(label1En.Id, Decimal?), result(0).NullablePriceWithDiscount)
        Assert.AreEqual(label1En.Id, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(0D, result(1).PriceWithDiscount)
        Assert.AreEqual(New Decimal?, result(1).NullablePriceWithDiscount)
        Assert.AreEqual(0, result(1).Tag)
      End Using

      ' entity to object
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(Nothing, result(1).Tag)
      End Using

      ' same as above using key value selectors
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(j) j.T1.Tag, Function(j) j.T2).
                        ToList()

        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(Nothing, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueFromSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            SelectAll()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(1).Label)
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            SelectAll()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueFromSubqueryOfTypeAnonymousType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
      End Using

      ' same as above, but include whole subquery result
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueFromSubqueryOfTypeValueTuple()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description))
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
      End Using

      ' same as above, but include whole subquery result
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description))
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueFromSubqueryOfTypeAdHocType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.StringValue1).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
      End Using

      ' same as above, but include whole subquery result
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.StringValue1).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValuesFromSubqueryOfComplexType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Label = x, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).LabelDescription)
      End Using

      Try
        Using db = CreateDbContext()
          Dim result = db.From(Of Article).
                          LeftJoin(Function(c)
                                     Return c.From(Of Label).
                                              Where(Function(x) x.Language = English).
                                              Select(Function(x) New With {Key .Id = x.Id, Key .Label = x, Key .Description = x.Description})
                                   End Function).
                          On(Function(j) j.T1.Id = j.T2.Id).
                          OrderBy(Function(j) j.T1.Id).
                          SelectAll().
                          Include(Sub(j) j.T1.Tag = j.T2.Label).
                          ToList()
        End Using

        Assert.Fail()
      Catch ex As AssertFailedException
        Assert.Fail()
      Catch ex As Exception
        ' expected
      End Try
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            SelectAll()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            SelectAll()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfComplexType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Label = x, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Label = label1En, Key .Description = label1En.Description}, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeEntityUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeEntityUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeAnonymousTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeAnonymousTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = 0, Key .Description = CType(Nothing, String)}, result(1).Tag)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(New With {Key .Id = 0, Key .Description = CType(Nothing, String)}, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeValueTupleUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description))
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeValueTupleUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      ' same as above, but include whole subquery result
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual((0, CType(Nothing, String)), result(1).Tag)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual((0, CType(Nothing, String)), result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeAdHocTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeWholeResultFromSubqueryOfTypeAdHocTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = 0, .StringValue1 = CType(Nothing, String)}, result(1).Tag)
      End Using

      ' override behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.Tag = j.T2, NonModelEntityCreationBehavior.AlwaysCreateInstance).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.AreEqual(New NonModelObject() With {.IntValue = 0, .StringValue1 = CType(Nothing, String)}, result(1).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueToResultOfSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        SelectAll()
                             End Function).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article1, result(0)) ' this only checks "model" properties
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(article2, result(1)) ' this only checks "model" properties
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueToResultOfSubqueryOfTypeAdHocType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Dim item1 = New NonModelObject(article1.Id, article1.Price) With {.StringValue1 = label1En.Description}
      Dim item2 = New NonModelObject(article2.Id, article2.Price) With {.StringValue1 = Nothing}

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Select(Function(x) New NonModelObject With {.IntValue = x.Id, .DecimalValue = x.Price})
                             End Function).
                        LeftJoin(Of Label)(Function(j) j.T1.IntValue = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.IntValue).
                        SelectAll().
                        Include(Sub(j) j.T1.StringValue1 = j.T2.Description).
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item1, result(0))
        Assert.AreEqual(item2, result(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeValueDirectlyInSubquery()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Article 1")

      InsertItems(article1, article2, label1En)

      Try
        Using db = CreateDbContext()
          Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            SelectAll().
                                            Include(Sub(x) x.Tag = x.Description)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ToList()

          Assert.Fail()
        End Using
      Catch ex As NotSupportedException
        ' expected
      Catch ex As Exception
        Assert.Fail()
      End Try
    End Sub

  End Class
End Namespace
