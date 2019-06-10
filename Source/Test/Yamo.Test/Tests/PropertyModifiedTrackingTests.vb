Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class PropertyModifiedTrackingTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub InsertRecordWithPropertyModifiedTracking()
      Dim item = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()

      Assert.IsTrue(item.IsAnyDbPropertyModified())

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())
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

      Assert.IsFalse(item.IsAnyDbPropertyModified())

      ' don't change any property and try to update

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(0, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())
      End Using

      ' now change one property, reset tracking, change another property and check, if only that property is updated
      item.Description = "boo"
      item.ResetDbPropertyModifiedTracking()
      item.IntValue = 642

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())
      End Using

      item.Description = "foo"

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordWithPropertyModifiedTracking()
      Dim item1 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item2 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item3 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        Assert.IsTrue(result.All(Function(x) Not x.IsAnyDbPropertyModified()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordWithPropertyModifiedTrackingAsJoinedTable()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItem).
                        Join(Of ItemWithPropertyModifiedTracking)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                        SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        Assert.IsTrue(result.All(Function(x) Not DirectCast(x.RelatedItem, ItemWithPropertyModifiedTracking).IsAnyDbPropertyModified()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfRecordWithPropertyModifiedTracking()
      Dim item1 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item2 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item3 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithPropertyModifiedTracking).
                        Select(Function(x) x).
                        ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not x.IsAnyDbPropertyModified()))

        Dim result2 = db.From(Of ItemWithPropertyModifiedTracking).
                        Select(Function(x) (x.Id, Entity:=x)).
                        ToList()
        Assert.AreEqual(3, result2.Count)
        Assert.IsTrue(result2.All(Function(x) Not x.Entity.IsAnyDbPropertyModified()))

        Dim result3 = db.From(Of ItemWithPropertyModifiedTracking).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        Assert.IsTrue(result3.All(Function(x) Not x.Entity.IsAnyDbPropertyModified()))
      End Using
    End Sub

  End Class
End Namespace
