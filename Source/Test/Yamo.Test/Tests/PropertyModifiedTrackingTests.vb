Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class PropertyModifiedTrackingTests
    Inherits BaseIntegrationTests

    Protected Const ItemWithPropertyModifiedTrackingArchiveTableName As String = "ItemWithPropertyModifiedTrackingArchive"

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
    Public Overridable Sub UpdateRecordWithPropertyModifiedTrackingWithSpecifiedTableName()
      Dim item = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item.Description = "foo"
      item.IntValue = 42

      InsertItemsToArchive(ItemWithPropertyModifiedTrackingArchiveTableName, item)

      Assert.IsFalse(item.IsAnyDbPropertyModified())

      ' don't change any property and try to update

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithPropertyModifiedTrackingArchive)).TableName
        Dim affectedRows = db.Update(Of ItemWithPropertyModifiedTracking)(table).Execute(item)
        Assert.AreEqual(0, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())
      End Using

      ' now change one property, reset tracking, change another property and check, if only that property is updated
      item.Description = "boo"
      item.ResetDbPropertyModifiedTracking()
      item.IntValue = 642

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithPropertyModifiedTrackingArchive)).TableName
        Dim affectedRows = db.Update(Of ItemWithPropertyModifiedTracking)(table).Execute(item)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())
      End Using

      item.Description = "foo"

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithPropertyModifiedTrackingArchive).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithPropertyModifiedTrackingUsingForceUpdateAllFields()
      Dim item = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item.Description = "foo"
      item.IntValue = 42

      Using db = CreateDbContext()
        db.Insert(item)

        db.Update(Of ItemWithPropertyModifiedTracking).
           Set(Sub(x) x.Description = "boo").
           Set(Sub(x) x.IntValue = 10).
           Execute()

        Dim result = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().FirstOrDefault()
        Assert.AreNotEqual(item, result)
        Assert.AreEqual(item.Id, result.Id)
        Assert.AreEqual("boo", result.Description)
        Assert.AreEqual(10, result.IntValue)
      End Using

      Assert.IsFalse(item.IsAnyDbPropertyModified())

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(0, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())

        Dim result = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().FirstOrDefault()
        Assert.AreNotEqual(item, result)
        Assert.AreEqual(item.Id, result.Id)
        Assert.AreEqual("boo", result.Description)
        Assert.AreEqual(10, result.IntValue)
      End Using

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item, forceUpdateAllFields:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())

        Dim result = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
        Assert.AreEqual(item.Id, result.Id)
        Assert.AreEqual("foo", result.Description)
        Assert.AreEqual(42, result.IntValue)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithPropertyModifiedTrackingWithSpecifiedTableNameUsingForceUpdateAllFields()
      Dim item = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      item.Description = "foo"
      item.IntValue = 42

      InsertItemsToArchive(ItemWithPropertyModifiedTrackingArchiveTableName, item)

      Using db = CreateDbContext()
        db.Update(Of ItemWithPropertyModifiedTracking)(ItemWithPropertyModifiedTrackingArchiveTableName).
           Set(Sub(x) x.Description = "boo").
           Set(Sub(x) x.IntValue = 10).
           Execute()

        Dim result = db.From(Of ItemWithPropertyModifiedTracking)(ItemWithPropertyModifiedTrackingArchiveTableName).SelectAll().FirstOrDefault()
        Assert.AreNotEqual(item, result)
        Assert.AreEqual(item.Id, result.Id)
        Assert.AreEqual("boo", result.Description)
        Assert.AreEqual(10, result.IntValue)
      End Using

      Assert.IsFalse(item.IsAnyDbPropertyModified())

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithPropertyModifiedTracking)(ItemWithPropertyModifiedTrackingArchiveTableName).Execute(item)
        Assert.AreEqual(0, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())

        Dim result = db.From(Of ItemWithPropertyModifiedTracking)(ItemWithPropertyModifiedTrackingArchiveTableName).SelectAll().FirstOrDefault()
        Assert.AreNotEqual(item, result)
        Assert.AreEqual(item.Id, result.Id)
        Assert.AreEqual("boo", result.Description)
        Assert.AreEqual(10, result.IntValue)
      End Using

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithPropertyModifiedTracking)(ItemWithPropertyModifiedTrackingArchiveTableName).Execute(item, forceUpdateAllFields:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.IsFalse(item.IsAnyDbPropertyModified())

        Dim result = db.From(Of ItemWithPropertyModifiedTracking)(ItemWithPropertyModifiedTrackingArchiveTableName).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
        Assert.AreEqual(item.Id, result.Id)
        Assert.AreEqual("foo", result.Description)
        Assert.AreEqual(42, result.IntValue)
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
        Dim result1 = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not x.IsAnyDbPropertyModified()))

        Dim result2 = db.From(Of ItemWithPropertyModifiedTracking).SelectAll().FirstOrDefault()
        Assert.IsTrue(Not result2.IsAnyDbPropertyModified())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordWithPropertyModifiedTrackingFromJoinedTable()
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
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithPropertyModifiedTracking)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not DirectCast(x.RelatedItem, ItemWithPropertyModifiedTracking).IsAnyDbPropertyModified()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithPropertyModifiedTracking)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()
        Assert.IsTrue(Not DirectCast(result2.RelatedItem, ItemWithPropertyModifiedTracking).IsAnyDbPropertyModified())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithPropertyModifiedTrackingFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item2 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item3 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                        SelectAll()
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not x.IsAnyDbPropertyModified()))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                        SelectAll()
                              End Function).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(Not result2.IsAnyDbPropertyModified())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithPropertyModifiedTrackingFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item2 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item3 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                        Select(Function(x) New NonModelObjectWithPropertyModifiedTracking With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not x.IsAnyDbPropertyModified()))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                        Select(Function(x) New NonModelObjectWithPropertyModifiedTracking With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(Not result2.IsAnyDbPropertyModified())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithPropertyModifiedTrackingFromJoinSubquery()
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
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not DirectCast(x.RelatedItem, ItemWithPropertyModifiedTracking).IsAnyDbPropertyModified()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(Not DirectCast(result2.RelatedItem, ItemWithPropertyModifiedTracking).IsAnyDbPropertyModified())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithPropertyModifiedTrackingFromJoinSubquery()
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
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                         Select(Function(x) New NonModelObjectWithPropertyModifiedTracking With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not DirectCast(x.RelatedItem, NonModelObjectWithPropertyModifiedTracking).IsAnyDbPropertyModified()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithPropertyModifiedTracking).
                                         Select(Function(x) New NonModelObjectWithPropertyModifiedTracking With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(Not DirectCast(result2.RelatedItem, NonModelObjectWithPropertyModifiedTracking).IsAnyDbPropertyModified())
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
                         Select(Function(x) x).
                         FirstOrDefault()
        Assert.IsTrue(Not result2.IsAnyDbPropertyModified())

        Dim result3 = db.From(Of ItemWithPropertyModifiedTracking).
                         Select(Function(x) (x.Id, Entity:=x)).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        Assert.IsTrue(result3.All(Function(x) Not x.Entity.IsAnyDbPropertyModified()))

        Dim result4 = db.From(Of ItemWithPropertyModifiedTracking).
                         Select(Function(x) (x.Id, Entity:=x)).
                         FirstOrDefault()
        Assert.IsTrue(Not result4.Entity.IsAnyDbPropertyModified())

        Dim result5 = db.From(Of ItemWithPropertyModifiedTracking).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         ToList()
        Assert.AreEqual(3, result5.Count)
        Assert.IsTrue(result5.All(Function(x) Not x.Entity.IsAnyDbPropertyModified()))

        Dim result6 = db.From(Of ItemWithPropertyModifiedTracking).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         FirstOrDefault()
        Assert.IsTrue(Not result6.Entity.IsAnyDbPropertyModified())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryModelEntityRecordWithPropertyModifiedTracking()
      Dim item1 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item2 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()
      Dim item3 = Me.ModelFactory.CreateItemWithPropertyModifiedTracking()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of ItemWithPropertyModifiedTracking)($"SELECT {Sql.Model.Columns(Of ItemWithPropertyModifiedTracking)} FROM ItemWithPropertyModifiedTracking ORDER BY Id")
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) Not x.IsAnyDbPropertyModified()))

        Dim result2 = db.QueryFirstOrDefault(Of ItemWithPropertyModifiedTracking)($"SELECT {Sql.Model.Columns(Of ItemWithPropertyModifiedTracking)} FROM ItemWithPropertyModifiedTracking ORDER BY Id")
        Assert.IsTrue(Not result2.IsAnyDbPropertyModified())
      End Using
    End Sub

  End Class
End Namespace
