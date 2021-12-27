Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlStringParametersTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub ParameterHandlingUsingFormattableString()
      Dim stringValue = "test -- 'value"" "

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 10
      item1.Nvarchar50Column = stringValue

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()
      item2.IntColumn = 20
      item2.Nvarchar50Column = stringValue

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      item3.IntColumn = 30

      InsertItems(item1, item2, item3)

      ' test ColumnsModelInfo handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        CollectionAssert.AreEqual({item1, item2, item3}, result)
      End Using

      ' test ColumnModelInfo handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)($"SELECT {Sql.Model.Column(Of ItemWithAllSupportedValues)(NameOf(ItemWithAllSupportedValues.IntColumn))} FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        CollectionAssert.AreEqual({10, 20, 30}, result)
      End Using

      ' test TableModelInfo handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)($"SELECT IntColumn FROM {Sql.Model.Table(Of ItemWithAllSupportedValues)} ORDER BY IntColumn")
        CollectionAssert.AreEqual({10, 20, 30}, result)
      End Using

      ' test RawSqlString handling
      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).TableName
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.IntColumn)).ColumnName

        Dim result = db.Query(Of Int32)($"SELECT {RawSqlString.Create(column)} FROM {RawSqlString.Create(table)} ORDER BY {RawSqlString.Create(column)}")
        CollectionAssert.AreEqual({10, 20, 30}, result)
      End Using

      ' test DbParameter handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)($"SELECT IntColumn FROM ItemWithAllSupportedValues WHERE {Me.TestEnvironment.CreateDbParameter(20, Data.DbType.Int32)} <= IntColumn ORDER BY IntColumn")
        CollectionAssert.AreEqual({20, 30}, result)
      End Using

      ' test normal parameter handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)($"SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Nvarchar50Column = {stringValue} ORDER BY IntColumn")
        CollectionAssert.AreEqual({10, 20}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ParameterHandlingUsingRawSqlStringWithParameters()
      Dim stringValue = "test -- 'value"" "

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 10
      item1.Nvarchar50Column = stringValue

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()
      item2.IntColumn = 20
      item2.Nvarchar50Column = stringValue

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      item3.IntColumn = 30

      InsertItems(item1, item2, item3)

      ' test ColumnsModelInfo handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of ItemWithAllSupportedValues)("SELECT {0} FROM ItemWithAllSupportedValues ORDER BY IntColumn", Sql.Model.Columns(Of ItemWithAllSupportedValues))
        CollectionAssert.AreEqual({item1, item2, item3}, result)
      End Using

      ' test ColumnModelInfo handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)("SELECT {0} FROM ItemWithAllSupportedValues ORDER BY IntColumn", Sql.Model.Column(Of ItemWithAllSupportedValues)(NameOf(ItemWithAllSupportedValues.IntColumn)))
        CollectionAssert.AreEqual({10, 20, 30}, result)
      End Using

      ' test TableModelInfo handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)("SELECT IntColumn FROM {0} ORDER BY IntColumn", Sql.Model.Table(Of ItemWithAllSupportedValues))
        CollectionAssert.AreEqual({10, 20, 30}, result)
      End Using

      ' test RawSqlString handling
      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).TableName
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.IntColumn)).ColumnName

        Dim result = db.Query(Of Int32)("SELECT {1} FROM {0} ORDER BY {1}", RawSqlString.Create(table), RawSqlString.Create(column))
        CollectionAssert.AreEqual({10, 20, 30}, result)
      End Using

      ' test DbParameter handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)("SELECT IntColumn FROM ItemWithAllSupportedValues WHERE {0} <= IntColumn ORDER BY IntColumn", Me.TestEnvironment.CreateDbParameter(20, Data.DbType.Int32))
        CollectionAssert.AreEqual({20, 30}, result)
      End Using

      ' test normal parameter handling
      Using db = CreateDbContext()
        Dim result = db.Query(Of Int32)("SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Nvarchar50Column = {0} ORDER BY IntColumn", stringValue)
        CollectionAssert.AreEqual({10, 20}, result)
      End Using
    End Sub

  End Class
End Namespace
