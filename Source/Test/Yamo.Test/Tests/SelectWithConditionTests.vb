Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithConditionTests
    Inherits BaseIntegrationTests

    ' TODO: SIP - implement tests for:
    ' - Join (also with usage of affected entity in select, where, etc.)
    ' - As
    ' × Where
    ' × And (after where)
    ' × GroupBy
    ' × Having
    ' × And (after having)
    ' × OrderBy
    ' × OrderByDescending
    ' × ThenBy
    ' × ThenByDescending
    ' × Limit
    ' × Custom select
    ' × Exclude column
    ' × Exclude table
    ' - Chained expressions
    ' - Nested ifs

    Protected Const English As String = "en"

    Protected Const German As String = "ger"

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalWhere()
      Dim items = CreateItems()

      items(0).IntColumn = 0
      items(1).IntColumn = 1
      items(2).IntColumn = 2
      items(3).IntColumn = 3
      items(4).IntColumn = 4

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.Where(Function(x) 2 < x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.Where(Function(x) 2 < x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp) exp.Where(Function(x) 2 < x.IntColumn),
                           Function(exp) exp.Where(Function(x) x.IntColumn < 2)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp) exp.Where(Function(x) 2 < x.IntColumn),
                           Function(exp) exp.Where(Function(x) x.IntColumn < 2)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalAndAfterWhere()
      Dim items = CreateItems()

      items(0).IntColumn = 0
      items(1).IntColumn = 1
      items(2).IntColumn = 2
      items(3).IntColumn = 3
      items(4).IntColumn = 4

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) x.IntColumn < 4).
                        If(True, Function(exp) exp.And(Function(x) 2 < x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3)}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) x.IntColumn < 4).
                        If(False, Function(exp) exp.And(Function(x) 2 < x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3)}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) x.IntColumn < 4).
                        If(True,
                           Function(exp) exp.And(Function(x) 2 < x.IntColumn),
                           Function(exp) exp.And(Function(x) x.IntColumn < 2)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3)}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        Where(Function(x) 0 < x.IntColumn).
                        If(False,
                           Function(exp) exp.And(Function(x) 2 < x.IntColumn),
                           Function(exp) exp.And(Function(x) x.IntColumn < 2)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(1)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalGroupBy()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "a"
      items(0).Nvarchar50ColumnNull = "x"

      items(1).Nvarchar50Column = "b"
      items(1).Nvarchar50ColumnNull = "z"

      items(2).Nvarchar50Column = "a"
      items(2).Nvarchar50ColumnNull = "y"

      items(3).Nvarchar50Column = "a"
      items(3).Nvarchar50ColumnNull = "x"

      items(4).Nvarchar50Column = "b"
      items(4).Nvarchar50ColumnNull = "x"

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.GroupBy(Function(x) x.Nvarchar50Column)).
                        Select(Function(x) x.Nvarchar50Column).
                        ToList()

        CollectionAssert.AreEquivalent({"a", "b"}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Try
          Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.GroupBy(Function(x) x.Nvarchar50Column)).
                        Select(Function(x) x.Nvarchar50Column).
                        ToList()

          Assert.Fail()
        Catch ex As NotSupportedException
        Catch ex As Exception
          Assert.Fail(ex.Message)
        End Try
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp) exp.GroupBy(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.GroupBy(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        Select(Function(x) Sql.Aggregate.Count()).
                        ToList()

        CollectionAssert.AreEquivalent({2, 3}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp) exp.GroupBy(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.GroupBy(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        Select(Function(x) Sql.Aggregate.Count()).
                        ToList()

        CollectionAssert.AreEquivalent({1, 1, 3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalHaving()
      Dim items = CreateItems(10)

      items(0).IntColumn = 10
      items(1).IntColumn = 20
      items(2).IntColumn = 20
      items(3).IntColumn = 30
      items(4).IntColumn = 30
      items(5).IntColumn = 30
      items(6).IntColumn = 40
      items(7).IntColumn = 40
      items(8).IntColumn = 40
      items(9).IntColumn = 40

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        If(True, Function(exp) exp.Having(Function(x) 2 < Sql.Aggregate.Count(x.IntColumn))).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({30, 40}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        If(False, Function(exp) exp.Having(Function(x) 2 < Sql.Aggregate.Count(x.IntColumn))).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({10, 20, 30, 40}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        If(True,
                           Function(exp) exp.Having(Function(x) 2 < Sql.Aggregate.Count(x.IntColumn)),
                           Function(exp) exp.Having(Function(x) 3 < Sql.Aggregate.Count(x.IntColumn))
                        ).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({30, 40}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        If(False,
                           Function(exp) exp.Having(Function(x) 2 < Sql.Aggregate.Count(x.IntColumn)),
                           Function(exp) exp.Having(Function(x) 3 < Sql.Aggregate.Count(x.IntColumn))
                        ).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({40}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalAndAfterHaving()
      Dim items = CreateItems(10)

      items(0).IntColumn = 10
      items(1).IntColumn = 20
      items(2).IntColumn = 20
      items(3).IntColumn = 30
      items(4).IntColumn = 30
      items(5).IntColumn = 30
      items(6).IntColumn = 40
      items(7).IntColumn = 40
      items(8).IntColumn = 40
      items(9).IntColumn = 40

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        Having(Function(x) Sql.Aggregate.Count(x.IntColumn) < 4).
                        If(True, Function(exp) exp.And(Function(x) 1 < Sql.Aggregate.Count(x.IntColumn))).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({20, 30}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        Having(Function(x) Sql.Aggregate.Count(x.IntColumn) < 4).
                        If(False, Function(exp) exp.And(Function(x) 1 < Sql.Aggregate.Count(x.IntColumn))).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({10, 20, 30}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        Having(Function(x) Sql.Aggregate.Count(x.IntColumn) < 4).
                        If(True,
                           Function(exp) exp.And(Function(x) 1 < Sql.Aggregate.Count(x.IntColumn)),
                           Function(exp) exp.And(Function(x) 2 < Sql.Aggregate.Count(x.IntColumn))
                        ).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({20, 30}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) x.IntColumn).
                        Having(Function(x) Sql.Aggregate.Count(x.IntColumn) < 4).
                        If(False,
                           Function(exp) exp.And(Function(x) 1 < Sql.Aggregate.Count(x.IntColumn)),
                           Function(exp) exp.And(Function(x) 2 < Sql.Aggregate.Count(x.IntColumn))
                        ).
                        Select(Function(x) x.IntColumn).
                        ToList()

        CollectionAssert.AreEquivalent({30}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalOrderBy()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "c"
      items(1).Nvarchar50Column = "b"
      items(2).Nvarchar50Column = "a"
      items(3).Nvarchar50Column = "d"
      items(4).Nvarchar50Column = "e"

      Dim itemA = items(2)
      Dim itemB = items(1)
      Dim itemC = items(0)
      Dim itemD = items(3)
      Dim itemE = items(4)

      items(0).IntColumn = 2
      items(1).IntColumn = 1
      items(2).IntColumn = 4
      items(3).IntColumn = 3
      items(4).IntColumn = 5

      Dim item1 = items(1)
      Dim item2 = items(0)
      Dim item3 = items(3)
      Dim item4 = items(2)
      Dim item5 = items(4)

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.OrderBy(Function(x) x.Nvarchar50Column)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA, itemB, itemC, itemD, itemE}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.OrderBy(Function(x) x.Nvarchar50Column)).
                        ThenBy(Function(x) x.IntColumn).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA, itemB, itemC, itemD, itemE}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.OrderBy(Function(x) x.Nvarchar50Column)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.OrderBy(Function(x) x.Nvarchar50Column)).
                        ThenBy(Function(x) x.IntColumn).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({item1, item2, item3, item4, item5}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp) exp.OrderBy(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.OrderBy(Function(x) x.IntColumn)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA, itemB, itemC, itemD, itemE}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp) exp.OrderBy(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.OrderBy(Function(x) x.IntColumn)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({item1, item2, item3, item4, item5}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalOrderByDescending()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "c"
      items(1).Nvarchar50Column = "b"
      items(2).Nvarchar50Column = "a"
      items(3).Nvarchar50Column = "d"
      items(4).Nvarchar50Column = "e"

      Dim itemA = items(2)
      Dim itemB = items(1)
      Dim itemC = items(0)
      Dim itemD = items(3)
      Dim itemE = items(4)

      items(0).IntColumn = 2
      items(1).IntColumn = 1
      items(2).IntColumn = 4
      items(3).IntColumn = 3
      items(4).IntColumn = 5

      Dim item1 = items(1)
      Dim item2 = items(0)
      Dim item3 = items(3)
      Dim item4 = items(2)
      Dim item5 = items(4)

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.OrderByDescending(Function(x) x.Nvarchar50Column)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemE, itemD, itemC, itemB, itemA}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.OrderByDescending(Function(x) x.Nvarchar50Column)).
                        ThenBy(Function(x) x.IntColumn).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemE, itemD, itemC, itemB, itemA}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.OrderByDescending(Function(x) x.Nvarchar50Column)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                          If(False, Function(exp) exp.OrderByDescending(Function(x) x.Nvarchar50Column)).
                          ThenBy(Function(x) x.IntColumn).
                          SelectAll().ToList()

        CollectionAssert.AreEqual({item1, item2, item3, item4, item5}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp) exp.OrderByDescending(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.OrderByDescending(Function(x) x.IntColumn)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemE, itemD, itemC, itemB, itemA}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp) exp.OrderByDescending(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.OrderByDescending(Function(x) x.IntColumn)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({item5, item4, item3, item2, item1}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalThenBy()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "b"
      items(0).IntColumn = 1
      items(0).Nvarchar50ColumnNull = "y"

      items(1).Nvarchar50Column = "b"
      items(1).IntColumn = 2
      items(1).Nvarchar50ColumnNull = "x"

      items(2).Nvarchar50Column = "a"
      items(2).IntColumn = 1
      items(2).Nvarchar50ColumnNull = "x"

      items(3).Nvarchar50Column = "c"
      items(3).IntColumn = 2
      items(3).Nvarchar50ColumnNull = "x"

      items(4).Nvarchar50Column = "c"
      items(4).IntColumn = 1
      items(4).Nvarchar50ColumnNull = "y"

      Dim itemA1 = items(2)
      Dim itemB1 = items(0)
      Dim itemB2 = items(1)
      Dim itemC1 = items(4)
      Dim itemC2 = items(3)

      Dim itemAX = items(2)
      Dim itemBX = items(1)
      Dim itemBY = items(0)
      Dim itemCX = items(3)
      Dim itemCY = items(4)

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(True, Function(exp) exp.ThenBy(Function(x) x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA1, itemB1, itemB2, itemC1, itemC2}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(False, Function(exp) exp.ThenBy(Function(x) x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
        Assert.AreEqual("a", result(0).Nvarchar50Column)
        Assert.AreEqual("b", result(1).Nvarchar50Column)
        Assert.AreEqual("b", result(2).Nvarchar50Column)
        Assert.AreEqual("c", result(3).Nvarchar50Column)
        Assert.AreEqual("c", result(4).Nvarchar50Column)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(True,
                           Function(exp) exp.ThenBy(Function(x) x.IntColumn),
                           Function(exp) exp.ThenBy(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA1, itemB1, itemB2, itemC1, itemC2}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(False,
                           Function(exp) exp.ThenBy(Function(x) x.IntColumn),
                           Function(exp) exp.ThenBy(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemAX, itemBX, itemBY, itemCX, itemCY}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalThenByDescending()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "b"
      items(0).IntColumn = 1
      items(0).Nvarchar50ColumnNull = "y"

      items(1).Nvarchar50Column = "b"
      items(1).IntColumn = 2
      items(1).Nvarchar50ColumnNull = "x"

      items(2).Nvarchar50Column = "a"
      items(2).IntColumn = 1
      items(2).Nvarchar50ColumnNull = "x"

      items(3).Nvarchar50Column = "c"
      items(3).IntColumn = 2
      items(3).Nvarchar50ColumnNull = "x"

      items(4).Nvarchar50Column = "c"
      items(4).IntColumn = 1
      items(4).Nvarchar50ColumnNull = "y"

      Dim itemA1 = items(2)
      Dim itemB1 = items(0)
      Dim itemB2 = items(1)
      Dim itemC1 = items(4)
      Dim itemC2 = items(3)

      Dim itemAX = items(2)
      Dim itemBX = items(1)
      Dim itemBY = items(0)
      Dim itemCX = items(3)
      Dim itemCY = items(4)

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(True, Function(exp) exp.ThenByDescending(Function(x) x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA1, itemB2, itemB1, itemC2, itemC1}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(False, Function(exp) exp.ThenByDescending(Function(x) x.IntColumn)).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
        Assert.AreEqual("a", result(0).Nvarchar50Column)
        Assert.AreEqual("b", result(1).Nvarchar50Column)
        Assert.AreEqual("b", result(2).Nvarchar50Column)
        Assert.AreEqual("c", result(3).Nvarchar50Column)
        Assert.AreEqual("c", result(4).Nvarchar50Column)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(True,
                           Function(exp) exp.ThenByDescending(Function(x) x.IntColumn),
                           Function(exp) exp.ThenByDescending(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemA1, itemB2, itemB1, itemC2, itemC1}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.Nvarchar50Column).
                        If(False,
                           Function(exp) exp.ThenByDescending(Function(x) x.IntColumn),
                           Function(exp) exp.ThenByDescending(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({itemAX, itemBY, itemBX, itemCY, itemCX}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalLimit()
      Dim items = CreateItems()

      items(0).IntColumn = 0
      items(1).IntColumn = 1
      items(2).IntColumn = 2
      items(3).IntColumn = 3
      items(4).IntColumn = 4

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True, Function(exp) exp.Limit(2)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True, Function(exp) exp.Limit(1, 2)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(1), items(2)}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(False, Function(exp) exp.Limit(2)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(False, Function(exp) exp.Limit(1, 2)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True,
                           Function(exp) exp.Limit(2),
                           Function(exp) exp.Limit(3)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(0), items(1)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True,
                           Function(exp) exp.Limit(1, 2),
                           Function(exp) exp.Limit(2, 3)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(1), items(2)}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(False,
                           Function(exp) exp.Limit(2),
                           Function(exp) exp.Limit(3)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(0), items(1), items(2)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(False,
                           Function(exp) exp.Limit(1, 2),
                           Function(exp) exp.Limit(2, 3)
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({items(2), items(3), items(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalCustomSelect()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "a"
      items(0).Nvarchar50ColumnNull = "k"
      items(0).IntColumn = 1

      items(1).Nvarchar50Column = "b"
      items(1).Nvarchar50ColumnNull = "l"
      items(1).IntColumn = 2

      items(2).Nvarchar50Column = "c"
      items(2).Nvarchar50ColumnNull = "m"
      items(2).IntColumn = 3

      items(3).Nvarchar50Column = "d"
      items(3).Nvarchar50ColumnNull = "n"
      items(3).IntColumn = 4

      items(4).Nvarchar50Column = "e"
      items(4).Nvarchar50ColumnNull = "o"
      items(4).IntColumn = 5

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True, Function(exp) exp.Select(Function(x) x.Nvarchar50Column)).
                        ToList()

        CollectionAssert.AreEqual({"a", "b", "c", "d", "e"}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Try
          Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(False, Function(exp) exp.Select(Function(x) x.Nvarchar50Column)).
                        ToList()

          Assert.Fail()
        Catch ex As NotSupportedException
        Catch ex As Exception
          Assert.Fail(ex.Message)
        End Try
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True,
                           Function(exp) exp.Select(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.Select(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        ToList()

        CollectionAssert.AreEqual({"a", "b", "c", "d", "e"}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(False,
                           Function(exp) exp.Select(Function(x) x.Nvarchar50Column),
                           Function(exp) exp.Select(Function(x) x.Nvarchar50ColumnNull)
                        ).
                        ToList()

        CollectionAssert.AreEqual({"k", "l", "m", "n", "o"}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalExcludeColumn()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "a"
      items(0).Nvarchar50ColumnNull = "k"
      items(0).IntColumn = 1
      items(0).IntColumnNull = 10

      items(1).Nvarchar50Column = "b"
      items(1).Nvarchar50ColumnNull = "l"
      items(1).IntColumn = 2
      items(1).IntColumnNull = 20

      items(2).Nvarchar50Column = "c"
      items(2).Nvarchar50ColumnNull = "m"
      items(2).IntColumn = 3
      items(2).IntColumnNull = 30

      items(3).Nvarchar50Column = "d"
      items(3).Nvarchar50ColumnNull = "n"
      items(3).IntColumn = 4
      items(3).IntColumnNull = 40

      items(4).Nvarchar50Column = "e"
      items(4).Nvarchar50ColumnNull = "o"
      items(4).IntColumn = 5
      items(4).IntColumnNull = 50

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        SelectAll().
                        If(True, Function(exp) exp.Exclude(Function(x) x.Nvarchar50ColumnNull)).
                        ToList()

        CollectionAssert.AreEqual({"a", "b", "c", "d", "e"}, result.Select(Function(x) x.Nvarchar50Column).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Nvarchar50ColumnNull Is Nothing))
        CollectionAssert.AreEqual({1, 2, 3, 4, 5}, result.Select(Function(x) x.IntColumn).ToArray())
        CollectionAssert.AreEqual({10, 20, 30, 40, 50}, result.Select(Function(x) x.IntColumnNull.Value).ToArray())
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        SelectAll().
                        If(False, Function(exp) exp.Exclude(Function(x) x.Nvarchar50ColumnNull)).
                        ToList()

        CollectionAssert.AreEqual(items, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        SelectAll().
                        If(True,
                           Function(exp) exp.Exclude(Function(x) x.Nvarchar50ColumnNull),
                           Function(exp) exp.Exclude(Function(x) x.IntColumnNull)
                        ).
                        ToList()

        CollectionAssert.AreEqual({"a", "b", "c", "d", "e"}, result.Select(Function(x) x.Nvarchar50Column).ToArray())
        Assert.IsTrue(result.All(Function(x) x.Nvarchar50ColumnNull Is Nothing))
        CollectionAssert.AreEqual({1, 2, 3, 4, 5}, result.Select(Function(x) x.IntColumn).ToArray())
        CollectionAssert.AreEqual({10, 20, 30, 40, 50}, result.Select(Function(x) x.IntColumnNull.Value).ToArray())
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        SelectAll().
                        If(False,
                           Function(exp) exp.Exclude(Function(x) x.Nvarchar50ColumnNull),
                           Function(exp) exp.Exclude(Function(x) x.IntColumnNull)
                        ).
                        ToList()

        CollectionAssert.AreEqual({"a", "b", "c", "d", "e"}, result.Select(Function(x) x.Nvarchar50Column).ToArray())
        CollectionAssert.AreEqual({"k", "l", "m", "n", "o"}, result.Select(Function(x) x.Nvarchar50ColumnNull).ToArray())
        CollectionAssert.AreEqual({1, 2, 3, 4, 5}, result.Select(Function(x) x.IntColumn).ToArray())
        Assert.IsTrue(result.All(Function(x) Not x.IntColumnNull.HasValue))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalExcludeTable()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1 = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2 = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3 = Me.ModelFactory.CreateLabel("", 3, English)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(11, 1)
      Dim article1Part2 = Me.ModelFactory.CreateArticlePart(12, 1)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(13, 2)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(14, 3)
      Dim article3Part2 = Me.ModelFactory.CreateArticlePart(15, 3)

      InsertItems(article1, article2, article3, label1, label2, label3, article1Part1, article1Part2, article2Part1, article3Part1, article3Part2)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        If(True, Function(exp) exp.ExcludeT2()).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
        CollectionAssert.AreEqual({article1Part1, article1Part2}, result(0).Parts)
        CollectionAssert.AreEqual({article2Part1}, result(1).Parts)
        CollectionAssert.AreEqual({article3Part1, article3Part2}, result(2).Parts)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        If(True, Function(exp) exp.ExcludeT2()).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1, result(0).Label)
        Assert.AreEqual(label2, result(1).Label)
        Assert.AreEqual(label3, result(2).Label)
        CollectionAssert.AreEqual({article1Part1, article1Part2}, result(0).Parts)
        CollectionAssert.AreEqual({article2Part1}, result(1).Parts)
        CollectionAssert.AreEqual({article3Part1, article3Part2}, result(2).Parts)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        If(True,
                           Function(exp) exp.ExcludeT2(),
                           Function(exp) exp.ExcludeT3()
                        ).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsTrue(result.All(Function(x) x.Label Is Nothing))
        CollectionAssert.AreEqual({article1Part1, article1Part2}, result(0).Parts)
        CollectionAssert.AreEqual({article2Part1}, result(1).Parts)
        CollectionAssert.AreEqual({article3Part1, article3Part2}, result(2).Parts)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        LeftJoin(Of ArticlePart)(Function(j) j.T1.Id = j.T3.ArticleId).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        If(False,
                           Function(exp) exp.ExcludeT2(),
                           Function(exp) exp.ExcludeT3()
                        ).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1, result(0).Label)
        Assert.AreEqual(label2, result(1).Label)
        Assert.AreEqual(label3, result(2).Label)
        Assert.IsTrue(result.All(Function(x) x.Parts.Count = 0))
      End Using
    End Sub

    Protected Overridable Function CreateItems(Optional count As Int32 = 5) As List(Of ItemWithAllSupportedValues)
      Return Enumerable.Range(0, count).Select(Function(x) Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()).ToList()
    End Function

  End Class
End Namespace
