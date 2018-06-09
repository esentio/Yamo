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

      Dim expected = New List(Of ItemWithAllSupportedValues) From {item1, item2, item3}
      Dim result As List(Of ItemWithAllSupportedValues)

      Using db = CreateDbContext()
        result = db.From(Of ItemWithAllSupportedValues).SelectAll().ToList()
      End Using

      CollectionAssert.AreEquivalent(expected, result)
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

  End Class
End Namespace
