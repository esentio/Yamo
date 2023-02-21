Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class InitializableTests
    Inherits BaseIntegrationTests

    Protected Const ItemWithInitializationArchiveTableName As String = "ItemWithInitializationArchive"

    <TestMethod()>
    Public Overridable Sub SelectRecordWithInitialization()
      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithInitialization).SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsInitialized))

        Dim result2 = db.From(Of ItemWithInitialization).SelectAll().FirstOrDefault()
        Assert.IsTrue(result2.IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordWithInitializationFromJoinedTable()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithInitialization)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, ItemWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithInitialization)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()
        Assert.IsTrue(DirectCast(result2.RelatedItem, ItemWithInitialization).IsInitialized)
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithInitialization)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItems.Single(), ItemWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithInitialization)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()
        Assert.IsTrue(DirectCast(result2.RelatedItems.Single(), ItemWithInitialization).IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithInitializationFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                        SelectAll()
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsInitialized))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                        SelectAll()
                              End Function).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(result2.IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithInitializationFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                        Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsInitialized))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                        Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(result2.IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithInitializationFromJoinSubquery()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()
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
                                Return c.From(Of ItemWithInitialization).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, ItemWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, ItemWithInitialization).IsInitialized)
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItems.Single(), ItemWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItems.Single(), ItemWithInitialization).IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithInitializationFromJoinSubquery()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()
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
                                Return c.From(Of ItemWithInitialization).
                                         Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, NonModelObjectWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                         Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, NonModelObjectWithInitialization).IsInitialized)
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                         Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItems.Single(), NonModelObjectWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithInitialization).
                                         Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItems.Single(), NonModelObjectWithInitialization).IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeModelEntityRecordWithInitialization()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithInitialization)(Function(j) j.T1.Id = j.T2.Id).
                         SelectAll().ExcludeT2().
                         Include(Sub(j) j.T1.RelatedItem = j.T2).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, ItemWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithInitialization)(Function(j) j.T1.Id = j.T2.Id).
                         SelectAll().ExcludeT2().
                         Include(Sub(j) j.T1.RelatedItem = j.T2).
                         FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, ItemWithInitialization).IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeNonModelAdHocTypeRecordWithInitialization()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of LinkedItem).
                         SelectAll().
                         Include(Sub(x) x.RelatedItem = New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description}).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) DirectCast(x.RelatedItem, NonModelObjectWithInitialization).IsInitialized))

        Dim result2 = db.From(Of LinkedItem).
                         SelectAll().
                         Include(Sub(x) x.RelatedItem = New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description}).
                         FirstOrDefault()

        Assert.IsTrue(DirectCast(result2.RelatedItem, NonModelObjectWithInitialization).IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfRecordWithInitialization()
      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithInitialization).
                         Select(Function(x) x).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsInitialized))

        Dim result2 = db.From(Of ItemWithInitialization).
                         Select(Function(x) x).
                         FirstOrDefault()
        Assert.IsTrue(result2.IsInitialized)

        Dim result3 = db.From(Of ItemWithInitialization).
                         Select(Function(x) (x.Id, Entity:=x)).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        Assert.IsTrue(result3.All(Function(x) x.Entity.IsInitialized))

        Dim result4 = db.From(Of ItemWithInitialization).
                         Select(Function(x) (x.Id, Entity:=x)).
                         FirstOrDefault()
        Assert.IsTrue(result4.Entity.IsInitialized)

        Dim result5 = db.From(Of ItemWithInitialization).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         ToList()
        Assert.AreEqual(3, result5.Count)
        Assert.IsTrue(result5.All(Function(x) x.Entity.IsInitialized))

        Dim result6 = db.From(Of ItemWithInitialization).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         FirstOrDefault()
        Assert.IsTrue(result6.Entity.IsInitialized)

        Dim result7 = db.From(Of ItemWithInitialization).
                         Select(Function(x) New ItemWithInitialization With {.Id = x.Id, .Description = x.Description}).
                         ToList()
        Assert.AreEqual(3, result7.Count)
        Assert.IsTrue(result7.All(Function(x) x.IsInitialized))

        Dim result8 = db.From(Of ItemWithInitialization).
                         Select(Function(x) New ItemWithInitialization With {.Id = x.Id, .Description = x.Description}).
                         FirstOrDefault()
        Assert.IsTrue(result8.IsInitialized)

        Dim result9 = db.From(Of ItemWithInitialization).
                         Select(Function(x) New ItemWithInitialization(x.Id) With {.Description = x.Description}).
                         ToList()
        Assert.AreEqual(3, result9.Count)
        Assert.IsTrue(result9.All(Function(x) x.IsInitialized))

        Dim result10 = db.From(Of ItemWithInitialization).
                          Select(Function(x) New ItemWithInitialization(x.Id) With {.Description = x.Description}).
                          FirstOrDefault()
        Assert.IsTrue(result10.IsInitialized)

        Dim result11 = db.From(Of ItemWithInitialization).
                          Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description}).
                          ToList()
        Assert.AreEqual(3, result11.Count)
        Assert.IsTrue(result11.All(Function(x) x.IsInitialized))

        Dim result12 = db.From(Of ItemWithInitialization).
                          Select(Function(x) New NonModelObjectWithInitialization With {.IntValue = x.Id, .StringValue = x.Description}).
                          FirstOrDefault()
        Assert.IsTrue(result12.IsInitialized)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryModelEntityRecordWithInitialization()
      Dim item1 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item2 = Me.ModelFactory.CreateItemWithInitialization()
      Dim item3 = Me.ModelFactory.CreateItemWithInitialization()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of ItemWithInitialization)($"SELECT {Sql.Model.Columns(Of ItemWithInitialization)} FROM ItemWithInitialization ORDER BY Id")
        Assert.AreEqual(3, result1.Count)
        Assert.IsTrue(result1.All(Function(x) x.IsInitialized))

        Dim result2 = db.QueryFirstOrDefault(Of ItemWithInitialization)($"SELECT {Sql.Model.Columns(Of ItemWithInitialization)} FROM ItemWithInitialization ORDER BY Id")
        Assert.IsTrue(result2.IsInitialized)
      End Using
    End Sub

  End Class
End Namespace
