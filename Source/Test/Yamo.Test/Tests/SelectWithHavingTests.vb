﻿Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithHavingTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectWithHavingCondition()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 42

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 6

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 6

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 6

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 9

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                        Having(Function(x) x.Nvarchar50Column = "Lorem").
                        Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                        ToList()

        CollectionAssert.AreEquivalent({("Lorem", 42), ("Lorem", 6), ("Lorem", 9)}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                         Having(Function(x) x.Nvarchar50Column = "Lorem" AndAlso x.IntColumn < 42).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 6), ("Lorem", 9)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithMultipleHavingConditions()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 42

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 6

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 6

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 6

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 9

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                         Having(Function(x) x.Nvarchar50Column = "Lorem").
                         And(Function(x) x.IntColumn < 42).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 6), ("Lorem", 9)}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                         Having().
                         And(Function(x) x.Nvarchar50Column = "Lorem").
                         And(Function(x) x.IntColumn < 42).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEquivalent({("Lorem", 6), ("Lorem", 9)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithFormattableSqlStringHavingCondition()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 42

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 6

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 6

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 6

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 9

      InsertItems(items)

      Using db = CreateDbContext()
        Dim v1 = "Lorem"
        Dim v2 = 42

        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                        Having(Function(x) DirectCast($"Nvarchar50Column = {v1} AND IntColumn < {v2}", FormattableString)).
                        Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                        ToList()

        CollectionAssert.AreEquivalent({("Lorem", 6), ("Lorem", 9)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithRawSqlStringHavingCondition()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 42

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 6

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 6

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 6

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 9

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                        Having("Nvarchar50Column = 'Lorem' AND IntColumn < 42").
                        Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                        ToList()

        CollectionAssert.AreEquivalent({("Lorem", 6), ("Lorem", 9)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithRawSqlStringHavingConditionWithParameters()
      Dim items = CreateItems()

      items(0).Nvarchar50Column = "Lorem"
      items(0).IntColumn = 42

      items(1).Nvarchar50Column = "Ipsum"
      items(1).IntColumn = 6

      items(2).Nvarchar50Column = "Lorem"
      items(2).IntColumn = 6

      items(3).Nvarchar50Column = "Ipsum"
      items(3).IntColumn = 6

      items(4).Nvarchar50Column = "Lorem"
      items(4).IntColumn = 9

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        GroupBy(Function(x) New With {x.Nvarchar50Column, x.IntColumn}).
                        Having("Nvarchar50Column = {0} AND IntColumn < {1}", "Lorem", 42).
                        Select(Function(x) (x.Nvarchar50Column, x.IntColumn)).
                        ToList()

        CollectionAssert.AreEquivalent({("Lorem", 6), ("Lorem", 9)}, result)
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
