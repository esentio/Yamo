Imports System.Data
Imports Yamo.Test
Imports Yamo.Test.Model
Imports Yamo.Test.SQLite.Model

Namespace Tests

  <TestClass()>
  Public Class DataTests
    Inherits Yamo.Test.Tests.DataTests

    Protected Overloads Property ModelFactory As SQLiteTestModelFactory

    Sub New()
      MyBase.New()
      Me.ModelFactory = New SQLiteTestModelFactory
    End Sub

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    <TestMethod()>
    Public Overridable Sub SelectRecordWithDateAndTimeOnlyFields()
      Dim item1 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithEmptyValues()
      item1.Id = 1
      Dim item2 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithMinValues()
      item2.Id = 2
      Dim item3 = Me.ModelFactory.CreateItemWithOnlySQLiteSupportedFieldsWithMaxValues()
      item3.Id = 3

      Insert(item1)
      Insert(item2)
      Insert(item3)

      Dim expected = New List(Of ItemWithOnlySQLiteSupportedFields) From {item1, item2, item3}
      Dim result As List(Of ItemWithOnlySQLiteSupportedFields)

      Using db = CreateDbContext()
        result = db.From(Of ItemWithOnlySQLiteSupportedFields).SelectAll().ToList()
      End Using

      CollectionAssert.AreEquivalent(expected, result)
    End Sub

    Protected Overridable Overloads Sub Insert(item As ItemWithOnlySQLiteSupportedFields)
      ' use explicit SQL rather than built-in insert support (that is tested elsewhere)

      Dim sql As FormattableString = $"
      INSERT INTO ItemWithOnlySQLiteSupportedFields
        (Id
        ,DateOnlyColumn
        ,DateOnlyColumnNull
        ,TimeOnlyColumn
        ,TimeOnlyColumnNull
        ,Nchar1Column
        ,Nchar1ColumnNull)
      VALUES
        ({item.Id}
        ,{item.DateOnlyColumn}
        ,{item.DateOnlyColumnNull}
        ,{item.TimeOnlyColumn}
        ,{item.TimeOnlyColumnNull}
        ,{item.Nchar1Column}
        ,{item.Nchar1ColumnNull}
      )"

      Using db = CreateDbContext()
        db.Execute(sql)
      End Using
    End Sub

  End Class
End Namespace