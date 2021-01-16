Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithHintsTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected MustOverride Function GetTableHints() As String

    <TestMethod()>
    Public Overridable Sub SelectWithHints()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).WithHints(GetTableHints()).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [Article] [T0] " & GetTableHints()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithHintsOfJoinedTable()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)().WithHints(GetTableHints()).On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsTrue(db.GetLastCommandText().Contains("JOIN [Label] [T1] " & GetTableHints()))
      End Using
    End Sub

  End Class
End Namespace
