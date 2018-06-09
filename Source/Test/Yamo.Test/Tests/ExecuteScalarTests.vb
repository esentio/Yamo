Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class ExecuteScalarTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.UniqueidentifierColumn = Guid.Empty

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.UniqueidentifierColumn = Guid.NewGuid

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Guid)($"SELECT UniqueidentifierColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.UniqueidentifierColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Guid)($"SELECT UniqueidentifierColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.UniqueidentifierColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.UniqueidentifierColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.UniqueidentifierColumnNull = Guid.Empty

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.UniqueidentifierColumnNull = Guid.NewGuid

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Guid?)($"SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.UniqueidentifierColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Guid?)($"SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.UniqueidentifierColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Guid?)($"SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.UniqueidentifierColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Nvarchar50ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Nvarchar50ColumnNull = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Nvarchar50ColumnNull = "lorem ipsum"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of String)($"SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Nvarchar50ColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of String)($"SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Nvarchar50ColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of String)($"SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Nvarchar50ColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BitColumn = False

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BitColumn = True

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Boolean)($"SELECT BitColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BitColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Boolean)($"SELECT BitColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BitColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BitColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BitColumnNull = False

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.BitColumnNull = True

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Boolean?)($"SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BitColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Boolean?)($"SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BitColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Boolean?)($"SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.BitColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.SmallintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.SmallintColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Int16)($"SELECT SmallintColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.SmallintColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Int16)($"SELECT SmallintColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.SmallintColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.SmallintColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.SmallintColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.SmallintColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Int16?)($"SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.SmallintColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Int16?)($"SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.SmallintColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Int16?)($"SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.SmallintColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Int32)($"SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.IntColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Int32)($"SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.IntColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Int32?)($"SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.IntColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Int32?)($"SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.IntColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Int32?)($"SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.IntColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BigintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BigintColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Int64)($"SELECT BigintColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BigintColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Int64)($"SELECT BigintColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BigintColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BigintColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BigintColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.BigintColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Int64?)($"SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BigintColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Int64?)($"SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BigintColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Int64?)($"SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.BigintColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.RealColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.RealColumn = 42.6

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Single)($"SELECT RealColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.RealColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Single)($"SELECT RealColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.RealColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.RealColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.RealColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.RealColumnNull = 42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Single?)($"SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.RealColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Single?)($"SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.RealColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Single?)($"SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.RealColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.FloatColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.FloatColumn = 42.6

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Double)($"SELECT FloatColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.FloatColumn, result1)

        Dim result2 = db.ExecuteScalar(Of Double)($"SELECT FloatColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.FloatColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.FloatColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.FloatColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.FloatColumnNull = 42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Double?)($"SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.FloatColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Double?)($"SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.FloatColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Double?)($"SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.FloatColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Numeric10and3Column = 42.6D

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Decimal)($"SELECT Numeric10and3Column FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Numeric10and3Column, result1)

        Dim result2 = db.ExecuteScalar(Of Decimal)($"SELECT Numeric10and3Column FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Numeric10and3Column, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Numeric10and3ColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Numeric10and3ColumnNull = 42.6D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Decimal?)($"SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Numeric10and3ColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of Decimal?)($"SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Numeric10and3ColumnNull, result2)

        Dim result3 = db.ExecuteScalar(Of Decimal?)($"SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Numeric10and3ColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfDateTime()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.DatetimeColumn = Helpers.Calendar.Now()

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.ExecuteScalar(Of DateTime)($"SELECT DatetimeColumn FROM ItemWithAllSupportedValues WHERE Id = {item.Id}")
        Assert.AreEqual(item.DatetimeColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfNullableDateTime()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.DatetimeColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.DatetimeColumnNull = Helpers.Calendar.Now()

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of DateTime?)($"SELECT DatetimeColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.DatetimeColumnNull, result1)

        Dim result2 = db.ExecuteScalar(Of DateTime?)($"SELECT DatetimeColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.DatetimeColumnNull, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub ExecuteScalarOfByteArray()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Varbinary50ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Varbinary50ColumnNull = New Byte() {}

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(10)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.ExecuteScalar(Of Byte())($"SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50ColumnNull, result1))

        Dim result2 = db.ExecuteScalar(Of Byte())($"SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, result2))

        Dim result3 = db.ExecuteScalar(Of Byte())($"SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result3))
      End Using
    End Sub

  End Class
End Namespace
