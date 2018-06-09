Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class UpdateTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub UpdateRecordWithAllSupportedValues()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Dim id = item.Id

      ' instead of setting all properties, we generate new POCO with the same id

      item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()
      item.Id = id
      UpdateRecordWithAllSupportedValues(item)

      item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      item.Id = id
      UpdateRecordWithAllSupportedValues(item)

      item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.Id = id
      UpdateRecordWithAllSupportedValues(item)
    End Sub

    Protected Overridable Sub UpdateRecordWithAllSupportedValues(item As ItemWithAllSupportedValues)
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(1, affectedRows)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateNonExistingRecord()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(item)
        Assert.AreEqual(0, affectedRows)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithAction()
      ' same as test below, just use API allowed only in VB.NET

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      ' update one column
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = "lorem").
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item2.Nvarchar50Column = "lorem"
        item3.Nvarchar50Column = "lorem"

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' update multiple columns
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = "lorem ipsum").
                              Set(Sub(x) x.Nvarchar50ColumnNull = "dolor sit").
                              Set(Sub(x) x.SmallintColumn = 6).
                              Set(Sub(x) x.IntColumn = 42).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum"
        item1.Nvarchar50ColumnNull = "dolor sit"
        item1.SmallintColumn = 6
        item1.IntColumn = 42
        item2.Nvarchar50Column = "lorem ipsum"
        item2.Nvarchar50ColumnNull = "dolor sit"
        item2.SmallintColumn = 6
        item2.IntColumn = 42
        item3.Nvarchar50Column = "lorem ipsum"
        item3.Nvarchar50ColumnNull = "dolor sit"
        item3.SmallintColumn = 6
        item3.IntColumn = 42

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set null value
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50ColumnNull = Nothing).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50ColumnNull = Nothing
        item2.Nvarchar50ColumnNull = Nothing
        item3.Nvarchar50ColumnNull = Nothing

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set with expression
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = x.Nvarchar50Column & " dolor sit").
                              Set(Sub(x) x.IntColumn = x.IntColumn + 1).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum dolor sit"
        item1.IntColumn = 43
        item2.Nvarchar50Column = "lorem ipsum dolor sit"
        item2.IntColumn = 43
        item3.Nvarchar50Column = "lorem ipsum dolor sit"
        item3.IntColumn = 43

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set with expression 2
      Using db = CreateDbContext()
        Dim stringValue1 = " 1"
        Dim stringValue2 = "2"
        Dim stringValue3 = "3"
        Dim stringValue4 = "4"
        Dim stringValue5 = "5"
        Dim stringValue6 = "6"
        Dim intValue = 1

        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = x.Nvarchar50Column & stringValue1 & stringValue2 & stringValue3 & stringValue4 & stringValue5 & stringValue6 & "7"). ' this tests special Concat overload
                              Set(Sub(x) x.IntColumn = x.IntColumn + x.SmallintColumn + intValue).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum dolor sit 1234567"
        item1.IntColumn = 50
        item2.Nvarchar50Column = "lorem ipsum dolor sit 1234567"
        item2.IntColumn = 50
        item3.Nvarchar50Column = "lorem ipsum dolor sit 1234567"
        item3.IntColumn = 50

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithValueSelector()
      ' same as test above, just use different API

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      ' update one column
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50Column, Function(x) "lorem").
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item2.Nvarchar50Column = "lorem"
        item3.Nvarchar50Column = "lorem"

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' update multiple columns
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50Column, Function(x) "lorem ipsum").
                              Set(Function(x) x.Nvarchar50ColumnNull, Function(x) "dolor sit").
                              Set(Function(x) x.SmallintColumn, Function(x) 6).
                              Set(Function(x) x.IntColumn, Function(x) 42).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum"
        item1.Nvarchar50ColumnNull = "dolor sit"
        item1.SmallintColumn = 6
        item1.IntColumn = 42
        item2.Nvarchar50Column = "lorem ipsum"
        item2.Nvarchar50ColumnNull = "dolor sit"
        item2.SmallintColumn = 6
        item2.IntColumn = 42
        item3.Nvarchar50Column = "lorem ipsum"
        item3.Nvarchar50ColumnNull = "dolor sit"
        item3.SmallintColumn = 6
        item3.IntColumn = 42

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set null value
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50ColumnNull, Function(x) Nothing).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50ColumnNull = Nothing
        item2.Nvarchar50ColumnNull = Nothing
        item3.Nvarchar50ColumnNull = Nothing

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set with expression
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50Column, Function(x) x.Nvarchar50Column & " dolor sit").
                              Set(Function(x) x.IntColumn, Function(x) x.IntColumn + 1).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum dolor sit"
        item1.IntColumn = 43
        item2.Nvarchar50Column = "lorem ipsum dolor sit"
        item2.IntColumn = 43
        item3.Nvarchar50Column = "lorem ipsum dolor sit"
        item3.IntColumn = 43

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set with expression 2
      Using db = CreateDbContext()
        Dim stringValue1 = " 1"
        Dim stringValue2 = "2"
        Dim stringValue3 = "3"
        Dim stringValue4 = "4"
        Dim stringValue5 = "5"
        Dim stringValue6 = "6"
        Dim intValue = 1

        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50Column, Function(x) x.Nvarchar50Column & stringValue1 & stringValue2 & stringValue3 & stringValue4 & stringValue5 & stringValue6 & "7"). ' this tests special Concat overload
                              Set(Function(x) x.IntColumn, Function(x) x.IntColumn + x.SmallintColumn + intValue).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum dolor sit 1234567"
        item1.IntColumn = 50
        item2.Nvarchar50Column = "lorem ipsum dolor sit 1234567"
        item2.IntColumn = 50
        item3.Nvarchar50Column = "lorem ipsum dolor sit 1234567"
        item3.IntColumn = 50

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithValue()
      ' similar as test above, just use different API

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      ' update one column
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50Column, "lorem").
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item2.Nvarchar50Column = "lorem"
        item3.Nvarchar50Column = "lorem"

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' update multiple columns
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) x.Nvarchar50Column, "lorem ipsum").
                              Set(Function(x) x.Nvarchar50ColumnNull, "dolor sit").
                              Set(Function(x) x.SmallintColumn, 6).
                              Set(Function(x) x.IntColumn, 42).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem ipsum"
        item1.Nvarchar50ColumnNull = "dolor sit"
        item1.SmallintColumn = 6
        item1.IntColumn = 42
        item2.Nvarchar50Column = "lorem ipsum"
        item2.Nvarchar50ColumnNull = "dolor sit"
        item2.SmallintColumn = 6
        item2.IntColumn = 42
        item3.Nvarchar50Column = "lorem ipsum"
        item3.Nvarchar50ColumnNull = "dolor sit"
        item3.SmallintColumn = 6
        item3.IntColumn = 42

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set null value
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              SetNull(Function(x) x.Nvarchar50ColumnNull).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50ColumnNull = Nothing
        item2.Nvarchar50ColumnNull = Nothing
        item3.Nvarchar50ColumnNull = Nothing

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithFormattableSqlString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      ' set value directly
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) DirectCast($"{x.Nvarchar50Column} = 'lorem'", FormattableString)).
                              Set(Function(x) DirectCast($"{x.IntColumn} = 42", FormattableString)).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item1.IntColumn = 42
        item2.Nvarchar50Column = "lorem"
        item2.IntColumn = 42
        item3.Nvarchar50Column = "lorem"
        item3.IntColumn = 42

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set value via variable
      Using db = CreateDbContext()
        Dim stringValue = "ipsum"
        Dim intValue = 42
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) DirectCast($"{x.Nvarchar50ColumnNull} = {stringValue}", FormattableString)).
                              Set(Function(x) DirectCast($"{x.IntColumnNull} = {intValue}", FormattableString)).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50ColumnNull = "ipsum"
        item1.IntColumnNull = 42
        item2.Nvarchar50ColumnNull = "ipsum"
        item2.IntColumnNull = 42
        item3.Nvarchar50ColumnNull = "ipsum"
        item3.IntColumnNull = 42

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set null value
      Using db = CreateDbContext()
        Dim stringValue = Nothing
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) DirectCast($"{x.Nvarchar50ColumnNull} = {stringValue}", FormattableString)).
                              Set(Function(x) DirectCast($"{x.IntColumnNull} = {Nothing}", FormattableString)).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50ColumnNull = Nothing
        item1.IntColumnNull = Nothing
        item2.Nvarchar50ColumnNull = Nothing
        item2.IntColumnNull = Nothing
        item3.Nvarchar50ColumnNull = Nothing
        item3.IntColumnNull = Nothing

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using

      ' set with expression
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Function(x) DirectCast($"{x.IntColumn} = {x.IntColumn} + 1", FormattableString)).
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.IntColumn = 43
        item2.IntColumn = 43
        item3.IntColumn = 43

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithRawSqlString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()

      InsertItems(item1, item2, item3)

      ' set value directly
      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set("Nvarchar50Column = 'lorem'").
                              Set("IntColumn = 42").
                              Execute()
        Assert.AreEqual(3, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item1.IntColumn = 42
        item2.Nvarchar50Column = "lorem"
        item2.IntColumn = 42
        item3.Nvarchar50Column = "lorem"
        item3.IntColumn = 42

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithCondition()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = "lorem").
                              Where(Function(x) x.IntColumn < 3).
                              Execute()
        Assert.AreEqual(2, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item2.Nvarchar50Column = "lorem"

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithFormattableSqlStringCondition()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = "lorem").
                              Where(Function(x) DirectCast($"{x.IntColumn} < 3", FormattableString)).
                              Execute()
        Assert.AreEqual(2, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item2.Nvarchar50Column = "lorem"

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub UpdateRecordsWithRawSqlStringCondition()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim affectedRows = db.Update(Of ItemWithAllSupportedValues).
                              Set(Sub(x) x.Nvarchar50Column = "lorem").
                              Where("IntColumn < 3").
                              Execute()
        Assert.AreEqual(2, affectedRows)

        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        item1.Nvarchar50Column = "lorem"
        item2.Nvarchar50Column = "lorem"

        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

  End Class
End Namespace
