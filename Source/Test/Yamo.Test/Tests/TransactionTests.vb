Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class TransactionTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub Rollback()
      Dim item = Me.ModelFactory.CreateItemWithIdentityId()

      Using db = CreateDbContext()
        db.Database.BeginTransaction()

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(0, item.Id)

        db.Database.RollbackTransaction()
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityId).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.IsNull(result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub Commit()
      Dim item = Me.ModelFactory.CreateItemWithIdentityId()

      Using db = CreateDbContext()
        db.Database.BeginTransaction()

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(0, item.Id)

        db.Database.CommitTransaction()
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityId).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

  End Class
End Namespace
