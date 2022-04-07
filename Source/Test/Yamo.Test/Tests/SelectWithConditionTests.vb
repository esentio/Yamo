Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithConditionTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    Protected Const French As String = "fr"

    Protected Const Italian As String = "it"

    Protected Const Spanish As String = "es"

    Protected Const Norwegian As String = "no"

    Protected Const LabelArchiveTableName As String = "LabelArchive"

    Protected MustOverride Function GetTableHints1() As String

    Protected MustOverride Function GetTableHints2() As String

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalWithHints()
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
                        If(True, Function(exp) exp.WithHints(GetTableHints1())).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [ItemWithAllSupportedValues] [T0] " & GetTableHints1()))
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.WithHints(GetTableHints1())).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
        Assert.IsFalse(db.GetLastCommandText().Contains("FROM [ItemWithAllSupportedValues] [T0] " & GetTableHints1()))
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp) exp.WithHints(GetTableHints1()),
                           Function(exp) exp.WithHints(GetTableHints2())
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [ItemWithAllSupportedValues] [T0] " & GetTableHints1()))
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp) exp.WithHints(GetTableHints1()),
                           Function(exp) exp.WithHints(GetTableHints2())
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent(items, result)
        Assert.IsTrue(db.GetLastCommandText().Contains("FROM [ItemWithAllSupportedValues] [T0] " & GetTableHints2()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalInnerJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True, Function(exp) exp.Join(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.IsNull(result1(0).Label)
        Assert.IsNull(result1(1).Label)
        Assert.IsNull(result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False, Function(exp) exp.Join(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.IsNull(result2(0).Label)
        Assert.IsNull(result2(1).Label)
        Assert.IsNull(result2(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.Join(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.Join(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1De, result1(0).Label)
        Assert.AreEqual(label2De, result1(1).Label)
        Assert.AreEqual(label3De, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.Join(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.Join(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1De, result2(0).Label)
        Assert.AreEqual(label2De, result2(1).Label)
        Assert.AreEqual(label3De, result2(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalLeftOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True, Function(exp) exp.LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True, Function(exp) exp.LeftJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False, Function(exp) exp.LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.IsNull(result1(0).Label)
        Assert.IsNull(result1(1).Label)
        Assert.IsNull(result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False, Function(exp) exp.LeftJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.IsNull(result2(0).Label)
        Assert.IsNull(result2(1).Label)
        Assert.IsNull(result2(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.LeftJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.LeftJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1De, result1(0).Label)
        Assert.AreEqual(label2De, result1(1).Label)
        Assert.AreEqual(label3De, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.LeftJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.LeftJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1De, result2(0).Label)
        Assert.AreEqual(label2De, result2(1).Label)
        Assert.AreEqual(label3De, result2(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalRightOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True, Function(exp) exp.RightJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True, Function(exp) exp.RightJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False, Function(exp) exp.RightJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.IsNull(result1(0).Label)
        Assert.IsNull(result1(1).Label)
        Assert.IsNull(result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False, Function(exp) exp.RightJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.IsNull(result2(0).Label)
        Assert.IsNull(result2(1).Label)
        Assert.IsNull(result2(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.RightJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.RightJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.RightJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.RightJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.RightJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.RightJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1De, result1(0).Label)
        Assert.AreEqual(label2De, result1(1).Label)
        Assert.AreEqual(label3De, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.RightJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.RightJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1De, result2(0).Label)
        Assert.AreEqual(label2De, result2(1).Label)
        Assert.AreEqual(label3De, result2(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalFullOuterJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True, Function(exp) exp.FullJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True, Function(exp) exp.FullJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False, Function(exp) exp.FullJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.IsNull(result1(0).Label)
        Assert.IsNull(result1(1).Label)
        Assert.IsNull(result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False, Function(exp) exp.FullJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.IsNull(result2(0).Label)
        Assert.IsNull(result2(1).Label)
        Assert.IsNull(result2(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.FullJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.FullJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1En, result1(0).Label)
        Assert.AreEqual(label2En, result1(1).Label)
        Assert.AreEqual(label3En, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(True,
                            Function(exp) exp.FullJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.FullJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1En, result2(0).Label)
        Assert.AreEqual(label2En, result2(1).Label)
        Assert.AreEqual(label3En, result2(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.FullJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.FullJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result1)
        Assert.AreEqual(label1De, result1(0).Label)
        Assert.AreEqual(label2De, result1(1).Label)
        Assert.AreEqual(label3De, result1(2).Label)

        ' same as above, but use different syntax
        Dim result2 = db.From(Of Article).
                         If(False,
                            Function(exp) exp.FullJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                            Function(exp) exp.FullJoin(Of Label).On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                         ).
                         OrderBy(Function(j) j.T1.Id).
                         SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result2)
        Assert.AreEqual(label1De, result2(0).Label)
        Assert.AreEqual(label2De, result2(1).Label)
        Assert.AreEqual(label3De, result2(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalCrossJoin()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim article1Part1 = Me.ModelFactory.CreateArticlePart(11, 1)
      Dim article2Part1 = Me.ModelFactory.CreateArticlePart(13, 2)
      Dim article3Part1 = Me.ModelFactory.CreateArticlePart(14, 3)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, article1Part1, article2Part1, article3Part1)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.CrossJoin(Of Label)()).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article1, article1, article2, article2, article2, article3, article3, article3}, result)
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 1).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 2).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 3).Select(Function(x) x.Label.Id).ToArray()))
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.CrossJoin(Of Label)()).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      ' doesn't make sense for cross join (unless multiple clauses are chained inside a condition)

      ' condition is false, apply false part
      ' doesn't make sense for cross join (unless multiple clauses are chained inside a condition)
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalInnerJoinWithSpecifiedTableName()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)
      InsertItemsToArchive(Of Label)(LabelArchiveTableName, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.Join(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.Join(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.Join(Of Label),
                           Function(exp) exp.Join(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.Join(Of Label),
                           Function(exp) exp.Join(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalLeftOuterJoinWithSpecifiedTableName()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)
      InsertItemsToArchive(Of Label)(LabelArchiveTableName, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.LeftJoin(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.LeftJoin(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.LeftJoin(Of Label),
                           Function(exp) exp.LeftJoin(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.LeftJoin(Of Label),
                           Function(exp) exp.LeftJoin(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalRightOuterJoinWithSpecifiedTableName()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)
      InsertItemsToArchive(Of Label)(LabelArchiveTableName, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.RightJoin(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.RightJoin(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.RightJoin(Of Label),
                           Function(exp) exp.RightJoin(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.RightJoin(Of Label),
                           Function(exp) exp.RightJoin(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalFullOuterJoinWithSpecifiedTableName()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)
      InsertItemsToArchive(Of Label)(LabelArchiveTableName, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.FullJoin(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.FullJoin(Of Label)(LabelArchiveTableName)).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.FullJoin(Of Label),
                           Function(exp) exp.FullJoin(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.FullJoin(Of Label),
                           Function(exp) exp.FullJoin(Of Label)(LabelArchiveTableName)
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalCrossJoinWithSpecifiedTableName()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)
      InsertItemsToArchive(Of Label)(LabelArchiveTableName, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.CrossJoin(Of Label)(LabelArchiveTableName)).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article1, article1, article2, article2, article2, article3, article3, article3}, result)
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 1 AndAlso x.Label.Language = German).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 2 AndAlso x.Label.Language = German).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 3 AndAlso x.Label.Language = German).Select(Function(x) x.Label.Id).ToArray()))
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.CrossJoin(Of Label)(LabelArchiveTableName)).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.CrossJoin(Of Label),
                           Function(exp) exp.CrossJoin(Of Label)(LabelArchiveTableName)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article1, article1, article2, article2, article2, article3, article3, article3}, result)
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 1 AndAlso x.Label.Language = English).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 2 AndAlso x.Label.Language = English).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 3 AndAlso x.Label.Language = English).Select(Function(x) x.Label.Id).ToArray()))
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.CrossJoin(Of Label),
                           Function(exp) exp.CrossJoin(Of Label)(LabelArchiveTableName)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article1, article1, article2, article2, article2, article3, article3, article3}, result)
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 1 AndAlso x.Label.Language = German).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 2 AndAlso x.Label.Language = German).Select(Function(x) x.Label.Id).ToArray()))
        CollectionAssert.AreEquivalent({1, 2, 3}, (result.Where(Function(x) x.Id = 3 AndAlso x.Label.Language = German).Select(Function(x) x.Label.Id).ToArray()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalOn()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label).
                        If(True,
                           Function(exp) exp.On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                           Function(exp) exp.On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label).
                        If(False,
                           Function(exp) exp.On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                           Function(exp) exp.On(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label3De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalWithHintsOfJoinedTable()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)().
                        If(True, Function(exp) exp.WithHints(GetTableHints1())).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsTrue(db.GetLastCommandText().Contains("JOIN [Label] [T1] " & GetTableHints1()))
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)().
                        If(False, Function(exp) exp.WithHints(GetTableHints1())).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsFalse(db.GetLastCommandText().Contains("JOIN [Label] [T1] " & GetTableHints1()))
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)().
                        If(True,
                           Function(exp) exp.WithHints(GetTableHints1()),
                           Function(exp) exp.WithHints(GetTableHints2())
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsTrue(db.GetLastCommandText().Contains("JOIN [Label] [T1] " & GetTableHints1()))
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)().
                        If(False,
                           Function(exp) exp.WithHints(GetTableHints1()),
                           Function(exp) exp.WithHints(GetTableHints2())
                        ).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsTrue(db.GetLastCommandText().Contains("JOIN [Label] [T1] " & GetTableHints2()))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalAs()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label2En, label3En)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        If(True, Function(exp) exp.As(Function(x) x.AlternativeLabel1)).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.AreEqual(label1En, result(0).AlternativeLabel1)
        Assert.AreEqual(label2En, result(1).AlternativeLabel1)
        Assert.AreEqual(label3En, result(2).AlternativeLabel1)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        If(False, Function(exp) exp.As(Function(x) x.AlternativeLabel1)).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        If(True,
                           Function(exp) exp.As(Function(x) x.AlternativeLabel1),
                           Function(exp) exp.As(Function(x) x.AlternativeLabel2)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.AreEqual(label1En, result(0).AlternativeLabel1)
        Assert.AreEqual(label2En, result(1).AlternativeLabel1)
        Assert.AreEqual(label3En, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        If(False,
                           Function(exp) exp.As(Function(x) x.AlternativeLabel1),
                           Function(exp) exp.As(Function(x) x.AlternativeLabel2)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.AreEqual(label1En, result(0).AlternativeLabel2)
        Assert.AreEqual(label2En, result(1).AlternativeLabel2)
        Assert.AreEqual(label3En, result(2).AlternativeLabel2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalJoinAndAffectedEntityIsUsedAlsoInOtherClauses()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id)).
                        OrderBy(Function(j) j.T1.Id).
                        If(True, Function(exp) exp.ThenByDescending(Function(j) j.T2.Language)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article1, article2, article2, article3, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label1De, result(1).Label)
        Assert.AreEqual(label2En, result(2).Label)
        Assert.AreEqual(label2De, result(3).Label)
        Assert.AreEqual(label3En, result(4).Label)
        Assert.AreEqual(label3De, result(5).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id)).
                        OrderBy(Function(j) j.T1.Id).
                        If(False, Function(exp) exp.ThenByDescending(Function(j) j.T2.Language)).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' Following commented test is valid for MS SQL Server, because it does not allow to order by NULL.
      ' But it'll work in SQLite. SQLite allows using NULL in clauses like ORDER BY or GROUP BY.
      ' So, let's consider this case as a valid scenario.
      'Using db = CreateDbContext()
      '  Try
      '    Dim result = db.From(Of Article).
      '                  If(False, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id)).
      '                  OrderBy(Function(j) j.T1.Id).
      '                  ThenByDescending(Function(j) j.T2.Language).
      '                  SelectAll().ToList()
      '
      '    Assert.Fail()
      '  Catch ex As AssertFailedException
      '    Assert.Fail()
      '  Catch ex As Exception
      '  End Try
      'End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                        ).
                        OrderByDescending(Function(j) j.T2.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article3, article2, article1}, result)
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label1En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                        ).
                        OrderByDescending(Function(j) j.T2.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article3, article2, article1}, result)
        Assert.AreEqual(label3De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label1De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalJoinAndAffectedEntityIsUsedAlsoInCustomSelect()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                        OrderBy(Function(j) j.T1.Id).
                        Select(Function(j) (Article:=j.T1, Label:=j.T2, Description:=j.T2.Description)).
                        ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0).Article)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label1En.Description, result(0).Description)
        Assert.AreEqual(article2, result(1).Article)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label2En.Description, result(1).Description)
        Assert.AreEqual(article3, result(2).Article)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.AreEqual(label3En.Description, result(2).Description)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        ' TODO: SIP - what about using SQL helper here? Write test also for this!
        Dim result = db.From(Of Article).
                        If(False, Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English)).
                        OrderBy(Function(j) j.T1.Id).
                        Select(Function(j) (Article:=j.T1, Label:=j.T2, Description:=j.T2.Description)).
                        ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0).Article)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(0).Description)
        Assert.AreEqual(article2, result(1).Article)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(1).Description)
        Assert.AreEqual(article3, result(2).Article)
        Assert.IsNull(result(2).Label)
        Assert.IsNull(result(2).Description)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        Select(Function(j) (Article:=j.T1, Label:=j.T2, Description:=j.T2.Description)).
                        ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0).Article)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label1En.Description, result(0).Description)
        Assert.AreEqual(article2, result(1).Article)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label2En.Description, result(1).Description)
        Assert.AreEqual(article3, result(2).Article)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.AreEqual(label3En.Description, result(2).Description)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English),
                           Function(exp) exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = German)
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        Select(Function(j) (Article:=j.T1, Label:=j.T2, Description:=j.T2.Description)).
                        ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0).Article)
        Assert.AreEqual(label1De, result(0).Label)
        Assert.AreEqual(label1De.Description, result(0).Description)
        Assert.AreEqual(article2, result(1).Article)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label2De.Description, result(1).Description)
        Assert.AreEqual(article3, result(2).Article)
        Assert.AreEqual(label3De, result(2).Label)
        Assert.AreEqual(label3De.Description, result(2).Description)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalJoinUsingSubquery()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                        [then]:=Function(exp)
                                  Return exp.Join(Function(c)
                                                    Return c.From(Of Label).
                                                             Where(Function(x) x.Language = English).
                                                             Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                                             ToSubquery()
                                                  End Function).
                                             On(Function(j) j.T1.Id = j.T2.Id)
                                End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(label2En.Description, result(1).LabelDescription)
        Assert.AreEqual(label3En.Description, result(2).LabelDescription)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                        [then]:=Function(exp)
                                  Return exp.Join(Function(c)
                                                    Return c.From(Of Label).
                                                             Where(Function(x) x.Language = English).
                                                             Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                                             ToSubquery()
                                                  End Function).
                                             On(Function(j) j.T1.Id = j.T2.Id)
                                End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).LabelDescription)
        Assert.IsNull(result(1).LabelDescription)
        Assert.IsNull(result(2).LabelDescription)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                        [then]:=Function(exp)
                                  Return exp.Join(Function(c)
                                                    Return c.From(Of Label).
                                                             Where(Function(x) x.Language = English).
                                                             Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                                             ToSubquery()
                                                  End Function).
                                             On(Function(j) j.T1.Id = j.T2.Id)
                                End Function,
                        otherwise:=Function(exp)
                                     Return exp.Join(Function(c)
                                                       Return c.From(Of Label).
                                                                Where(Function(x) x.Language = German).
                                                                Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                                                ToSubquery()
                                                     End Function).
                                             On(Function(j) j.T1.Id = j.T2.Id)
                                   End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En.Description, result(0).LabelDescription)
        Assert.AreEqual(label2En.Description, result(1).LabelDescription)
        Assert.AreEqual(label3En.Description, result(2).LabelDescription)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                        [then]:=Function(exp)
                                  Return exp.Join(Function(c)
                                                    Return c.From(Of Label).
                                                             Where(Function(x) x.Language = English).
                                                             Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                                             ToSubquery()
                                                  End Function).
                                             On(Function(j) j.T1.Id = j.T2.Id)
                                End Function,
                        otherwise:=Function(exp)
                                     Return exp.Join(Function(c)
                                                       Return c.From(Of Label).
                                                                Where(Function(x) x.Language = German).
                                                                Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                                                ToSubquery()
                                                     End Function).
                                             On(Function(j) j.T1.Id = j.T2.Id)
                                   End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        Include(Sub(j) j.T1.LabelDescription = j.T2.Description).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1De.Description, result(0).LabelDescription)
        Assert.AreEqual(label2De.Description, result(1).LabelDescription)
        Assert.AreEqual(label3De.Description, result(2).LabelDescription)
      End Using
    End Sub

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
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp) exp.GroupBy(Function(x) x.Nvarchar50Column)).
                        Select(Function(x) x.Nvarchar50Column).
                        ToList()

        CollectionAssert.AreEquivalent({"a", "b", "a", "a", "b"}, result)
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
    Public Overridable Sub SelectWithConditionalSelectAll()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        ' maybe this should be forbidden as well
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        If(True, Function(exp) exp.SelectAll()).
                        ToList()

        CollectionAssert.AreEqual(items, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Try
          Dim result = db.From(Of ItemWithAllSupportedValues).
                          OrderBy(Function(x) x.IntColumn).
                          If(False, Function(exp) exp.SelectAll()).
                          ToList()

          Assert.Fail()
        Catch ex As InvalidOperationException
        Catch ex As Exception
          Assert.Fail(ex.Message)
        End Try
      End Using

      ' condition is true, apply true part
      ' doesn't make sense for select (unless multiple clauses are chained inside a condition)

      ' condition is false, apply false part
      ' doesn't make sense for select (unless multiple clauses are chained inside a condition)
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalSelectCount()
      Dim items = CreateItems()

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        ' maybe this should be forbidden as well
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp.SelectCount())

        Assert.AreEqual(5, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Try
          Dim result = db.From(Of ItemWithAllSupportedValues).
                          If(False, Function(exp) exp.SelectCount())

          Assert.Fail()
        Catch ex As InvalidOperationException
        Catch ex As Exception
          Assert.Fail(ex.Message)
        End Try
      End Using

      ' condition is true, apply true part
      ' doesn't make sense for select count (unless multiple clauses are chained inside a condition)

      ' condition is false, apply false part
      ' doesn't make sense for select count (unless multiple clauses are chained inside a condition)
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
        ' maybe this should be forbidden as well
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
        Catch ex As InvalidOperationException
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
                        If(False, Function(exp) exp.ExcludeT2()).
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

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalDistinct()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        If(True, Function(exp) exp.Distinct()).
                        ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().
                        ExcludeT2().
                        If(False, Function(exp) exp.Distinct()).
                        ToList()

        CollectionAssert.AreEqual({article1, article1, article2, article2, article3, article3}, result)
      End Using

      ' condition is true, apply true part
      ' doesn't make sense for distinct (unless multiple clauses are chained inside a condition)

      ' condition is false, apply false part
      ' doesn't make sense for distinct (unless multiple clauses are chained inside a condition)
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalToList()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        ' maybe this should be forbidden as well
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        SelectAll().
                        If(True, Function(exp) exp.ToList())

        CollectionAssert.AreEqual(items, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Try
          Dim result = db.From(Of ItemWithAllSupportedValues).
                          OrderBy(Function(x) x.IntColumn).
                          SelectAll().
                          If(False, Function(exp) exp.ToList())

          Assert.Fail()
        Catch ex As InvalidOperationException
        Catch ex As Exception
          Assert.Fail(ex.Message)
        End Try
      End Using

      ' condition is true, apply true part
      ' doesn't make sense for ToList (unless multiple clauses are chained inside a condition)

      ' condition is false, apply false part
      ' doesn't make sense for ToList (unless multiple clauses are chained inside a condition)
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithConditionalFirstOrDefault()
      Dim items = CreateItems()

      items(0).IntColumn = 1
      items(1).IntColumn = 2
      items(2).IntColumn = 3
      items(3).IntColumn = 4
      items(4).IntColumn = 5

      InsertItems(items)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        ' maybe this should be forbidden as well
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        OrderBy(Function(x) x.IntColumn).
                        SelectAll().
                        If(True, Function(exp) exp.FirstOrDefault())

        Assert.AreEqual(items(0), result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Try
          Dim result = db.From(Of ItemWithAllSupportedValues).
                          OrderBy(Function(x) x.IntColumn).
                          SelectAll().
                          If(False, Function(exp) exp.FirstOrDefault())

          Assert.Fail()
        Catch ex As InvalidOperationException
        Catch ex As Exception
          Assert.Fail(ex.Message)
        End Try
      End Using

      ' condition is true, apply true part
      ' doesn't make sense for FirstOrDefault (unless multiple clauses are chained inside a condition)

      ' condition is false, apply false part
      ' doesn't make sense for FirstOrDefault (unless multiple clauses are chained inside a condition)
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithChainedConditionalParts()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2En, label3En, label1De, label2De, label3De)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True, Function(exp)
                                   Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                              Where(Function(j) j.T2.Language = English).
                                              OrderByDescending(Function(j) j.T2.Id)
                                 End Function).
                        ThenBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article3, article2, article1}, result)
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label1En, result(2).Label)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False, Function(exp)
                                    Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                               Where(Function(j) j.T2.Language = English).
                                               OrderByDescending(Function(j) j.T2.Id)
                                  End Function).
                        ThenBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        Where(Function(j) j.T2.Language = English).
                                        OrderByDescending(Function(j) j.T2.Id)
                           End Function,
                           Function(exp)
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        Where(Function(j) j.T2.Language = German).
                                        OrderByDescending(Function(j) j.T2.Id)
                           End Function
                        ).
                        ThenBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article3, article2, article1}, result)
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label1En, result(2).Label)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        Where(Function(j) j.T2.Language = English).
                                        OrderByDescending(Function(j) j.T2.Id)
                           End Function,
                           Function(exp)
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        Where(Function(j) j.T2.Language = German).
                                        OrderByDescending(Function(j) j.T2.Id)
                           End Function
                        ).
                        ThenBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article3, article2, article1}, result)
        Assert.AreEqual(label3De, result(0).Label)
        Assert.AreEqual(label2De, result(1).Label)
        Assert.AreEqual(label1De, result(2).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithNestedConditionalParts()
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
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(True, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(True,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(4)}, result)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(True, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(True,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3), items(4)}, result)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                       If(True, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(True, Function(exp2) exp2.And(Function(x) x.IntColumn < 3))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(False, Function(exp2) exp2.And(Function(x) x.IntColumn < 3))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(2), items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(True,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(True,
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 3),
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 2)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(3), items(4)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(False,
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 3),
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 2)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(4)}, result)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                       If(True, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(True, Function(exp2) exp2.And(Function(x) x.IntColumn < 3))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False, Function(exp2) exp2.And(Function(x) 2 < x.IntColumn))
                           End Function,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(False, Function(exp2) exp2.And(Function(x) x.IntColumn < 3))
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2), items(3)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(True,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(True,
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 3),
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 2)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1), items(2)}, result)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Where(Function(x) 1 < x.IntColumn).
                                        If(False,
                                           Function(exp2) exp2.And(Function(x) 2 < x.IntColumn),
                                           Function(exp2) exp2.And(Function(x) 3 < x.IntColumn)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Where(Function(x) x.IntColumn < 4).
                                        If(False,
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 3),
                                           Function(exp2) exp2.And(Function(x) x.IntColumn < 2)
                                        )
                           End Function
                        ).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({items(0), items(1)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithNestedJoinConditionalParts()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2En = Me.ModelFactory.CreateLabel("", 2, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      Dim label1De = Me.ModelFactory.CreateLabel("", 1, German)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      Dim label1Fr = Me.ModelFactory.CreateLabel("", 1, French)
      Dim label2Fr = Me.ModelFactory.CreateLabel("", 2, French)
      Dim label3Fr = Me.ModelFactory.CreateLabel("", 3, French)

      Dim label1It = Me.ModelFactory.CreateLabel("", 1, Italian)
      Dim label2It = Me.ModelFactory.CreateLabel("", 2, Italian)
      Dim label3It = Me.ModelFactory.CreateLabel("", 3, Italian)

      Dim label1Es = Me.ModelFactory.CreateLabel("", 1, Spanish)
      Dim label2Es = Me.ModelFactory.CreateLabel("", 2, Spanish)
      Dim label3Es = Me.ModelFactory.CreateLabel("", 3, Spanish)

      Dim label1No = Me.ModelFactory.CreateLabel("", 1, Norwegian)
      Dim label2No = Me.ModelFactory.CreateLabel("", 2, Norwegian)
      Dim label3No = Me.ModelFactory.CreateLabel("", 3, Norwegian)

      InsertItems(article1, article2, article3)
      InsertItems(label1En, label2En, label3En)
      InsertItems(label1De, label2De, label3De)
      InsertItems(label1Fr, label2Fr, label3Fr)
      InsertItems(label1It, label2It, label3It)
      InsertItems(label1Es, label2Es, label3Es)
      InsertItems(label1No, label2No, label3No)

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.AreEqual(label1De, result(0).AlternativeLabel1)
        Assert.AreEqual(label2De, result(1).AlternativeLabel1)
        Assert.AreEqual(label3De, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.AreEqual(label1De, result(0).AlternativeLabel1)
        Assert.AreEqual(label2De, result(1).AlternativeLabel1)
        Assert.AreEqual(label3De, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.AreEqual(label1Fr, result(0).AlternativeLabel2)
        Assert.AreEqual(label2Fr, result(1).AlternativeLabel2)
        Assert.AreEqual(label3Fr, result(2).AlternativeLabel2)
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.IsNull(result(0).Label)
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(True, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.AreEqual(label1De, result(0).AlternativeLabel1)
        Assert.AreEqual(label2De, result(1).AlternativeLabel1)
        Assert.AreEqual(label3De, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(False, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(True,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Norwegian).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.AreEqual(label1De, result(0).AlternativeLabel1)
        Assert.AreEqual(label2De, result(1).AlternativeLabel1)
        Assert.AreEqual(label3De, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(True,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(False,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Norwegian).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(label2En, result(1).Label)
        Assert.AreEqual(label3En, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.AreEqual(label1Fr, result(0).AlternativeLabel2)
        Assert.AreEqual(label2Fr, result(1).AlternativeLabel2)
        Assert.AreEqual(label3Fr, result(2).AlternativeLabel2)
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(True, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1It, result(0).Label)
        Assert.AreEqual(label2It, result(1).Label)
        Assert.AreEqual(label3It, result(2).Label)
        Assert.AreEqual(label1Es, result(0).AlternativeLabel1)
        Assert.AreEqual(label2Es, result(1).AlternativeLabel1)
        Assert.AreEqual(label3Es, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1))
                           End Function,
                           Function(exp)
                             ' condition is false, apply nothing
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(False, Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1))
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1It, result(0).Label)
        Assert.AreEqual(label2It, result(1).Label)
        Assert.AreEqual(label3It, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(True,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is true, apply true part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(True,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Norwegian).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1It, result(0).Label)
        Assert.AreEqual(label2It, result(1).Label)
        Assert.AreEqual(label3It, result(2).Label)
        Assert.AreEqual(label1Es, result(0).AlternativeLabel1)
        Assert.AreEqual(label2Es, result(1).AlternativeLabel1)
        Assert.AreEqual(label3Es, result(2).AlternativeLabel1)
        Assert.IsNull(result(0).AlternativeLabel2)
        Assert.IsNull(result(1).AlternativeLabel2)
        Assert.IsNull(result(2).AlternativeLabel2)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        If(False,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        If(False,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = German).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = French).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function,
                           Function(exp)
                             ' condition is false, apply false part
                             Return exp.Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = Italian).
                                        If(False,
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Spanish).As(Function(j) j.T1.AlternativeLabel1),
                                           Function(exp2) exp2.Join(Of Label)(Function(j) j.T1.Id = j.T3.Id AndAlso j.T3.Language = Norwegian).As(Function(j) j.T1.AlternativeLabel2)
                                        )
                           End Function
                        ).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEqual({article1, article2, article3}, result)
        Assert.AreEqual(label1It, result(0).Label)
        Assert.AreEqual(label2It, result(1).Label)
        Assert.AreEqual(label3It, result(2).Label)
        Assert.IsNull(result(0).AlternativeLabel1)
        Assert.IsNull(result(1).AlternativeLabel1)
        Assert.IsNull(result(2).AlternativeLabel1)
        Assert.AreEqual(label1No, result(0).AlternativeLabel2)
        Assert.AreEqual(label2No, result(1).AlternativeLabel2)
        Assert.AreEqual(label3No, result(2).AlternativeLabel2)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectAndTryConditionalExecution()
      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True, Function(exp) exp).
                        SelectAll().ToList()
      End Using

      ' condition is false, apply nothing
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False, Function(exp)
                                    Throw New Exception("This should not be executed.")
                                    Return exp
                                  End Function).
                        SelectAll().ToList()
      End Using

      ' condition is true, apply true part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(True,
                           Function(exp) exp,
                           Function(exp)
                             Throw New Exception("This should not be executed.")
                             Return exp
                           End Function
                        ).
                        SelectAll().ToList()
      End Using

      ' condition is false, apply false part
      Using db = CreateDbContext()
        Dim result = db.From(Of ItemWithAllSupportedValues).
                        If(False,
                           Function(exp)
                             Throw New Exception("This should not be executed.")
                             Return exp
                           End Function,
                           Function(exp) exp
                        ).
                        SelectAll().ToList()
      End Using
    End Sub

    Protected Overridable Function CreateItems(Optional count As Int32 = 5) As List(Of ItemWithAllSupportedValues)
      Return Enumerable.Range(0, count).Select(Function(x) Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()).ToList()
    End Function

  End Class
End Namespace
