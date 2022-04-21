Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithExplicitRelationship
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitRelationships()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, 1)

      Dim linkedItem1Child1 = Me.ModelFactory.CreateLinkedItemChild(1, 1)
      Dim linkedItem1Child2 = Me.ModelFactory.CreateLinkedItemChild(2, 1)
      Dim linkedItem1Child3 = Me.ModelFactory.CreateLinkedItemChild(3, 1)

      InsertItems(linkedItem1, linkedItem2)
      InsertItems(linkedItem1Child1, linkedItem1Child2, linkedItem1Child3)

      ' relationship is not defined in model
      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItem).
                        Join(Of LinkedItem)(Function(j) j.T1.Id = j.T2.PreviousId.Value).As(Function(x) x.NextItem).
                        Join(Of LinkedItemChild)(Function(j) j.T1.Id = j.T3.LinkedItemId).As(Function(j) j.T1.Children).
                        SelectAll().ToList()

        Assert.AreEqual(1, result.Count)

        Dim linkedItem1Result = result.First()
        Assert.AreEqual(linkedItem1, linkedItem1Result)
        Assert.IsNotNull(linkedItem1Result.NextItem)
        Assert.AreEqual(linkedItem2, linkedItem1Result.NextItem)
        Assert.AreEqual(3, linkedItem1Result.Children.Count)
        Assert.IsNotNull(linkedItem1Result.Children.SingleOrDefault(Function(x) x.Id = 1))
        Assert.IsNotNull(linkedItem1Result.Children.SingleOrDefault(Function(x) x.Id = 2))
        Assert.IsNotNull(linkedItem1Result.Children.SingleOrDefault(Function(x) x.Id = 3))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            SelectAll()
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
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeEntityUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
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

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
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

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x)
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
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeEntityUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.AlwaysCreateInstance)
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
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            SelectAll()
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({label1En}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({label3En, label3De}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeEntityUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({label1En}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({label3En, label3De}, result(2).Tags)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({label1En}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({label3En, label3De}, result(2).Tags)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) x)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({label1En}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({label3En, label3De}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeEntityUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      ' NonModelEntityCreationBehavior should not have any effect here

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) x, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({label1En}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({label3En, label3De}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeAnonymousTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, result(2).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, result(2).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New With {Key .Id = label1En.Id, Key .Description = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, result(2).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeAnonymousTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
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
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeAnonymousTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New With {Key .Id = label1En.Id, Key .Description = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, New With {Key .Id = label3De.Id, Key .Description = label3De.Description}}, result(2).Tags)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New With {Key .Id = label1En.Id, Key .Description = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, New With {Key .Id = label3De.Id, Key .Description = label3De.Description}}, result(2).Tags)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New With {Key .Id = label1En.Id, Key .Description = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, New With {Key .Id = label3De.Id, Key .Description = label3De.Description}}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeAnonymousTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New With {Key .Id = x.Id, Key .Description = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New With {Key .Id = label1En.Id, Key .Description = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        CollectionAssert.AreEquivalent({New With {Key .Id = 0, Key .Description = CType(Nothing, String)}}, result(1).Tags)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New With {Key .Id = label3En.Id, Key .Description = label3En.Description}, New With {Key .Id = label3De.Id, Key .Description = label3De.Description}}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeValueTupleUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual((label3En.Id, label3En.Description), result(2).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual((label3En.Id, label3En.Description), result(2).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description))
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual((label1En.Id, label1En.Description), result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual((label3En.Id, label3En.Description), result(2).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeValueTupleUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.AlwaysCreateInstance)
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
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeValueTupleUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({(label1En.Id, label1En.Description)}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({(label3En.Id, label3En.Description), (label3De.Id, label3De.Description)}, result(2).Tags)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({(label1En.Id, label1En.Description)}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({(label3En.Id, label3En.Description), (label3De.Id, label3De.Description)}, result(2).Tags)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description))
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({(label1En.Id, label1En.Description)}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({(label3En.Id, label3En.Description), (label3De.Id, label3De.Description)}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeValueTupleUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) (Id:=x.Id, Description:=x.Description), NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.Id).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({(label1En.Id, label1En.Description)}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        CollectionAssert.AreEquivalent({ValueTuple.Create(Of Int32, String)(0, Nothing)}, result(1).Tags)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({(label3En.Id, label3En.Description), (label3De.Id, label3De.Description)}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeAdHocTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, result(2).Tag)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, result(2).Tag)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}, result(0).Tag)
        Assert.AreEqual(article2, result(1))
        Assert.IsNull(result(1).Tag)
        Assert.AreEqual(article3, result(2))
        Assert.AreEqual(New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, result(2).Tag)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueFromSubqueryOfTypeAdHocTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Where(Function(x) x.Language = English).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
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

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeAdHocTypeUsingNullIfAllColumnsAreNullBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.NullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, New NonModelObject() With {.IntValue = label3De.Id, .StringValue1 = label3De.Description}}, result(2).Tags)
      End Using

      ' same as above, but assume infer behavior
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.InferOrNullIfAllColumnsAreNull)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, New NonModelObject() With {.IntValue = label3De.Id, .StringValue1 = label3De.Description}}, result(2).Tags)
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description})
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        Assert.AreEqual(0, result(1).Tags.Count)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, New NonModelObject() With {.IntValue = label3De.Id, .StringValue1 = label3De.Description}}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitCollectionRelationshipSettingValueFromSubqueryOfTypeAdHocTypeUsingAlwaysCreateInstanceBehavior()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Function(c)
                                   Return c.From(Of Label).
                                            Select(Function(x) New NonModelObject() With {.IntValue = x.Id, .StringValue1 = x.Description}, NonModelEntityCreationBehavior.AlwaysCreateInstance)
                                 End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tags).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(article1, result(0))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label1En.Id, .StringValue1 = label1En.Description}}, result(0).Tags)
        Assert.AreEqual(article2, result(1))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = 0, .StringValue1 = Nothing}}, result(1).Tags)
        Assert.AreEqual(article3, result(2))
        CollectionAssert.AreEquivalent({New NonModelObject() With {.IntValue = label3En.Id, .StringValue1 = label3En.Description}, New NonModelObject() With {.IntValue = label3De.Id, .StringValue1 = label3De.Description}}, result(2).Tags)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueToResultOfSubqueryOfTypeEntity()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        SelectAll()
                             End Function).
                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id AndAlso j.T2.Language = English).
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
    Public Overridable Sub SelectWithExplicitReferenceRelationshipSettingValueToResultOfSubqueryOfTypeAdHocType()
      Dim article1 = Me.ModelFactory.CreateArticle(1, 100D)
      Dim article2 = Me.ModelFactory.CreateArticle(2, 200D)
      Dim article3 = Me.ModelFactory.CreateArticle(3, 300D)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label2De = Me.ModelFactory.CreateLabel("", 2, German)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label2De, label3En, label3De)

      Dim item1 = New NonModelObject(article1.Id, article1.Price)
      Dim item2 = New NonModelObject(article2.Id, article2.Price)
      Dim item3 = New NonModelObject(article3.Id, article3.Price)

      Using db = CreateDbContext()
        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Select(Function(x) New NonModelObject With {.IntValue = x.Id, .DecimalValue = x.Price})
                             End Function).
                        LeftJoin(Of Label)(Function(j) j.T1.IntValue = j.T2.Id AndAlso j.T2.Language = English).
                        As(Function(x) x.Label).
                        OrderBy(Function(j) j.T1.IntValue).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.AreEqual(item1, result(0))
        Assert.AreEqual(label1En, result(0).Label)
        Assert.AreEqual(item2, result(1))
        Assert.IsNull(result(1).Label)
        Assert.AreEqual(item3, result(2))
        Assert.AreEqual(label3En, result(2).Label)
      End Using
    End Sub

  End Class
End Namespace
