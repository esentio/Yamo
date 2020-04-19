Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlHelperDateTimeTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameYear()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2001, 1, 2, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2003, 2, 1, 8, 0, 0)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2003, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameYear(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameYear(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameQuarter()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(1999, 12, 31, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2001, 2, 1, 8, 0, 0)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 3, 8, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2001, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameQuarter(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameQuarter(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameMonth()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2000, 2, 2, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2000, 3, 1, 8, 0, 0)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2001, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameMonth(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameMonth(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameDay()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2000, 1, 2, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2000, 2, 1, 8, 0, 0)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2001, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameDay(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameDay(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameHour()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2000, 1, 2, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2000, 1, 1, 9, 0, 0)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 30, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2001, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1, 8, 0, 0)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameHour(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameHour(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameMinute()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2000, 1, 2, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2000, 1, 1, 8, 1, 0)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2001, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1, 8, 0, 0)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameMinute(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameMinute(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameSecond()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2000, 1, 2, 8, 0, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 1)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2001, 1, 1, 8, 0, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1, 8, 0, 0)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameSecond(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameSecond(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeSameMillisecond()
      Dim items = CreateItems()

      items(0).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0, 0)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = New DateTime(2000, 1, 1, 7, 59, 59, 0)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0, 800)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 0, 0)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = New DateTime(2000, 1, 1, 8, 0, 1, 0)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim value = New DateTime(2000, 1, 1, 8, 0, 0, 0)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameMillisecond(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateTime.SameMillisecond(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    Protected Overridable Function CreateItems() As List(Of ItemWithAllSupportedValues)
      Return New List(Of ItemWithAllSupportedValues) From {
        Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues(),
        Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues(),
        Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues(),
        Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues(),
        Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      }
    End Function

  End Class
End Namespace
