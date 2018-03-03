﻿Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class AuditFieldsTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub InsertRecordWithCreatedFieldsAndSetAutoFields()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(IsApproximatelyNow(item.Created))
        Assert.AreEqual(userId, item.CreatedUserId)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithCreatedFieldsAndDontSetAutoFields()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      item.Created = Helpers.Calendar.Now.AddYears(-10)

      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item, setAutoFields:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(IsApproximatelyNow(item.Created))
        Assert.AreNotEqual(userId, item.CreatedUserId)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithCreatedFieldsAndSetAutoFields()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      item.Description = "updated"

      Assert.IsFalse(item.Modified.HasValue)
      Assert.IsFalse(item.ModifiedUserId.HasValue)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Update(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(item.Modified.HasValue)
        Assert.IsTrue(IsApproximatelyNow(item.Modified.Value))
        Assert.IsTrue(item.ModifiedUserId.HasValue)
        Assert.AreEqual(userId, item.ModifiedUserId.Value)
      End Using

      item.Description = "updated2"

      userId += 1

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Update(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(item.Modified.HasValue)
        Assert.IsTrue(IsApproximatelyNow(item.Modified.Value))
        Assert.IsTrue(item.ModifiedUserId.HasValue)
        Assert.AreEqual(userId, item.ModifiedUserId.Value)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithCreatedFieldsAndSetAutoFieldsWhenRecordHasPropertyModifiedTrackingAndIsNotChanged()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Assert.IsFalse(item.Modified.HasValue)
      Assert.IsFalse(item.ModifiedUserId.HasValue)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Update(item)
        Assert.AreEqual(0, affectedRows)
        Assert.IsFalse(item.Modified.HasValue)
        Assert.IsFalse(item.ModifiedUserId.HasValue)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithCreatedFieldsAndDontSetAutoFields()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      item.Description = "updated"

      Assert.IsFalse(item.Modified.HasValue)
      Assert.IsFalse(item.ModifiedUserId.HasValue)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Update(item, setAutoFields:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.Modified.HasValue)
        Assert.IsFalse(item.ModifiedUserId.HasValue)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteRecord()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Assert.IsFalse(item.Deleted.HasValue)
      Assert.IsFalse(item.DeletedUserId.HasValue)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.SoftDelete(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(item.Deleted.HasValue)
        Assert.IsTrue(IsApproximatelyNow(item.Deleted.Value))
        Assert.IsTrue(item.DeletedUserId.HasValue)
        Assert.AreEqual(userId, item.DeletedUserId.Value)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteUpdatedRecord()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Dim originalDescription = item.Description
      item.Description = "updated"

      Assert.IsFalse(item.Deleted.HasValue)
      Assert.IsFalse(item.DeletedUserId.HasValue)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.SoftDelete(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsTrue(item.Deleted.HasValue)
        Assert.IsTrue(IsApproximatelyNow(item.Deleted.Value))
        Assert.IsTrue(item.DeletedUserId.HasValue)
        Assert.AreEqual(userId, item.DeletedUserId.Value)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAuditFields).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreNotEqual(item, result)
        item.Description = originalDescription
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteRecordThatDoesntSupportSoftDelete()
      Dim item = Me.ModelFactory.CreateItemWithIdentityId()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Try
        Using db = CreateDbContext()
          db.UserId = userId

          Dim affectedRows = db.SoftDelete(item)
        End Using

        Assert.Fail()
      Catch ex As NotSupportedException
      Catch ex As Exception
        Assert.Fail(ex.Message)
      End Try
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteNonExistingRecord()
      Dim item = Me.ModelFactory.CreateItemWithAuditFields()
      Dim userId = 42

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.SoftDelete(item)
        Assert.AreEqual(0, affectedRows)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeleteAllRecords()
      Dim item1 = Me.ModelFactory.CreateItemWithAuditFields()
      Dim item2 = Me.ModelFactory.CreateItemWithAuditFields()
      Dim item3 = Me.ModelFactory.CreateItemWithAuditFields()

      Dim userId = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.SoftDelete(Of ItemWithAuditFields).Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAuditFields).SelectAll().ToList()
        Assert.IsTrue(result.All(Function(x) x.Deleted.HasValue AndAlso IsApproximatelyNow(x.Deleted.Value) AndAlso x.DeletedUserId.HasValue AndAlso x.DeletedUserId.Value = userId))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SoftDeletelRecordsWithCondition()
      Dim item1 = Me.ModelFactory.CreateItemWithAuditFields()
      item1.Description = "d"

      Dim item2 = Me.ModelFactory.CreateItemWithAuditFields()
      item2.Description = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAuditFields()
      item3.Description = "d"

      Dim userId = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        db.UserId = userId

        Dim affectedRows = db.SoftDelete(Of ItemWithAuditFields).Where(Function(x) x.Description = "d").Execute()
        Assert.AreEqual(2, affectedRows)

        Dim result = db.From(Of ItemWithAuditFields).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(x) x.Id = item1.Id AndAlso x.Deleted.HasValue AndAlso IsApproximatelyNow(x.Deleted.Value) AndAlso x.DeletedUserId.HasValue AndAlso x.DeletedUserId.Value = userId))
        Assert.IsNotNull(result.SingleOrDefault(Function(x) x.Id = item2.Id AndAlso Not (x.Deleted.HasValue AndAlso IsApproximatelyNow(x.Deleted.Value) AndAlso x.DeletedUserId.HasValue AndAlso x.DeletedUserId.Value = userId)))
        Assert.IsNotNull(result.SingleOrDefault(Function(x) x.Id = item3.Id AndAlso x.Deleted.HasValue AndAlso IsApproximatelyNow(x.Deleted.Value) AndAlso x.DeletedUserId.HasValue AndAlso x.DeletedUserId.Value = userId))
      End Using
    End Sub

    Protected Overridable Function IsApproximatelyNow(value As DateTime) As Boolean
      Return Math.Abs(DateTime.Now.Subtract(value).TotalSeconds) < 5
    End Function

  End Class
End Namespace
