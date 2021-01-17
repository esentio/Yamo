Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class WithHintsTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected MustOverride Function GetSelectTableHints() As String

    Protected MustOverride Function GetInsertTableHints() As String

    Protected MustOverride Function GetUpdateTableHints() As String

    Protected MustOverride Function GetDeleteTableHints() As String

    Protected MustOverride Function GetSoftDeleteTableHints() As String

    <TestMethod()>
    Public Overridable Sub SelectWithHints()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).WithHints(GetSelectTableHints()).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [Article] [T0] " & GetSelectTableHints()))
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
                        Join(Of Label)().WithHints(GetSelectTableHints()).On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsTrue(db.GetLastCommandText().Contains("JOIN [Label] [T1] " & GetSelectTableHints()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithHints()
      Dim article = Me.ModelFactory.CreateArticle(1)

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(Of Article)().WithHints(GetInsertTableHints()).Execute(article)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("INTO [Article] " & GetInsertTableHints()))

        Dim result = db.From(Of Article).SelectAll().FirstOrDefault()
        Assert.AreEqual(article, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithHints()
      Dim article = Me.ModelFactory.CreateArticle(1)

      InsertItems(article)

      Using db = CreateDbContext()
        article.Price = 42.6D

        Dim affectedRows = db.Update(Of Article).WithHints(GetUpdateTableHints()).Execute(article)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("UPDATE [Article] " & GetUpdateTableHints()))

        Dim result = db.From(Of Article).SelectAll().FirstOrDefault()
        Assert.AreEqual(article, result)

        affectedRows = db.Update(Of Article).WithHints(GetUpdateTableHints()).Set(Sub(x) x.Price = 420.6D).Execute()
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("UPDATE [Article] " & GetUpdateTableHints()))

        article.Price = 420.6D

        result = db.From(Of Article).SelectAll().FirstOrDefault()
        Assert.AreEqual(article, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordWithHints()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Delete(Of Article).WithHints(GetDeleteTableHints()).Execute(article1)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [Article] " & GetDeleteTableHints()))

        Dim result = db.From(Of Article).SelectAll().ToList()
        CollectionAssert.AreEquivalent({article2, article3}, result)

        affectedRows = db.Delete(Of Article).WithHints(GetDeleteTableHints()).Where(Function(x) x.Id = 2).Execute()
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [Article] " & GetDeleteTableHints()))

        result = db.From(Of Article).SelectAll().ToList()
        CollectionAssert.AreEquivalent({article3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteRecordWithHints()
      Dim item1 = Me.ModelFactory.CreateItemWithAuditFields()
      Dim item2 = Me.ModelFactory.CreateItemWithAuditFields()
      Dim item3 = Me.ModelFactory.CreateItemWithAuditFields()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.UserId = 42

        Dim affectedRows = db.SoftDelete(Of ItemWithAuditFields).WithHints(GetDeleteTableHints()).Execute(item1)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("UPDATE [ItemWithAuditFields] " & GetDeleteTableHints()))

        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) Not x.Deleted.HasValue).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item2, item3}, result)

        affectedRows = db.SoftDelete(Of ItemWithAuditFields).WithHints(GetDeleteTableHints()).Where(Function(x) x.Id = item2.Id).Execute()
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(db.GetLastCommandText().Contains("UPDATE [ItemWithAuditFields] " & GetDeleteTableHints()))

        result = db.From(Of ItemWithAuditFields).Where(Function(x) Not x.Deleted.HasValue).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item3}, result)
      End Using
    End Sub

  End Class
End Namespace
