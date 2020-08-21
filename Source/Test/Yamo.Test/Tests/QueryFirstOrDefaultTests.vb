Imports Yamo.Test.Model
Imports Yamo

Namespace Tests

  Public MustInherit Class QueryFirstOrDefaultTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultUsingFormattableString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 10
      item1.Nvarchar50Column = "lorem"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 20
      item2.Nvarchar50Column = "ipsum"

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 30
      item3.Nvarchar50Column = "dolor"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).TableName
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.Nvarchar50Column)).ColumnName

        Dim result = db.QueryFirstOrDefault(Of Int32)($"SELECT IntColumn FROM {RawSqlString.Create(table)} WHERE {RawSqlString.Create(column)} = {item2.Nvarchar50Column}")
        Assert.AreEqual(item2.IntColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultUsingRawSqlString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 10
      item1.Nvarchar50Column = "lorem"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 20
      item2.Nvarchar50Column = "ipsum"

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 30
      item3.Nvarchar50Column = "dolor"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result = db.QueryFirstOrDefault(Of Int32)("SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Nvarchar50Column = 'ipsum'")
        Assert.AreEqual(item2.IntColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultUsingRawSqlStringWithParameters()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 10
      item1.Nvarchar50Column = "lorem"

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 20
      item2.Nvarchar50Column = "ipsum"

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumn = 30
      item3.Nvarchar50Column = "dolor"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).TableName
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.Nvarchar50Column)).ColumnName

        Dim result = db.QueryFirstOrDefault(Of Int32)("SELECT IntColumn FROM {0} WHERE {1} = {2}", RawSqlString.Create(table), RawSqlString.Create(column), "ipsum")
        Assert.AreEqual(item2.IntColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.UniqueidentifierColumn = Guid.Empty

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.UniqueidentifierColumn = Guid.NewGuid

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Guid)($"SELECT UniqueidentifierColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.UniqueidentifierColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Guid)($"SELECT UniqueidentifierColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.UniqueidentifierColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableGuid()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.UniqueidentifierColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.UniqueidentifierColumnNull = Guid.Empty

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.UniqueidentifierColumnNull = Guid.NewGuid

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Guid?)($"SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.UniqueidentifierColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Guid?)($"SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.UniqueidentifierColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Guid?)($"SELECT UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.UniqueidentifierColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfString()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Nvarchar50ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Nvarchar50ColumnNull = ""

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Nvarchar50ColumnNull = "lorem ipsum"

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of String)($"SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Nvarchar50ColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of String)($"SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Nvarchar50ColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of String)($"SELECT Nvarchar50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Nvarchar50ColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BitColumn = False

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BitColumn = True

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Boolean)($"SELECT BitColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BitColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Boolean)($"SELECT BitColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BitColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableBoolean()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BitColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BitColumnNull = False

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.BitColumnNull = True

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Boolean?)($"SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BitColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Boolean?)($"SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BitColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Boolean?)($"SELECT BitColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.BitColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.SmallintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.SmallintColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Int16)($"SELECT SmallintColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.SmallintColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Int16)($"SELECT SmallintColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.SmallintColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableInt16()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.SmallintColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.SmallintColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.SmallintColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Int16?)($"SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.SmallintColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Int16?)($"SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.SmallintColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Int16?)($"SELECT SmallintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.SmallintColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Int32)($"SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.IntColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Int32)($"SELECT IntColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.IntColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableInt32()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.IntColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.IntColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.IntColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Int32?)($"SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.IntColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Int32?)($"SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.IntColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Int32?)($"SELECT IntColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.IntColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BigintColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BigintColumn = 42

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Int64)($"SELECT BigintColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BigintColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Int64)($"SELECT BigintColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BigintColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableInt64()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.BigintColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.BigintColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.BigintColumnNull = 42

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Int64?)($"SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.BigintColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Int64?)($"SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.BigintColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Int64?)($"SELECT BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.BigintColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.RealColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.RealColumn = 42.6

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Single)($"SELECT RealColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.RealColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Single)($"SELECT RealColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.RealColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableSingle()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.RealColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.RealColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.RealColumnNull = 42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Single?)($"SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.RealColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Single?)($"SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.RealColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Single?)($"SELECT RealColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.RealColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.FloatColumn = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.FloatColumn = 42.6

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Double)($"SELECT FloatColumn FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.FloatColumn, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Double)($"SELECT FloatColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.FloatColumn, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableDouble()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.FloatColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.FloatColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.FloatColumnNull = 42.6

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Double?)($"SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.FloatColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Double?)($"SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.FloatColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Double?)($"SELECT FloatColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.FloatColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Numeric10and3Column = 0

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Numeric10and3Column = 42.6D

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Decimal)($"SELECT Numeric10and3Column FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Numeric10and3Column, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Decimal)($"SELECT Numeric10and3Column FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Numeric10and3Column, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableDecimal()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Numeric10and3ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Numeric10and3ColumnNull = 0

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Numeric10and3ColumnNull = 42.6D

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Decimal?)($"SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.Numeric10and3ColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of Decimal?)($"SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.Numeric10and3ColumnNull, result2)

        Dim result3 = db.QueryFirstOrDefault(Of Decimal?)($"SELECT Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Numeric10and3ColumnNull, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfDateTime()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.DatetimeColumn = Helpers.Calendar.Now()

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result = db.QueryFirstOrDefault(Of DateTime)($"SELECT DatetimeColumn FROM ItemWithAllSupportedValues WHERE Id = {item.Id}")
        Assert.AreEqual(item.DatetimeColumn, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfNullableDateTime()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.DatetimeColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.DatetimeColumnNull = Helpers.Calendar.Now()

      InsertItems(item1, item2)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of DateTime?)($"SELECT DatetimeColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual(item1.DatetimeColumnNull, result1)

        Dim result2 = db.QueryFirstOrDefault(Of DateTime?)($"SELECT DatetimeColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual(item2.DatetimeColumnNull, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfByteArray()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item1.Varbinary50ColumnNull = Nothing

      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item2.Varbinary50ColumnNull = New Byte() {}

      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item3.Varbinary50ColumnNull = Helpers.Data.CreateRandomByteArray(10)

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of Byte())($"SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item1.Varbinary50ColumnNull, result1))

        Dim result2 = db.QueryFirstOrDefault(Of Byte())($"SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item2.Varbinary50ColumnNull, result2))

        Dim result3 = db.QueryFirstOrDefault(Of Byte())($"SELECT Varbinary50ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result3))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfModel()
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      InsertItems(item)

      Using db = CreateDbContext()
        Dim result1 = db.QueryFirstOrDefault(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.IsNull(result1)

        Dim result2 = db.QueryFirstOrDefault(Of ItemWithAllSupportedValues)($"SELECT {Sql.Model.Columns(Of ItemWithAllSupportedValues)} FROM ItemWithAllSupportedValues WHERE Id = {item.Id}")
        Assert.AreEqual(item, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfValueTuple()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      Dim item2 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMinValues()
      Dim item3 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithMaxValues()

      InsertItems(item1, item2, item3)

      Using db = CreateDbContext()
        Dim result1null = db.QueryFirstOrDefault(Of (Guid, Guid?)?)("SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Guid, Guid?)?, result1null)

        Dim result1nullWithValue = db.QueryFirstOrDefault(Of (Guid, Guid?)?)($"SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.UniqueidentifierColumn, item1.UniqueidentifierColumnNull), result1nullWithValue.Value)

        Dim result1empty = db.QueryFirstOrDefault(Of (Guid, Guid?))("SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Guid, Guid?), result1empty)

        Dim result1 = db.QueryFirstOrDefault(Of (Guid, Guid?))($"SELECT UniqueidentifierColumn, UniqueidentifierColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.UniqueidentifierColumn, item1.UniqueidentifierColumnNull), result1)


        Dim result2null = db.QueryFirstOrDefault(Of (String, String, String)?)("SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of String, String, String)?, result2null)

        Dim result2nullWithValue = db.QueryFirstOrDefault(Of (String, String, String)?)($"SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual((item2.Nvarchar50Column, item2.Nvarchar50ColumnNull, item2.NvarcharMaxColumn), result2nullWithValue.Value)

        Dim result2empty = db.QueryFirstOrDefault(Of (String, String, String))("SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of String, String, String), result2empty)

        Dim result2 = db.QueryFirstOrDefault(Of (String, String, String))($"SELECT Nvarchar50Column, Nvarchar50ColumnNull, NvarcharMaxColumn FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual((item2.Nvarchar50Column, item2.Nvarchar50ColumnNull, item2.NvarcharMaxColumn), result2)


        Dim result3null = db.QueryFirstOrDefault(Of (String, Boolean, Boolean?, Int16)?)("SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of String, Boolean, Boolean?, Int16)?, result3null)

        Dim result3nullWithValue = db.QueryFirstOrDefault(Of (String, Boolean, Boolean?, Int16)?)($"SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual((item3.NvarcharMaxColumnNull, item3.BitColumn, item3.BitColumnNull, item3.SmallintColumn), result3nullWithValue.Value)

        Dim result3empty = db.QueryFirstOrDefault(Of (String, Boolean, Boolean?, Int16))("SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of String, Boolean, Boolean?, Int16), result3empty)

        Dim result3 = db.QueryFirstOrDefault(Of (String, Boolean, Boolean?, Int16))($"SELECT NvarcharMaxColumnNull, BitColumn, BitColumnNull, SmallintColumn FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual((item3.NvarcharMaxColumnNull, item3.BitColumn, item3.BitColumnNull, item3.SmallintColumn), result3)


        Dim result4null = db.QueryFirstOrDefault(Of (Int16?, Int32, Int32?, Int64, Int64?)?)("SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Int16?, Int32, Int32?, Int64, Int64?)?, result4null)

        Dim result4nullWithValue = db.QueryFirstOrDefault(Of (Int16?, Int32, Int32?, Int64, Int64?)?)($"SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.SmallintColumnNull, item1.IntColumn, item1.IntColumnNull, item1.BigintColumn, item1.BigintColumnNull), result4nullWithValue.Value)

        Dim result4empty = db.QueryFirstOrDefault(Of (Int16?, Int32, Int32?, Int64, Int64?))("SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Int16?, Int32, Int32?, Int64, Int64?), result4empty)

        Dim result4 = db.QueryFirstOrDefault(Of (Int16?, Int32, Int32?, Int64, Int64?))($"SELECT SmallintColumnNull, IntColumn, IntColumnNull, BigintColumn, BigintColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item1.Id}")
        Assert.AreEqual((item1.SmallintColumnNull, item1.IntColumn, item1.IntColumnNull, item1.BigintColumn, item1.BigintColumnNull), result4)


        Dim result5null = db.QueryFirstOrDefault(Of (Single, Single?, Double, Double?, Decimal, Decimal?)?)("SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Single, Single?, Double, Double?, Decimal, Decimal?)?, result5null)

        Dim result5nullWithValue = db.QueryFirstOrDefault(Of (Single, Single?, Double, Double?, Decimal, Decimal?)?)($"SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual((item2.RealColumn, item2.RealColumnNull, item2.FloatColumn, item2.FloatColumnNull, item2.Numeric10and3Column, item2.Numeric10and3ColumnNull), result5nullWithValue.Value)

        Dim result5empty = db.QueryFirstOrDefault(Of (Single, Single?, Double, Double?, Decimal, Decimal?))("SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Single, Single?, Double, Double?, Decimal, Decimal?), result5empty)

        Dim result5 = db.QueryFirstOrDefault(Of (Single, Single?, Double, Double?, Decimal, Decimal?))($"SELECT RealColumn, RealColumnNull, FloatColumn, FloatColumnNull, Numeric10and3Column, Numeric10and3ColumnNull FROM ItemWithAllSupportedValues WHERE Id = {item2.Id}")
        Assert.AreEqual((item2.RealColumn, item2.RealColumnNull, item2.FloatColumn, item2.FloatColumnNull, item2.Numeric10and3Column, item2.Numeric10and3ColumnNull), result5)


        Dim result6null = db.QueryFirstOrDefault(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid)?)("SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid)?, result6null)

        Dim result6nullWithValue = db.QueryFirstOrDefault(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid)?)($"SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Numeric15and0Column, result6nullWithValue.Value.Item1)
        Assert.AreEqual(item3.Numeric15and0ColumnNull, result6nullWithValue.Value.Item2)
        Assert.AreEqual(item3.DatetimeColumn, result6nullWithValue.Value.Item3)
        Assert.AreEqual(item3.DatetimeColumnNull, result6nullWithValue.Value.Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50Column, result6nullWithValue.Value.Item5))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result6nullWithValue.Value.Item6))
        Assert.AreEqual(item3.Id, result6nullWithValue.Value.Item7)

        Dim result6empty = db.QueryFirstOrDefault(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid))("SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid), result6empty)

        Dim result6 = db.QueryFirstOrDefault(Of (Decimal, Decimal?, DateTime, DateTime?, Byte(), Byte(), Guid))($"SELECT Numeric15and0Column, Numeric15and0ColumnNull, DatetimeColumn, DatetimeColumnNull, Varbinary50Column, Varbinary50ColumnNull, Id FROM ItemWithAllSupportedValues WHERE Id = {item3.Id}")
        Assert.AreEqual(item3.Numeric15and0Column, result6.Item1)
        Assert.AreEqual(item3.Numeric15and0ColumnNull, result6.Item2)
        Assert.AreEqual(item3.DatetimeColumn, result6.Item3)
        Assert.AreEqual(item3.DatetimeColumnNull, result6.Item4)
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50Column, result6.Item5))
        Assert.IsTrue(Helpers.Compare.AreByteArraysEqual(item3.Varbinary50ColumnNull, result6.Item6))
        Assert.AreEqual(item3.Id, result6.Item7)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfvalueTupleWithModel()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim article1LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 1, English)
      Dim article3LabelEn = Me.ModelFactory.CreateLabel(NameOf(Article), 3, English)
      Dim article3LabelDe = Me.ModelFactory.CreateLabel(NameOf(Article), 3, German)

      InsertItems(article1, article2, article3, article1LabelEn, article3LabelEn, article3LabelDe)

      Using db = CreateDbContext()
        Dim result1null = db.QueryFirstOrDefault(Of (Article, Int32, Label)?)($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Article, Int32, Label)?, result1null)

        Dim result1empty = db.QueryFirstOrDefault(Of (Article, Int32, Label))($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Article, Int32, Label), result1empty)

        Dim result1nullWithValue = db.QueryFirstOrDefault(Of (Article, Int32, Label)?)($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE a.Id = {article1.Id}")
        Assert.AreEqual((article1, 42, article1LabelEn), result1nullWithValue.Value)

        Dim result1 = db.QueryFirstOrDefault(Of (Article, Int32, Label))($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE a.Id = {article1.Id}")
        Assert.AreEqual((article1, 42, article1LabelEn), result1)

        Dim result2nullWithValue = db.QueryFirstOrDefault(Of (Article, Int32, Label)?)($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE a.Id = {article2.Id}")
        Assert.AreEqual((article2, 42, DirectCast(Nothing, Label)), result2nullWithValue.Value)

        Dim result2 = db.QueryFirstOrDefault(Of (Article, Int32, Label))($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("l")} FROM Article AS a LEFT JOIN Label AS l ON a.Id = l.Id WHERE a.Id = {article2.Id}")
        Assert.AreEqual((article2, 42, DirectCast(Nothing, Label)), result2)

        ' selecting same table twice
        Dim result3nullWithValue = db.QueryFirstOrDefault(Of (Article, Int32, Label, Label)?)($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("le")}, {Sql.Model.Columns(Of Label)("lg")} FROM Article AS a LEFT JOIN Label AS le ON a.Id = le.Id AND le.Language = {English} LEFT JOIN Label AS lg ON a.Id = lg.Id AND lg.Language = {German} WHERE a.Id = {article3.Id}")
        Assert.AreEqual((article3, 42, article3LabelEn, article3LabelDe), result3nullWithValue.Value)

        Dim result3 = db.QueryFirstOrDefault(Of (Article, Int32, Label, Label))($"SELECT {Sql.Model.Columns(Of Article)("a")}, 42, {Sql.Model.Columns(Of Label)("le")}, {Sql.Model.Columns(Of Label)("lg")} FROM Article AS a LEFT JOIN Label AS le ON a.Id = le.Id AND le.Language = {English} LEFT JOIN Label AS lg ON a.Id = lg.Id AND lg.Language = {German} WHERE a.Id = {article3.Id}")
        Assert.AreEqual((article3, 42, article3LabelEn, article3LabelDe), result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub QueryFirstOrDefaultOfLargeValueTuple()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItems(article1, article2, article3)

      Using db = CreateDbContext()
        ' 7 elements: (Int32, Int32, Int32, Int32, Int32, Int32, Int32)
        Dim result1 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32))("SELECT 1, 2, 3, 4, 5, 6, Id FROM Article WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32), result1)

        Dim result2 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32)?)("SELECT 1, 2, 3, 4, 5, 6, Id FROM Article WHERE 1 = 2")
        Assert.IsFalse(result2.HasValue)

        Dim result3 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32))($"SELECT 1, 2, 3, 4, 5, 6, Id FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 1), result3)

        Dim result4 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32)?)($"SELECT 1, 2, 3, 4, 5, 6, Id FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 1), result4.Value)

        ' 8 elements: (Int32, Int32, Int32, Int32, Int32, Int32, Int32, (Int32))
        Dim result5 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32))("SELECT 1, 2, 3, 4, 5, 6, 7, Id FROM Article WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Int32))(0, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Int32)(0)), result5)

        Dim result6 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)?)("SELECT 1, 2, 3, 4, 5, 6, 7, Id FROM Article WHERE 1 = 2")
        Assert.IsFalse(result6.HasValue)

        Dim result7 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32))($"SELECT 1, 2, 3, 4, 5, 6, 7, Id FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 1), result7)

        Dim result8 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)?)($"SELECT 1, 2, 3, 4, 5, 6, 7, Id FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 1), result8.Value)

        ' 15 elements: (Int32, Int32, Int32, Int32, Int32, Int32, Int32, (Int32, Int32, Int32, Int32, Int32, Int32, Int32, (Article)))
        Dim result9 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article))($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()} FROM Article WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Article)))(0, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Article))(0, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Article)(Nothing))), result9)

        Dim result10 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article)?)($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()} FROM Article WHERE 1 = 2")
        Assert.IsFalse(result10.HasValue)

        Dim result11 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article))($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()} FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 8, 1, 10, 11, 12, 13, 14, article1), result11)

        Dim result12 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article)?)($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()} FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 8, 1, 10, 11, 12, 13, 14, article1), result12.Value)

        ' 16 elements (Int32, Int32, Int32, Int32, Int32, Int32, Int32, (Int32, Int32, Int32, Int32, Int32, Int32, Int32, (Article, Int32)))
        Dim result13 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article, Int32))($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()}, 16 FROM Article WHERE 1 = 2")
        Assert.AreEqual(New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Article, Int32)))(0, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Int32, Int32, Int32, Int32, Int32, Int32, Int32, ValueTuple(Of Article, Int32))(0, 0, 0, 0, 0, 0, 0, New ValueTuple(Of Article, Int32)(Nothing, 0))), result13)

        Dim result14 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article, Int32)?)($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()}, 16 FROM Article WHERE 1 = 2")
        Assert.IsFalse(result14.HasValue)

        Dim result15 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article, Int32))($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()}, 16 FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 8, 1, 10, 11, 12, 13, 14, article1, 16), result15)

        Dim result16 = db.QueryFirstOrDefault(Of (Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Article, Int32)?)($"SELECT 1, 2, 3, 4, 5, 6, 7, 8, Id, 10, 11, 12, 13, 14, {Sql.Model.Columns(Of Article)()}, 16 FROM Article WHERE Id = {article1.Id}")
        Assert.AreEqual((1, 2, 3, 4, 5, 6, 7, 8, 1, 10, 11, 12, 13, 14, article1, 16), result16.Value)
      End Using
    End Sub

  End Class
End Namespace
