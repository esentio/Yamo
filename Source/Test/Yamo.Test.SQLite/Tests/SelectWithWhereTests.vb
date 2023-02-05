Imports Microsoft.Data.Sqlite
Imports Yamo.Test
Imports Yamo.Test.Model
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
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.DateOnlyColumn = today).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) today < x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) today <= x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) today > x.DateOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) today >= x.DateOnlyColumn).SelectAll().ToList()
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
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.DateOnlyColumnNull.Value = today).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.DateOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) Not x.DateOnlyColumnNull.HasValue).SelectAll().ToList()
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
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.TimeOnlyColumn = time).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) time < x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) time <= x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) time > x.TimeOnlyColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) time >= x.TimeOnlyColumn).SelectAll().ToList()
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
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.TimeOnlyColumnNull.Value = time).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.TimeOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) Not x.TimeOnlyColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByChar()
      Dim items = CreateItems()

      items(0).Nchar1Column = "a"c
      items(1).Nchar1Column = "b"c
      items(2).Nchar1Column = "c"c
      items(3).Nchar1Column = "d"c
      items(4).Nchar1Column = "e"c

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.Nchar1Column = "c"c).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableChar()
      Dim items = CreateItems()

      items(0).Nchar1ColumnNull = "a"c
      items(1).Nchar1ColumnNull = "b"c
      items(2).Nchar1ColumnNull = "c"c
      items(3).Nchar1ColumnNull = Nothing
      items(4).Nchar1ColumnNull = Char.MinValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.Nchar1ColumnNull.Value = "c"c).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.Nchar1ColumnNull.Value = Char.MinValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(4), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) Not x.Nchar1ColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithOnlySQLiteSupportedFields).Where(Function(x) x.Nchar1ColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(4)}, result)
      End Using
    End Sub

    Protected Overrides Sub SelectRecordUsingXorOperator(expected As ICollection)
      Try
        Using db = CreateDbContext()
          Dim result = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) (x.IntColumn Xor 3) < 2).
                          SelectAll().ToList()

          Assert.AreEqual(2, result.Count)
          CollectionAssert.AreEquivalent(expected, result)
        End Using

        Assert.Fail()
      Catch ex As SqliteException
        ' error: 'unrecognized token: "^"'
        If Not ex.Message.Contains("^") Then
          Assert.Fail(ex.Message)
        End If
      Catch ex As Exception
        Assert.Fail(ex.Message)
      End Try
    End Sub

    Protected Overloads Function CreateItems() As List(Of ItemWithOnlySQLiteSupportedFields)
      Return New List(Of ItemWithOnlySQLiteSupportedFields) From {
        Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues(),
        Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      }
    End Function

  End Class
End Namespace