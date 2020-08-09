Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class DeleteTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub DeleteRecord()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Delete(item2)
        Assert.AreEqual(1, affectedRows)
      End Using

      Using db = CreateDbContext()
        Dim items = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()
        Assert.AreEqual(2, items.Count)
        Assert.IsFalse(items.Any(Function(x) x.Id = item2.Id))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteNonExistingRecord()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Delete(item)
        Assert.AreEqual(0, affectedRows)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteAllRecords()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues).Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectCount()
        Assert.AreEqual(0, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordsWithCondition()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Nvarchar50Column = "d"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Nvarchar50Column = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Nvarchar50Column = "d"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column = "d").Execute()
        Assert.AreEqual(2, affectedRows)

        Dim item = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Id = item2.Id).SelectAll().FirstOrDefault()
        Assert.IsNotNull(item)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordsWithFormattableSqlString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Nvarchar50Column = "d"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Nvarchar50Column = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Nvarchar50Column = "d"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim value = "d"
        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues).Where(Function(x) DirectCast($"{x.Nvarchar50Column} = {value}", FormattableString)).Execute()
        Assert.AreEqual(2, affectedRows)

        Dim item = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Id = item2.Id).SelectAll().FirstOrDefault()
        Assert.IsNotNull(item)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordsWithRawSqlString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Nvarchar50Column = "d"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Nvarchar50Column = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Nvarchar50Column = "d"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues).Where("Nvarchar50Column = 'd'").Execute()
        Assert.AreEqual(2, affectedRows)

        Dim item = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Id = item2.Id).SelectAll().FirstOrDefault()
        Assert.IsNotNull(item)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordsWithRawSqlStringWithparameters()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Nvarchar50Column = "d"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Nvarchar50Column = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Nvarchar50Column = "d"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.Nvarchar50Column)).ColumnName

        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues).Where("{0} = {1}", RawSqlString.Create(column), "d").Execute()
        Assert.AreEqual(2, affectedRows)

        Dim item = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Id = item2.Id).SelectAll().FirstOrDefault()
        Assert.IsNotNull(item)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordWithSpecifiedTableName()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValuesArchive)).TableName
        InsertItemsToArchive(db, table, item1, item2, item3)
      End Using

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValuesArchive)).TableName

        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues)(table).Execute(item2)
        Assert.AreEqual(1, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValuesArchive).SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({item1, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub DeleteRecordsWithSpecifiedTableName()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      item1.IntColumn = 1
      item2.IntColumn = 2
      item3.IntColumn = 3

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValuesArchive)).TableName
        InsertItemsToArchive(db, table, item1, item2, item3)
      End Using

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValuesArchive)).TableName

        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues)(table).
                              Where(Function(x) x.IntColumn = 2).
                              Execute()
        Assert.AreEqual(1, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValuesArchive).SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({item1, item3}, result)
      End Using

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValuesArchive)).TableName

        Dim affectedRows = db.Delete(Of ItemWithAllSupportedValues)(table).
                              Execute()
        Assert.AreEqual(2, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValuesArchive).SelectCount()
        Assert.AreEqual(0, result)
      End Using
    End Sub

  End Class
End Namespace
