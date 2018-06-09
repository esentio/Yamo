Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithSqlStringConditionTests
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
        Dim value = "amet"
        ' NOTE: DirectCast is needed (VB.NET compiler bug/feature?)
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) DirectCast($"{x.Nvarchar50Column} = {value}", FormattableString)).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
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
        Dim result = db.From(Of ItemWithAllSupportedValues).Where("Nvarchar50Column = 'amet'").SelectAll().ToList()
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
