Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectDistinctTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectWithDistinct()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim label1De = Me.ModelFactory.CreateLabel(NameOf(Article), 1, German)
      Dim label2En = Me.ModelFactory.CreateLabel(NameOf(Article), 2, English)
      Dim label2De = Me.ModelFactory.CreateLabel(NameOf(Article), 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)

      InsertItems(article1, article2, article3, label1En, label1De, label2En, label2De, label3En)

      ' distinct doesn't make difference here
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id = 1).
                        SelectAll().
                        Distinct().
                        FirstOrDefault()

        Assert.AreEqual(article1, result)
      End Using

      ' distinct doesn't make difference here
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        SelectAll().
                        Distinct().
                        ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(article1, article1Result)

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(article2, article2Result)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(article3, article3Result)
      End Using

      ' TODO: uncomment when calling FirstOrDefault is supported in this scenario
      'Using db = CreateDbContext()
      '  Dim result = db.From(Of Article).
      '                  Join(Of Label)(Function(a, l) a.Id = l.Id).
      '                  Where(Function(j) j.T1.Id = 1).
      '                  SelectAll().
      '                  Distinct().
      '                  FirstOrDefault()

      '  Assert.AreEqual(article1, result)
      'End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        SelectAll().
                        ExcludeT2().
                        Distinct().
                        ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(article1, article1Result)

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(article2, article2Result)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(article3, article3Result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectWithDistinct()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim label1De = Me.ModelFactory.CreateLabel(NameOf(Article), 1, German)
      Dim label2En = Me.ModelFactory.CreateLabel(NameOf(Article), 2, English)
      Dim label2De = Me.ModelFactory.CreateLabel(NameOf(Article), 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)

      InsertItems(article1, article2, article3, label1En, label1De, label2En, label2De, label3En)

      ' distinct doesn't make difference here
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id = 1).
                        Select(Function(x) x).
                        Distinct().
                        FirstOrDefault()

        Assert.AreEqual(article1, result)
      End Using

      ' distinct doesn't make difference here
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Select(Function(x) x).
                        Distinct().
                        ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(article1, article1Result)

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(article2, article2Result)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(article3, article3Result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        Where(Function(j) j.T1.Id = 1).
                        Select(Function(j) j.T1).
                        Distinct().
                        FirstOrDefault()

        Assert.AreEqual(article1, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        Select(Function(j) j.T1).
                        Distinct().
                        ToList()

        Assert.AreEqual(3, result.Count)

        Dim article1Result = result.First(Function(a) a.Id = 1)
        Assert.AreEqual(article1, article1Result)

        Dim article2Result = result.First(Function(a) a.Id = 2)
        Assert.AreEqual(article2, article2Result)

        Dim article3Result = result.First(Function(a) a.Id = 3)
        Assert.AreEqual(article3, article3Result)
      End Using

      ' distinct doesn't make difference here
      Using db = CreateDbContext()
        Dim result = db.From(Of Label).
                        Where(Function(x) x.Id = 3).
                        Select(Function(x) x.Language).
                        Distinct().
                        FirstOrDefault()

        Assert.AreEqual(English, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).
                        Select(Function(x) x.Language).
                        Distinct().
                        ToList()

        Assert.AreEqual(2, result.Count)
        Assert.IsTrue(result.Contains(English))
        Assert.IsTrue(result.Contains(German))
      End Using
    End Sub

  End Class
End Namespace
