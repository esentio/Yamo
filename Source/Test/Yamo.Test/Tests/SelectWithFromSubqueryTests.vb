Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithFromSubqueryTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 70D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English)

      InsertItems(article1, article2, article3, article4, label1En, label3En, label4En)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        SelectAll()
                             End Function).
                        Join(Of Label)(Function(a, l) a.Id = l.Id AndAlso l.Language = English).
                        OrderBy(Function(l) l.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(article1, result(1))
        Assert.AreEqual(label1En, result(1).Label)
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        SelectAll()
                             End Function).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(article1, result(1))
        Assert.AreEqual(label1En, result(1).Label)
      End Using

      ' use custom select
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) x)
                             End Function).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article3, result(0))
        Assert.AreEqual(label3En, result(0).Label)
        Assert.AreEqual(article1, result(1))
        Assert.AreEqual(label1En, result(1).Label)
      End Using

      ' use custom select, but specify properties
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New Article With {.Id = x.Id, .Price = x.Price})
                             End Function).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(New Article With {.Id = article3.Id, .Price = article3.Price}, result(0))
        ' NOTE: Label instances won't be set, because in this case, Article will be
        ' represented by NonModelEntity and hence relationship won't be found.
        ' Should this be changed in the future?
        'Assert.AreEqual(label3En, result(0).Label)
        Assert.IsNull(result(0).Label)
        Assert.AreEqual(New Article With {.Id = article1.Id, .Price = article1.Price}, result(1))
        'Assert.AreEqual(label1En, result(1).Label)
        Assert.IsNull(result(1).Label)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAnonymousType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 70D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English)

      InsertItems(article1, article2, article3, article4, label1En, label3En, label4En)

      Dim item1 = New With {Key .Id = article1.Id, Key .Price = article1.Price}
      Dim item3 = New With {Key .Id = article3.Id, Key .Price = article3.Price}

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New With {Key .Id = x.Id, Key .Price = x.Price})
                             End Function).
                        Join(Of Label)(Function(a, l) a.Id = l.Id AndAlso l.Language = English).
                        OrderBy(Function(l) l.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item3, result(0))
        Assert.AreEqual(item1, result(1))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New With {Key .Id = x.Id, Key .Price = x.Price})
                             End Function).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item3, result(0))
        Assert.AreEqual(item1, result(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeValueTuple()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 70D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English)

      InsertItems(article1, article2, article3, article4, label1En, label3En, label4En)

      Dim item1 = (Id:=article1.Id, Price:=article1.Price)
      Dim item3 = (Id:=article3.Id, Price:=article3.Price)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) (Id:=x.Id, Price:=x.Price))
                             End Function).
                        Join(Of Label)(Function(a, l) a.Id = l.Id AndAlso l.Language = English).
                        OrderBy(Function(l) l.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item3, result(0))
        Assert.AreEqual(item1, result(1))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) (Id:=x.Id, Price:=x.Price))
                             End Function).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.Price).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item3, result(0))
        Assert.AreEqual(item1, result(1))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAdHocType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 70D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label4En = Me.ModelFactory.CreateLabel("", 4, English)

      InsertItems(article1, article2, article3, article4, label1En, label3En, label4En)

      Dim item1 = New NonModelObject(article1.Id, article1.Price)
      Dim item3 = New NonModelObject(article3.Id, article3.Price)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New NonModelObject With {.IntValue = x.Id, .DecimalValue = x.Price})
                             End Function).
                        Join(Of Label)(Function(a, l) a.IntValue = l.Id AndAlso l.Language = English).
                        OrderBy(Function(l) l.DecimalValue).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item3, result(0))
        Assert.AreEqual(item1, result(1))
      End Using

      ' same as above, but use IJoin
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New NonModelObject With {.IntValue = x.Id, .DecimalValue = x.Price})
                             End Function).
                        Join(Of Label)(Function(j) j.T1.IntValue = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.DecimalValue).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(item3, result(0))
        Assert.AreEqual(item1, result(1))
      End Using

      ' use property with unknown column mapping
      Try
        Using db = CreateDbContext()
          Dim result = db.From(Function(c)
                                 Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New NonModelObject(x.Id, x.Price))
                               End Function).
                        Join(Of Label)(Function(a, l) a.IntValue = l.Id AndAlso l.Language = English).
                        OrderBy(Function(l) l.DecimalValue).
                        SelectAll().ToList()
        End Using
        Assert.Fail()
      Catch ex As Exception
      End Try

      ' same as above, but use IJoin
      Try
        Using db = CreateDbContext()
          Dim result = db.From(Function(c)
                                 Return c.From(Of Article).
                                        Where(Function(x) x.Id < 4).
                                        Select(Function(x) New NonModelObject(x.Id, x.Price))
                               End Function).
                        Join(Of Label)(Function(j) j.T1.IntValue = j.T2.Id AndAlso j.T2.Language = English).
                        OrderBy(Function(j) j.T1.DecimalValue).
                        SelectAll().ToList()
        End Using
        Assert.Fail()
      Catch ex As Exception
      End Try
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeEntityUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(x) x.T2, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(label1En, result(1))
        Assert.AreEqual(label3En, result(2))
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(x) x.T2, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(label1En, result(1))
        Assert.AreEqual(label3En, result(2))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(x) x.T2)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(label1En, result(1))
        Assert.AreEqual(label3En, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeEntityUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(x) x.T2, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(label1En, result(1))
        Assert.AreEqual(label3En, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAnonymousTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      Dim item1 = New With {Key .Id = label1En.Id, Key .Description = label1En.Description}
      Dim item3 = New With {Key .Id = label3En.Id, Key .Description = label3En.Description}

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New With {Key .Id = j.T2.Id, Key .Description = j.T2.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New With {Key .Id = j.T2.Id, Key .Description = j.T2.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New With {Key .Id = j.T2.Id, Key .Description = j.T2.Description})
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAnonymousTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      Dim item1 = New With {Key .Id = label1En.Id, Key .Description = label1En.Description}
      Dim item3 = New With {Key .Id = label3En.Id, Key .Description = label3En.Description}
      Dim itemEmpty = New With {Key .Id = 0, Key .Description = CType(Nothing, String)}

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New With {Key .Id = j.T2.Id, Key .Description = j.T2.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(itemEmpty, result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeValueTupleUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      Dim item1 = (Id:=label1En.Id, Description:=label1En.Description)
      Dim item3 = (Id:=label3En.Id, Description:=label3En.Description)
      Dim itemEmpty = (Id:=0, Description:=CType(Nothing, String))

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) (Id:=j.T2.Id, Description:=j.T2.Description), NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(itemEmpty, result(0)) ' because this is value type, it won't be null
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) (Id:=j.T2.Id, Description:=j.T2.Description), NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(itemEmpty, result(0)) ' because this is value type, it won't be null
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) (Id:=j.T2.Id, Description:=j.T2.Description))
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(itemEmpty, result(0)) ' because this is value type, it won't be null
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeValueTupleUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      Dim item1 = (Id:=label1En.Id, Description:=label1En.Description)
      Dim item3 = (Id:=label3En.Id, Description:=label3En.Description)
      Dim itemEmpty = (Id:=0, Description:=CType(Nothing, String))

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) (Id:=j.T2.Id, Description:=j.T2.Description), NonModelEntityCreationBehavior.AlwaysCreateInstance)
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(itemEmpty, result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAdHocTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      Dim item1 = New NonModelObject(label1En.Id, label1En.Description)
      Dim item3 = New NonModelObject(label3En.Id, label3En.Description)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New NonModelObject With {.IntValue = j.T2.Id, .StringValue1 = j.T2.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.IntValue).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New NonModelObject With {.IntValue = j.T2.Id, .StringValue1 = j.T2.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                             End Function).
                        OrderBy(Function(x) x.IntValue).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New NonModelObject With {.IntValue = j.T2.Id, .StringValue1 = j.T2.Description})
                             End Function).
                        OrderBy(Function(x) x.IntValue).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNull(result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSubqueryOfTypeAdHocTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)

      InsertItems(article1, article2, article3, label1En, label3En)

      Dim item1 = New NonModelObject(label1En.Id, label1En.Description)
      Dim item3 = New NonModelObject(label3En.Id, label3En.Description)
      Dim itemEmpty = New NonModelObject(0, CType(Nothing, String))

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
                                        Select(Function(j) New NonModelObject With {.IntValue = j.T2.Id, .StringValue1 = j.T2.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                             End Function).
                        OrderBy(Function(x) x.IntValue).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(itemEmpty, result(0))
        Assert.AreEqual(item1, result(1))
        Assert.AreEqual(item3, result(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithNestedSubquery()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 90D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 80D)
      Dim article4 = Me.ModelFactory.CreateArticle(4, 70D)

      InsertItems(article1, article2, article3, article4)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Function(c2)
                                               Return c2.From(Of Article).
                                                         Where(Function(x) 1 < x.Id).
                                                         SelectAll()
                                             End Function).
                                        Where(Function(x) x.Id < 4).
                                        SelectAll()
                             End Function).
                        OrderBy(Function(x) x.Id).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(article2, result(0))
        Assert.AreEqual(article3, result(1))
      End Using
    End Sub

  End Class
End Namespace
