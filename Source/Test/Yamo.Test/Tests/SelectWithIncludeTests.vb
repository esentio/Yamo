Imports Yamo.Test.Model

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

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(a) a.PriceWithDiscount = a.Price * 0.9D).
                        Include(Sub(a) a.LabelDescription = "foo").
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("foo", result.LabelDescription)
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
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article1", result.LabelDescription)
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
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article1", result.LabelDescription)
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
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article1", "Article2", "Article3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Sub(j) j.T1.PriceWithDiscount = j.T1.Price * 0.9D).
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article1", "Article2", "Article3"}, result.Select(Function(x) x.LabelDescription).ToArray())
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

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(a) a.PriceWithDiscount, Function(a) a.Price * 0.9D).
                        Include(Function(a) a.LabelDescription, Function(l) l.Description).
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article1", result.LabelDescription)
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
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article1", result.LabelDescription)
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
                        FirstOrDefault()

        Assert.AreEqual(article1, result) ' this only checks "model" properties
        Assert.AreEqual(article1.Price * 0.9D, result.PriceWithDiscount)
        Assert.AreEqual("Article1", result.LabelDescription)
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
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article1", "Article2", "Article3"}, result.Select(Function(x) x.LabelDescription).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
      End Using

      ' same as above, but don't exclude label
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        Include(Function(j) j.T1.PriceWithDiscount, Function(j) j.T1.Price * 0.9D).
                        Include(Function(j) j.T1.LabelDescription, Function(j) j.T2.Description).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({article1.Price * 0.9D, article2.Price * 0.9D, article3.Price * 0.9D}, result.Select(Function(x) x.PriceWithDiscount).ToArray())
        CollectionAssert.AreEqual({"Article1", "Article2", "Article3"}, result.Select(Function(x) x.LabelDescription).ToArray())
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
        CollectionAssert.AreEqual({"Article1", "Article2", "Article3"}, result.Select(Function(x) x.LabelDescription).ToArray())
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
                        Include(Sub(j) j.T1.Tag = New With {.LabelId = j.T2.Id, .LabelDescription = j.T2.Description}).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result) ' this only checks "model" properties
        CollectionAssert.AreEqual({label1En, label2En, label3En}, result.Select(Function(x) x.Label).ToArray())
        CollectionAssert.AreEqual({New With {.LabelId = label1En.Id, .LabelDescription = label1En.Description}, New With {.LabelId = label2En.Id, .LabelDescription = label2En.Description}, New With {.LabelId = label3En.Id, .LabelDescription = label3En.Description}}, result.Select(Function(x) x.Tag).ToArray())
      End Using
    End Sub
  End Class
End Namespace
