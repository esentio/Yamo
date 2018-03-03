Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlHelperDateDiffTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameYear()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameYear(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameYear(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameQuarter()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameQuarter(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameQuarter(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameMonth()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameMonth(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameMonth(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameDay()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameDay(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameDay(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameHour()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameHour(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameHour(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameMinute()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameMinute(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameMinute(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameSecond()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameSecond(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameSecond(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateDiffSameMillisecond()
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameMillisecond(x.DatetimeColumn, value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)

        result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Sql.DateDiff.SameMillisecond(x.DatetimeColumnNull.Value, value)).SelectAll().ToList()
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
