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

      Dim item = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item.DateOnlyColumn = today

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.QueryFirstOrDefault(Of DateOnly)($"SELECT DateOnlyColumn FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item.Id}")
        Assert.AreEqual(item.DateOnlyColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableDateOnly()
      Dim today = Helpers.Calendar.DateOnlyNow()

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item1.DateOnlyColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item2.DateOnlyColumnNull = today

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of DateOnly?)($"SELECT DateOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.DateOnlyColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of DateOnly?)($"SELECT DateOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.DateOnlyColumnNull, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item.TimeOnlyColumn = time

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.QueryFirstOrDefault(Of TimeOnly)($"SELECT TimeOnlyColumn FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item.Id}")
        Assert.AreEqual(item.TimeOnlyColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableTimeOnly()
      Dim time = New TimeOnly(10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item1.TimeOnlyColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      item2.TimeOnlyColumnNull = time

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of TimeOnly?)($"SELECT TimeOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.TimeOnlyColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of TimeOnly?)($"SELECT TimeOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.TimeOnlyColumnNull, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overrides Sub QueryFirstOrDefaultOfValueTuple()
      MyBase.QueryFirstOrDefaultOfValueTuple()

      Dim item1 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithDateAndTimeOnlyFieldsWithMaxValues()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1null = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?)?)("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of DateOnly, DateOnly?, TimeOnly, TimeOnly?)?, result1null)

        Dim result1nullWithValue = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?)?)($"SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.DateOnlyColumn, item1.DateOnlyColumnNull, item1.TimeOnlyColumn, item1.TimeOnlyColumnNull), result1nullWithValue.Value)

        Dim result1empty = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?))("SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of DateOnly, DateOnly?, TimeOnly, TimeOnly?), result1empty)

        Dim result1 = db.QueryFirstOrDefault(Of (DateOnly, DateOnly?, TimeOnly, TimeOnly?))($"SELECT DateOnlyColumn, DateOnlyColumnNull, TimeOnlyColumn, TimeOnlyColumnNull FROM ItemWithDateAndTimeOnlyFields WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.DateOnlyColumn, item1.DateOnlyColumnNull, item1.TimeOnlyColumn, item1.TimeOnlyColumnNull), result1)
      End Using
    End Sub

  End Class
End Namespace