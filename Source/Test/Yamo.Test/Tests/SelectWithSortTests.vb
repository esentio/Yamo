Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithSortTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "ger"

    <TestMethod()>
    Public Overridable Sub SelectWithOrderBy()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "c")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "a")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "d")
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English, "b")

      InsertItems(label1En, label2En, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderBy(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        Assert.AreEqual("a", result(0).Description)
        Assert.AreEqual("b", result(1).Description)
        Assert.AreEqual("c", result(2).Description)
        Assert.AreEqual("d", result(3).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByDescending()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "c")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "a")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "d")
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English, "b")

      InsertItems(label1En, label2En, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderByDescending(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        Assert.AreEqual("d", result(0).Description)
        Assert.AreEqual("c", result(1).Description)
        Assert.AreEqual("b", result(2).Description)
        Assert.AreEqual("a", result(3).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByMultipleColumns()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "a")
      Dim label1Ger = Me.ModelFactory.CreateLabel("", 1, German, "b")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "c")
      Dim label2Ger = Me.ModelFactory.CreateLabel("", 2, German, "d")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "e")
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German, "f")

      InsertItems(label1En, label1Ger, label2En, label2Ger, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderBy(Function(l) l.Language).ThenBy(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(English, result(0).Language)
        Assert.AreEqual("a", result(0).Description)
        Assert.AreEqual(English, result(1).Language)
        Assert.AreEqual("c", result(1).Description)
        Assert.AreEqual(English, result(2).Language)
        Assert.AreEqual("e", result(2).Description)
        Assert.AreEqual(German, result(3).Language)
        Assert.AreEqual("b", result(3).Description)
        Assert.AreEqual(German, result(4).Language)
        Assert.AreEqual("d", result(4).Description)
        Assert.AreEqual(German, result(5).Language)
        Assert.AreEqual("f", result(5).Description)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderByDescending(Function(l) l.Language).ThenBy(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(German, result(0).Language)
        Assert.AreEqual("b", result(0).Description)
        Assert.AreEqual(German, result(1).Language)
        Assert.AreEqual("d", result(1).Description)
        Assert.AreEqual(German, result(2).Language)
        Assert.AreEqual("f", result(2).Description)
        Assert.AreEqual(English, result(3).Language)
        Assert.AreEqual("a", result(3).Description)
        Assert.AreEqual(English, result(4).Language)
        Assert.AreEqual("c", result(4).Description)
        Assert.AreEqual(English, result(5).Language)
        Assert.AreEqual("e", result(5).Description)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderBy(Function(l) l.Language).ThenByDescending(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(English, result(0).Language)
        Assert.AreEqual("e", result(0).Description)
        Assert.AreEqual(English, result(1).Language)
        Assert.AreEqual("c", result(1).Description)
        Assert.AreEqual(English, result(2).Language)
        Assert.AreEqual("a", result(2).Description)
        Assert.AreEqual(German, result(3).Language)
        Assert.AreEqual("f", result(3).Description)
        Assert.AreEqual(German, result(4).Language)
        Assert.AreEqual("d", result(4).Description)
        Assert.AreEqual(German, result(5).Language)
        Assert.AreEqual("b", result(5).Description)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderByDescending(Function(l) l.Language).ThenByDescending(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(German, result(0).Language)
        Assert.AreEqual("f", result(0).Description)
        Assert.AreEqual(German, result(1).Language)
        Assert.AreEqual("d", result(1).Description)
        Assert.AreEqual(German, result(2).Language)
        Assert.AreEqual("b", result(2).Description)
        Assert.AreEqual(English, result(3).Language)
        Assert.AreEqual("e", result(3).Description)
        Assert.AreEqual(English, result(4).Language)
        Assert.AreEqual("c", result(4).Description)
        Assert.AreEqual(English, result(5).Language)
        Assert.AreEqual("a", result(5).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByColumnsFromMultipleTables()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      article1.Price = 30

      Dim article2 = Me.ModelFactory.CreateArticle(2)
      article2.Price = 10

      Dim article3 = Me.ModelFactory.CreateArticle(3)
      article3.Price = 20

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "a")
      Dim label1Ger = Me.ModelFactory.CreateLabel("", 1, German, "b")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "c")
      Dim label2Ger = Me.ModelFactory.CreateLabel("", 2, German, "d")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "e")
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German, "f")

      InsertItems(article1, article2, article3, label1En, label1Ger, label2En, label2Ger, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(a) a.Price).
                        ThenBy(Function(l) l.Description).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(10D, result(0).Price)
        Assert.AreEqual("c", result(0).Label.Description)
        Assert.AreEqual(10D, result(1).Price)
        Assert.AreEqual("d", result(1).Label.Description)
        Assert.AreEqual(20D, result(2).Price)
        Assert.AreEqual("e", result(2).Label.Description)
        Assert.AreEqual(20D, result(3).Price)
        Assert.AreEqual("f", result(3).Label.Description)
        Assert.AreEqual(30D, result(4).Price)
        Assert.AreEqual("a", result(4).Label.Description)
        Assert.AreEqual(30D, result(5).Price)
        Assert.AreEqual("b", result(5).Label.Description)
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(j) j.T1.Price).
                        ThenBy(Function(j) j.T2.Description).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(10D, result(0).Price)
        Assert.AreEqual("c", result(0).Label.Description)
        Assert.AreEqual(10D, result(1).Price)
        Assert.AreEqual("d", result(1).Label.Description)
        Assert.AreEqual(20D, result(2).Price)
        Assert.AreEqual("e", result(2).Label.Description)
        Assert.AreEqual(20D, result(3).Price)
        Assert.AreEqual("f", result(3).Label.Description)
        Assert.AreEqual(30D, result(4).Price)
        Assert.AreEqual("a", result(4).Label.Description)
        Assert.AreEqual(30D, result(5).Price)
        Assert.AreEqual("b", result(5).Label.Description)
      End Using
    End Sub

  End Class
End Namespace
