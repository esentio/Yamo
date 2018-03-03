Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class UpdateTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithAllSupportedValues()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Dim id = item.Id

      ' instead of setting all properties, we generate new POCO with the same id

      item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()
      item.Id = id
      UpdateRecordWithAllSupportedValues(item)

      item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      item.Id = id
      UpdateRecordWithAllSupportedValues(item)

      item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.Id = id
      UpdateRecordWithAllSupportedValues(item)
    End Sub

    Protected Overridable Sub UpdateRecordWithAllSupportedValues(item As ItemWithAllSupportedValues)
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithPropertyModifiedTracking()
      Dim item = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item.Description = "foo"
      item.IntValue = 42

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Assert.IsFalse(item.IsAnyPropertyModified())

      ' don't change any property and try to update

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(0, affectedRows)
        Assert.IsFalse(item.IsAnyPropertyModified())
      End Using

      ' now change one property, reset tracking, change another property and check, if only that property is updated
      item.Description = "boo"
      item.ResetPropertyModifiedTracking()
      item.IntValue = 642

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.IsAnyPropertyModified())
      End Using

      item.Description = "foo"

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateNonExistingRecord()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(0, affectedRows)
      End Using
    End Sub

  End Class
End Namespace
