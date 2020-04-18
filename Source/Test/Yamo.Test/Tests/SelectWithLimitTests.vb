Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithLimitTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectOrderedWithLimit()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 10)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 20)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 30)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 40)
      Dim article5 = Me.ModelFactory.CreateArticle(5, 50)
      Dim article6 = Me.ModelFactory.CreateArticle(6, 60)
      Dim article7 = Me.ModelFactory.CreateArticle(7, 70)

      InsertItems(article1, article2, article3, article4, article5, article6, article7)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(x) x.Price).
                        Limit(3).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(article3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectOrderedWithLimitAndOffset()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 10)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 20)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 30)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 40)
      Dim article5 = Me.ModelFactory.CreateArticle(5, 50)
      Dim article6 = Me.ModelFactory.CreateArticle(6, 60)
      Dim article7 = Me.ModelFactory.CreateArticle(7, 70)

      InsertItems(article1, article2, article3, article4, article5, article6, article7)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        OrderBy(Function(x) x.Price).
                        Limit(3, 2).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article4, result(0))
        Assert.AreEqual(article5, result(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUnorderedWithLimit()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 10)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 20)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 30)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 40)
      Dim article5 = Me.ModelFactory.CreateArticle(5, 50)
      Dim article6 = Me.ModelFactory.CreateArticle(6, 60)
      Dim article7 = Me.ModelFactory.CreateArticle(7, 70)

      InsertItems(article1, article2, article3, article4, article5, article6, article7)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Limit(3).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUnorderedWithLimitAndOffset()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 10)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 20)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 30)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 40)
      Dim article5 = Me.ModelFactory.CreateArticle(5, 50)
      Dim article6 = Me.ModelFactory.CreateArticle(6, 60)
      Dim article7 = Me.ModelFactory.CreateArticle(7, 70)

      InsertItems(article1, article2, article3, article4, article5, article6, article7)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Limit(3, 2).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithLimitFromMultipleTables()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 10)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 20)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 30)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 40)
      Dim article5 = Me.ModelFactory.CreateArticle(5, 50)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "a")
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German, "b")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "c")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "d")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "e")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "f")
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English, "g")
      Dim label4De = Me.ModelFactory.CreateLabel("", 4, German, "h")
      Dim label5En = Me.ModelFactory.CreateLabel("", 5, English, "i")
      Dim label5De = Me.ModelFactory.CreateLabel("", 5, German, "j")

      InsertItems(article1, article2, article3, article4, article5, label1En, label1De, label2En, label2De, label3En, label3De, label4En, label4De, label5En, label5De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        Where(Function(a, l) l.Language = English).
                        OrderByDescending(Function(a) a.Price).
                        Limit(3).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article5, result(0))
        Assert.AreEqual(label5En, result(0).Label)
        Assert.AreEqual(article4, result(1))
        Assert.AreEqual(label4En, result(1).Label)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(label3En, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithLimitAndOffsetFromMultipleTables()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 10)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 20)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 30)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 40)
      Dim article5 = Me.ModelFactory.CreateArticle(5, 50)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "a")
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German, "b")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "c")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "d")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "e")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "f")
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English, "g")
      Dim label4De = Me.ModelFactory.CreateLabel("", 4, German, "h")
      Dim label5En = Me.ModelFactory.CreateLabel("", 5, English, "i")
      Dim label5De = Me.ModelFactory.CreateLabel("", 5, German, "j")

      InsertItems(article1, article2, article3, article4, article5, label1En, label1De, label2En, label2De, label3En, label3De, label4En, label4De, label5En, label5De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        Where(Function(a, l) l.Language = English).
                        OrderByDescending(Function(a) a.Price).
                        Limit(3, 2).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article2, result(0))
        Assert.AreEqual(label2En, result(0).Label)
        Assert.AreEqual(article1, result(1))
        Assert.AreEqual(label1En, result(1).Label)
      End Using
    End Sub

  End Class
End Namespace
