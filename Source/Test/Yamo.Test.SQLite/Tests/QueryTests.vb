Imports Yamo.Test
Imports Yamo.Test.SQLite.Model

Namespace Tests

  <TestClass()>
  Public Class QueryTests
    Inherits Yamo.Test.Tests.QueryTests

    Protected Overloads Property ModelFactory As SQLiteTestModelFactory

    Sub New()
      MyBase.New()
      Me.ModelFactory = New SQLiteTestModelFactory
    End Sub

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overridable Sub QueryOfDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.DateOnlyColumn = today

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.DateOnlyColumn = today.AddDays(1)

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of DateOnly)("SELECT DateOnlyColumn FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of DateOnly)("SELECT DateOnlyColumn FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.DateOnlyColumn, result2(0))
        Assert.AreEqual(item2.DateOnlyColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.DateOnlyColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.DateOnlyColumnNull = today

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of DateOnly?)("SELECT DateOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of DateOnly?)("SELECT DateOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.DateOnlyColumnNull, result2(0))
        Assert.AreEqual(item2.DateOnlyColumnNull, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.TimeOnlyColumn = time

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.TimeOnlyColumn = time.AddHours(1)

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of TimeOnly)("SELECT TimeOnlyColumn FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of TimeOnly)("SELECT TimeOnlyColumn FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.TimeOnlyColumn, result2(0))
        Assert.AreEqual(item2.TimeOnlyColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.TimeOnlyColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.TimeOnlyColumnNull = time

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of TimeOnly?)("SELECT TimeOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of TimeOnly?)("SELECT TimeOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.TimeOnlyColumnNull, result2(0))
        Assert.AreEqual(item2.TimeOnlyColumnNull, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfChar()
      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.Nchar1Column = Char.MinValue

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.Nchar1Column = "a"c

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Char)("SELECT Nchar1Column FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Char)("SELECT Nchar1Column FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.Nchar1Column, result2(0))
        Assert.AreEqual(item2.Nchar1Column, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableChar()
      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.Nchar1ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.Nchar1ColumnNull = Char.MinValue

      Dim item3 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item3.Nchar1ColumnNull = "a"c

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Char?)("SELECT Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Char?)("SELECT Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.Nchar1ColumnNull, result2(0))
        Assert.AreEqual(item2.Nchar1ColumnNull, result2(1))
        Assert.AreEqual(item3.Nchar1ColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overrides Sub QueryOfValueTuple()
      MyBase.QueryOfValueTuple()

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithMaxValues()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1empty = db.Query(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?))("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2 ORDER BY Id")
        Assert.AreEqual(0, result1empty.Count)

        Dim result1null = db.Query(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?)?)("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(3, result1null.Count)
        Assert.AreEqual((item1.DateOnlyColumn, item1.DateOnlyColumnNull, item1.TimeOnlyColumn, item1.TimeOnlyColumnNull, item1.Nchar1Column, item1.Nchar1ColumnNull), result1null(0).Value)
        Assert.AreEqual((item2.DateOnlyColumn, item2.DateOnlyColumnNull, item2.TimeOnlyColumn, item2.TimeOnlyColumnNull, item2.Nchar1Column, item2.Nchar1ColumnNull), result1null(1).Value)
        Assert.AreEqual((item3.DateOnlyColumn, item3.DateOnlyColumnNull, item3.TimeOnlyColumn, item3.TimeOnlyColumnNull, item3.Nchar1Column, item3.Nchar1ColumnNull), result1null(2).Value)

        Dim result1 = db.Query(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?))("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields ORDER BY Id")
        Assert.AreEqual(3, result1.Count)
        Assert.AreEqual((item1.DateOnlyColumn, item1.DateOnlyColumnNull, item1.TimeOnlyColumn, item1.TimeOnlyColumnNull, item1.Nchar1Column, item1.Nchar1ColumnNull), result1(0))
        Assert.AreEqual((item2.DateOnlyColumn, item2.DateOnlyColumnNull, item2.TimeOnlyColumn, item2.TimeOnlyColumnNull, item2.Nchar1Column, item2.Nchar1ColumnNull), result1(1))
        Assert.AreEqual((item3.DateOnlyColumn, item3.DateOnlyColumnNull, item3.TimeOnlyColumn, item3.TimeOnlyColumnNull, item3.Nchar1Column, item3.Nchar1ColumnNull), result1(2))
      End Using
    End Sub

  End Class
End Namespace