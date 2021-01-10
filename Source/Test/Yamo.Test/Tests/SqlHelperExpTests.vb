Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SqlHelperExpTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectUsingAs()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 1

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4

      items(4).IntColumn = 5
      items(4).IntColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.As(Of Int32)(x.IntColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({1, 2, 0, 4, 0}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.As(Of Int32?)(x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(1), New Int32?(2), New Int32?(3), New Int32?(4), New Int32?(5)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingRaw()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 1

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4

      items(4).IntColumn = 5
      items(4).IntColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.IntColumn)).ColumnName
        Dim one = 1
        Dim two = 2

        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)($"{x.IntColumn} + 1 + {one}")).
                         ToList()

        CollectionAssert.AreEqual({3, 4, 5, 6, 7}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)($"{RawSqlString.Create(column)} + 1 + {one}")).
                         ToList()

        CollectionAssert.AreEqual({3, 4, 5, 6, 7}, result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)($"{New RawSqlString(column)} + 1 + {one}")).
                         ToList()

        CollectionAssert.AreEqual({3, 4, 5, 6, 7}, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32?)($"{x.IntColumnNull} + 1")).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(2), New Int32?(3), New Int32?(), New Int32?(5), New Int32?()}, result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)($"1 + 2")).
                         ToList()

        CollectionAssert.AreEqual({3, 3, 3, 3, 3}, result5)

        Dim result6 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of String)($"'foo'")).
                         ToList()

        CollectionAssert.AreEqual({"foo", "foo", "foo", "foo", "foo"}, result6)
      End Using

      Using db = CreateDbContext()
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.IntColumn)).ColumnName
        Dim one = 1
        Dim two = 2

        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)("IntColumn + 1 + {0}", one)).
                         ToList()

        CollectionAssert.AreEqual({3, 4, 5, 6, 7}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)("{0} + 1 + {1}", RawSqlString.Create(column), one)).
                         ToList()

        CollectionAssert.AreEqual({3, 4, 5, 6, 7}, result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32?)("IntColumnNull + 1")).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(2), New Int32?(3), New Int32?(), New Int32?(5), New Int32?()}, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of Int32)("1 + 2")).
                         ToList()

        CollectionAssert.AreEqual({3, 3, 3, 3, 3}, result4)

        Dim result5 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Raw(Of String)("'foo'")).
                         ToList()

        CollectionAssert.AreEqual({"foo", "foo", "foo", "foo", "foo"}, result5)
      End Using

      Using db = CreateDbContext()
        Dim column = db.Model.GetEntity(GetType(ItemWithAllSupportedValues)).GetProperty(NameOf(ItemWithAllSupportedValues.IntColumn)).ColumnName
        Dim one = 1
        Dim four = 4

        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.Exp.Raw(Of Boolean)($"IntColumn < {four}")).
                         And(Function(x) one < x.IntColumn).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) x.IntColumn).
                         ToList()

        CollectionAssert.AreEqual({2, 3}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Where(Function(x) Sql.Exp.Raw(Of Boolean)("{0} < {1}", RawSqlString.Create(column), four)).
                         And(Function(x) one < x.IntColumn).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) x.IntColumn).
                         ToList()

        CollectionAssert.AreEqual({2, 3}, result1)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingCoalesce()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 10
      items(0).SmallintColumnNull = 100

      items(1).IntColumn = 2
      items(1).IntColumnNull = 20
      items(1).SmallintColumnNull = Nothing

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing
      items(2).SmallintColumnNull = 300

      items(3).IntColumn = 4
      items(3).IntColumnNull = Nothing
      items(3).SmallintColumnNull = Nothing

      items(4).IntColumn = 5
      items(4).IntColumnNull = Nothing
      items(4).SmallintColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Coalesce(Of Int32)(x.IntColumnNull, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({10, 20, 3, 4, 5}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Coalesce(Of Int32)(x.SmallintColumnNull, x.IntColumnNull, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({100, 20, 300, 4, 5}, result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Coalesce(Of Int32)(x.SmallintColumnNull, x.IntColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({100, 20, 300, 0, 0}, result3)

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.Coalesce(Of Int32?)(x.SmallintColumnNull, x.IntColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(100), New Int32?(20), New Int32?(300), New Int32?(), New Int32?()}, result4)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingIsNull()
      ' same test as SelectUsingIfNull

      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 10
      items(0).SmallintColumnNull = 100

      items(1).IntColumn = 2
      items(1).IntColumnNull = 20
      items(1).SmallintColumnNull = Nothing

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing
      items(2).SmallintColumnNull = 300

      items(3).IntColumn = 4
      items(3).IntColumnNull = Nothing
      items(3).SmallintColumnNull = Nothing

      items(4).IntColumn = 5
      items(4).IntColumnNull = Nothing
      items(4).SmallintColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IsNull(Of Int32)(x.IntColumnNull, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({10, 20, 3, 4, 5}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IsNull(Of Int32)(x.IntColumnNull, x.SmallintColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({10, 20, 300, 0, 0}, result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IsNull(Of Int32?)(x.IntColumnNull, x.SmallintColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(10), New Int32?(20), New Int32?(300), New Int32?(), New Int32?()}, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingIfNull()
      ' same test as SelectUsingIsNull

      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 10
      items(0).SmallintColumnNull = 100

      items(1).IntColumn = 2
      items(1).IntColumnNull = 20
      items(1).SmallintColumnNull = Nothing

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing
      items(2).SmallintColumnNull = 300

      items(3).IntColumn = 4
      items(3).IntColumnNull = Nothing
      items(3).SmallintColumnNull = Nothing

      items(4).IntColumn = 5
      items(4).IntColumnNull = Nothing
      items(4).SmallintColumnNull = Nothing

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IfNull(Of Int32)(x.IntColumnNull, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({10, 20, 3, 4, 5}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IfNull(Of Int32)(x.IntColumnNull, x.SmallintColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({10, 20, 300, 0, 0}, result2)

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IfNull(Of Int32?)(x.IntColumnNull, x.SmallintColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(10), New Int32?(20), New Int32?(300), New Int32?(), New Int32?()}, result3)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingNullIf()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 10

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4

      items(4).IntColumn = 5
      items(4).IntColumnNull = 50

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.NullIf(Of Int32)(x.IntColumnNull, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({10, 0, 0, 0, 50}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.NullIf(Of Int32?)(x.IntColumnNull, x.IntColumn)).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(10), New Int32?(), New Int32?(), New Int32?(), New Int32?(50)}, result2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingIIf()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(0).IntColumnNull = 10

      items(1).IntColumn = 2
      items(1).IntColumnNull = 2

      items(2).IntColumn = 3
      items(2).IntColumnNull = Nothing

      items(3).IntColumn = 4
      items(3).IntColumnNull = 4

      items(4).IntColumn = 5
      items(4).IntColumnNull = 50

      InsertItems(items)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IIf(Of Int32)(x.IntColumnNull.HasValue, x.IntColumnNull, x.IntColumn + 1)).
                         ToList()

        CollectionAssert.AreEqual({10, 2, 4, 4, 50}, result1)

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         OrderBy(Function(x) x.IntColumn).
                         Select(Function(x) Sql.Exp.IIf(Of Int32?)(x.IntColumn = x.IntColumnNull.Value, x.IntColumn + 1, x.IntColumnNull)).
                         ToList()

        CollectionAssert.AreEqual({New Int32?(10), New Int32?(3), New Int32?(), New Int32?(5), New Int32?(50)}, result2)
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
