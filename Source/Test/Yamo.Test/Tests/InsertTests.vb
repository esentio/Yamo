Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class InsertTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub InsertRecordWithAllSupportedValues()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item1)
        Assert.AreEqual(1, affectedRows)

        affectedRows = db.Insert(item2)
        Assert.AreEqual(1, affectedRows)

        affectedRows = db.Insert(item3)
        Assert.AreEqual(1, affectedRows)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()

        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithAllSupportedValuesWithSpecifiedTableName()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValuesArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithAllSupportedValues)(table).Execute(item1)
        Assert.AreEqual(1, affectedRows)

        affectedRows = db.Insert(Of ItemWithAllSupportedValues)(table).Execute(item2)
        Assert.AreEqual(1, affectedRows)

        affectedRows = db.Insert(Of ItemWithAllSupportedValues)(table).Execute(item3)
        Assert.AreEqual(1, affectedRows)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValuesArchive).SelectAll().ToList()

        CollectionAssert.AreEquivalent({item1, item2, item3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithDefaultValueIdAndUseDbIdentityAndDefaults()
      Dim item = Me.ModelFactory.CreateItemWithDefaultValueId()

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item, useDbIdentityAndDefaults:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(Guid.Empty, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDefaultValueId).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithDefaultValueIdAndUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Dim item = Me.ModelFactory.CreateItemWithDefaultValueId()

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithDefaultValueIdArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithDefaultValueId)(table).Execute(item, useDbIdentityAndDefaults:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(Guid.Empty, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDefaultValueIdArchive).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithDefaultValueIdAndDontUseDbIdentityAndDefaults()
      Dim id = Guid.NewGuid()
      Dim item = Me.ModelFactory.CreateItemWithDefaultValueId(id)

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item, useDbIdentityAndDefaults:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.AreEqual(id, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDefaultValueId).Where(Function(x) x.Id = id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithDefaultValueIdAndDontUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Dim id = Guid.NewGuid()
      Dim item = Me.ModelFactory.CreateItemWithDefaultValueId(id)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithDefaultValueIdArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithDefaultValueId)(table).Execute(item, useDbIdentityAndDefaults:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.AreEqual(id, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithDefaultValueIdArchive).Where(Function(x) x.Id = id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndUseDbIdentityAndDefaults()
      Dim item = Me.ModelFactory.CreateItemWithIdentityId()

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item, useDbIdentityAndDefaults:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(0, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityId).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Dim item = Me.ModelFactory.CreateItemWithIdentityId()

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithIdentityIdArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithIdentityId)(table).Execute(item, useDbIdentityAndDefaults:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(0, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityIdArchive).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndDontUseDbIdentityAndDefaults()
      Dim id = 42
      Dim item = Me.ModelFactory.CreateItemWithIdentityId(id)

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item, useDbIdentityAndDefaults:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.AreEqual(id, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityId).Where(Function(x) x.Id = id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndDontUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Dim id = 42
      Dim item = Me.ModelFactory.CreateItemWithIdentityId(id)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithIdentityIdArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithIdentityId)(table).Execute(item, useDbIdentityAndDefaults:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.AreEqual(id, item.Id)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityIdArchive).Where(Function(x) x.Id = id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndDefaultValuesAndUseDbIdentityAndDefaults()
      Dim item = Me.ModelFactory.CreateItemWithIdentityIdAndDefaultValues()

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item, useDbIdentityAndDefaults:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(0, item.Id)
        Assert.AreNotEqual(Guid.Empty, item.UniqueidentifierValue)
        Assert.AreEqual(42, item.IntValue) ' 42 is default value in SQL
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityIdAndDefaultValues).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndDefaultValuesAndUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Dim item = Me.ModelFactory.CreateItemWithIdentityIdAndDefaultValues()

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithIdentityIdAndDefaultValuesArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithIdentityIdAndDefaultValues)(table).Execute(item, useDbIdentityAndDefaults:=True)
        Assert.AreEqual(1, affectedRows)
        Assert.AreNotEqual(0, item.Id)
        Assert.AreNotEqual(Guid.Empty, item.UniqueidentifierValue)
        Assert.AreEqual(42, item.IntValue) ' 42 is default value in SQL
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityIdAndDefaultValuesArchive).Where(Function(x) x.Id = item.Id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndDefaultValuesAndDontUseDbIdentityAndDefaults()
      Dim id = 42
      Dim uniqueidentifierValue = Guid.NewGuid()
      Dim intValue = 6942
      Dim item = Me.ModelFactory.CreateItemWithIdentityIdAndDefaultValues(id, uniqueidentifierValue, intValue)

      Using db = CreateDbContext()
        Dim affectedRows = db.Insert(item, useDbIdentityAndDefaults:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.AreEqual(id, item.Id)
        Assert.AreEqual(uniqueidentifierValue, item.UniqueidentifierValue)
        Assert.AreEqual(intValue, item.IntValue)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityIdAndDefaultValues).Where(Function(x) x.Id = id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub InsertRecordWithIdentityIdAndDefaultValuesAndDontUseDbIdentityAndDefaultsWithSpecifiedTableName()
      Dim id = 42
      Dim uniqueidentifierValue = Guid.NewGuid()
      Dim intValue = 6942
      Dim item = Me.ModelFactory.CreateItemWithIdentityIdAndDefaultValues(id, uniqueidentifierValue, intValue)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithIdentityIdAndDefaultValuesArchive)).TableName

        Dim affectedRows = db.Insert(Of ItemWithIdentityIdAndDefaultValues)(table).Execute(item, useDbIdentityAndDefaults:=False)
        Assert.AreEqual(1, affectedRows)
        Assert.AreEqual(id, item.Id)
        Assert.AreEqual(uniqueidentifierValue, item.UniqueidentifierValue)
        Assert.AreEqual(intValue, item.IntValue)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithIdentityIdAndDefaultValuesArchive).Where(Function(x) x.Id = id).SelectAll().FirstOrDefault()
        Assert.AreEqual(item, result)
      End Using
    End Sub

  End Class
End Namespace
