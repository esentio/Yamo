Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithWhereTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectRecordByGuid()
      Dim items = CreateItems()

      items(0).UniqueidentifierColumn = Guid.NewGuid()
      items(1).UniqueidentifierColumn = Guid.NewGuid()
      items(2).UniqueidentifierColumn = Guid.NewGuid()
      items(3).UniqueidentifierColumn = Guid.NewGuid()
      items(4).UniqueidentifierColumn = Guid.Empty

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.UniqueidentifierColumn = items(2).UniqueidentifierColumn).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableGuid()
      Dim items = CreateItems()

      items(0).UniqueidentifierColumnNull = Guid.NewGuid()
      items(1).UniqueidentifierColumnNull = Guid.NewGuid()
      items(2).UniqueidentifierColumnNull = Guid.NewGuid()
      items(3).UniqueidentifierColumnNull = Nothing
      items(4).UniqueidentifierColumnNull = Guid.Empty

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.UniqueidentifierColumnNull.Value = items(2).UniqueidentifierColumnNull.Value).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.UniqueidentifierColumnNull.Value = Guid.Empty).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(4), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.UniqueidentifierColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.UniqueidentifierColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByString()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "lorem ipsum"
      items(1).Nvarchar50Column = "dolor sit"
      items(2).Nvarchar50Column = "amet"
      items(3).Nvarchar50Column = "lorem ipsum dolor sit amet"
      items(4).Nvarchar50Column = ""

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column = "amet").SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column = items(2).Nvarchar50Column).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column = "").SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(4), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column Is Nothing).SelectAll().ToList()
        Assert.AreEqual(0, result.Count)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column IsNot Nothing).SelectAll().ToList()
        Assert.AreEqual(5, result.Count)
        CollectionAssert.AreEquivalent(items, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column.StartsWith("dol")).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using

      Using db = CreateDbContext()
        Dim value = "dol"
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column.StartsWith(value)).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column.EndsWith("sum")).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(0), result(0))
      End Using

      Using db = CreateDbContext()
        Dim value = "sum"
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column.EndsWith(value)).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(0), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column.Contains("ipsum")).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using

      Using db = CreateDbContext()
        Dim value = "ipsum"
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column.Contains(value)).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50Column < "b").SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) "b" < x.Nvarchar50Column).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableString()
      Dim items = CreateItems()

      items(0).Nvarchar50ColumnNull = "lorem ipsum"
      items(1).Nvarchar50ColumnNull = "dolor sit"
      items(2).Nvarchar50ColumnNull = "amet"
      items(3).Nvarchar50ColumnNull = Nothing
      items(4).Nvarchar50ColumnNull = ""

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50ColumnNull = "").SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(4), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50ColumnNull Is Nothing).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Nvarchar50ColumnNull IsNot Nothing).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByBoolean()
      Dim items = CreateItems()

      items(0).BitColumn = True
      items(1).BitColumn = False
      items(2).BitColumn = True
      items(3).BitColumn = False
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumn = True).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumn = False).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(1), items(3)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.BitColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(1), items(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableBoolean()
      Dim items = CreateItems()

      items(0).BitColumnNull = True
      items(1).BitColumnNull = False
      items(2).BitColumnNull = Nothing
      items(3).BitColumnNull = True
      items(4).BitColumnNull = False

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumnNull.Value = True).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumnNull.Value).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(3)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumnNull.Value = False).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(1), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.BitColumnNull.Value).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(1), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BitColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.BitColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByInt16()
      Dim items = CreateItems()

      items(0).SmallintColumn = Int16.MinValue
      items(1).SmallintColumn = -42
      items(2).SmallintColumn = 0
      items(3).SmallintColumn = 42
      items(4).SmallintColumn = Int16.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.SmallintColumn = 42).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 < x.SmallintColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 <= x.SmallintColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 > x.SmallintColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 >= x.SmallintColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableInt16()
      Dim items = CreateItems()

      items(0).SmallintColumnNull = Int16.MinValue
      items(1).SmallintColumnNull = Nothing
      items(2).SmallintColumnNull = 0
      items(3).SmallintColumnNull = 42
      items(4).SmallintColumnNull = Int16.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.SmallintColumnNull.Value = 42).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.SmallintColumnNull.Value = 0).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.SmallintColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.SmallintColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByInt32()
      Dim items = CreateItems()

      items(0).IntColumn = Int32.MinValue
      items(1).IntColumn = -42
      items(2).IntColumn = 0
      items(3).IntColumn = 42
      items(4).IntColumn = Int32.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.IntColumn = 42).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 < x.IntColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 <= x.IntColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 > x.IntColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 >= x.IntColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableInt32()
      Dim items = CreateItems()

      items(0).IntColumnNull = Int32.MinValue
      items(1).IntColumnNull = Nothing
      items(2).IntColumnNull = 0
      items(3).IntColumnNull = 42
      items(4).IntColumnNull = Int32.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.IntColumnNull.Value = 42).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.IntColumnNull.Value = 0).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.IntColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.IntColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByInt64()
      Dim items = CreateItems()

      items(0).BigintColumn = Int64.MinValue
      items(1).BigintColumn = -42
      items(2).BigintColumn = 0
      items(3).BigintColumn = 42
      items(4).BigintColumn = Int64.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BigintColumn = 42).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 < x.BigintColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 <= x.BigintColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 > x.BigintColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 >= x.BigintColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableInt64()
      Dim items = CreateItems()

      items(0).BigintColumnNull = Int64.MinValue
      items(1).BigintColumnNull = Nothing
      items(2).BigintColumnNull = 0
      items(3).BigintColumnNull = 42
      items(4).BigintColumnNull = Int64.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BigintColumnNull.Value = 42).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BigintColumnNull.Value = 0).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.BigintColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.BigintColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordBySingle()
      Dim items = CreateItems()

      items(0).RealColumn = Single.MinValue
      items(1).RealColumn = -42.5F
      items(2).RealColumn = 0
      items(3).RealColumn = 42.5F ' 42.6 "doesn't work" because of deviation in SQLite when filtering by equals
      items(4).RealColumn = Single.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.RealColumn = 42.5F).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 < x.RealColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 <= x.RealColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 > x.RealColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 >= x.RealColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableSingle()
      Dim items = CreateItems()

      items(0).RealColumnNull = Single.MinValue
      items(1).RealColumnNull = Nothing
      items(2).RealColumnNull = 0
      items(3).RealColumnNull = 42.5F ' 42.6 "doesn't work" because of deviation in SQLite when filtering by equals
      items(4).RealColumnNull = Single.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.RealColumnNull.Value = 42.5F).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.RealColumnNull.Value = 0).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.RealColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.RealColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDouble()
      Dim items = CreateItems()

      items(0).FloatColumn = Double.MinValue
      items(1).FloatColumn = -42.6
      items(2).FloatColumn = 0
      items(3).FloatColumn = 42.6
      items(4).FloatColumn = Double.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.FloatColumn = 42.6).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 < x.FloatColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 <= x.FloatColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 > x.FloatColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 >= x.FloatColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableDouble()
      Dim items = CreateItems()

      items(0).FloatColumnNull = Double.MinValue
      items(1).FloatColumnNull = Nothing
      items(2).FloatColumnNull = 0
      items(3).FloatColumnNull = 42.6
      items(4).FloatColumnNull = Double.MaxValue

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.FloatColumnNull.Value = 42.6).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.FloatColumnNull.Value = 0).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.FloatColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.FloatColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDecimal()
      Dim items = CreateItems()

      items(0).Numeric10and3Column = -9999999.999D
      items(1).Numeric10and3Column = -42.6D
      items(2).Numeric10and3Column = 0D
      items(3).Numeric10and3Column = 42.6D
      items(4).Numeric10and3Column = 9999999.999D

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Numeric10and3Column = 42.6D).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 < x.Numeric10and3Column).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 <= x.Numeric10and3Column).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 > x.Numeric10and3Column).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) 0 >= x.Numeric10and3Column).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableDecimal()
      Dim items = CreateItems()

      items(0).Numeric10and3ColumnNull = -9999999.999D
      items(1).Numeric10and3ColumnNull = Nothing
      items(2).Numeric10and3ColumnNull = 0D
      items(3).Numeric10and3ColumnNull = 42.6D
      items(4).Numeric10and3ColumnNull = 9999999.999D

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Numeric10and3ColumnNull.Value = 42.6D).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Numeric10and3ColumnNull.Value = 0).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Numeric10and3ColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.Numeric10and3ColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDate()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.Now().Date

      items(0).DateColumn = Helpers.Calendar.GetSqlServerMinDate()
      items(1).DateColumn = today.AddDays(-42)
      items(2).DateColumn = today
      items(3).DateColumn = today.AddDays(42)
      items(4).DateColumn = Helpers.Calendar.GetSqlServerMaxDate()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.DateColumn = today).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) today < x.DateColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) today <= x.DateColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) today > x.DateColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) today >= x.DateColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableDate()
      Dim items = CreateItems()

      Dim today = Helpers.Calendar.Now().Date

      items(0).DateColumnNull = Helpers.Calendar.GetSqlServerMinDate()
      items(1).DateColumnNull = Nothing
      items(2).DateColumnNull = today
      items(3).DateColumnNull = today.AddDays(42)
      items(4).DateColumnNull = Helpers.Calendar.GetSqlServerMaxDate()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.DateColumnNull.Value = today).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.DateColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.DateColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByTime()
      Dim items = CreateItems()

      Dim time = New TimeSpan(0, 10, 20, 30, 500)

      items(0).TimeColumn = Helpers.Calendar.GetSqlServerMinTime()
      items(1).TimeColumn = time.Subtract(TimeSpan.FromHours(1))
      items(2).TimeColumn = time
      items(3).TimeColumn = time.Add(TimeSpan.FromHours(1))
      items(4).TimeColumn = Helpers.Calendar.GetSqlServerMaxTime()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.TimeColumn = time).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) time < x.TimeColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) time <= x.TimeColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) time > x.TimeColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) time >= x.TimeColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableTime()
      Dim items = CreateItems()

      Dim time = New TimeSpan(0, 10, 20, 30, 500)

      items(0).TimeColumnNull = Helpers.Calendar.GetSqlServerMinTime()
      items(1).TimeColumnNull = Nothing
      items(2).TimeColumnNull = time
      items(3).TimeColumnNull = time.Add(TimeSpan.FromHours(1))
      items(4).TimeColumnNull = Helpers.Calendar.GetSqlServerMaxTime()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.TimeColumnNull.Value = time).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.TimeColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.TimeColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByDateTime()
      Dim items = CreateItems()

      Dim now = Helpers.Calendar.Now()

      items(0).DatetimeColumn = Helpers.Calendar.GetSqlServerMinDateTime()
      items(1).DatetimeColumn = now.AddDays(-42)
      items(2).DatetimeColumn = now
      items(3).DatetimeColumn = now.AddDays(42)
      items(4).DatetimeColumn = Helpers.Calendar.GetSqlServerMaxDateTime()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.DatetimeColumn = now).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) now < x.DatetimeColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) now <= x.DatetimeColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) now > x.DatetimeColumn).SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) now >= x.DatetimeColumn).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableDateTime()
      Dim items = CreateItems()

      Dim now = Helpers.Calendar.Now()

      items(0).DatetimeColumnNull = Helpers.Calendar.GetSqlServerMinDateTime()
      items(1).DatetimeColumnNull = Nothing
      items(2).DatetimeColumnNull = now
      items(3).DatetimeColumnNull = now.AddDays(42)
      items(4).DatetimeColumnNull = Helpers.Calendar.GetSqlServerMaxDateTime()

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.DatetimeColumnNull.Value = now).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.DatetimeColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) Not x.DatetimeColumnNull.HasValue).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByByteArray()
      Dim items = CreateItems()

      Dim now = Helpers.Calendar.Now()

      items(0).Varbinary50Column = Helpers.Data.CreateRandomByteArray(50)
      items(1).Varbinary50Column = Helpers.Data.CreateRandomByteArray(50)
      items(2).Varbinary50Column = {}
      items(3).Varbinary50Column = Helpers.Data.CreateRandomByteArray(50)
      items(4).Varbinary50Column = Helpers.Data.CreateRandomByteArray(50)

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Varbinary50Column Is items(3).Varbinary50Column).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Varbinary50Column Is New Byte() {}).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(2), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByNullableByteArray()
      Dim items = CreateItems()

      Dim now = Helpers.Calendar.Now()

      items(0).Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(50)
      items(1).Varbinary50ColumnNull = Nothing
      items(2).Varbinary50ColumnNull = {}
      items(3).Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(50)
      items(4).Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(50)

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Varbinary50ColumnNull Is items(3).Varbinary50ColumnNull).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(3), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Varbinary50ColumnNull IsNot Nothing).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) x.Varbinary50ColumnNull Is Nothing).SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(1), result(0))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordUsingIn()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      Using db = CreateDbContext()
        Dim values = New Int32() {}
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) values.Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(0, result.Count)
      End Using

      Using db = CreateDbContext()
        Dim values = New Int32() {1, 3, 5}
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) values.Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim values = New List(Of Int32) From {1, 3, 5}
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) values.Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) (New Int32() {}).Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(0, result.Count)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) New Int32() {1, 3, 5}.Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) New Int32(2) {1, 3, 5}.Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).Where(Function(x) {1, 3, 5}.Contains(x.IntColumn)).SelectAll().ToList()
        Assert.AreEqual(3, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(2), items(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordByMultipleWhereConditions()
      Dim items = CreateItems()

      items(0).IntColumn = 42
      items(0).Nvarchar50Column = "foo"
      items(1).IntColumn = 420
      items(1).Nvarchar50Column = "foo"
      items(2).Nvarchar50Column = "bar"

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) x.IntColumn = 42).
                        And(Function(x) x.Nvarchar50Column = "foo").
                        SelectAll().ToList()
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(0), result(0))
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) x.IntColumn = 42 OrElse x.IntColumn = 420).
                        And(Function(x) x.Nvarchar50Column = "foo").
                        SelectAll().ToList()
        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) x.IntColumn = 42 OrElse x.IntColumn = 420).
                        And(Function(x) x.Nvarchar50Column = "bar").
                        SelectAll().ToList()
        Assert.AreEqual(0, result.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectRecordBySequentiallyBuildingWhereConditions()
      Dim items = CreateItems()

      items(0).IntColumn = 42
      items(0).Nvarchar50Column = "foo"
      items(1).Nvarchar50Column = "foo"

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where().
                        SelectAll().ToList()
        Assert.AreEqual(5, result.Count)
      End Using

      Using db = CreateDbContext()
        Dim exp = db.From(Of ItemWithAllSupportedValues).Where()

        exp.And(Function(x) x.Nvarchar50Column = "foo")

        Dim result = exp.SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim exp = db.From(Of ItemWithAllSupportedValues).Where()

        exp.And(Function(x) x.IntColumn = 42)
        exp.And(Function(x) x.Nvarchar50Column = "foo")

        Dim result = exp.SelectAll().ToList()

        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(items(0), result(0))
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
