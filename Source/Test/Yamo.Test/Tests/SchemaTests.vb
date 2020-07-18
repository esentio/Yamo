Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SchemaTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub InsertRecordInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item1)
        Assert.AreEqual(1, affectedRows)

        affectedRows = db.Insert(item2)
        Assert.AreEqual(1, affectedRows)

        affectedRows = db.Insert(item3)
        Assert.AreEqual(1, affectedRows)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemInSchema).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      item1.Description = "aaa"
      item2.Description = "bbb"
      item3.Description = "ccc"

      InsertItems(item1, item2, item3)

      item2.Description = "bbb - updated"

      Using db = CreateDbContext()
        db.Update(item2)

        Dim result = db.From(Of ItemInSchema).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      item1.Description = "aaa"
      item2.Description = "bbb"
      item3.Description = "ccc"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.Update(Of ItemInSchema).Set(Sub(x) x.Description = "updated").Execute()

        item1.Description = "updated"
        item2.Description = "updated"
        item3.Description = "updated"

        Dim result = db.From(Of ItemInSchema).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.Delete(item2)

        Dim result = db.From(Of ItemInSchema).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordsInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.Delete(Of ItemInSchema).Where(Function(x) 1 < x.Id).Execute()

        Dim result = db.From(Of ItemInSchema).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteRecordInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.SoftDelete(item2)

        Dim result = db.From(Of ItemInSchema).Where(Function(x) Not x.Deleted.HasValue).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteRecordsInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.SoftDelete(Of ItemInSchema).Where(Function(x) 1 < x.Id).Execute()

        Dim result = db.From(Of ItemInSchema).Where(Function(x) Not x.Deleted.HasValue).SelectAll().ToList()
        CollectionAssert.AreEquivalent({item1}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordsInNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemInSchema).
                         SelectAll().ToList()

        CollectionAssert.AreEquivalent({item1, item2, item3}, result1)

        Dim result2 = db.From(Of ItemInSchema).
                         Select(Function(x) x).ToList()

        CollectionAssert.AreEquivalent({item1, item2, item3}, result2)

        Dim result3 = db.From(Of ItemInSchema).
                         Select(Function(x) x.Id).ToList()

        CollectionAssert.AreEquivalent({1, 2, 3}, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordsInNonDefaultSchemaWithJoin()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(item1, item2, item3, article1, article2, article3)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemInSchema).
                        Join(Of Article)(Function(j) j.T1.Id = j.T2.Id).
                        Select(Function(j) (j.T1, j.T2)).ToList()

        CollectionAssert.AreEquivalent({(item1, article1), (item2, article2), (item3, article3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordsWithJoinToNonDefaultSchema()
      Dim item1 = Me.ModelFactory.CreateItemInSchema(1)
      Dim item2 = Me.ModelFactory.CreateItemInSchema(2)
      Dim item3 = Me.ModelFactory.CreateItemInSchema(3)

      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(item1, item2, item3, article1, article2, article3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of ItemInSchema)(Function(j) j.T1.Id = j.T2.Id).
                        Select(Function(j) (j.T1, j.T2)).ToList()

        CollectionAssert.AreEquivalent({(article1, item1), (article2, item2), (article3, item3)}, result)
      End Using
    End Sub

  End Class
End Namespace
