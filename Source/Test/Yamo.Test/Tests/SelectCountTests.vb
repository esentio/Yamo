Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectCountTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectCountWhenEmptyTable()
      Using db = CreateDbContext()
        Dim result = db.From(Of Label).SelectCount()
        Assert.AreEqual(0, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectCountWithoutCondition()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English)

      InsertItems(label1En, label1De, label2En, label2De, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).SelectCount()
        Assert.AreEqual(6, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectCountWithCondition()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English)

      InsertItems(label1En, label1De, label2En, label2De, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).Where(Function(l) l.Language = English).SelectCount()
        Assert.AreEqual(4, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectCountWithJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label1De, label2En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).Join(Of Label)(Function(a, l) a.Id = l.Id).SelectCount()
        Assert.AreEqual(6, result)
      End Using
    End Sub

  End Class
End Namespace
