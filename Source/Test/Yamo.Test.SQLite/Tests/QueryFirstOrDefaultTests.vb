Imports Yamo.Test
Imports Yamo.Test.SQLite.Model

Namespace Tests

  <TestClass()>
  Public Class QueryFirstOrDefaultTests
    Inherits Yamo.Test.Tests.QueryFirstOrDefaultTests

    Protected Overloads Property ModelFactory As SQLiteTestModelFactory

    Sub New()
      MyBase.New()
      Me.ModelFactory = New SQLiteTestModelFactory
    End Sub

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item.DateOnlyColumn = today

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.QueryFirstOrDefault(Of DateOnly)($"SELECT DateOnlyColumn FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item.Id}")
        Assert.AreEqual(item.DateOnlyColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.DateOnlyColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.DateOnlyColumnNull = today

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of DateOnly?)($"SELECT DateOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.DateOnlyColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of DateOnly?)($"SELECT DateOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.DateOnlyColumnNull, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item.TimeOnlyColumn = time

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.QueryFirstOrDefault(Of TimeOnly)($"SELECT TimeOnlyColumn FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item.Id}")
        Assert.AreEqual(item.TimeOnlyColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.TimeOnlyColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.TimeOnlyColumnNull = time

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of TimeOnly?)($"SELECT TimeOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.TimeOnlyColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of TimeOnly?)($"SELECT TimeOnlyColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.TimeOnlyColumnNull, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfChar()
      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.Nchar1Column = Char.MinValue

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.Nchar1Column = "a"c

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Char)($"SELECT Nchar1Column FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Nchar1Column, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Char)($"SELECT Nchar1Column FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Nchar1Column, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableChar()
      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.Nchar1ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item2.Nchar1ColumnNull = Char.MinValue

      Dim item3 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item3.Nchar1ColumnNull = "a"c

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Char?)($"SELECT Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Nchar1ColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Char?)($"SELECT Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Nchar1ColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Char?)($"SELECT Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Nchar1ColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overrides Sub QueryFirstOrDefaultOfValueTuple()
      MyBase.QueryFirstOrDefaultOfValueTuple()

      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithMaxValues()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1null = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?)?)("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?)?, result1null)

        Dim result1nullWithValue = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?)?)($"SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.DateOnlyColumn, item1.DateOnlyColumnNull, item1.TimeOnlyColumn, item1.TimeOnlyColumnNull, item1.Nchar1Column, item1.Nchar1ColumnNull), result1nullWithValue.Value)

        Dim result1empty = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?))("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?), result1empty)

        Dim result1 = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?, Char, Char?))($"SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull, Nchar1Column, Nchar1ColumnNull FROM ItemWithOnlySQLiteSupportedFields WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.DateOnlyColumn, item1.DateOnlyColumnNull, item1.TimeOnlyColumn, item1.TimeOnlyColumnNull, item1.Nchar1Column, item1.Nchar1ColumnNull), result1)
      End Using
    End Sub

  End Class
End Namespace