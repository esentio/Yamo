Imports System.Linq.Expressions
Imports System.Data
Imports Yamo.Test.Model

Namespace Tests

  <TestClass()>
  Public Class PredicateBuilderTests
    Inherits BaseUnitTests

    <TestMethod()>
    Public Overridable Sub BuildAndWith2Predicates()
      Dim items = CreateItems()

      Dim exp1 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 2 = 0
      Dim exp2 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 3 = 0

      Dim exp = PredicateBuilder.And(exp1, exp2)
      Dim predicate = exp.Compile()

      Dim result = items.Where(predicate).ToList()

      CollectionAssert.AreEquivalent({items(0), items(6), items(12)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildAndWithParamArray()
      Dim items = CreateItems()

      Dim exp1 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 2 = 0
      Dim exp2 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 3 = 0
      Dim exp3 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 4 = 0

      ' 0 items
      Dim exps As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))() = {}
      Dim expWith0Items = PredicateBuilder.And(exps)
      Dim predicateWith0Items = expWith0Items.Compile()

      Dim result = items.Where(predicateWith0Items).ToList()

      CollectionAssert.AreEquivalent(items, result)

      ' 1 item
      Dim expWith1Item = PredicateBuilder.And(exp3)
      Dim predicateWith1Item = expWith1Item.Compile()

      result = items.Where(predicateWith1Item).ToList()

      CollectionAssert.AreEquivalent({items(0), items(4), items(8), items(12)}, result)

      ' 3 items
      Dim expWith3Items = PredicateBuilder.And(exp1, exp2, exp3)
      Dim predicateWith3Items = expWith3Items.Compile()

      result = items.Where(predicateWith3Items).ToList()

      CollectionAssert.AreEquivalent({items(0), items(12)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildAndWithCollection()
      Dim items = CreateItems()

      Dim exp1 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 2 = 0
      Dim exp2 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 3 = 0
      Dim exp3 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 4 = 0

      Dim exps As List(Of Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)))

      ' 0 items
      exps = New List(Of Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)))()
      Dim expWith0Items = PredicateBuilder.And(exps)
      Dim predicateWith0Items = expWith0Items.Compile()

      Dim result = items.Where(predicateWith0Items).ToList()

      CollectionAssert.AreEquivalent(items, result)

      ' 1 item
      exps = {exp3}.ToList()
      Dim expWith1Item = PredicateBuilder.And(exps)
      Dim predicateWith1Item = expWith1Item.Compile()

      result = items.Where(predicateWith1Item).ToList()

      CollectionAssert.AreEquivalent({items(0), items(4), items(8), items(12)}, result)

      ' 3 items
      exps = {exp1, exp2, exp3}.ToList()
      Dim expWith3Items = PredicateBuilder.And(exps)
      Dim predicateWith3Items = expWith3Items.Compile()

      result = items.Where(predicateWith3Items).ToList()

      CollectionAssert.AreEquivalent({items(0), items(12)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildOrWith2Predicates()
      Dim items = CreateItems()

      Dim exp1 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 5 = 0
      Dim exp2 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 6 = 0

      Dim exp = PredicateBuilder.Or(exp1, exp2)

      Dim predicate = exp.Compile()

      Dim result = items.Where(predicate).ToList()

      CollectionAssert.AreEquivalent({items(0), items(5), items(6), items(10), items(12), items(15)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildOrWithParamArray()
      Dim items = CreateItems()

      Dim exp1 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 5 = 0
      Dim exp2 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 6 = 0
      Dim exp3 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 7 = 0

      ' 0 items
      Dim exps As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean))() = {}
      Dim expWith0Items = PredicateBuilder.Or(exps)
      Dim predicateWith0Items = expWith0Items.Compile()

      Dim result = items.Where(predicateWith0Items).ToList()

      CollectionAssert.AreEquivalent(items, result)

      ' 1 item
      Dim expWith1Item = PredicateBuilder.Or(exp3)
      Dim predicateWith1Item = expWith1Item.Compile()

      result = items.Where(predicateWith1Item).ToList()

      CollectionAssert.AreEquivalent({items(0), items(7), items(14)}, result)

      ' 3 items
      Dim expWith3Items = PredicateBuilder.Or(exp1, exp2, exp3)
      Dim predicateWith3Items = expWith3Items.Compile()

      result = items.Where(predicateWith3Items).ToList()

      CollectionAssert.AreEquivalent({items(0), items(5), items(6), items(7), items(10), items(12), items(14), items(15)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildOrWithCollection()
      Dim items = CreateItems()

      Dim exp1 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 5 = 0
      Dim exp2 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 6 = 0
      Dim exp3 As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) x.IntColumn Mod 7 = 0

      Dim exps As List(Of Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)))

      ' 0 items
      exps = New List(Of Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)))()
      Dim expWith0Items = PredicateBuilder.Or(exps)
      Dim predicateWith0Items = expWith0Items.Compile()

      Dim result = items.Where(predicateWith0Items).ToList()

      CollectionAssert.AreEquivalent(items, result)

      ' 1 item
      exps = {exp3}.ToList()
      Dim expWith1Item = PredicateBuilder.Or(exps)
      Dim predicateWith1Item = expWith1Item.Compile()

      result = items.Where(predicateWith1Item).ToList()

      CollectionAssert.AreEquivalent({items(0), items(7), items(14)}, result)

      ' 3 items
      exps = {exp1, exp2, exp3}.ToList()
      Dim expWith3Items = PredicateBuilder.Or(exps)
      Dim predicateWith3Items = expWith3Items.Compile()

      result = items.Where(predicateWith3Items).ToList()

      CollectionAssert.AreEquivalent({items(0), items(5), items(6), items(7), items(10), items(12), items(14), items(15)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildNot()
      Dim items = CreateItems()

      Dim originalExp As Expression(Of Func(Of ItemWithAllSupportedValues, Boolean)) = Function(x) 2 < x.IntColumn

      Dim exp = PredicateBuilder.Not(originalExp)
      Dim predicate = exp.Compile()

      Dim result = items.Where(predicate).ToList()

      CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildTrue()
      Dim items = CreateItems()

      Dim exp = PredicateBuilder.True(Of ItemWithAllSupportedValues)
      Dim predicate = exp.Compile()

      Dim result = items.Where(predicate).ToList()

      CollectionAssert.AreEquivalent(items, result)
    End Sub

    <TestMethod()>
    Public Overridable Sub BuildFalse()
      Dim items = CreateItems()

      Dim exp = PredicateBuilder.False(Of ItemWithAllSupportedValues)
      Dim predicate = exp.Compile()

      Dim result = items.Where(predicate).ToList()

      Assert.IsFalse(result.Any())
    End Sub

    Protected Overridable Function CreateItems() As List(Of ItemWithAllSupportedValues)
      Dim list = New List(Of ItemWithAllSupportedValues)

      For i = 0 To 15
        Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
        item.IntColumn = i
        list.Add(item)
      Next

      Return list
    End Function

  End Class
End Namespace
