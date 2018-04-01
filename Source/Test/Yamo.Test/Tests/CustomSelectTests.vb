Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class CustomSelectTests
    Inherits TestsBase

    Protected Const English As String = "en"

    Protected Const German As String = "ger"

    <TestMethod()>
    Public Overridable Sub CustomSelectOfGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.UniqueidentifierColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.UniqueidentifierColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.UniqueidentifierColumn).
                         FirstOrDefault()
        Assert.AreEqual(Guid.Empty, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.UniqueidentifierColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.UniqueidentifierColumn, item2.UniqueidentifierColumn, item3.UniqueidentifierColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.UniqueidentifierColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumnNull = Guid.NewGuid

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumnNull = Guid.NewGuid

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.UniqueidentifierColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.UniqueidentifierColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.UniqueidentifierColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.UniqueidentifierColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.UniqueidentifierColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.UniqueidentifierColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.UniqueidentifierColumnNull, item2.UniqueidentifierColumnNull, item3.UniqueidentifierColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.UniqueidentifierColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.Nvarchar50ColumnNull = ""

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.Nvarchar50ColumnNull = "dolor sit"

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.Nvarchar50ColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.Nvarchar50ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.Nvarchar50ColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.Nvarchar50ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.Nvarchar50ColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Nvarchar50ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Nvarchar50ColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.Nvarchar50ColumnNull, item2.Nvarchar50ColumnNull, item3.Nvarchar50ColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Nvarchar50ColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.BitColumn = False

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.BitColumn = True

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.BitColumn = False

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.BitColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.BitColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BitColumn).
                         FirstOrDefault()
        Assert.AreEqual(False, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.BitColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.BitColumn, item2.BitColumn, item3.BitColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BitColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.BitColumnNull = False

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.BitColumnNull = True

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.BitColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.BitColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.BitColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.BitColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.BitColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BitColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.BitColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.BitColumnNull, item2.BitColumnNull, item3.BitColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BitColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.SmallintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.SmallintColumn = 42

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.SmallintColumn = -42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.SmallintColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.SmallintColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.SmallintColumn).
                         FirstOrDefault()
        Assert.AreEqual(0S, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.SmallintColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.SmallintColumn, item2.SmallintColumn, item3.SmallintColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.SmallintColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.SmallintColumnNull = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.SmallintColumnNull = 42

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.SmallintColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.SmallintColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.SmallintColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.SmallintColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.SmallintColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.SmallintColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.SmallintColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.SmallintColumnNull, item2.SmallintColumnNull, item3.SmallintColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.SmallintColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.IntColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.IntColumn = 42

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.IntColumn = -42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.IntColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.IntColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.IntColumn).
                         FirstOrDefault()
        Assert.AreEqual(0, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.IntColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.IntColumn, item2.IntColumn, item3.IntColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.IntColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.IntColumnNull = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.IntColumnNull = 42

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.IntColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.IntColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.IntColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.IntColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.IntColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.IntColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.IntColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.IntColumnNull, item2.IntColumnNull, item3.IntColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.IntColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.BigintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.BigintColumn = 42

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.BigintColumn = -42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.BigintColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.BigintColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BigintColumn).
                         FirstOrDefault()
        Assert.AreEqual(0L, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.BigintColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.BigintColumn, item2.BigintColumn, item3.BigintColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BigintColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.BigintColumnNull = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.BigintColumnNull = 42

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.BigintColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.BigintColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.BigintColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.BigintColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.BigintColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BigintColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.BigintColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.BigintColumnNull, item2.BigintColumnNull, item3.BigintColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.BigintColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.RealColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.RealColumn = 42.6

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.RealColumn = -42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.RealColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.RealColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.RealColumn).
                         FirstOrDefault()
        Assert.AreEqual(0F, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.RealColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.RealColumn, item2.RealColumn, item3.RealColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.RealColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.RealColumnNull = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.RealColumnNull = 42.6

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.RealColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.RealColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.RealColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.RealColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.RealColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.RealColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.RealColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.RealColumnNull, item2.RealColumnNull, item3.RealColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.RealColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.FloatColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.FloatColumn = 42.6

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.FloatColumn = -42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.FloatColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.FloatColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.FloatColumn).
                         FirstOrDefault()
        Assert.AreEqual(0R, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.FloatColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.FloatColumn, item2.FloatColumn, item3.FloatColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.FloatColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.FloatColumnNull = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.FloatColumnNull = 42.6

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.FloatColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.FloatColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.FloatColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.FloatColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.FloatColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.FloatColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.FloatColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.FloatColumnNull, item2.FloatColumnNull, item3.FloatColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.FloatColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.Numeric10and3Column = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.Numeric10and3Column = -42.6D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.Numeric10and3Column).
                         FirstOrDefault()
        Assert.AreEqual(item2.Numeric10and3Column, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Numeric10and3Column).
                         FirstOrDefault()
        Assert.AreEqual(0D, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Numeric10and3Column).
                         ToList()
        CollectionAssert.AreEquivalent({item1.Numeric10and3Column, item2.Numeric10and3Column, item3.Numeric10and3Column}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Numeric10and3Column).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.Numeric10and3ColumnNull = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.Numeric10and3ColumnNull = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.Numeric10and3ColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.Numeric10and3ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.Numeric10and3ColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.Numeric10and3ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.Numeric10and3ColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Numeric10and3ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Numeric10and3ColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.Numeric10and3ColumnNull, item2.Numeric10and3ColumnNull, item3.Numeric10and3ColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Numeric10and3ColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfDateTime()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.DatetimeColumn = Helpers.Calendar.Now()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.DatetimeColumn = Helpers.Calendar.Now().AddHours(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.DatetimeColumn = Helpers.Calendar.Now().AddDays(-42)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DatetimeColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.DatetimeColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeColumn).
                         FirstOrDefault()
        Assert.AreEqual(DateTime.MinValue, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.DatetimeColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DatetimeColumn, item2.DatetimeColumn, item3.DatetimeColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDateTime()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.DatetimeColumnNull = Helpers.Calendar.Now()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.DatetimeColumnNull = Helpers.Calendar.Now().AddHours(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.DatetimeColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DatetimeColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.DatetimeColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.DatetimeColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.DatetimeColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.DatetimeColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DatetimeColumnNull, item2.DatetimeColumnNull, item3.DatetimeColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfByteArray()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.Varbinary50ColumnNull = New Byte() {}

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(10)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.Varbinary50ColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.Varbinary50ColumnNull).
                         FirstOrDefault()
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, result1))

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.Varbinary50ColumnNull).
                         FirstOrDefault()
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result2))

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Varbinary50ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Varbinary50ColumnNull).
                         ToList()
        Assert.AreEqual(3, result4.Count)
        Assert.IsTrue(result4.Any(Function(x) Helpers.Compare.AreByteArraysEqual(item1.Varbinary50ColumnNull, x)))
        Assert.IsTrue(result4.Any(Function(x) Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, x)))
        Assert.IsTrue(result4.Any(Function(x) Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, x)))

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Varbinary50ColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfEntity()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select entity
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x).
                         FirstOrDefault()
        Assert.AreEqual(item2, result1)

        ' select entity, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result2)

        ' select entities
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x).
                         ToList()
        CollectionAssert.AreEquivalent({item1, item2, item3}, result3)

        ' select entities, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfJoinedEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        ' select entity
        Dim result1 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) l).
                         FirstOrDefault()
        Assert.AreEqual(label1En, result1)

        ' select entity, but no row is returned
        Dim result2 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) l).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result2)

        ' select entities
        Dim result3 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article3.Id).
                         Select(Function(a, l) l).
                         ToList()
        CollectionAssert.AreEquivalent({label3En, label3Ger}, result3)

        ' select entities, but no row is returned
        Dim result4 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) l).
                         ToList()
        Assert.AreEqual(0, result4.Count)

        ' select entity (using IJoin)
        Dim result5 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(j) j.T2).
                         FirstOrDefault()
        Assert.AreEqual(label1En, result1)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfValueWithExpression()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item.Nvarchar50ColumnNull = "lorem"
      item.BitColumn = False
      item.SmallintColumnNull = Nothing
      item.IntColumn = 42
      item.IntColumnNull = 6

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Nvarchar50ColumnNull & " ipsum").
                         FirstOrDefault()
        Assert.AreEqual("lorem ipsum", result1)

        ' TODO: SIP - is it worth to add support for negation in selects?
        'Dim result2 = db.From(Of ItemWithAllSupportedValues).
        '                 Select(Function(x) Not x.BitColumn).
        '                 FirstOrDefault()
        'Assert.AreEqual(48, result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.IntColumn + 3).
                         FirstOrDefault()
        Assert.AreEqual(45, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.IntColumn + x.IntColumnNull.Value).
                         FirstOrDefault()
        Assert.AreEqual(48, result4)

        ' TODO: SIP - is it worth to add support for conversion to non-null value for scenarios like this?
        'Dim result5 = db.From(Of ItemWithAllSupportedValues).
        '                 Select(Function(x) x.IntColumn + x.SmallintColumnNull.Value).
        '                 FirstOrDefault()
        'Assert.AreEqual(42, result5)

        'Dim result6 = db.From(Of ItemWithAllSupportedValues).
        '                 Select(Function(x) x.SmallintColumnNull.Value).
        '                 FirstOrDefault()
        'Assert.AreEqual(0S, result6)

        ' TODO: SIP - is it worth to add support for null value detection for scenarios like this?
        'Dim result7 = db.From(Of ItemWithAllSupportedValues).
        '                 Select(Function(x) x.SmallintColumnNull.HasValue).
        '                 FirstOrDefault()
        'Assert.AreEqual(False, result7)

        'Dim result8 = db.From(Of ItemWithAllSupportedValues).
        '                 Select(Function(x) x.IntColumnNull.HasValue).
        '                 FirstOrDefault()
        'Assert.AreEqual(True, result8)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNonNullableValueWhichMightBeNullInSqlResult()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) l.Id).
                         FirstOrDefault()
        Assert.AreEqual(0, result1)

        Dim result2 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) CType(l.Id, Int32?)).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result2)

        Dim result3 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) CType(l.Id, Int32?)).
                         FirstOrDefault()
        Assert.AreEqual(New Int32?(label1En.Id), result3)

        Dim result4 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(a, l) l.Id).
                         ToList()
        CollectionAssert.AreEquivalent({label1En.Id, 0, label3En.Id, label3Ger.Id}, result4)

        Dim result5 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(a, l) CType(l.Id, Int32?)).
                         ToList()
        CollectionAssert.AreEquivalent({New Int32?(label1En.Id), New Int32?(), New Int32?(label3En.Id), New Int32?(label3Ger.Id)}, result5)

        Dim result6 = db.From(Of Article).
                         Where(Function(a) a.Id = -1).
                         Select(Function(a) a.Id).
                         FirstOrDefault()
        Assert.AreEqual(0, result6)

        Dim result7 = db.From(Of Article).
                         Where(Function(a) a.Id = -1).
                         Select(Function(a) CType(a.Id, Int32?)).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result7)
      End Using
    End Sub










    ' TODO: SIP - test property reset
    ' TODO: SIP - test valuetuple
    ' TODO: SIP - test anonymous type


    <TestMethod()>
    Public Overridable Sub CustomSelect1()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues

      InsertItems(item1)

      ' TODO: SIP - implement

      Using db = CreateDbContext()
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Nvarchar50Column).
                         FirstOrDefault()

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x).
                         FirstOrDefault()

        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn, Item:=x)).
                         FirstOrDefault()

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) New With {x.Nvarchar50Column, x.IntColumn, .Item = x}).
                         FirstOrDefault()

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn, Item:=x)).
                         ToList()

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) New With {x.Nvarchar50Column, x.IntColumn, .Item = x}).
                         ToList()

        'Dim a = result3(0).Nvarchar50Column

        Assert.Fail()

      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelect2()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(a, l) (Price:=a.Price, Artile:=a, Label:=l))

        Dim result2 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(j) (Price:=j.T1.Price, Artile:=j.T1, Label:=j.T2))


        Assert.Fail()

      End Using
    End Sub

  End Class
End Namespace
