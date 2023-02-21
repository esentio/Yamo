Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SupportDbLoadTests
    Inherits BaseIntegrationTests

    Protected Const ItemWithSupportDbLoadArchiveTableName As String = "ItemWithSupportDbLoadArchive"

    <TestMethod()>
    Public Overridable Sub SelectRecordWithSupportDbLoad()
      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithSupportDbLoad).SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsLoaded))

        Dim result2 = db.From(Of ItemWithSupportDbLoad).SelectAll().FirstOrDefault()
        Assert.IsTrue(result2.IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordWithSupportDbLoadFromJoinedTable()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithSupportDbLoad)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, ItemWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithSupportDbLoad)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()
        Assert.IsTrue(DirectCast(result2.RelatedItem, ItemWithSupportDbLoad).IsLoaded)
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithSupportDbLoad)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItems.Single(), ItemWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithSupportDbLoad)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()
        Assert.IsTrue(DirectCast(result2.RelatedItems.Single(), ItemWithSupportDbLoad).IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithSupportDbLoadFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         SelectAll()
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsLoaded))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         SelectAll()
                              End Function).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(result2.IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithSupportDbLoadFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsLoaded))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(result2.IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithSupportDbLoadFromJoinSubquery()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, ItemWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, ItemWithSupportDbLoad).IsLoaded)
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItems.Single(), ItemWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItems.Single(), ItemWithSupportDbLoad).IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithSupportDbLoadFromJoinSubquery()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, NonModelObjectWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, NonModelObjectWithSupportDbLoad).IsLoaded)
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItems.Single(), NonModelObjectWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithSupportDbLoad).
                                         Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItems.Single(), NonModelObjectWithSupportDbLoad).IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeModelEntityRecordWithSupportDbLoad()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithSupportDbLoad)(Function(j) j.T1.Id = j.T2.Id).
                         SelectAll().ExcludeT2().
                         Include(Sub(j) j.T1.RelatedItem = j.T2).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, ItemWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithSupportDbLoad)(Function(j) j.T1.Id = j.T2.Id).
                         SelectAll().ExcludeT2().
                         Include(Sub(j) j.T1.RelatedItem = j.T2).
                         FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, ItemWithSupportDbLoad).IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeNonModelAdHocTypeRecordWithSupportDbLoad()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         SelectAll().
                         Include(Sub(x) x.RelatedItem = New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description}).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, NonModelObjectWithSupportDbLoad).IsLoaded))

        Dim result2 = db.From(Of LinkedItem).
                         SelectAll().
                         Include(Sub(x) x.RelatedItem = New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description}).
                         FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, NonModelObjectWithSupportDbLoad).IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfRecordWithSupportDbLoad()
      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) x).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsLoaded))

        Dim result2 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) x).
                         FirstOrDefault()
        Assert.IsTrue(result2.IsLoaded)

        Dim result3 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) (x.Id, Entity:=x)).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        Assert.IsTrue(result3.All(Function(x) x.Entity.IsLoaded))

        Dim result4 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) (x.Id, Entity:=x)).
                         FirstOrDefault()
        Assert.IsTrue(result4.Entity.IsLoaded)

        Dim result5 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         ToList()
        Assert.AreEqual(3, result5.Count)
        Assert.IsTrue(result5.All(Function(x) x.Entity.IsLoaded))

        Dim result6 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         FirstOrDefault()
        Assert.IsTrue(result6.Entity.IsLoaded)

        Dim result7 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) New ItemWithSupportDbLoad With {.Id = x.Id, .Description = x.Description}).
                         ToList()
        Assert.AreEqual(3, result7.Count)
        Assert.IsTrue(result7.All(Function(x) x.IsLoaded))

        Dim result8 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) New ItemWithSupportDbLoad With {.Id = x.Id, .Description = x.Description}).
                         FirstOrDefault()
        Assert.IsTrue(result8.IsLoaded)

        Dim result9 = db.From(Of ItemWithSupportDbLoad).
                         Select(Function(x) New ItemWithSupportDbLoad(x.Id) With {.Description = x.Description}).
                         ToList()
        Assert.AreEqual(3, result9.Count)
        Assert.IsTrue(result9.All(Function(x) x.IsLoaded))

        Dim result10 = db.From(Of ItemWithSupportDbLoad).
                          Select(Function(x) New ItemWithSupportDbLoad(x.Id) With {.Description = x.Description}).
                          FirstOrDefault()
        Assert.IsTrue(result10.IsLoaded)

        Dim result11 = db.From(Of ItemWithSupportDbLoad).
                          Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description}).
                          ToList()
        Assert.AreEqual(3, result11.Count)
        Assert.IsTrue(result11.All(Function(x) x.IsLoaded))

        Dim result12 = db.From(Of ItemWithSupportDbLoad).
                          Select(Function(x) New NonModelObjectWithSupportDbLoad With {.IntValue = x.Id, .StringValue = x.Description}).
                          FirstOrDefault()
        Assert.IsTrue(result12.IsLoaded)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryModelEntityRecordWithSupportDbLoad()
      Dim item1 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item2 = Me.ModelFactory.CreateItemWithSupportDbLoad()
      Dim item3 = Me.ModelFactory.CreateItemWithSupportDbLoad()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of ItemWithSupportDbLoad)($"SELECT {Sql.Model.Columns(Of ItemWithSupportDbLoad)} FROM ItemWithSupportDbLoad ORDER BY Id")
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsLoaded))

        Dim result2 = db.QueryFirstOrDefault(Of ItemWithSupportDbLoad)($"SELECT {Sql.Model.Columns(Of ItemWithSupportDbLoad)} FROM ItemWithSupportDbLoad ORDER BY Id")
        Assert.IsTrue(result2.IsLoaded)
      End Using
    End Sub

  End Class
End Namespace
