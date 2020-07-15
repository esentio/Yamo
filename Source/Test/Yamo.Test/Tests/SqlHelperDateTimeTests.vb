Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlHelperDateTimeTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeGetCurrentDateTime()
      Dim items = CreateItems()

      Dim now = Helpers.Calendar.Now()

      items(0).DatetimeColumn = now.AddDays(1)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = now.AddHours(-1)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = now.AddDays(-1)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = now.AddHours(1)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = now.AddMinutes(10)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.DateTime.GetCurrentDateTime() < x.DatetimeColumn).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        CollectionAssert.AreEquivalent({items(0), items(3), items(4)}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.DateTime.GetCurrentDateTime() < x.DatetimeColumnNull.Value).
                         SelectAll().ToList()

        Assert.AreEqual(2, result2.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlDateTimeGetCurrentDateTime()
      Dim items = CreateItems()

      Dim now = Helpers.Calendar.Now()

      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        Select(Function(x) (Id:=x.IntColumn, Now:=Sql.DateTime.GetCurrentDateTime())).
                        ToList()

        Assert.AreEqual(5, result.Count)
        CollectionAssert.AreEqual({1, 2, 3, 4, 5}, result.Select(Function(x) x.Id).ToArray())
        Assert.IsTrue(result.All(Function(x) now.AddSeconds(-10) < x.Now AndAlso x.Now < now.AddSeconds(10)))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeGetCurrentDate()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.Now().Date

      items(0).DatetimeColumn = today.AddDays(1)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = today.AddMonths(-1)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = today.AddDays(-1)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = today.AddMonths(1)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = today.AddYears(1)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.DateTime.GetCurrentDate() < x.DatetimeColumn).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        CollectionAssert.AreEquivalent({items(0), items(3), items(4)}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.DateTime.GetCurrentDate() < x.DatetimeColumnNull.Value).
                         SelectAll().ToList()

        Assert.AreEqual(2, result2.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlDateTimeGetCurrentDate()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.Now().Date

      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        Select(Function(x) (Id:=x.IntColumn, Today:=Sql.DateTime.GetCurrentDate())).
                        ToList()

        Assert.AreEqual(5, result.Count)
        CollectionAssert.AreEqual({1, 2, 3, 4, 5}, result.Select(Function(x) x.Id).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Today = today))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTimeUsingSqlDateTimeGetDate()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.Now().Date

      items(0).DatetimeColumn = today.AddDays(1)
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).DatetimeColumn = today.AddHours(-1)
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).DatetimeColumn = today.AddDays(-1)
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).DatetimeColumn = today.AddHours(1)
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).DatetimeColumn = today.AddMinutes(10)
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        ' NOTE: simple 'today' could be used instead of 'Sql.DateTime.GetDate(today)', but it does not work in SQLite.
        ' "Problem" is datetime string representation. Let's assume today is 2020-07-13.
        ' This returns 1 record, which is unexpected at first sight:
        ' select * from ItemWithAllSupportedValues where '2020-07-13 00:00:00' <= date(DatetimeColumn)
        ' This returns 3 records, as expected:
        ' select * from ItemWithAllSupportedValues where '2020-07-13' <= date(DatetimeColumn)

        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.DateTime.GetDate(today) <= Sql.DateTime.GetDate(x.DatetimeColumn)).
                         SelectAll().ToList()

        Assert.AreEqual(3, result1.Count)
        CollectionAssert.AreEquivalent({items(0), items(3), items(4)}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.DateTime.GetDate(today) <= Sql.DateTime.GetDate(x.DatetimeColumnNull.Value)).
                         SelectAll().ToList()

        Assert.AreEqual(2, result2.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result2)

        ' TODO: SIP - this will fail. Consider it a bug or a feature?
        'Dim result3 = db.From(Of ItemWithAllSupportedValues).
        '                 Where(Function(x) Sql.DateTime.GetDate(today) <= Sql.DateTime.GetDate(x.DatetimeColumnNull).Value).
        '                 SelectAll().ToList()

        'Assert.AreEqual(2, result3.Count)
        'CollectionAssert.AreEquivalent({items(0), items(3)}, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlDateTimeGetDate()
      Dim items = CreateItems()

      Dim dt1 = New DateTime(2000, 1, 1)
      Dim dt2 = New DateTime(2002, 2, 2, 8, 0, 0)
      Dim dt3 = New DateTime(2003, 3, 3, 15, 30, 0)
      Dim dt4 = New DateTime(2004, 4, 4, 23, 59, 59)
      Dim dt5 = New DateTime(2005, 5, 5)

      items(0).IntColumn = 1
      items(0).DatetimeColumn = dt1
      items(0).DatetimeColumnNull = items(0).DatetimeColumn
      items(1).IntColumn = 2
      items(1).DatetimeColumn = dt2
      items(1).DatetimeColumnNull = items(1).DatetimeColumn
      items(2).IntColumn = 3
      items(2).DatetimeColumn = dt3
      items(2).DatetimeColumnNull = items(2).DatetimeColumn
      items(3).IntColumn = 4
      items(3).DatetimeColumn = dt4
      items(3).DatetimeColumnNull = items(3).DatetimeColumn
      items(4).IntColumn = 5
      items(4).DatetimeColumn = dt5
      items(4).DatetimeColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        Select(Function(x) (Id:=x.IntColumn, Date1:=Sql.DateTime.GetDate(x.DatetimeColumn), Date2:=Sql.DateTime.GetDate(x.DatetimeColumnNull.Value), Date3:=Sql.DateTime.GetDate(x.DatetimeColumnNull))).
                        ToList()

        Dim expected = {
          (1, dt1.Date, dt1.Date, New DateTime?(dt1.Date)),
          (2, dt2.Date, dt2.Date, New DateTime?(dt2.Date)),
          (3, dt3.Date, dt3.Date, New DateTime?(dt3.Date)),
          (4, dt4.Date, dt4.Date, New DateTime?(dt4.Date)),
          (5, dt5.Date, DateTime.MinValue, New DateTime?())
        }

        Assert.AreEqual(5, result.Count)
        CollectionAssert.AreEqual(expected, result)
      End Using
    End Sub

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
