Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class QueryTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub QueryOfGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.UniqueidentifierColumn = Guid.Empty

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.UniqueidentifierColumn = Guid.NewGuid

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Guid)("SELECT UniqueidentifierColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Guid)("SELECT UniqueidentifierColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.UniqueidentifierColumn, result2(0))
        Assert.AreEqual(item2.UniqueidentifierColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.UniqueidentifierColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.UniqueidentifierColumnNull = Guid.Empty

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.UniqueidentifierColumnNull = Guid.NewGuid

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Guid?)("SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Guid?)("SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.UniqueidentifierColumnNull, result2(0))
        Assert.AreEqual(item2.UniqueidentifierColumnNull, result2(1))
        Assert.AreEqual(item3.UniqueidentifierColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.Nvarchar50ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.Nvarchar50ColumnNull = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.Nvarchar50ColumnNull = "lorem ipsum"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of String)("SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of String)("SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.Nvarchar50ColumnNull, result2(0))
        Assert.AreEqual(item2.Nvarchar50ColumnNull, result2(1))
        Assert.AreEqual(item3.Nvarchar50ColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.BitColumn = False

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.BitColumn = True

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Boolean)("SELECT BitColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Boolean)("SELECT BitColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.BitColumn, result2(0))
        Assert.AreEqual(item2.BitColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.BitColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.BitColumnNull = False

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.BitColumnNull = True

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Boolean?)("SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Boolean?)("SELECT BitColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.BitColumnNull, result2(0))
        Assert.AreEqual(item2.BitColumnNull, result2(1))
        Assert.AreEqual(item3.BitColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.SmallintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.SmallintColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Int16)("SELECT SmallintColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Int16)("SELECT SmallintColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.SmallintColumn, result2(0))
        Assert.AreEqual(item2.SmallintColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.SmallintColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.SmallintColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.SmallintColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Int16?)("SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Int16?)("SELECT SmallintColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.SmallintColumnNull, result2(0))
        Assert.AreEqual(item2.SmallintColumnNull, result2(1))
        Assert.AreEqual(item3.SmallintColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.SmallintColumn = 1
      item1.IntColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.SmallintColumn = 2
      item2.IntColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Int32)("SELECT IntColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY SmallintColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Int32)("SELECT IntColumn FROM ItemWithAllSupportedValues ORDER BY SmallintColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.IntColumn, result2(0))
        Assert.AreEqual(item2.IntColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.IntColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.IntColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.IntColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Int32?)("SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Int32?)("SELECT IntColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.IntColumnNull, result2(0))
        Assert.AreEqual(item2.IntColumnNull, result2(1))
        Assert.AreEqual(item3.IntColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.BigintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.BigintColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Int64)("SELECT BigintColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Int64)("SELECT BigintColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.BigintColumn, result2(0))
        Assert.AreEqual(item2.BigintColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.BigintColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.BigintColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.BigintColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Int64?)("SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Int64?)("SELECT BigintColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.BigintColumnNull, result2(0))
        Assert.AreEqual(item2.BigintColumnNull, result2(1))
        Assert.AreEqual(item3.BigintColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.RealColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.RealColumn = 42.6

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Single)("SELECT RealColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Single)("SELECT RealColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.RealColumn, result2(0))
        Assert.AreEqual(item2.RealColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.RealColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.RealColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.RealColumnNull = 42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Single?)("SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Single?)("SELECT RealColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.RealColumnNull, result2(0))
        Assert.AreEqual(item2.RealColumnNull, result2(1))
        Assert.AreEqual(item3.RealColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.FloatColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.FloatColumn = 42.6

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Double)("SELECT FloatColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Double)("SELECT FloatColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.FloatColumn, result2(0))
        Assert.AreEqual(item2.FloatColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.FloatColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.FloatColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.FloatColumnNull = 42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Double?)("SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Double?)("SELECT FloatColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.FloatColumnNull, result2(0))
        Assert.AreEqual(item2.FloatColumnNull, result2(1))
        Assert.AreEqual(item3.FloatColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.Numeric10and3Column = 42.6D

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Decimal)("SELECT Numeric10and3Column FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Decimal)("SELECT Numeric10and3Column FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.Numeric10and3Column, result2(0))
        Assert.AreEqual(item2.Numeric10and3Column, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.Numeric10and3ColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.Numeric10and3ColumnNull = 42.6D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Decimal?)("SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Decimal?)("SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1.Numeric10and3ColumnNull, result2(0))
        Assert.AreEqual(item2.Numeric10and3ColumnNull, result2(1))
        Assert.AreEqual(item3.Numeric10and3ColumnNull, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfDateTime()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.DatetimeColumn = Helpers.Calendar.Now()

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item1.DatetimeColumn = item1.DatetimeColumn.AddDays(1).AddHours(1)

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of DateTime)("SELECT DatetimeColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of DateTime)("SELECT DatetimeColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.DatetimeColumn, result2(0))
        Assert.AreEqual(item2.DatetimeColumn, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfNullableDateTime()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.DatetimeColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.DatetimeColumnNull = Helpers.Calendar.Now()

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of DateTime?)("SELECT DatetimeColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of DateTime?)("SELECT DatetimeColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(2, result2.Count)
        Assert.AreEqual(item1.DatetimeColumnNull, result2(0))
        Assert.AreEqual(item2.DatetimeColumnNull, result2(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfByteArray()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1
      item1.Varbinary50ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 2
      item2.Varbinary50ColumnNull = New Byte() {}

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 3
      item3.Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(10)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of Byte())("SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of Byte())("SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50ColumnNull, result2(0)))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, result2(1)))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result2(2)))
      End Using
    End Sub

  End Class
End Namespace
