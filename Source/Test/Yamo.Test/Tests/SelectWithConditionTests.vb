Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithConditionTests
    Inherits BaseIntegrationTests

    ' TODO: SIP - implement tests for:
    ' - Join (also with usage of affected entity in select, where, etc.)
    ' - As
    ' × Where
    ' × And
    ' - GroupBy
    ' - Having
    ' × OrderBy
    ' × OrderByDescending
    ' × ThenBy
    ' × ThenByDescending
    ' × Limit
    ' × Custom select
    ' - Exclude column
    ' - Exclude table
    ' - Chained expressions
    ' - Nested ifs

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
    Public Overridable Sub SelectWithConditionalAnd()
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
