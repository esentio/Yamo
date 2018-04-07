Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlHelperAggregateTests
    Inherits TestsBase

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlCountAll()
      Dim items = CreateItems()

      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumnNull = 4
      items(3).BitColumn = True

      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Count()).
                         FirstOrDefault()

        Assert.AreEqual(5, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Count()).
                         FirstOrDefault()

        Assert.AreEqual(0, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlCount()
      Dim items = CreateItems()

      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumnNull = 4
      items(3).BitColumn = True

      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Count(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(3, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Count(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(0, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlCountDistinct()
      Dim items = CreateItems()

      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumnNull = 1
      items(3).BitColumn = True

      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.CountDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(2, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.CountDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(0, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlSum()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Sum(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(7, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Sum(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(7), result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Sum(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Sum(CType(x.IntColumn, Int32?))).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.Sum(x.IntColumn), Int32?)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Sum(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result6)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlSumDistinct()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 1
      items(3).IntColumnNull = 1
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.SumDistinct(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(3, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.SumDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(3), result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.SumDistinct(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.SumDistinct(CType(x.IntColumn, Int32?))).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.SumDistinct(x.IntColumn), Int32?)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.SumDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result6)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlAvg()
      Dim items = CreateItems()

      items(0).IntColumn = 10
      items(0).IntColumnNull = 10
      items(0).BitColumn = True

      items(1).IntColumn = 20
      items(1).IntColumnNull = 20
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 40
      items(3).IntColumnNull = 40
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Avg(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(14, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Avg(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(23), result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Avg(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Avg(CType(x.IntColumn, Int32?))).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.Avg(x.IntColumn), Int32?)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Avg(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result6)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlAvgDistinct()
      Dim items = CreateItems()

      items(0).IntColumn = 10
      items(0).IntColumnNull = 10
      items(0).BitColumn = True

      items(1).IntColumn = 20
      items(1).IntColumnNull = 20
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 10
      items(3).IntColumnNull = 10
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.AvgDistinct(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(10, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.AvgDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(15), result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.AvgDistinct(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.AvgDistinct(CType(x.IntColumn, Int32?))).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.AvgDistinct(x.IntColumn), Int32?)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.AvgDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result6)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlStdev()
      Dim items = CreateItems()

      items(0).IntColumn = 10
      items(0).IntColumnNull = 10
      items(0).BitColumn = True

      items(1).IntColumn = 20
      items(1).IntColumnNull = 20
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 40
      items(3).IntColumnNull = 40
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Stdev(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(16.7332005306815R, result1, 0.00001R)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Stdev(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(15.2752523165195R, result2, 0.00001R)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Stdev(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0R, result3, 0.00001R)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.Stdev(x.IntColumn), Double?)).
                         FirstOrDefault()

        Assert.AreEqual(New Double?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.Stdev(x.IntColumnNull), Double?)).
                         FirstOrDefault()

        Assert.AreEqual(New Double?(), result5)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlStdevDistinct()
      Dim items = CreateItems()

      items(0).IntColumn = 10
      items(0).IntColumnNull = 10
      items(0).BitColumn = True

      items(1).IntColumn = 20
      items(1).IntColumnNull = 20
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 10
      items(3).IntColumnNull = 10
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.StdevDistinct(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(10.0R, result1, 0.00001R)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.StdevDistinct(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(7.07106781186548R, result2, 0.00001R)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.StdevDistinct(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0R, result3, 0.00001R)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.StdevDistinct(x.IntColumn), Double?)).
                         FirstOrDefault()

        Assert.AreEqual(New Double?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.StdevDistinct(x.IntColumnNull), Double?)).
                         FirstOrDefault()

        Assert.AreEqual(New Double?(), result5)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlMin()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Min(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Min(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(1), result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Min(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Min(CType(x.IntColumn, Int32?))).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.Min(x.IntColumn), Int32?)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Min(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result6)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingSqlMax()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 1
      items(0).BitColumn = True

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2
      items(1).BitColumn = True

      items(2).IntColumn = 0
      items(2).IntColumnNull = Nothing
      items(2).BitColumn = True

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4
      items(3).BitColumn = True

      items(4).IntColumn = 0
      items(4).IntColumnNull = Nothing
      items(4).BitColumn = True

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Max(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(4, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) Sql.Aggregate.Max(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(4), result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Max(x.IntColumn)).
                         FirstOrDefault()

        Assert.AreEqual(0, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Max(CType(x.IntColumn, Int32?))).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) CType(Sql.Aggregate.Max(x.IntColumn), Int32?)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) x.BitColumn = False).
                         Select(Function(x) Sql.Aggregate.Max(x.IntColumnNull)).
                         FirstOrDefault()

        Assert.AreEqual(New Int32?(), result6)
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
