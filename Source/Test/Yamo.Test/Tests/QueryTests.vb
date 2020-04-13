Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class QueryTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

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

    <TestMethod()>
    Public Overridable Sub QueryOfModel()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      item2.IntColumn = 2

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()
      item3.IntColumn = 3

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual(item1, result2(0))
        Assert.AreEqual(item2, result2(1))
        Assert.AreEqual(item3, result2(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfValueTuple()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 1

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      item2.IntColumn = 2

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()
      item3.IntColumn = 3

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1empty = db.Query(Of (Guid, Guid?))("SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result1empty.Count)

        Dim result1null = db.Query(Of (Guid, Guid?)?)("SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result1null.Count)
        Assert.AreEqual((item1.UniqueidentifierColumn, item1.UniqueidentifierColumnNull), result1null(0).Value)
        Assert.AreEqual((item2.UniqueidentifierColumn, item2.UniqueidentifierColumnNull), result1null(1).Value)
        Assert.AreEqual((item3.UniqueidentifierColumn, item3.UniqueidentifierColumnNull), result1null(2).Value)

        Dim result1 = db.Query(Of (Guid, Guid?))("SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result1.Count)
        Assert.AreEqual((item1.UniqueidentifierColumn, item1.UniqueidentifierColumnNull), result1(0))
        Assert.AreEqual((item2.UniqueidentifierColumn, item2.UniqueidentifierColumnNull), result1(1))
        Assert.AreEqual((item3.UniqueidentifierColumn, item3.UniqueidentifierColumnNull), result1(2))


        Dim result2empty = db.Query(Of (String, String, String))("SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result2empty.Count)

        Dim result2null = db.Query(Of (String, String, String)?)("SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2null.Count)
        Assert.AreEqual((item1.Nvarchar50Column, item1.Nvarchar50ColumnNull, item1.NvarcharMaxColumn), result2null(0).Value)
        Assert.AreEqual((item2.Nvarchar50Column, item2.Nvarchar50ColumnNull, item2.NvarcharMaxColumn), result2null(1).Value)
        Assert.AreEqual((item3.Nvarchar50Column, item3.Nvarchar50ColumnNull, item3.NvarcharMaxColumn), result2null(2).Value)

        Dim result2 = db.Query(Of (String, String, String))("SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result2.Count)
        Assert.AreEqual((item1.Nvarchar50Column, item1.Nvarchar50ColumnNull, item1.NvarcharMaxColumn), result2(0))
        Assert.AreEqual((item2.Nvarchar50Column, item2.Nvarchar50ColumnNull, item2.NvarcharMaxColumn), result2(1))
        Assert.AreEqual((item3.Nvarchar50Column, item3.Nvarchar50ColumnNull, item3.NvarcharMaxColumn), result2(2))


        Dim result3empty = db.Query(Of (String, Boolean, Boolean?, Int16))("SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result3empty.Count)

        Dim result3null = db.Query(Of (String, Boolean, Boolean?, Int16)?)("SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result3null.Count)
        Assert.AreEqual((item1.NvarcharMaxColumnNull, item1.BitColumn, item1.BitColumnNull, item1.SmallintColumn), result3null(0).Value)
        Assert.AreEqual((item2.NvarcharMaxColumnNull, item2.BitColumn, item2.BitColumnNull, item2.SmallintColumn), result3null(1).Value)
        Assert.AreEqual((item3.NvarcharMaxColumnNull, item3.BitColumn, item3.BitColumnNull, item3.SmallintColumn), result3null(2).Value)

        Dim result3 = db.Query(Of (String, Boolean, Boolean?, Int16))("SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result3.Count)
        Assert.AreEqual((item1.NvarcharMaxColumnNull, item1.BitColumn, item1.BitColumnNull, item1.SmallintColumn), result3(0))
        Assert.AreEqual((item2.NvarcharMaxColumnNull, item2.BitColumn, item2.BitColumnNull, item2.SmallintColumn), result3(1))
        Assert.AreEqual((item3.NvarcharMaxColumnNull, item3.BitColumn, item3.BitColumnNull, item3.SmallintColumn), result3(2))


        Dim result4empty = db.Query(Of (Int16?, Int32, Int32?, Int64, Int64?))("SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result4empty.Count)

        Dim result4null = db.Query(Of (Int16?, Int32, Int32?, Int64, Int64?)?)("SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result4null.Count)
        Assert.AreEqual((item1.SmallintColumnNull, item1.IntColumn, item1.IntColumnNull, item1.BigintColumn, item1.BigintColumnNull), result4null(0).Value)
        Assert.AreEqual((item2.SmallintColumnNull, item2.IntColumn, item2.IntColumnNull, item2.BigintColumn, item2.BigintColumnNull), result4null(1).Value)
        Assert.AreEqual((item3.SmallintColumnNull, item3.IntColumn, item3.IntColumnNull, item3.BigintColumn, item3.BigintColumnNull), result4null(2).Value)

        Dim result4 = db.Query(Of (Int16?, Int32, Int32?, Int64, Int64?))("SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result4.Count)
        Assert.AreEqual((item1.SmallintColumnNull, item1.IntColumn, item1.IntColumnNull, item1.BigintColumn, item1.BigintColumnNull), result4(0))
        Assert.AreEqual((item2.SmallintColumnNull, item2.IntColumn, item2.IntColumnNull, item2.BigintColumn, item2.BigintColumnNull), result4(1))
        Assert.AreEqual((item3.SmallintColumnNull, item3.IntColumn, item3.IntColumnNull, item3.BigintColumn, item3.BigintColumnNull), result4(2))


        Dim result5empty = db.Query(Of (Single, Single?, Double, Double?, Decimal, Decimal?))("SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result5empty.Count)

        Dim result5null = db.Query(Of (Single, Single?, Double, Double?, Decimal, Decimal?)?)("SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result5null.Count)
        Assert.AreEqual((item1.RealColumn, item1.RealColumnNull, item1.FloatColumn, item1.FloatColumnNull, item1.Numeric10and3Column, item1.Numeric10and3ColumnNull), result5null(0).Value)
        Assert.AreEqual((item2.RealColumn, item2.RealColumnNull, item2.FloatColumn, item2.FloatColumnNull, item2.Numeric10and3Column, item2.Numeric10and3ColumnNull), result5null(1).Value)
        Assert.AreEqual((item3.RealColumn, item3.RealColumnNull, item3.FloatColumn, item3.FloatColumnNull, item3.Numeric10and3Column, item3.Numeric10and3ColumnNull), result5null(2).Value)

        Dim result5 = db.Query(Of (Single, Single?, Double, Double?, Decimal, Decimal?))("SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result5.Count)
        Assert.AreEqual((item1.RealColumn, item1.RealColumnNull, item1.FloatColumn, item1.FloatColumnNull, item1.Numeric10and3Column, item1.Numeric10and3ColumnNull), result5(0))
        Assert.AreEqual((item2.RealColumn, item2.RealColumnNull, item2.FloatColumn, item2.FloatColumnNull, item2.Numeric10and3Column, item2.Numeric10and3ColumnNull), result5(1))
        Assert.AreEqual((item3.RealColumn, item3.RealColumnNull, item3.FloatColumn, item3.FloatColumnNull, item3.Numeric10and3Column, item3.Numeric10and3ColumnNull), result5(2))


        Dim result6empty = db.Query(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid))("SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues WHERE 1 = 2 ORDER BY IntColumn")
        Assert.AreEqual(0, result6empty.Count)

        Dim result6null = db.Query(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid)?)("SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result6null.Count)
        Assert.AreEqual(item1.Numeric15and0Column, result6null(0).Value.Item1)
        Assert.AreEqual(item1.Numeric15and0ColumnNull, result6null(0).Value.Item2)
        Assert.AreEqual(item1.DatetimeColumn, result6null(0).Value.Item3)
        Assert.AreEqual(item1.DatetimeColumnNull, result6null(0).Value.Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50Column, result6null(0).Value.Item5))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50ColumnNull, result6null(0).Value.Item6))
        Assert.AreEqual(item1.Id, result6null(0).Value.Item7)
        Assert.AreEqual(item2.Numeric15and0Column, result6null(1).Value.Item1)
        Assert.AreEqual(item2.Numeric15and0ColumnNull, result6null(1).Value.Item2)
        Assert.AreEqual(item2.DatetimeColumn, result6null(1).Value.Item3)
        Assert.AreEqual(item2.DatetimeColumnNull, result6null(1).Value.Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, result6null(1).Value.Item6))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50Column, result6null(1).Value.Item5))
        Assert.AreEqual(item2.Id, result6null(1).Value.Item7)
        Assert.AreEqual(item3.Numeric15and0Column, result6null(2).Value.Item1)
        Assert.AreEqual(item3.Numeric15and0ColumnNull, result6null(2).Value.Item2)
        Assert.AreEqual(item3.DatetimeColumn, result6null(2).Value.Item3)
        Assert.AreEqual(item3.DatetimeColumnNull, result6null(2).Value.Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50Column, result6null(2).Value.Item5))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result6null(2).Value.Item6))
        Assert.AreEqual(item3.Id, result6null(2).Value.Item7)


        Dim result6 = db.Query(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid))("SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues ORDER BY IntColumn")
        Assert.AreEqual(3, result6.Count)
        Assert.AreEqual(item1.Numeric15and0Column, result6(0).Item1)
        Assert.AreEqual(item1.Numeric15and0ColumnNull, result6(0).Item2)
        Assert.AreEqual(item1.DatetimeColumn, result6(0).Item3)
        Assert.AreEqual(item1.DatetimeColumnNull, result6(0).Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50Column, result6(0).Item5))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50ColumnNull, result6(0).Item6))
        Assert.AreEqual(item1.Id, result6(0).Item7)
        Assert.AreEqual(item2.Numeric15and0Column, result6(1).Item1)
        Assert.AreEqual(item2.Numeric15and0ColumnNull, result6(1).Item2)
        Assert.AreEqual(item2.DatetimeColumn, result6(1).Item3)
        Assert.AreEqual(item2.DatetimeColumnNull, result6(1).Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, result6(1).Item6))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50Column, result6(1).Item5))
        Assert.AreEqual(item2.Id, result6(1).Item7)
        Assert.AreEqual(item3.Numeric15and0Column, result6(2).Item1)
        Assert.AreEqual(item3.Numeric15and0ColumnNull, result6(2).Item2)
        Assert.AreEqual(item3.DatetimeColumn, result6(2).Item3)
        Assert.AreEqual(item3.DatetimeColumnNull, result6(2).Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50Column, result6(2).Item5))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result6(2).Item6))
        Assert.AreEqual(item3.Id, result6(2).Item7)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryOfValueTupleWithModel()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article3LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)
      Dim article3LabelDe = Me.ModelFactory.CreateLabel(NameOf(Article), 3, German)

      InsertItems(article1, article2, article3, article1LabelEn, article3LabelEn, article3LabelDe)

      Using db = CreateDbContext()
        Dim result1 = db.Query(Of (Article, Int32, Label)?)($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE 1 = 2 ORDER BY a.Id, l.Language")
        Assert.AreEqual(0, result1.Count)

        Dim result2 = db.Query(Of (Article, Int32, Label))($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id ORDER BY a.Id, l.Language")
        Assert.AreEqual(4, result2.Count)
        Assert.AreEqual((article1, 42, article1LabelEn), result2(0))
        Assert.AreEqual((article2, 42, DirectCast(Nothing, Label)), result2(1))
        Assert.AreEqual((article3, 42, article3LabelEn), result2(2))
        Assert.AreEqual((article3, 42, article3LabelDe), result2(3))

        ' selecting same table twice
        Dim result3 = db.Query(Of (Article, Int32, Label, Label))($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("le")}, {Sql.Model.Columns(Of Label)("lg")} FROM Article AS a LEFT JOIN Label AS le ON a.Id = le.Id AND le.Language = {English} LEFT JOIN Label AS lg ON a.Id = lg.Id AND lg.Language = {German} ORDER BY a.Id")
        Assert.AreEqual(3, result3.Count)
        Assert.AreEqual((article1, 42, article1LabelEn, DirectCast(Nothing, Label)), result3(0))
        Assert.AreEqual((article2, 42, DirectCast(Nothing, Label), DirectCast(Nothing, Label)), result3(1))
        Assert.AreEqual((article3, 42, article3LabelEn, article3LabelDe), result3(2))
      End Using
    End Sub

  End Class
End Namespace
