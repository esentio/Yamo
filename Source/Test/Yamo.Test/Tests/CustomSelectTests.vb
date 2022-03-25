Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class CustomSelectTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

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
    Public Overridable Sub CustomSelectOfDate()
      Dim today = Helpers.Calendar.Now().Date

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.DateColumn = today

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.DateColumn = today.AddDays(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.DateColumn = today.AddDays(-42)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DateColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.DateColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DateColumn).
                         FirstOrDefault()
        Assert.AreEqual(DateTime.MinValue, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.DateColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DateColumn, item2.DateColumn, item3.DateColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DateColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDate()
      Dim today = Helpers.Calendar.Now().Date

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.DateColumnNull = today

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.DateColumnNull = today.AddDays(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.DateColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DateColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.DateColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.DateColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.DateColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DateColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.DateColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DateColumnNull, item2.DateColumnNull, item3.DateColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DateColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfTime()
      Dim time = New TimeSpan(0, 10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.TimeColumn = time

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.TimeColumn = time.Add(TimeSpan.FromHours(1))

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.TimeColumn = time.Subtract(TimeSpan.FromHours(1))

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.TimeColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.TimeColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.TimeColumn).
                         FirstOrDefault()
        Assert.AreEqual(TimeSpan.Zero, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.TimeColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.TimeColumn, item2.TimeColumn, item3.TimeColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.TimeColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableTime()
      Dim time = New TimeSpan(0, 10, 20, 30, 500)

      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.TimeColumnNull = time

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.TimeColumnNull = time.Add(TimeSpan.FromHours(1))

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.TimeColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.TimeColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.TimeColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.TimeColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.TimeColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.TimeColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.TimeColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.TimeColumnNull, item2.TimeColumnNull, item3.TimeColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.TimeColumnNull).
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
    Public Overridable Sub CustomSelectOfDateTime2()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.Datetime2Column = Helpers.Calendar.Now()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.Datetime2Column = Helpers.Calendar.Now().AddHours(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.Datetime2Column = Helpers.Calendar.Now().AddDays(-42)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.Datetime2Column).
                         FirstOrDefault()
        Assert.AreEqual(item2.Datetime2Column, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Datetime2Column).
                         FirstOrDefault()
        Assert.AreEqual(DateTime.MinValue, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Datetime2Column).
                         ToList()
        CollectionAssert.AreEquivalent({item1.Datetime2Column, item2.Datetime2Column, item3.Datetime2Column}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Datetime2Column).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDateTime2()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.Datetime2ColumnNull = Helpers.Calendar.Now()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.Datetime2ColumnNull = Helpers.Calendar.Now().AddHours(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.Datetime2ColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.Datetime2ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.Datetime2ColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.Datetime2ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.Datetime2ColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Datetime2ColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.Datetime2ColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.Datetime2ColumnNull, item2.Datetime2ColumnNull, item3.Datetime2ColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.Datetime2ColumnNull).
                         ToList()
        Assert.AreEqual(0, result5.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfDateTimeOffset()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.DatetimeoffsetColumn = Helpers.Calendar.OffsetNow()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.DatetimeoffsetColumn = Helpers.Calendar.OffsetNow().AddHours(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.DatetimeoffsetColumn = Helpers.Calendar.OffsetNow().AddDays(-42)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DatetimeoffsetColumn).
                         FirstOrDefault()
        Assert.AreEqual(item2.DatetimeoffsetColumn, result1)

        ' select value, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeoffsetColumn).
                         FirstOrDefault()
        Assert.AreEqual(DateTimeOffset.MinValue, result2)

        ' select values
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.DatetimeoffsetColumn).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DatetimeoffsetColumn, item2.DatetimeoffsetColumn, item3.DatetimeoffsetColumn}, result3)

        ' select values, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeoffsetColumn).
                         ToList()
        Assert.AreEqual(0, result4.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableDateTimeOffset()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.DatetimeoffsetColumnNull = Helpers.Calendar.OffsetNow()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.DatetimeoffsetColumnNull = Helpers.Calendar.OffsetNow().AddHours(42)

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.DatetimeoffsetColumnNull = Nothing

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select value
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) x.DatetimeoffsetColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item2.DatetimeoffsetColumnNull, result1)

        ' select null value
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item3.Id).
                         Select(Function(x) x.DatetimeoffsetColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(item3.DatetimeoffsetColumnNull, result2)

        ' select value, but no row is returned
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeoffsetColumnNull).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select values
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) x.DatetimeoffsetColumnNull).
                         ToList()
        CollectionAssert.AreEquivalent({item1.DatetimeoffsetColumnNull, item2.DatetimeoffsetColumnNull, item3.DatetimeoffsetColumnNull}, result4)

        ' select values, but no row is returned
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) x.DatetimeoffsetColumnNull).
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
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

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

        ' select entity, but row with empty entity is returned
        Dim result3 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) l).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result3)

        ' select entities
        Dim result4 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article3.Id).
                         Select(Function(a, l) l).
                         ToList()
        CollectionAssert.AreEquivalent({label3En, label3De}, result4)

        ' select entities, but no row is returned
        Dim result5 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) l).
                         ToList()
        Assert.AreEqual(0, result5.Count)

        ' select entities, but row with empty entity is returned
        Dim result6 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) l).
                         ToList()
        Assert.AreEqual(1, result6.Count)
        Assert.IsNull(result6(0))

        ' select entity (using IJoin)
        Dim result7 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(j) j.T2).
                         FirstOrDefault()
        Assert.AreEqual(label1En, result7)
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
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

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
        CollectionAssert.AreEquivalent({label1En.Id, 0, label3En.Id, label3De.Id}, result4)

        Dim result5 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(a, l) CType(l.Id, Int32?)).
                         ToList()
        CollectionAssert.AreEquivalent({New Int32?(label1En.Id), New Int32?(), New Int32?(label3En.Id), New Int32?(label3De.Id)}, result5)

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

    <TestMethod()>
    Public Overridable Sub CustomSelectOfValueTuple()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 0
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 42
      item2.Numeric10and3Column = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 9
      item3.Numeric10and3Column = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select ValueTuple with simple values
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) (x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column)).
                         FirstOrDefault()
        Assert.AreEqual((item2.UniqueidentifierColumn, item2.BitColumn, item2.Nvarchar50Column), result1)

        ' select ValueTuple with simple values, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) (x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column)).
                         FirstOrDefault()
        Assert.AreEqual(New ValueTuple(Of Guid, Boolean, String), result2)

        ' select ValueTuples
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) (x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column)).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        Assert.IsTrue(result3.Contains((item1.UniqueidentifierColumn, item1.BitColumn, item1.Nvarchar50Column)))
        Assert.IsTrue(result3.Contains((item2.UniqueidentifierColumn, item2.BitColumn, item2.Nvarchar50Column)))
        Assert.IsTrue(result3.Contains((item3.UniqueidentifierColumn, item3.BitColumn, item3.Nvarchar50Column)))

        ' select ValueTuples, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) (x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column)).
                         ToList()
        Assert.AreEqual(0, result4.Count)

        ' select ValueTuple with custom field names
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) (Value1:=x.UniqueidentifierColumn, Value2:=x.BitColumn, x.Nvarchar50Column)).
                         FirstOrDefault()
        Assert.AreEqual((item2.UniqueidentifierColumn, item2.BitColumn, item2.Nvarchar50Column), result5)

        ' select ValueTuple with simple and entity values
        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) (x.IntColumn, x.Numeric10and3Column, Entity:=x, x.Nvarchar50Column)).
                         FirstOrDefault()
        Assert.AreEqual((item2.IntColumn, item2.Numeric10and3Column, item2, item2.Nvarchar50Column), result6)
        Assert.AreEqual(item2, result6.Entity)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfNullableValueTuple()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 0
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 42
      item2.Numeric10and3Column = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 9
      item3.Numeric10and3Column = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim expected As (Guid, Boolean, String)? = (item2.UniqueidentifierColumn, item2.BitColumn, item2.Nvarchar50Column)
        Dim expectedNull As (Guid, Boolean, String)? = Nothing

        ' select ValueTuple with simple values
        Dim result1a = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Function(x) New(Guid, Boolean, String)?((x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column))).
                          FirstOrDefault()
        Assert.AreEqual(expected, result1a)

        ' select ValueTuple with simple values - with explicit cast
        Dim result1b = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Function(x) CType((x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column), (Guid, Boolean, String)?)).
                          FirstOrDefault()
        Assert.AreEqual(expected, result1b)

        ' select ValueTuple with simple values - with implicit cast
        Dim result1c = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Of (Guid, Boolean, String)?)(Function(x) (x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column)).
                          FirstOrDefault()
        Assert.AreEqual(expected, result1c)

        ' select ValueTuple with simple values, but no row is returned
        Dim result2a = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          Select(Function(x) New(Guid, Boolean, String)?((x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column))).
                          FirstOrDefault()
        Assert.AreEqual(expectedNull, result2a)

        ' select ValueTuple with simple values, but no row is returned - with explicit cast
        Dim result2b = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          Select(Function(x) CType((x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column), (Guid, Boolean, String)?)).
                          FirstOrDefault()
        Assert.AreEqual(expectedNull, result2b)

        ' select ValueTuple with simple values, but no row is returned - with implicit cast
        Dim result2c = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          Select(Of (Guid, Boolean, String)?)(Function(x) (x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column)).
                          FirstOrDefault()
        Assert.AreEqual(expectedNull, result2c)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfLargeValueTuple()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      Using db = CreateDbContext()
        ' select large ValueTuple
        Dim result1 = db.From(Of Article).
                         Where(Function(a) a.Id = article1.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a)).
                         FirstOrDefault()
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article1), result1)

        Dim result2 = db.From(Of Article).
                         Where(Function(a) a.Id = article1.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a, 9, 10, 11, 12, 13, 14, 15, 16)).
                         FirstOrDefault()
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article1, 9, 10, 11, 12, 13, 14, 15, 16), result2)

        ' select large ValueTuple, but no row is returned
        Dim result3 = db.From(Of Article).
                         Where(Function(a) a.Id = article1.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a)).
                         FirstOrDefault()
        Assert.AreEqual(New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Article))(1, 2, 3, 4, 5, 6, 7, New ValueTuple(Of Article)(article1)), result3)

        Dim result4 = db.From(Of Article).
                         Where(Function(a) a.Id = -1).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a, 9, 10, 11, 12, 13, 14, 15, 16)).
                         FirstOrDefault()
        Assert.AreEqual(New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Article, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Int32, Int32)))(0, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Article, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Int32, Int32))(Nothing, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Int32, Int32)(0, 0))), result4)

        ' select large ValueTuples
        Dim result5 = db.From(Of Article).
                         OrderBy(Function(a) a.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a)).
                         ToList()
        Assert.AreEqual(3, result5.Count)
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article1), result5(0))
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article2), result5(1))
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article3), result5(2))

        Dim result6 = db.From(Of Article).
                         OrderBy(Function(a) a.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a, 9, 10, 11, 12, 13, 14, 15, 16)).
                         ToList()
        Assert.AreEqual(3, result6.Count)
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article1, 9, 10, 11, 12, 13, 14, 15, 16), result6(0))
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article2, 9, 10, 11, 12, 13, 14, 15, 16), result6(1))
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, article3, 9, 10, 11, 12, 13, 14, 15, 16), result6(2))

        ' select large ValueTuples, but no row is returned
        Dim result7 = db.From(Of Article).
                         Where(Function(a) a.Id = -1).
                         OrderBy(Function(a) a.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a)).
                         ToList()
        Assert.AreEqual(0, result7.Count)

        Dim result8 = db.From(Of Article).
                         Where(Function(a) a.Id = -1).
                         OrderBy(Function(a) a.Id).
                         Select(Function(a) (1, 2, 3, 4, 5, 6, 7, Article:=a, 9, 10, 11, 12, 13, 14, 15, 16)).
                         ToList()
        Assert.AreEqual(0, result8.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfValueTupleWithMultipleEntities()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        ' select ValueTuple with 2 entities
        Dim result1 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) (Article:=a, Label:=l)).
                         FirstOrDefault()
        Assert.AreEqual(article1, result1.Article)
        Assert.AreEqual(label1En, result1.Label)

        ' select ValueTuple with 2 entities, but one is missing
        Dim result2 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) (Article:=a, Label:=l)).
                         FirstOrDefault()
        Assert.AreEqual(article2, result2.Article)
        Assert.AreEqual(Nothing, result2.Label)

        ' select ValueTuple with simple and entity values
        Dim result3 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) (Price:=a.Price, Article:=a, Label:=l, LabelId:=l.Id, Description:=l.Description)).
                         FirstOrDefault()
        Assert.AreEqual(article1.Price, result3.Price)
        Assert.AreEqual(article1, result3.Article)
        Assert.AreEqual(label1En, result3.Label)
        Assert.AreEqual(label1En.Id, result3.LabelId)
        Assert.AreEqual(label1En.Description, result3.Description)

        ' select ValueTuple with simple and entity values, but some are missing
        Dim result4 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) (Price:=a.Price, Article:=a, Label:=l, LabelId:=l.Id, Description:=l.Description)).
                         FirstOrDefault()
        Assert.AreEqual(article2.Price, result4.Price)
        Assert.AreEqual(article2, result4.Article)
        Assert.AreEqual(Nothing, result4.Label)
        Assert.AreEqual(0, result4.LabelId)
        Assert.AreEqual(Nothing, result4.Description)

        ' select ValueTuple with simple and entity values, but some are missing and converted to nullable(s)
        Dim result5 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) (Price:=a.Price, Article:=a, Label:=l, LabelId:=CType(l.Id, Int32?), Description:=l.Description)).
                         FirstOrDefault()
        Assert.AreEqual(article2.Price, result5.Price)
        Assert.AreEqual(article2, result5.Article)
        Assert.AreEqual(Nothing, result5.Label)
        Assert.AreEqual(New Int32?(), result5.LabelId)
        Assert.AreEqual(Nothing, result5.Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAnonymousType()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 0
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 42
      item2.Numeric10and3Column = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 9
      item3.Numeric10and3Column = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select anonymous type with simple values
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New With {x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column}).
                         FirstOrDefault()
        Assert.AreEqual(item2.UniqueidentifierColumn, result1.UniqueidentifierColumn)
        Assert.AreEqual(item2.BitColumn, result1.BitColumn)
        Assert.AreEqual(item2.Nvarchar50Column, result1.Nvarchar50Column)

        ' select anonymous type with simple values, but no row is returned
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) New With {x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column}).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result2)

        ' select anonymous types
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) New With {x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column}).
                         ToList()
        Assert.AreEqual(3, result3.Count)
        Assert.IsTrue(result3.Any(Function(x) x.UniqueidentifierColumn = item1.UniqueidentifierColumn AndAlso x.BitColumn = item1.BitColumn AndAlso x.Nvarchar50Column = item1.Nvarchar50Column))
        Assert.IsTrue(result3.Any(Function(x) x.UniqueidentifierColumn = item2.UniqueidentifierColumn AndAlso x.BitColumn = item2.BitColumn AndAlso x.Nvarchar50Column = item2.Nvarchar50Column))
        Assert.IsTrue(result3.Any(Function(x) x.UniqueidentifierColumn = item3.UniqueidentifierColumn AndAlso x.BitColumn = item3.BitColumn AndAlso x.Nvarchar50Column = item3.Nvarchar50Column))

        ' select anonymous types, but no row is returned
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) New With {x.UniqueidentifierColumn, x.BitColumn, x.Nvarchar50Column}).
                         ToList()
        Assert.AreEqual(0, result4.Count)

        ' select anonymous type with custom field names
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New With {.Value1 = x.UniqueidentifierColumn, .Value2 = x.BitColumn, x.Nvarchar50Column}).
                         FirstOrDefault()
        Assert.AreEqual(item2.UniqueidentifierColumn, result5.Value1)
        Assert.AreEqual(item2.BitColumn, result5.Value2)
        Assert.AreEqual(item2.Nvarchar50Column, result5.Nvarchar50Column)

        ' select anonymous type with simple and entity values
        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New With {x.IntColumn, x.Numeric10and3Column, .Entity = x, x.Nvarchar50Column}).
                         FirstOrDefault()
        Assert.AreEqual(item2.IntColumn, result6.IntColumn)
        Assert.AreEqual(item2.Numeric10and3Column, result6.Numeric10and3Column)
        Assert.AreEqual(item2, result6.Entity)
        Assert.AreEqual(item2.Nvarchar50Column, result6.Nvarchar50Column)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAnonymousTypeWithMultipleEntities()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        ' select anonymous type with 2 entities
        Dim result1 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) New With {.Article = a, .Label = l}).
                         FirstOrDefault()
        Assert.AreEqual(article1, result1.Article)
        Assert.AreEqual(label1En, result1.Label)

        ' select anonymous type with 2 entities, but one is missing
        Dim result2 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) New With {.Article = a, .Label = l}).
                         FirstOrDefault()
        Assert.AreEqual(article2, result2.Article)
        Assert.AreEqual(Nothing, result2.Label)

        ' select anonymous type with simple and entity values
        Dim result3 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) New With {.Price = a.Price, .Article = a, .Label = l, .LabelId = l.Id, .Description = l.Description}).
                         FirstOrDefault()
        Assert.AreEqual(article1.Price, result3.Price)
        Assert.AreEqual(article1, result3.Article)
        Assert.AreEqual(label1En, result3.Label)
        Assert.AreEqual(label1En.Id, result3.LabelId)
        Assert.AreEqual(label1En.Description, result3.Description)

        ' select anonymous type with simple and entity values, but some are missing
        Dim result4 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) New With {.Price = a.Price, .Article = a, .Label = l, .LabelId = l.Id, .Description = l.Description}).
                         FirstOrDefault()
        Assert.AreEqual(article2.Price, result4.Price)
        Assert.AreEqual(article2, result4.Article)
        Assert.AreEqual(Nothing, result4.Label)
        Assert.AreEqual(0, result4.LabelId)
        Assert.AreEqual(Nothing, result4.Description)

        ' select anonymous type with simple and entity values, but some are missing and converted to nullable(s)
        Dim result5 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) New With {.Price = a.Price, .Article = a, .Label = l, .LabelId = CType(l.Id, Int32?), .Description = l.Description}).
                         FirstOrDefault()
        Assert.AreEqual(article2.Price, result5.Price)
        Assert.AreEqual(article2, result5.Article)
        Assert.AreEqual(Nothing, result5.Label)
        Assert.AreEqual(New Int32?(), result5.LabelId)
        Assert.AreEqual(Nothing, result5.Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAdHocType()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 1
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 2
      item2.Numeric10and3ColumnNull = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 3
      item3.Numeric10and3ColumnNull = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select non-model type with simple values using member init
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull), result1)

        ' select non-model type with simple values using different member init
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject With {.GuidValue = x.UniqueidentifierColumn, .BooleanValue = x.BitColumn}).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2.BitColumn), result2)

        ' select non-model type with simple and entity values using different member init
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject With {.GuidValue = x.UniqueidentifierColumn, .ItemWithAllSupportedValues = x}).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2), result3)
        Assert.AreEqual(item2, result3.ItemWithAllSupportedValues)

        ' select non-model type with simple values using constructor
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull), result4)

        ' select non-model type with simple values using different constructor
        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject(x.UniqueidentifierColumn, x.BitColumn)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2.BitColumn), result5)

        ' select non-model type with simple and entity values using different constructor
        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject(x.UniqueidentifierColumn, x)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2), result6)
        Assert.AreEqual(item2, result6.ItemWithAllSupportedValues)

        ' select non-model type with simple values using combination of constructor and member init
        Dim result7 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelObject(x.UniqueidentifierColumn, x.BitColumn) With {.StringValue = x.Nvarchar50Column, .ItemWithAllSupportedValues = x, .NullableDecimalValue = x.Numeric15and0ColumnNull}).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2.BitColumn) With {.StringValue = item2.Nvarchar50Column, .ItemWithAllSupportedValues = item2, .NullableDecimalValue = item2.Numeric15and0ColumnNull}, result7)
        Assert.AreEqual(item2, result7.ItemWithAllSupportedValues)

        ' select non-model type with simple values, but no row is returned
        Dim result8 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) New NonModelObject With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}).
                         FirstOrDefault()
        Assert.AreEqual(Nothing, result8)

        ' select non-model types
        Dim result9 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) New NonModelObject With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}).
                         ToList()
        Assert.AreEqual(3, result9.Count)
        Assert.AreEqual(New NonModelObject(item1.UniqueidentifierColumn, item1.Nvarchar50Column, item1.Numeric10and3ColumnNull), result9(0))
        Assert.AreEqual(New NonModelObject(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull), result9(1))
        Assert.AreEqual(New NonModelObject(item3.UniqueidentifierColumn, item3.Nvarchar50Column, item3.Numeric10and3ColumnNull), result9(2))

        ' select non-model types, but no row is returned
        Dim result10 = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          OrderBy(Function(x) x.IntColumn).
                          Select(Function(x) New NonModelObject With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}).
                          ToList()
        Assert.AreEqual(0, result10.Count)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAdHocTypeThatIsGeneric()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 1
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 2
      item2.Numeric10and3ColumnNull = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 3
      item3.Numeric10and3ColumnNull = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select non-model generic type with simple values
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelGenericObject(Of Guid, String)(x.UniqueidentifierColumn, x.Nvarchar50Column)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelGenericObject(Of Guid, String)(item2.UniqueidentifierColumn, item2.Nvarchar50Column), result1)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAdHocTypeWithMultipleEntities()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        ' select non-model type with 2 entities
        Dim result1 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article1.Id).
                         Select(Function(a, l) New NonModelObject(a, l)).
                         FirstOrDefault()
        Assert.AreEqual(article1, result1.Article)
        Assert.AreEqual(label1En, result1.Label)

        ' select non-model type with 2 entities, but one is missing
        Dim result2 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         Where(Function(a, l) a.Id = article2.Id).
                         Select(Function(a, l) New NonModelObject(a, l)).
                         FirstOrDefault()
        Assert.AreEqual(article2, result2.Article)
        Assert.AreEqual(Nothing, result2.Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAdHocTypeThatIsModelEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      Using db = CreateDbContext()
        ' select model type
        Dim result1 = db.From(Of Article).
                         Where(Function(x) x.Id = article2.Id).
                         Select(Function(x) New Article With {.Id = x.Id, .Price = x.Price}).
                         FirstOrDefault()
        Assert.AreEqual(article2, result1)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAdHocTypeThatIsStruct()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 1
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 2
      item2.Numeric10and3ColumnNull = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 3
      item3.Numeric10and3ColumnNull = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select non-model struct type with simple values using member init
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelStruct With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull), result1)

        ' select non-model struct type with simple and entity values using different member init
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelStruct With {.GuidValue = x.UniqueidentifierColumn, .ItemWithAllSupportedValues = x}).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct(item2.UniqueidentifierColumn, item2), result2)

        ' select non-model struct type with simple values using constructor
        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull), result3)

        ' select non-model struct type with simple and entity values using different constructor
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelStruct(x.UniqueidentifierColumn, x)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct(item2.UniqueidentifierColumn, item2), result4)

        ' NOTE: this doesn't compile and there is following error: "Expression cannot be converted into an expression tree."
        '' select non-model struct type with simple values using combination of constructor and member init
        'Dim result5 = db.From(Of ItemWithAllSupportedValues).
        '                 Where(Function(x) x.Id = item2.Id).
        '                 Select(Function(x) New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull) With {.IntValue = x.IntColumn}).
        '                 FirstOrDefault()
        'Assert.AreEqual(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull) With {.IntValue = item2.IntColumn}, result5)

        ' select non-model struct type with simple values, but no row is returned
        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = Guid.NewGuid).
                         Select(Function(x) New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull)).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct, result6)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelectOfAdHocTypeThatIsNullableStruct()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item1.UniqueidentifierColumn = Guid.NewGuid
      item1.BitColumn = False
      item1.Nvarchar50Column = ""
      item1.SmallintColumnNull = Nothing
      item1.IntColumn = 1
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item2.UniqueidentifierColumn = Guid.NewGuid
      item2.BitColumn = True
      item2.Nvarchar50Column = "lorem ipsum"
      item2.SmallintColumnNull = 6
      item2.IntColumn = 2
      item2.Numeric10and3ColumnNull = 42.6D

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues
      item3.UniqueidentifierColumn = Guid.NewGuid
      item3.BitColumn = True
      item3.Nvarchar50Column = "dolor sit"
      item3.SmallintColumnNull = 3
      item3.IntColumn = 3
      item3.Numeric10and3ColumnNull = 100D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        ' select non-model nullable struct type with simple values using member init
        Dim result1a = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Function(x) New NonModelStruct?(New NonModelStruct With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull})).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull)), result1a)

        ' select non-model nullable struct type with simple values using member init - with explicit cast
        Dim result1b = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Function(x) CType(New NonModelStruct With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}, NonModelStruct?)).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull)), result1b)

        ' select non-model nullable struct type with simple values using member init - with implicit cast
        Dim result1c = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Of NonModelStruct?)(Function(x) New NonModelStruct With {.GuidValue = x.UniqueidentifierColumn, .StringValue = x.Nvarchar50Column, .NullableDecimalValue = x.Numeric10and3ColumnNull}).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull)), result1c)

        ' select non-model nullable struct type with simple and entity values using different member init
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelStruct?(New NonModelStruct With {.GuidValue = x.UniqueidentifierColumn, .ItemWithAllSupportedValues = x})).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2)), result2)

        ' select non-model nullable struct type with simple values using constructor
        Dim result3a = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Function(x) New NonModelStruct?(New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull))).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull)), result3a)

        ' select non-model nullable struct type with simple values using constructor - with explicit cast
        Dim result3b = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Function(x) CType(New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull), NonModelStruct?)).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull)), result3b)

        ' select non-model nullable struct type with simple values using constructor - with implicit cast
        Dim result3c = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = item2.Id).
                          Select(Of NonModelStruct?)(Function(x) New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull)).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull)), result3c)

        ' select non-model nullable struct type with simple and entity values using different constructor
        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.Id = item2.Id).
                         Select(Function(x) New NonModelStruct?(New NonModelStruct(x.UniqueidentifierColumn, x))).
                         FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2)), result4)

        ' NOTE: this doesn't compile and there is following error: "Expression cannot be converted into an expression tree."
        '' select non-model struct type with simple values using combination of constructor and member init
        'Dim result5 = db.From(Of ItemWithAllSupportedValues).
        '                 Where(Function(x) x.Id = item2.Id).
        '                 Select(Function(x) New NonModelStruct?(New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull) With {.IntValue = x.IntColumn})).
        '                 FirstOrDefault()
        'Assert.AreEqual(New NonModelStruct?(New NonModelStruct(item2.UniqueidentifierColumn, item2.Nvarchar50Column, item2.Numeric10and3ColumnNull) With {.IntValue = item2.IntColumn}), result5)

        ' select non-model nullable struct type with simple values, but no row is returned
        Dim result6a = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          Select(Function(x) New NonModelStruct?(New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull))).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?, result6a)

        ' select non-model nullable struct type with simple values, but no row is returned - with explicit cast
        Dim result6b = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          Select(Function(x) CType(New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull), NonModelStruct?)).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?, result6b)

        ' select non-model nullable struct type with simple values, but no row is returned - with implicit cast
        Dim result6c = db.From(Of ItemWithAllSupportedValues).
                          Where(Function(x) x.Id = Guid.NewGuid).
                          Select(Of NonModelStruct?)(Function(x) New NonModelStruct(x.UniqueidentifierColumn, x.Nvarchar50Column, x.Numeric10and3ColumnNull)).
                          FirstOrDefault()
        Assert.AreEqual(New NonModelStruct?, result6c)
      End Using
    End Sub

  End Class
End Namespace
