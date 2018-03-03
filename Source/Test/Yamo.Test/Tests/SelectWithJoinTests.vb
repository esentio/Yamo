Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithJoinTests
    Inherits TestsBase

    Protected Const English As String = "en"

    Protected Const German As String = "ger"

    <TestMethod()>
    Public Overridable Sub SelectWith1To1RelationshipUsingInnerJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).Join(Of Label)(Function(a, l) a.Id = l.Id).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1To1RelationshipUsingLeftOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1To1RelationshipUsingRightOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).RightJoin(Of Label)(Function(a, l) a.Id = l.Id).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1To1RelationshipUsingFullOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).FullJoin(Of Label)(Function(a, l) a.Id = l.Id).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1To1RelationshipUsingCrossJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).CrossJoin(Of Label).SelectAll().ToList()
        Assert.AreEqual(9, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 1))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1ToNRelationshipUsingInnerJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      InsertItems(article1, article2, article3, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).Join(Of ArticlePart)(Function(a, p) a.Id = p.ArticleId).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1ToNRelationshipUsingLeftOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      InsertItems(article1, article2, article3, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).LeftJoin(Of ArticlePart)(Function(a, p) a.Id = p.ArticleId).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1ToNRelationshipUsingRightOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      InsertItems(article1, article2, article3, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).RightJoin(Of ArticlePart)(Function(a, p) a.Id = p.ArticleId).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1ToNRelationshipUsingFullOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      InsertItems(article1, article2, article3, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).FullJoin(Of ArticlePart)(Function(a, p) a.Id = p.ArticleId).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1ToNRelationshipUsingCrossJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      InsertItems(article1, article2, article3, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).CrossJoin(Of ArticlePart).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(5, article1Result.Parts.Count)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 4001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(5, article2Result.Parts.Count)
        Assert.IsNotNull(article2Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))
        Assert.IsNotNull(article2Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article2Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article2Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article2Result.Parts.SingleOrDefault(Function(p) p.Id = 4001))

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(5, article3Result.Parts.Count)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 4001))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1To1And1ToNRelationships()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1LabelEn = Me.ModelFactory.CreateLabel("", 1, English)
      Dim article3LabelEn = Me.ModelFactory.CreateLabel("", 3, English)
      Dim article3LabelGer = Me.ModelFactory.CreateLabel("", 3, German)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      InsertItems(article1, article2, article3, article1LabelEn, article3LabelEn, article3LabelGer, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(a, l) a.Id = l.Id AndAlso l.Language = English).
                        LeftJoin(Of ArticlePart)(Function(a As Article, p As ArticlePart) a.Id = p.ArticleId).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.Id = 1)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.Id = 3 AndAlso article3Result.Label.Language = English)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003))
      End Using

      ' same as above, but join in different order
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticlePart)(Function(a, p) a.Id = p.ArticleId).
                        LeftJoin(Of Label)(Function(a As Article, l As Label) a.Id = l.Id AndAlso l.Language = English).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.Id = 1)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.Id = 3 AndAlso article3Result.Label.Language = English)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003))
      End Using

      ' multiply by label (don't filter by language)
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                        LeftJoin(Of ArticlePart)(Function(a As Article, p As ArticlePart) a.Id = p.ArticleId).
                        SelectAll().ToList()

        Assert.AreEqual(4, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.Id = 1)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3EnResult = result.First(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English)
        Assert.AreEqual(3, article3EnResult.Parts.Count)
        Assert.IsNotNull(article3EnResult.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3EnResult.Parts.SingleOrDefault(Function(p) p.Id = 3002))
        Assert.IsNotNull(article3EnResult.Parts.SingleOrDefault(Function(p) p.Id = 3003))

        Dim article3GerResult = result.First(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German)
        Assert.AreEqual(3, article3GerResult.Parts.Count)
        Assert.IsNotNull(article3GerResult.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3GerResult.Parts.SingleOrDefault(Function(p) p.Id = 3002))
        Assert.IsNotNull(article3GerResult.Parts.SingleOrDefault(Function(p) p.Id = 3003))
      End Using

      ' same as above, but join in different order
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticlePart)(Function(a, p) a.Id = p.ArticleId).
                        LeftJoin(Of Label)(Function(a As Article, l As Label) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(4, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.Id = 1)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3EnResult = result.First(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English)
        Assert.AreEqual(3, article3EnResult.Parts.Count)
        Assert.IsNotNull(article3EnResult.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3EnResult.Parts.SingleOrDefault(Function(p) p.Id = 3002))
        Assert.IsNotNull(article3EnResult.Parts.SingleOrDefault(Function(p) p.Id = 3003))

        Dim article3GerResult = result.First(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German)
        Assert.AreEqual(3, article3GerResult.Parts.Count)
        Assert.IsNotNull(article3GerResult.Parts.SingleOrDefault(Function(p) p.Id = 3001))
        Assert.IsNotNull(article3GerResult.Parts.SingleOrDefault(Function(p) p.Id = 3002))
        Assert.IsNotNull(article3GerResult.Parts.SingleOrDefault(Function(p) p.Id = 3003))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWith1To1And1ToNAnd1To1Relationships()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article3LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)
      Dim article3LabelGer = Me.ModelFactory.CreateLabel(NameOf(Article), 3, German)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)
      Dim article4Part1 = Me.ModelFactory.CreateArticlePart(4001, 4)

      Dim article1Part1LabelEn = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1001, English)
      Dim article3Part1LabelEn = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3001, English)
      Dim article3Part2LabelEn = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3002, English)
      Dim article3Part2LabelGer = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3002, German)

      InsertItems(article1, article2, article3, article1LabelEn, article3LabelEn, article3LabelGer, article1Part1, article3Part1, article3Part2, article3Part3, article4Part1, article1Part1LabelEn, article3Part1LabelEn, article3Part2LabelEn, article3Part2LabelGer)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(a, l) a.Id = l.Id AndAlso l.Language = English).
                        LeftJoin(Of ArticlePart)(Function(a As Article, p As ArticlePart) a.Id = p.ArticleId).
                        LeftJoin(Of Label)(Function(p As ArticlePart, l As Label) p.Id = l.Id AndAlso l.Language = English).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.TableId = NameOf(Article) AndAlso article1Result.Label.Language = English AndAlso article1Result.Label.Id = article1Result.Id)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.TableId = NameOf(Article) AndAlso article3Result.Label.Language = English AndAlso article3Result.Label.Id = article3Result.Id)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003 AndAlso p.Label Is Nothing))
      End Using

      ' same as above, but join in different order
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticlePart)(Function(a As Article, p As ArticlePart) a.Id = p.ArticleId).
                        LeftJoin(Of Label)(Function(a As Article, l As Label) a.Id = l.Id AndAlso l.Language = English).
                        LeftJoin(Of Label)(Function(p As ArticlePart, l As Label) p.Id = l.Id AndAlso l.Language = English).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.TableId = NameOf(Article) AndAlso article1Result.Label.Language = English AndAlso article1Result.Label.Id = article1Result.Id)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.TableId = NameOf(Article) AndAlso article3Result.Label.Language = English AndAlso article3Result.Label.Id = article3Result.Id)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003 AndAlso p.Label Is Nothing))
      End Using

      ' same as above, but join in different order
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticlePart)(Function(a As Article, p As ArticlePart) a.Id = p.ArticleId).
                        LeftJoin(Of Label)(Function(p As ArticlePart, l As Label) p.Id = l.Id AndAlso l.Language = English).
                        LeftJoin(Of Label)(Function(a As Article, l As Label) a.Id = l.Id AndAlso l.Language = English).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.TableId = NameOf(Article) AndAlso article1Result.Label.Language = English AndAlso article1Result.Label.Id = article1Result.Id)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.TableId = NameOf(Article) AndAlso article3Result.Label.Language = English AndAlso article3Result.Label.Id = article3Result.Id)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003 AndAlso p.Label Is Nothing))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        LeftJoin(Of Label)(Function(j) j.T3.Id = j.T4.Id AndAlso j.T4.Language = English).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.TableId = NameOf(Article) AndAlso article1Result.Label.Language = English AndAlso article1Result.Label.Id = article1Result.Id)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.TableId = NameOf(Article) AndAlso article3Result.Label.Language = English AndAlso article3Result.Label.Id = article3Result.Id)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Language = English AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003 AndAlso p.Label Is Nothing))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithMultiple1To1And1ToNAndMToNRelationships()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article3LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(3001, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(3002, 3)
      Dim article3Part3 = Me.ModelFactory.CreateArticlePart(3003, 3)

      Dim article1Part1LabelEn = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 1001, English)
      Dim article3Part1LabelEn = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3001, English)
      Dim article3Part2LabelEn = Me.ModelFactory.CreateLabel(NameOf(ArticlePart), 3002, English)

      Dim category1 = Me.ModelFactory.CreateCategory(1)
      Dim category2 = Me.ModelFactory.CreateCategory(2)
      Dim category3 = Me.ModelFactory.CreateCategory(3)

      Dim category1LabelEn = Me.ModelFactory.CreateLabel(NameOf(Category), 1, English)
      Dim category2LabelEn = Me.ModelFactory.CreateLabel(NameOf(Category), 2, English)
      Dim category3LabelEn = Me.ModelFactory.CreateLabel(NameOf(Category), 3, English)

      Dim article1Category1 = Me.ModelFactory.CreateArticleCategory(1, 1)
      Dim article1Category2 = Me.ModelFactory.CreateArticleCategory(1, 2)
      Dim article3Category3 = Me.ModelFactory.CreateArticleCategory(3, 3)

      InsertItems(article1, article2, article3)
      InsertItems(article1LabelEn, article3LabelEn)
      InsertItems(article1Part1, article3Part1, article3Part2, article3Part3)
      InsertItems(article1Part1LabelEn, article3Part1LabelEn, article3Part2LabelEn)
      InsertItems(category1, category2, category3)
      InsertItems(category1LabelEn, category2LabelEn, category3LabelEn)
      InsertItems(article1Category1, article1Category2, article3Category3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(a, l) l.TableId = NameOf(Article) AndAlso a.Id = l.Id).
                        LeftJoin(Of ArticlePart)(Function(a As Article, p As ArticlePart) a.Id = p.ArticleId).
                        LeftJoin(Of Label)(Function(p As ArticlePart, l As Label) l.TableId = NameOf(ArticlePart) AndAlso p.Id = l.Id).
                        LeftJoin(Of ArticleCategory)(Function(a As Article, ac As ArticleCategory) a.Id = ac.ArticleId).
                        LeftJoin(Of Category)(Function(ac As ArticleCategory, c As Category) ac.CategoryId = c.Id).As(Function(a) a.Categories).
                        LeftJoin(Of Label)(Function(c As Category, l As Label) l.TableId = NameOf(Category) AndAlso c.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(1, article1Result.Parts.Count)
        Assert.IsTrue(article1Result.Label IsNot Nothing AndAlso article1Result.Label.TableId = NameOf(Article) AndAlso article1Result.Label.Id = article1Result.Id)
        Assert.IsNotNull(article1Result.Parts.SingleOrDefault(Function(p) p.Id = 1001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Id = p.Id))
        Assert.AreEqual(2, article1Result.Categories.Count)
        Assert.IsNotNull(article1Result.Categories.SingleOrDefault(Function(c) c.Id = 1 AndAlso c.Label IsNot Nothing AndAlso c.Label.TableId = NameOf(Category) AndAlso c.Label.Id = c.Id))
        Assert.IsNotNull(article1Result.Categories.SingleOrDefault(Function(c) c.Id = 2 AndAlso c.Label IsNot Nothing AndAlso c.Label.TableId = NameOf(Category) AndAlso c.Label.Id = c.Id))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.IsTrue(article2Result.Label Is Nothing)
        Assert.AreEqual(0, article2Result.Parts.Count)
        Assert.AreEqual(0, article2Result.Categories.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(3, article3Result.Parts.Count)
        Assert.IsTrue(article3Result.Label IsNot Nothing AndAlso article3Result.Label.TableId = NameOf(Article) AndAlso article3Result.Label.Id = article3Result.Id)
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3001 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3002 AndAlso p.Label IsNot Nothing AndAlso p.Label.TableId = NameOf(ArticlePart) AndAlso p.Label.Id = p.Id))
        Assert.IsNotNull(article3Result.Parts.SingleOrDefault(Function(p) p.Id = 3003 AndAlso p.Label Is Nothing))
        Assert.AreEqual(1, article3Result.Categories.Count)
        Assert.IsNotNull(article3Result.Categories.SingleOrDefault(Function(c) c.Id = 3 AndAlso c.Label IsNot Nothing AndAlso c.Label.TableId = NameOf(Category) AndAlso c.Label.Id = c.Id))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithMToNRelationship()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim category1 = Me.ModelFactory.CreateCategory(1)
      Dim category2 = Me.ModelFactory.CreateCategory(2)
      Dim category3 = Me.ModelFactory.CreateCategory(3)

      Dim article1Category1 = Me.ModelFactory.CreateArticleCategory(1, 1)
      Dim article1Category2 = Me.ModelFactory.CreateArticleCategory(1, 2)
      Dim article3Category3 = Me.ModelFactory.CreateArticleCategory(3, 3)

      InsertItems(article1, article2, article3)
      InsertItems(category1, category2, category3)
      InsertItems(article1Category1, article1Category2, article3Category3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticleCategory)(Function(a As Article, ac As ArticleCategory) a.Id = ac.ArticleId).
                        LeftJoin(Of Category)(Function(ac As ArticleCategory, c As Category) ac.CategoryId = c.Id).As(Function(a) a.Categories).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(2, article1Result.Categories.Count)
        Assert.IsNotNull(article1Result.Categories.SingleOrDefault(Function(c) c.Id = 1))
        Assert.IsNotNull(article1Result.Categories.SingleOrDefault(Function(c) c.Id = 2))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(0, article2Result.Categories.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(1, article3Result.Categories.Count)
        Assert.IsNotNull(article3Result.Categories.SingleOrDefault(Function(c) c.Id = 3))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of ArticleCategory)(Function(a As Article, ac As ArticleCategory) a.Id = ac.ArticleId).
                        LeftJoin(Of Category)(Function(ac As ArticleCategory, c As Category) ac.CategoryId = c.Id).As(Function(j) j.T1.Categories).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(2, article1Result.Categories.Count)
        Assert.IsNotNull(article1Result.Categories.SingleOrDefault(Function(c) c.Id = 1))
        Assert.IsNotNull(article1Result.Categories.SingleOrDefault(Function(c) c.Id = 2))

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(0, article2Result.Categories.Count)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(1, article3Result.Categories.Count)
        Assert.IsNotNull(article3Result.Categories.SingleOrDefault(Function(c) c.Id = 3))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithMultiple1To1RelationshipsOfSameEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)
      Dim article4 = Me.ModelFactory.CreateArticle(4)
      Dim article5 = Me.ModelFactory.CreateArticle(5)

      Dim articleSubstitution1 = Me.ModelFactory.CreateArticleSubstitution(1, 2)
      Dim articleSubstitution2 = Me.ModelFactory.CreateArticleSubstitution(1, 3)
      Dim articleSubstitution3 = Me.ModelFactory.CreateArticleSubstitution(3, 4)
      Dim articleSubstitution4 = Me.ModelFactory.CreateArticleSubstitution(5, 4)

      InsertItems(article1, article2, article3, article4, article5)
      InsertItems(articleSubstitution1, articleSubstitution2, articleSubstitution3, articleSubstitution4)

      Using db = CreateDbContext()
        Dim result = db.From(Of ArticleSubstitution).
                        LeftJoin(Of Article)(Function(s As ArticleSubstitution, a As Article) s.OriginalArticleId = a.Id).As(Function(s) s.Original).
                        LeftJoin(Of Article)(Function(s As ArticleSubstitution, a As Article) s.SubstitutionArticleId = a.Id).As(Function(s) s.Substitution).
                        SelectAll().ToList()

        Assert.AreEqual(4, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(s) s.OriginalArticleId = 1 AndAlso s.SubstitutionArticleId = 2))
        Assert.IsNotNull(result.SingleOrDefault(Function(s) s.OriginalArticleId = 1 AndAlso s.SubstitutionArticleId = 3))
        Assert.IsNotNull(result.SingleOrDefault(Function(s) s.OriginalArticleId = 3 AndAlso s.SubstitutionArticleId = 4))
        Assert.IsNotNull(result.SingleOrDefault(Function(s) s.OriginalArticleId = 5 AndAlso s.SubstitutionArticleId = 4))
        Assert.IsTrue(result.All(Function(s) s.OriginalArticleId = s.Original.Id AndAlso s.SubstitutionArticleId = s.Substitution.Id))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithRelationshipNotDefinedInModel()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, 1)

      Dim linkedItem1Child1 = Me.ModelFactory.CreateLinkedItemChild(1, 1)
      Dim linkedItem1Child2 = Me.ModelFactory.CreateLinkedItemChild(2, 1)
      Dim linkedItem1Child3 = Me.ModelFactory.CreateLinkedItemChild(3, 1)

      InsertItems(linkedItem1, linkedItem2)
      InsertItems(linkedItem1Child1, linkedItem1Child2, linkedItem1Child3)

      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItem).
                        Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.PreviousId.Value).As(Function(j) j.T1.NextItem).
                        Join(Of LinkedItemChild)(Function(j) j.T1.Id = j.T3.LinkedItemId).As(Function(j) j.T1.Children).
                        SelectAll().ToList()

        Assert.AreEqual(1, result.Count)

        Dim linkedItem1Result = result.First()
        Assert.AreEqual(linkedItem1, linkedItem1Result)
        Assert.IsNotNull(linkedItem1Result.NextItem)
        Assert.AreEqual(linkedItem2, linkedItem1Result.NextItem)
        Assert.AreEqual(3, linkedItem1Result.Children.Count)
        Assert.IsNotNull(linkedItem1Result.Children.SingleOrDefault(Function(x) x.Id = 1))
        Assert.IsNotNull(linkedItem1Result.Children.SingleOrDefault(Function(x) x.Id = 2))
        Assert.IsNotNull(linkedItem1Result.Children.SingleOrDefault(Function(x) x.Id = 3))
      End Using
    End Sub

  End Class
End Namespace
