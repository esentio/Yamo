Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class ActionsTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectRecordWithActionHistory()
      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of ItemWithActionHistory).SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of ItemWithActionHistory).SelectAll().FirstOrDefault()
        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordWithActionHistoryFromJoinedTable()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItem).
                OfType(Of ItemWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()
        CheckHistory(expectedHistory, DirectCast(result2.RelatedItem, ItemWithActionHistory).GetActionHistory())
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()
        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItems.Single()).
                OfType(Of ItemWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()
        CheckHistory(expectedHistory, DirectCast(result2.RelatedItems.Single(), ItemWithActionHistory).GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithActionHistoryFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                        SelectAll()
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                        SelectAll()
                              End Function).
                         SelectAll().FirstOrDefault()

        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithActionHistoryFromFromSubquery()
      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()

        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         SelectAll().FirstOrDefault()

        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithActionHistoryFromJoinSubquery()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItem).
                OfType(Of ItemWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        CheckHistory(expectedHistory, DirectCast(result2.RelatedItem, ItemWithActionHistory).GetActionHistory())
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItems.Single()).
                OfType(Of ItemWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         SelectAll()
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()

        CheckHistory(expectedHistory, DirectCast(result2.RelatedItems.Single(), ItemWithActionHistory).GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithActionHistoryFromJoinSubquery()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItem).
                OfType(Of NonModelObjectWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItem).
                         SelectAll().FirstOrDefault()

        CheckHistory(expectedHistory, DirectCast(result2.RelatedItem, NonModelObjectWithActionHistory).GetActionHistory())
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItems).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItems.Single()).
                OfType(Of NonModelObjectWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         On(Function(j) j.T1.Id = j.T2.IntValue).As(Function(x) x.RelatedItems).
                         SelectAll().FirstOrDefault()

        CheckHistory(expectedHistory, DirectCast(result2.RelatedItems.Single(), NonModelObjectWithActionHistory).GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeModelEntityRecordWithActionHistory()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of LinkedItem).
                         Join(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         SelectAll().ExcludeT2().
                         Include(Sub(j) j.T1.RelatedItem = j.T2).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItem).
                OfType(Of ItemWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         Join(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         SelectAll().ExcludeT2().
                         Include(Sub(j) j.T1.RelatedItem = j.T2).
                         FirstOrDefault()

        CheckHistory(expectedHistory, DirectCast(result2.RelatedItem, ItemWithActionHistory).GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIncludeNonModelAdHocTypeRecordWithActionHistory()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()

        Dim result1 = db.From(Of LinkedItem).
                         SelectAll().
                         Include(Sub(x) x.RelatedItem = New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description}).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        result1.Select(Function(x) x.RelatedItem).
                OfType(Of NonModelObjectWithActionHistory).ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of LinkedItem).
                         SelectAll().
                         Include(Sub(x) x.RelatedItem = New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description}).
                         FirstOrDefault()

        CheckHistory(expectedHistory, DirectCast(result2.RelatedItem, NonModelObjectWithActionHistory).GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfRecordWithActionHistory()
      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) x).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) x).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result2.GetActionHistory())

        Dim result3 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) (x.Id, Entity:=x)).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        result3.Select(Function(x) x.Entity).
                ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result4 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) (x.Id, Entity:=x)).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result4.Entity.GetActionHistory())

        Dim result5 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         ToList()
        Assert.AreEqual(3, result5.Count)
        result5.Select(Function(x) x.Entity).
                ToList().
                ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result6 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New With {x.Id, .Entity = x}).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result6.Entity.GetActionHistory())

        expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()

        Dim result7 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New ItemWithActionHistory With {.Id = x.Id, .Description = x.Description}).
                         ToList()
        Assert.AreEqual(3, result7.Count)
        result7.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result8 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New ItemWithActionHistory With {.Id = x.Id, .Description = x.Description}).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result8.GetActionHistory())

        expectedHistory = GetExpectedHistoryForCustomSelectWith1DbPropertySetViaInitializer()

        Dim result9 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New ItemWithActionHistory(x.Id) With {.Description = x.Description}).
                         ToList()
        Assert.AreEqual(3, result9.Count)
        result9.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result10 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New ItemWithActionHistory(x.Id) With {.Description = x.Description}).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result10.GetActionHistory())

        expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()

        Dim result11 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description}).
                         ToList()
        Assert.AreEqual(3, result11.Count)
        result11.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result12 = db.From(Of ItemWithActionHistory).
                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description}).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result12.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryModelEntityRecordWithActionHistory()
      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()

      Using db = CreateDbContext()
        db.Insert(item1)
        db.Insert(item2)
        db.Insert(item3)
      End Using

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSet()

        Dim result1 = db.Query(Of ItemWithActionHistory)($"SELECT {Sql.Model.Columns(Of ItemWithActionHistory)} FROM ItemWithActionHistory ORDER BY Id")
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.QueryFirstOrDefault(Of ItemWithActionHistory)($"SELECT {Sql.Model.Columns(Of ItemWithActionHistory)} FROM ItemWithActionHistory ORDER BY Id")
        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectModelEntityRecordWithActionHistoryWithRelationshipAndInclude()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSetAnd1RelationshipSetAnd1IncludeSet()

        Dim result1 = db.From(Of ItemWithActionHistory).
                         Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of ItemWithActionHistory).
                         Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForSelectWith3DbPropertiesSetAnd1RelationshipSetAnd1IncludeSet()

        Dim result1 = db.From(Of ItemWithActionHistory).
                         Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Of ItemWithActionHistory).
                         Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordWithActionHistoryWithRelationshipAndInclude()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithActionHistory()
      item3.Id = 3

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
        db.Insert(item3, useDbIdentityAndDefaults:=False)
      End Using

      ' reference navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializerAnd1RelationshipSetAnd1IncludeSet()

        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         Join(Of LinkedItem)(Function(j) j.T1.IntValue = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         Join(Of LinkedItem)(Function(j) j.T1.IntValue = j.T2.Id).As(Function(x) x.RelatedItem).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using

      ' collection navigation
      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializerAnd1RelationshipSetAnd1IncludeSet()

        Dim result1 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         Join(Of LinkedItem)(Function(j) j.T1.IntValue = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         ToList()
        Assert.AreEqual(3, result1.Count)
        result1.ForEach(Sub(x) CheckHistory(expectedHistory, x.GetActionHistory()))

        Dim result2 = db.From(Function(c)
                                Return c.From(Of ItemWithActionHistory).
                                         Select(Function(x) New NonModelObjectWithActionHistory With {.IntValue = x.Id, .StringValue = x.Description})
                              End Function).
                         Join(Of LinkedItem)(Function(j) j.T1.IntValue = j.T2.Id).As(Function(x) x.RelatedItems).
                         SelectAll().
                         Include(Sub(j) j.T1.IncludedValue = j.T2.Id).
                         FirstOrDefault()
        CheckHistory(expectedHistory, result2.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordThatIsStruct()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
      End Using

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()
        Dim expectedHistoryStructDefault = GetExpectedHistoryForStructDefault()

        Dim result1 = db.From(Of LinkedItem).
                         LeftJoin(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         OrderBy(Function(j) j.T1.Id).
                         Select(Function(j) New NonModelStructWithActionHistory With {.IntValue = j.T2.Id, .StringValue = j.T2.Description}).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        CheckHistory(expectedHistory, result1(0).GetActionHistory())
        CheckHistory(expectedHistory, result1(1).GetActionHistory())
        CheckHistory(expectedHistoryStructDefault, result1(2).GetActionHistory())

        Dim result2 = db.From(Of LinkedItem).
                         LeftJoin(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         OrderBy(Function(j) j.T1.Id).
                         Select(Function(j) New NonModelStructWithActionHistory With {.IntValue = j.T2.Id, .StringValue = j.T2.Description}).
                         FirstOrDefault()

        CheckHistory(expectedHistory, result2.GetActionHistory())

        Dim result3 = db.From(Of LinkedItem).
                         LeftJoin(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         OrderByDescending(Function(j) j.T1.Id).
                         Select(Function(j) New NonModelStructWithActionHistory With {.IntValue = j.T2.Id, .StringValue = j.T2.Description}).
                         FirstOrDefault()

        CheckHistory(expectedHistoryStructDefault, result3.GetActionHistory())
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectNonModelAdHocTypeRecordThatIsNullableStruct()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, Nothing)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, Nothing)

      InsertItems(linkedItem1, linkedItem2, linkedItem3)

      Dim item1 = Me.ModelFactory.CreateItemWithActionHistory()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithActionHistory()
      item2.Id = 2

      Using db = CreateDbContext()
        db.Insert(item1, useDbIdentityAndDefaults:=False)
        db.Insert(item2, useDbIdentityAndDefaults:=False)
      End Using

      Using db = CreateDbContext()
        Dim expectedHistory = GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer()
        Dim expectedHistoryStructDefault = GetExpectedHistoryForStructDefault()

        Dim result1 = db.From(Of LinkedItem).
                         LeftJoin(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         OrderBy(Function(j) j.T1.Id).
                         Select(Of NonModelStructWithActionHistory?)(Function(j) New NonModelStructWithActionHistory With {.IntValue = j.T2.Id, .StringValue = j.T2.Description}).
                         ToList()

        Assert.AreEqual(3, result1.Count)
        CheckHistory(expectedHistory, result1(0).Value.GetActionHistory())
        CheckHistory(expectedHistory, result1(1).Value.GetActionHistory())
        Assert.IsFalse(result1(2).HasValue)

        Dim result2 = db.From(Of LinkedItem).
                         LeftJoin(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         OrderBy(Function(j) j.T1.Id).
                         Select(Of NonModelStructWithActionHistory?)(Function(j) New NonModelStructWithActionHistory With {.IntValue = j.T2.Id, .StringValue = j.T2.Description}).
                         FirstOrDefault()

        CheckHistory(expectedHistory, result2.Value.GetActionHistory())

        Dim result3 = db.From(Of LinkedItem).
                         LeftJoin(Of ItemWithActionHistory)(Function(j) j.T1.Id = j.T2.Id).
                         OrderByDescending(Function(j) j.T1.Id).
                         Select(Of NonModelStructWithActionHistory?)(Function(j) New NonModelStructWithActionHistory With {.IntValue = j.T2.Id, .StringValue = j.T2.Description}).
                         FirstOrDefault()

        Assert.IsFalse(result3.HasValue)
      End Using
    End Sub

    Protected Sub CheckHistory(expected As ActionValue(), actual As ActionValue())
      CollectionAssert.AreEqual(expected, actual)
    End Sub

    Protected Function GetExpectedHistoryForSelectWith3DbPropertiesSet() As ActionValue()
      Return {
        ActionValue.Created,
        ActionValue.BeginLoadCalled,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.InitializeCalled,
        ActionValue.ResetDbPropertyModifiedTrackingCalled,
        ActionValue.EndLoadCalled
      }
    End Function

    Protected Function GetExpectedHistoryForCustomSelectWith1DbPropertySetViaInitializer() As ActionValue()
      Return {
        ActionValue.Created,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.BeginLoadCalled,
        ActionValue.InitializeCalled,
        ActionValue.ResetDbPropertyModifiedTrackingCalled,
        ActionValue.EndLoadCalled
      }
    End Function

    Protected Function GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializer() As ActionValue()
      Return {
        ActionValue.Created,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.BeginLoadCalled,
        ActionValue.InitializeCalled,
        ActionValue.ResetDbPropertyModifiedTrackingCalled,
        ActionValue.EndLoadCalled
      }
    End Function

    Protected Function GetExpectedHistoryForSelectWith3DbPropertiesSetAnd1RelationshipSetAnd1IncludeSet() As ActionValue()
      Return {
        ActionValue.Created,
        ActionValue.BeginLoadCalled,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.InitializeCalled,
        ActionValue.SetIncludeProperty,
        ActionValue.SetRelationshipProperty,
        ActionValue.ResetDbPropertyModifiedTrackingCalled,
        ActionValue.EndLoadCalled
      }
    End Function

    Protected Function GetExpectedHistoryForCustomSelectWith2DbPropertiesSetViaInitializerAnd1RelationshipSetAnd1IncludeSet() As ActionValue()
      Return {
        ActionValue.Created,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.ModifyDbPropertyValue,
        ActionValue.BeginLoadCalled,
        ActionValue.InitializeCalled,
        ActionValue.SetIncludeProperty,
        ActionValue.SetRelationshipProperty,
        ActionValue.ResetDbPropertyModifiedTrackingCalled,
        ActionValue.EndLoadCalled
      }
    End Function

    Protected Function GetExpectedHistoryForStructDefault() As ActionValue()
      Return {
        ActionValue.Created
      }
    End Function

  End Class
End Namespace
