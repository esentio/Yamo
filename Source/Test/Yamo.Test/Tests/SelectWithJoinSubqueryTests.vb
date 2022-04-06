﻿Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithJoinSubqueryTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(c)
                                         Return c.From(Of Label).
                                                  Where(Function(x) x.Language = English).
                                                  SelectAll().
                                                  ToSubquery()
                                       End Function).
                        On(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(l) l.Description).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(article1, result(1))
        Assert.AreEqual(label1En, result(1).Label)
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(c)
                                         Return c.From(Of Label).
                                                  Where(Function(x) x.Language = English).
                                                  SelectAll().
                                                  ToSubquery()
                                       End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T2.Description).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(article1, result(1))
        Assert.AreEqual(label1En, result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAnonymousType()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of Label).
                                        Where(Function(x) x.Language = English).
                                        Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                        ToSubquery()
                             End Function).
                        On(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(l) l.Description).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(article1, result(1))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of Label).
                                        Where(Function(x) x.Language = English).
                                        Select(Function(x) New With {.Id = x.Id, .Description = x.Description}).
                                        ToSubquery()
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T2.Description).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(article1, result(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeValueTuple()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of Label).
                                        Where(Function(x) x.Language = English).
                                        Select(Function(x) (Id:=x.Id, Description:=x.Description)).
                                        ToSubquery()
                             End Function).
                        On(Function(a, l) a.Id = l.Id).
                        OrderBy(Function(l) l.Description).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(article1, result(1))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of Label).
                                        Where(Function(x) x.Language = English).
                                        Select(Function(x) (Id:=x.Id, Description:=x.Description)).
                                        ToSubquery()
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        OrderBy(Function(j) j.T2.Description).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(article1, result(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAdHocType()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of Label).
                                        Where(Function(x) x.Language = English).
                                        Select(Function(x) New NonModelObject(x.Language) With {.IntValue = x.Id, .StringValue1 = x.Description}).
                                        ToSubquery()
                             End Function).
                        On(Function(a, l) a.Id = l.IntValue).
                        OrderBy(Function(l) l.StringValue1).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(article1, result(1))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of Label).
                                        Where(Function(x) x.Language = English).
                                        Select(Function(x) New NonModelObject(x.Language) With {.IntValue = x.Id, .StringValue1 = x.Description}).
                                        ToSubquery()
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        OrderBy(Function(j) j.T2.StringValue1).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(article1, result(1))
      End Using

      ' use property with unknown column mapping
      Try
        Using db = CreateDbContext()
          Dim result = db.From(Of Article).
                          Join(Function(c)
                                 Return c.From(Of Label).
                                          Where(Function(x) x.Language = English).
                                          Select(Function(x) New NonModelObject(x.Id, x.Description, x.Language)).
                                          ToSubquery()
                               End Function).
                          On(Function(a, l) a.Id = l.IntValue).
                          OrderBy(Function(l) l.StringValue1).
                          SelectAll().ToList()
        End Using
        Assert.Fail()
      Catch ex As Exception
      End Try

      ' same as above, but use IJoin
      Try
        Using db = CreateDbContext()
          Dim result = db.From(Of Article).
                          Join(Function(c)
                                 Return c.From(Of Label).
                                          Where(Function(x) x.Language = English).
                                          Select(Function(x) New NonModelObject(x.Id, x.Description, x.Language)).
                                          ToSubquery()
                               End Function).
                          On(Function(j) j.T1.Id = j.T2.IntValue).
                          OrderBy(Function(j) j.T2.StringValue1).
                          SelectAll().ToList()
        End Using
        Assert.Fail()
      Catch ex As Exception
      End Try
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeEntityWithExplicitRelationship()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(Function(c)
                                             Return c.From(Of Label).
                                                      Where(Function(x) x.Language = English).
                                                      SelectAll().
                                                      ToSubquery()
                                           End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(label1En, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Label)
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.IsNull(result(2).Label)
        Assert.AreEqual(label3En, result(2).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAnonymousTypeWithExplicitRelationship()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}).
                                            ToSubquery()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(New With {Key .Id = 0, Key .Description = CType(Nothing, String)}, result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, result(2).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeValueTupleWithExplicitRelationship()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description)).
                                            ToSubquery()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(ValueTuple.Create(Of Int32, String)(0, Nothing), result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual((label3En.Id, label3En.Description), result(2).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAdHocTypeWithExplicitRelationship()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English, "ddd")
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German, "ccc")
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English, "bbb")
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German, "aaa")

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}).
                                            ToSubquery()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(New NonModelObject() With {.IntValue = 0, .StringValue1 = Nothing}, result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, result(2).Tag)
      End Using
    End Sub


    ' TODO: SIP - implement subquery
    ' × ad hoc
    ' × anonymous
    ' × value tuple
    ' × As()
    ' As() pre non model entitu
    ' include
    ' include pre non model entitu
    ' ci query obsahuje stlpce
    ' conditional

  End Class
End Namespace