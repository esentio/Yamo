Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithGroupByTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "ger"

    <TestMethod()>
    Public Overridable Sub SelectWithGroupByOneColumn()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(1).Nvarchar50Column = "Ipsum"
      items(2).Nvarchar50Column = "Lorem"
      items(3).Nvarchar50Column = "Ipsum"
      items(4).Nvarchar50Column = "Lorem"

      InsertItems(items)

      Using db = CreateDbContext()
        ' use property for grouping
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) x.Nvarchar50Column).
                         Select(Function(x) x.Nvarchar50Column).
                         ToList()

        CollectionAssert.AreEquivalent({"Lorem", "Ipsum"}, result1)

        ' use anonymous type with single property for grouping
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) New With {x.Nvarchar50Column}).
                         Select(Function(x) x.Nvarchar50Column).
                         ToList()

        CollectionAssert.AreEquivalent({"Lorem", "Ipsum"}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithGroupByMultipleColumns()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 42

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 6

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 42

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 6

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 6

      InsertItems(items)

      Using db = CreateDbContext()
        ' use anonymous type for grouping
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 42), ("Ipsum", 6), ("Lorem", 6)}, result1)

        ' use ValueTuple for grouping
        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 42), ("Ipsum", 6), ("Lorem", 6)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithGroupByEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Lorem")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Ipsum")
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German, "Ipsum")

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        ' use entity for grouping
        Dim result1 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         GroupBy(Function(a As Article) a).
                         Select(Function(a, l) a).
                         ToList()

        CollectionAssert.AreEquivalent({article1, article2, article3}, result1)

        ' use entity (with IJoin) for grouping
        Dim result2 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         GroupBy(Function(j) j.T1).
                         Select(Function(a, l) a).
                         ToList()

        CollectionAssert.AreEquivalent({article1, article2, article3}, result2)

        ' use anonymous type with single entity for grouping
        Dim result3 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         GroupBy(Function(j) New With {j.T1}).
                         Select(Function(a, l) a).
                         ToList()

        CollectionAssert.AreEquivalent({article1, article2, article3}, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithGroupByEntityAndColumn()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "Lorem")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "Ipsum")
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German, "Ipsum")

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        ' use anonymous type for grouping
        Dim result1 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         GroupBy(Function(j) New With {j.T1, j.T2.Description}).
                         Select(Function(a, l) (a, l.Description)).
                         ToList()

        CollectionAssert.AreEquivalent({(article1, "Lorem"), (article2, DirectCast(Nothing, String)), (article3, "Ipsum")}, result1)

        ' use ValueTuple for grouping
        Dim result2 = db.From(Of Article).
                         LeftJoin(Of Label)(Function(a, l) a.Id = l.Id).
                         GroupBy(Function(j) (j.T1, j.T2.Description)).
                         Select(Function(a, l) (a, l.Description)).
                         ToList()

        CollectionAssert.AreEquivalent({(article1, "Lorem"), (article2, DirectCast(Nothing, String)), (article3, "Ipsum")}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithGroupByAndAggregateFunctions()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 10
      items(0).Numeric10and3Column = 11.1D

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 20
      items(1).Numeric10and3Column = 22.2D

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 30
      items(2).Numeric10and3Column = 33.3D

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 40
      items(3).Numeric10and3Column = 44.4D

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 50
      items(4).Numeric10and3Column = 55.5D

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) x.Nvarchar50Column).
                         Select(Function(x) (x.Nvarchar50Column, Sql.Aggregate.Count())).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 3), ("Ipsum", 2)}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) x.Nvarchar50Column).
                         Select(Function(x) New With {x.Nvarchar50Column, Sql.Aggregate.Count()}).
                         ToList()

        Assert.AreEqual(2, result2.Count)
        Assert.IsTrue(result2.Any(Function(x) x.Nvarchar50Column = "Lorem" AndAlso x.Count = 3))
        Assert.IsTrue(result2.Any(Function(x) x.Nvarchar50Column = "Ipsum" AndAlso x.Count = 2))

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) x.Nvarchar50Column).
                         Select(Function(x) (x.Nvarchar50Column, Sql.Aggregate.Sum(x.IntColumn), Sql.Aggregate.Max(x.Numeric10and3Column))).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 90, 55.5D), ("Ipsum", 60, 44.4D)}, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) x.Nvarchar50Column).
                         Select(Function(x) New With {x.Nvarchar50Column, .Sum = Sql.Aggregate.Sum(x.IntColumn), .Max = Sql.Aggregate.Max(x.Numeric10and3Column)}).
                         ToList()

        Assert.AreEqual(2, result4.Count)
        Assert.IsTrue(result4.Any(Function(x) x.Nvarchar50Column = "Lorem" AndAlso x.Sum = 90 AndAlso x.Max = 55.5D))
        Assert.IsTrue(result4.Any(Function(x) x.Nvarchar50Column = "Ipsum" AndAlso x.Sum = 60 AndAlso x.Max = 44.4D))
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
