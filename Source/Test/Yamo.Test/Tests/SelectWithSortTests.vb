Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithSortTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectWithOrderBy()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "c")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "a")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "d")
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English, "b")

      InsertItems(label1En, label2En, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderBy(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        Assert.AreEqual("a", result(0).Description)
        Assert.AreEqual("b", result(1).Description)
        Assert.AreEqual("c", result(2).Description)
        Assert.AreEqual("d", result(3).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByDescending()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "c")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "a")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "d")
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English, "b")

      InsertItems(label1En, label2En, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderByDescending(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(4, result.Count)
        Assert.AreEqual("d", result(0).Description)
        Assert.AreEqual("c", result(1).Description)
        Assert.AreEqual("b", result(2).Description)
        Assert.AreEqual("a", result(3).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByMultipleColumns()
      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "a")
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German, "b")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "c")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "d")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "e")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "f")

      InsertItems(label1En, label1De, label2En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderBy(Function(l) l.Language).ThenBy(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(German, result(0).Language)
        Assert.AreEqual("b", result(0).Description)
        Assert.AreEqual(German, result(1).Language)
        Assert.AreEqual("d", result(1).Description)
        Assert.AreEqual(German, result(2).Language)
        Assert.AreEqual("f", result(2).Description)
        Assert.AreEqual(English, result(3).Language)
        Assert.AreEqual("a", result(3).Description)
        Assert.AreEqual(English, result(4).Language)
        Assert.AreEqual("c", result(4).Description)
        Assert.AreEqual(English, result(5).Language)
        Assert.AreEqual("e", result(5).Description)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderByDescending(Function(l) l.Language).ThenBy(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(English, result(0).Language)
        Assert.AreEqual("a", result(0).Description)
        Assert.AreEqual(English, result(1).Language)
        Assert.AreEqual("c", result(1).Description)
        Assert.AreEqual(English, result(2).Language)
        Assert.AreEqual("e", result(2).Description)
        Assert.AreEqual(German, result(3).Language)
        Assert.AreEqual("b", result(3).Description)
        Assert.AreEqual(German, result(4).Language)
        Assert.AreEqual("d", result(4).Description)
        Assert.AreEqual(German, result(5).Language)
        Assert.AreEqual("f", result(5).Description)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderBy(Function(l) l.Language).ThenByDescending(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(German, result(0).Language)
        Assert.AreEqual("f", result(0).Description)
        Assert.AreEqual(German, result(1).Language)
        Assert.AreEqual("d", result(1).Description)
        Assert.AreEqual(German, result(2).Language)
        Assert.AreEqual("b", result(2).Description)
        Assert.AreEqual(English, result(3).Language)
        Assert.AreEqual("e", result(3).Description)
        Assert.AreEqual(English, result(4).Language)
        Assert.AreEqual("c", result(4).Description)
        Assert.AreEqual(English, result(5).Language)
        Assert.AreEqual("a", result(5).Description)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Label).OrderByDescending(Function(l) l.Language).ThenByDescending(Function(l) l.Description).SelectAll().ToList()
        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(English, result(0).Language)
        Assert.AreEqual("e", result(0).Description)
        Assert.AreEqual(English, result(1).Language)
        Assert.AreEqual("c", result(1).Description)
        Assert.AreEqual(English, result(2).Language)
        Assert.AreEqual("a", result(2).Description)
        Assert.AreEqual(German, result(3).Language)
        Assert.AreEqual("f", result(3).Description)
        Assert.AreEqual(German, result(4).Language)
        Assert.AreEqual("d", result(4).Description)
        Assert.AreEqual(German, result(5).Language)
        Assert.AreEqual("b", result(5).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByColumnsFromMultipleTables()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 30)

      Dim article2 = Me.ModelFactory.CreateArticle(2, 10)

      Dim article3 = Me.ModelFactory.CreateArticle(3, 20)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "a")
      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German, "b")
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English, "c")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "d")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "e")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "f")

      InsertItems(article1, article2, article3, label1En, label1De, label2En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(a) a.Price).
                        ThenBy(Function(l) l.Description).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(10D, result(0).Price)
        Assert.AreEqual("c", result(0).Label.Description)
        Assert.AreEqual(10D, result(1).Price)
        Assert.AreEqual("d", result(1).Label.Description)
        Assert.AreEqual(20D, result(2).Price)
        Assert.AreEqual("e", result(2).Label.Description)
        Assert.AreEqual(20D, result(3).Price)
        Assert.AreEqual("f", result(3).Label.Description)
        Assert.AreEqual(30D, result(4).Price)
        Assert.AreEqual("a", result(4).Label.Description)
        Assert.AreEqual(30D, result(5).Price)
        Assert.AreEqual("b", result(5).Label.Description)
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(j) j.T1.Price).
                        ThenBy(Function(j) j.T2.Description).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(10D, result(0).Price)
        Assert.AreEqual("c", result(0).Label.Description)
        Assert.AreEqual(10D, result(1).Price)
        Assert.AreEqual("d", result(1).Label.Description)
        Assert.AreEqual(20D, result(2).Price)
        Assert.AreEqual("e", result(2).Label.Description)
        Assert.AreEqual(20D, result(3).Price)
        Assert.AreEqual("f", result(3).Label.Description)
        Assert.AreEqual(30D, result(4).Price)
        Assert.AreEqual("a", result(4).Label.Description)
        Assert.AreEqual(30D, result(5).Price)
        Assert.AreEqual("b", result(5).Label.Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByFormattableSqlString()
      Dim label1En = Me.ModelFactory.CreateLabel("aaa", 1, English, "a")
      Dim label2En = Me.ModelFactory.CreateLabel("aaa", 2, English, "b")
      Dim label3En = Me.ModelFactory.CreateLabel("aaa", 3, English, "c")
      Dim label4En = Me.ModelFactory.CreateLabel("bbb", 4, English, "a")
      Dim label5En = Me.ModelFactory.CreateLabel("bbb", 5, English, "b")
      Dim label6En = Me.ModelFactory.CreateLabel("bbb", 6, English, "c")

      InsertItems(label1En, label2En, label3En, label4En, label5En, label6En)

      Using db = CreateDbContext()
        ' NOTE: DirectCast is needed (VB.NET compiler bug/feature?)
        Dim result = db.From(Of Label).
                        OrderBy(Function(x) DirectCast($"{x.TableId} DESC", FormattableString)).
                        ThenBy(Function(x) DirectCast($"{x.Id} * -1", FormattableString)).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(label6En, result(0))
        Assert.AreEqual(label5En, result(1))
        Assert.AreEqual(label4En, result(2))
        Assert.AreEqual(label3En, result(3))
        Assert.AreEqual(label2En, result(4))
        Assert.AreEqual(label1En, result(5))
      End Using

      Using db = CreateDbContext()
        ' NOTE: DirectCast is needed (VB.NET compiler bug/feature?)
        Dim result = db.From(Of Label).
                        OrderByDescending(Function(x) DirectCast($"{x.TableId}", FormattableString)).
                        ThenByDescending(Function(x) DirectCast($"{x.Id} * -1", FormattableString)).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(label4En, result(0))
        Assert.AreEqual(label5En, result(1))
        Assert.AreEqual(label6En, result(2))
        Assert.AreEqual(label1En, result(3))
        Assert.AreEqual(label2En, result(4))
        Assert.AreEqual(label3En, result(5))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByRawSqlString()
      Dim label1En = Me.ModelFactory.CreateLabel("aaa", 1, English, "a")
      Dim label2En = Me.ModelFactory.CreateLabel("aaa", 2, English, "b")
      Dim label3En = Me.ModelFactory.CreateLabel("aaa", 3, English, "c")
      Dim label4En = Me.ModelFactory.CreateLabel("bbb", 4, English, "a")
      Dim label5En = Me.ModelFactory.CreateLabel("bbb", 5, English, "b")
      Dim label6En = Me.ModelFactory.CreateLabel("bbb", 6, English, "c")

      InsertItems(label1En, label2En, label3En, label4En, label5En, label6En)

      Using db = CreateDbContext()
        ' NOTE: DirectCast is needed (VB.NET compiler bug/feature?)
        Dim result = db.From(Of Label).
                        OrderBy("TableId DESC").
                        ThenBy("Id * -1").
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(label6En, result(0))
        Assert.AreEqual(label5En, result(1))
        Assert.AreEqual(label4En, result(2))
        Assert.AreEqual(label3En, result(3))
        Assert.AreEqual(label2En, result(4))
        Assert.AreEqual(label1En, result(5))
      End Using

      Using db = CreateDbContext()
        ' NOTE: DirectCast is needed (VB.NET compiler bug/feature?)
        Dim result = db.From(Of Label).
                        OrderByDescending("TableId").
                        ThenByDescending("Id * -1").
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(label4En, result(0))
        Assert.AreEqual(label5En, result(1))
        Assert.AreEqual(label6En, result(2))
        Assert.AreEqual(label1En, result(3))
        Assert.AreEqual(label2En, result(4))
        Assert.AreEqual(label3En, result(5))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithOrderByRawSqlStringWithParameters()
      Dim label1En = Me.ModelFactory.CreateLabel("aaa", 1, English, "a")
      Dim label2En = Me.ModelFactory.CreateLabel("aaa", 2, English, "b")
      Dim label3En = Me.ModelFactory.CreateLabel("aaa", 3, English, "c")
      Dim label4En = Me.ModelFactory.CreateLabel("bbb", 4, English, "a")
      Dim label5En = Me.ModelFactory.CreateLabel("bbb", 5, English, "b")
      Dim label6En = Me.ModelFactory.CreateLabel("bbb", 6, English, "c")

      InsertItems(label1En, label2En, label3En, label4En, label5En, label6En)

      Using db = CreateDbContext()
        Dim tableIdColumn = db.Model.GetEntity(GetType(Label)).GetProperty(NameOf(Label.TableId)).ColumnName
        Dim idColumn = db.Model.GetEntity(GetType(Label)).GetProperty(NameOf(Label.Id)).ColumnName

        Dim result = db.From(Of Label).
                        OrderBy("{0} DESC", RawSqlString.Create(tableIdColumn)).
                        ThenBy("{0} * {1}", RawSqlString.Create(idColumn), -1).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(label6En, result(0))
        Assert.AreEqual(label5En, result(1))
        Assert.AreEqual(label4En, result(2))
        Assert.AreEqual(label3En, result(3))
        Assert.AreEqual(label2En, result(4))
        Assert.AreEqual(label1En, result(5))
      End Using

      Using db = CreateDbContext()
        Dim tableIdColumn = db.Model.GetEntity(GetType(Label)).GetProperty(NameOf(Label.TableId)).ColumnName
        Dim idColumn = db.Model.GetEntity(GetType(Label)).GetProperty(NameOf(Label.Id)).ColumnName

        Dim result = db.From(Of Label).
                        OrderByDescending("{0}", RawSqlString.Create(tableIdColumn)).
                        ThenByDescending("{0} * {1}", RawSqlString.Create(idColumn), -1).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.AreEqual(label4En, result(0))
        Assert.AreEqual(label5En, result(1))
        Assert.AreEqual(label6En, result(2))
        Assert.AreEqual(label1En, result(3))
        Assert.AreEqual(label2En, result(4))
        Assert.AreEqual(label3En, result(5))
      End Using
    End Sub

  End Class
End Namespace
