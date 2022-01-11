Imports Yamo.Test
Imports Yamo.Test.SQLite.Model

Namespace Tests

  <TestClass()>
  Public Class SelectWithWhereTests
    Inherits Yamo.Test.Tests.SelectWithWhereTests

    Protected Overloads Property ModelFactory As SQLiteTestModelFactory

    Sub New()
      MyBase.New()
      Me.ModelFactory = New SQLiteTestModelFactory
    End Sub

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateOnly()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.DateOnlyNow()

      items(0).DateOnlyColumn = Helpers.Calendar.GetSqlServerMinDateAsDateOnly()
      items(1).DateOnlyColumn = today.AddDays(-42)
      items(2).DateOnlyColumn = today
      items(3).DateOnlyColumn = today.AddDays(42)
      items(4).DateOnlyColumn = Helpers.Calendar.GetSqlServerMaxDateAsDateOnly()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) x.DateOnlyColumn = today).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) today < x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) today <= x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) today > x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) today >= x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableDateOnly()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.DateOnlyNow()

      items(0).DateOnlyColumnNull = Helpers.Calendar.GetSqlServerMinDateAsDateOnly()
      items(1).DateOnlyColumnNull = Nothing
      items(2).DateOnlyColumnNull = today
      items(3).DateOnlyColumnNull = today.AddDays(42)
      items(4).DateOnlyColumnNull = Helpers.Calendar.GetSqlServerMaxDateAsDateOnly()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) x.DateOnlyColumnNull.Value = today).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) x.DateOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) Not x.DateOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByTimeOnly()
      Dim items = CreateItems()

      Dim time = New TimeOnly(10, 20, 30, 500)

      items(0).TimeOnlyColumn = Helpers.Calendar.GetSqlServerMinTimeAsTimeOnly()
      items(1).TimeOnlyColumn = time.AddHours(-1)
      items(2).TimeOnlyColumn = time
      items(3).TimeOnlyColumn = time.AddHours(1)
      items(4).TimeOnlyColumn = Helpers.Calendar.GetSqlServerMaxTimeAsTimeOnly()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) x.TimeOnlyColumn = time).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) time < x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) time <= x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) time > x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) time >= x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableTimeOnly()
      Dim items = CreateItems()

      Dim time = New TimeOnly(10, 20, 30, 500)

      items(0).TimeOnlyColumnNull = Helpers.Calendar.GetSqlServerMinTimeAsTimeOnly()
      items(1).TimeOnlyColumnNull = Nothing
      items(2).TimeOnlyColumnNull = time
      items(3).TimeOnlyColumnNull = time.AddHours(1)
      items(4).TimeOnlyColumnNull = Helpers.Calendar.GetSqlServerMaxTimeAsTimeOnly()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) x.TimeOnlyColumnNull.Value = time).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) x.TimeOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDateAndTimeOnlyFields).Where(Function(x) Not x.TimeOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    Protected Overloads Function CreateItems() As List(Of ItemWithDateAndTimeOnlyFields)
      Return New List(Of ItemWithDateAndTimeOnlyFields) From {
        Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      }
    End Function

  End Class
End Namespace