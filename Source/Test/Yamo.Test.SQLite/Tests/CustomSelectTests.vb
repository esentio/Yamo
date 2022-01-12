Imports Yamo.Test
Imports Yamo.Test.SQLite.Model

Namespace Tests

  <TestClass()>
  Public Class CustomSelectTests
    Inherits Yamo.Test.Tests.CustomSelectTests

    Protected Overloads Property ModelFactory As SQLiteTestModelFactory

    Sub New()
      MyBase.New()
      Me.ModelFactory = New SQLiteTestModelFactory
    End Sub

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overridable Sub CustomSelectOfDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item1.DateOnlyColumn = today

      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item2.DateOnlyColumn = today.AddDays(42)

      Dim item3 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item3.DateOnlyColumn = today.AddDays(-42)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DateOnlyColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.DateOnlyColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.DateOnlyColumn).
                         FirstOrDefault()
        Assert.AreEqual(DateOnly.MinValue, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Select(Function(x) x.DateOnlyColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DateOnlyColumn, item2.DateOnlyColumn, item3.DateOnlyColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.DateOnlyColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item1.DateOnlyColumnNull = today

      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item2.DateOnlyColumnNull = today.AddDays(42)

      Dim item3 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item3.DateOnlyColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DateOnlyColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.DateOnlyColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.DateOnlyColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.DateOnlyColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.DateOnlyColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Select(Function(x) x.DateOnlyColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DateOnlyColumnNull, item2.DateOnlyColumnNull, item3.DateOnlyColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.DateOnlyColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item1.TimeOnlyColumn = time

      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item2.TimeOnlyColumn = time.AddHours(1)

      Dim item3 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item3.TimeOnlyColumn = time.AddHours(-1)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.TimeOnlyColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.TimeOnlyColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.TimeOnlyColumn).
                         FirstOrDefault()
        Assert.AreEqual(TimeOnly.MinValue, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Select(Function(x) x.TimeOnlyColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.TimeOnlyColumn, item2.TimeOnlyColumn, item3.TimeOnlyColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.TimeOnlyColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item1.TimeOnlyColumnNull = time

      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item2.TimeOnlyColumnNull = time.AddHours(1)

      Dim item3 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues
      item3.TimeOnlyColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.TimeOnlyColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.TimeOnlyColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.TimeOnlyColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.TimeOnlyColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.TimeOnlyColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Select(Function(x) x.TimeOnlyColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.TimeOnlyColumnNull, item2.TimeOnlyColumnNull, item3.TimeOnlyColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithDateAndTimeOnlyFields).
                         Where(Function(x) x.Id = Int32.MinValue).
                         Select(Function(x) x.TimeOnlyColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

  End Class
End Namespace