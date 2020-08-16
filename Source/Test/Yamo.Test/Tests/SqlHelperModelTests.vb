Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlHelperModelTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub QueryUsingColumns()
      Dim items = CreateItems()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.Query(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM ItemWithAllSupportedValues")
        CollectionAssert.AreEquivalent(items, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryUsingColumnsWithAlias()
      Dim items = CreateItems()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.Query(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)("t")} FROM ItemWithAllSupportedValues t")
        CollectionAssert.AreEquivalent(items, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryUsingColumn()
      Dim items = CreateItems()
      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)($"SELECT {Sql.Model.Column(Of ItemWithAllSupportedValues)(NameOf(ItemWithAllSupportedValues.IntColumn))} FROM ItemWithAllSupportedValues")
        CollectionAssert.AreEquivalent({1, 2, 3, 4, 5}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryUsingColumnWithAlias()
      Dim items = CreateItems()
      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)($"SELECT {Sql.Model.Column(Of ItemWithAllSupportedValues)(NameOf(ItemWithAllSupportedValues.IntColumn), "t")} FROM ItemWithAllSupportedValues t")
        CollectionAssert.AreEquivalent({1, 2, 3, 4, 5}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryUsingTable()
      Dim items = CreateItems()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.Query(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM {Sql.Model.Table(Of ItemWithAllSupportedValues)}")
        CollectionAssert.AreEquivalent(items, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryUsingRawSqlString()
      Dim items = CreateItems()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.Query(Of ItemWithAllSupportedValues)("SELECT {0} FROM {1}", Sql.Model.Columns(Of ItemWithAllSupportedValues), Sql.Model.Table(Of ItemWithAllSupportedValues))
        CollectionAssert.AreEquivalent(items, result)
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
