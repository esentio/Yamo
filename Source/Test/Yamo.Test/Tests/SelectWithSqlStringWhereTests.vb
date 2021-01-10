Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithSqlStringWhereTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectRecordByFormattableSqlString()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "lorem ipsum"
      items(1).Nvarchar50Column = "dolor sit"
      items(2).Nvarchar50Column = "amet"
      items(3).Nvarchar50Column = "lorem ipsum dolor sit amet"
      items(4).Nvarchar50Column = ""

      InsertItems(items)

      Using db = CreateDbContext()
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.Nvarchar50Column)).ColumnName
        Dim value = "amet"

        ' NOTE: DirectCast is needed (VB.NET compiler bug/feature?)
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) DirectCast($"{x.Nvarchar50Column} = {value}", FormattableString)).
                         SelectAll().ToList()

        Assert.AreEqual(1, result1.Count)
        Assert.AreEqual(items(2), result1(0))

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) DirectCast($"{RawSqlString.Create(column)} = {value}", FormattableString)).
                         SelectAll().ToList()

        Assert.AreEqual(1, result2.Count)
        Assert.AreEqual(items(2), result2(0))

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) DirectCast($"{New RawSqlString(column)} = {value}", FormattableString)).
                         SelectAll().ToList()

        Assert.AreEqual(1, result3.Count)
        Assert.AreEqual(items(2), result3(0))

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) DirectCast($"NOT {New RawSqlString(column)} = {value}", FormattableString)).
                         And(Function(x) x.Nvarchar50Column.EndsWith("t")).
                         SelectAll().ToList()

        Assert.AreEqual(2, result4.Count)
        CollectionAssert.AreEquivalent({items(1), items(3)}, result4)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByRawSqlString()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "lorem ipsum"
      items(1).Nvarchar50Column = "dolor sit"
      items(2).Nvarchar50Column = "amet"
      items(3).Nvarchar50Column = "lorem ipsum dolor sit amet"
      items(4).Nvarchar50Column = ""

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where("Nvarchar50Column = 'amet'").
                        SelectAll().ToList()

        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByRawSqlStringWithParameters()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "lorem ipsum"
      items(1).Nvarchar50Column = "dolor sit"
      items(2).Nvarchar50Column = "amet"
      items(3).Nvarchar50Column = "lorem ipsum dolor sit amet"
      items(4).Nvarchar50Column = ""

      InsertItems(items)

      Using db = CreateDbContext()
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.Nvarchar50Column)).ColumnName

        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where("{0} = {1}", RawSqlString.Create(column), "amet").
                        SelectAll().ToList()

        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
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
