Imports Yamo.Metadata
Imports Yamo.Test.Infrastructure
Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectColumnsTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    <TestMethod()>
    Public Overridable Sub SelectWithModelDefinedRelationshipUsingSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim labelEn = Me.ModelFactory.CreateLabel("", 1, English)

      InsertItems(article, labelEn)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())

        ' ensure relationship is defined
        Assert.IsNotNull(articleEntity.GetRelationshipNavigation(NameOf(article.Label)))

        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        SelectAll(SelectColumnsBehavior.SelectAllColumns).FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(labelEn, result.Label)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(labelEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitRelationshipUsingSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        ' ensure relationship is not defined
        Assert.IsFalse(db.Model.GetEntity(article.GetType()).GetRelationshipNavigations(item.GetType()).Any())

        Dim result = db.From(Of Article).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).
                        As(Function(x) x.Tag).
                        SelectAll(SelectColumnsBehavior.SelectAllColumns).FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(itemEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithoutDefinedRelationshipUsingSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        ' ensure relationship is not defined
        Assert.IsFalse(db.Model.GetEntity(article.GetType()).GetRelationshipNavigations(item.GetType()).Any())

        Dim result = db.From(Of Article).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).
                        SelectAll(SelectColumnsBehavior.SelectAllColumns).FirstOrDefault()

        Assert.AreEqual(article, result)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(itemEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitRelationshipUsingJoinSubqueryAndSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id
      item.Nvarchar50Column = "foo"

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ItemWithAllSupportedValues).
                                        Select(Function(x) New With {Key .IntValue = x.IntColumn, Key .StringValue = x.Nvarchar50Column})
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        SelectAll(SelectColumnsBehavior.SelectAllColumns).FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(New With {Key .IntValue = item.IntColumn, Key .StringValue = item.Nvarchar50Column}, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New NonModelEntityColumnData(2))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New NonModelEntityColumnData(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithoutDefinedRelationshipUsingJoinSubqueryAndSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id
      item.Nvarchar50Column = "foo"

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ItemWithAllSupportedValues).
                                        Select(Function(x) New With {Key .IntValue = x.IntColumn, Key .StringValue = x.Nvarchar50Column})
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        SelectAll(SelectColumnsBehavior.SelectAllColumns).FirstOrDefault()

        Assert.AreEqual(article, result)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New NonModelEntityColumnData(2))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New NonModelEntityColumnData(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingJoinSubqueryWithJoinsThatIsUsingSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim articlePart1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim articlePart2 = Me.ModelFactory.CreateArticlePart(1002, 1)
      Dim articlePart1LabelEn = Me.ModelFactory.CreateLabel("", 1001, English)
      Dim articlePart2LabelEn = Me.ModelFactory.CreateLabel("", 1002, English)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, articlePart1, articlePart2, articlePart1LabelEn, articlePart2LabelEn, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim articlePartEntity = db.Model.GetEntity(articlePart1.GetType())
        Dim labelEntity = db.Model.GetEntity(articlePart1LabelEn.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ArticlePart).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        SelectAll(SelectColumnsBehavior.SelectAllColumns)
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.ArticleId).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T3.IntColumn).As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article, result)
        CollectionAssert.AreEqual({articlePart1, articlePart2}, result.Parts)
        Assert.IsTrue(result.Parts.All(Function(x) x.Label Is Nothing))
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New AliasedEntityColumnData(articlePartEntity), New EntityColumnData(itemEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New EntityColumnData(articlePartEntity), New EntityColumnData(labelEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingFromSubqueryWithJoinsThatIsUsingSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim labelEn = Me.ModelFactory.CreateLabel("", 1, English)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, labelEn, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        SelectAll(SelectColumnsBehavior.SelectAllColumns)
                             End Function).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.IsNull(result.Label)
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New AliasedEntityColumnData(articleEntity), New EntityColumnData(itemEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(labelEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeUsingSelectAllColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim labelEn = Me.ModelFactory.CreateLabel("", 1, English)

      InsertItems(article, labelEn)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())

        ' ensure relationship is defined
        Assert.IsNotNull(articleEntity.GetRelationshipNavigation(NameOf(article.Label)))

        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        SelectAll(SelectColumnsBehavior.SelectAllColumns).ExcludeT2().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.IsNull(result.Label)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithModelDefinedRelationshipUsingExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim labelEn = Me.ModelFactory.CreateLabel("", 1, English)

      InsertItems(article, labelEn)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())

        ' ensure relationship is defined
        Assert.IsNotNull(articleEntity.GetRelationshipNavigation(NameOf(article.Label)))

        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns).FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(labelEn, result.Label)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(labelEntity))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())

        ' ensure relationship is defined
        Assert.IsNotNull(articleEntity.GetRelationshipNavigation(NameOf(article.Label)))

        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(labelEn, result.Label)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(labelEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitRelationshipUsingExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        ' ensure relationship is not defined
        Assert.IsFalse(db.Model.GetEntity(article.GetType()).GetRelationshipNavigations(item.GetType()).Any())

        Dim result = db.From(Of Article).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).
                        As(Function(x) x.Tag).
                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns).FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(itemEntity))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        ' ensure relationship is not defined
        Assert.IsFalse(db.Model.GetEntity(article.GetType()).GetRelationshipNavigations(item.GetType()).Any())

        Dim result = db.From(Of Article).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).
                        As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(itemEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithoutDefinedRelationshipUsingExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        ' ensure relationship is not defined
        Assert.IsFalse(db.Model.GetEntity(article.GetType()).GetRelationshipNavigations(item.GetType()).Any())

        Dim result = db.From(Of Article).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).
                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns).FirstOrDefault()

        Assert.AreEqual(article, result)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        ' ensure relationship is not defined
        Assert.IsFalse(db.Model.GetEntity(article.GetType()).GetRelationshipNavigations(item.GetType()).Any())

        Dim result = db.From(Of Article).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExplicitRelationshipUsingJoinSubqueryAndExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id
      item.Nvarchar50Column = "foo"

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ItemWithAllSupportedValues).
                                        Select(Function(x) New With {Key .IntValue = x.IntColumn, Key .StringValue = x.Nvarchar50Column})
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns).FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(New With {Key .IntValue = item.IntColumn, Key .StringValue = item.Nvarchar50Column}, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New NonModelEntityColumnData(2))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New NonModelEntityColumnData(2))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ItemWithAllSupportedValues).
                                        Select(Function(x) New With {Key .IntValue = x.IntColumn, Key .StringValue = x.Nvarchar50Column})
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.AreEqual(New With {Key .IntValue = item.IntColumn, Key .StringValue = item.Nvarchar50Column}, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New NonModelEntityColumnData(2))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New NonModelEntityColumnData(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithoutDefinedRelationshipUsingJoinSubqueryAndExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id
      item.Nvarchar50Column = "foo"

      InsertItems(article, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ItemWithAllSupportedValues).
                                        Select(Function(x) New With {Key .IntValue = x.IntColumn, Key .StringValue = x.Nvarchar50Column})
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns).FirstOrDefault()

        Assert.AreEqual(article, result)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New NonModelEntityColumnData(2))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ItemWithAllSupportedValues).
                                        Select(Function(x) New With {Key .IntValue = x.IntColumn, Key .StringValue = x.Nvarchar50Column})
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.IntValue).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New NonModelEntityColumnData(2))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingJoinSubqueryWithJoinsThatIsUsingExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim articlePart1 = Me.ModelFactory.CreateArticlePart(1001, 1)
      Dim articlePart2 = Me.ModelFactory.CreateArticlePart(1002, 1)
      Dim articlePart1LabelEn = Me.ModelFactory.CreateLabel("", 1001, English)
      Dim articlePart2LabelEn = Me.ModelFactory.CreateLabel("", 1002, English)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, articlePart1, articlePart2, articlePart1LabelEn, articlePart2LabelEn, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim articlePartEntity = db.Model.GetEntity(articlePart1.GetType())
        Dim labelEntity = db.Model.GetEntity(articlePart1LabelEn.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ArticlePart).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns)
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.ArticleId).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T3.IntColumn).As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article, result)
        CollectionAssert.AreEqual({articlePart1, articlePart2}, result.Parts)
        Assert.IsTrue(result.Parts.All(Function(x) x.Label Is Nothing))
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(articlePartEntity), New EntityColumnData(itemEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New EntityColumnData(articlePartEntity))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim articlePartEntity = db.Model.GetEntity(articlePart1.GetType())
        Dim labelEntity = db.Model.GetEntity(articlePart1LabelEn.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Of Article).
                        Join(Function(c)
                               Return c.From(Of ArticlePart).
                                        LeftJoin(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        SelectAll()
                             End Function).
                        On(Function(j) j.T1.Id = j.T2.ArticleId).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T3.IntColumn).As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows)

        Assert.AreEqual(article, result)
        CollectionAssert.AreEqual({articlePart1, articlePart2}, result.Parts)
        Assert.IsTrue(result.Parts.All(Function(x) x.Label Is Nothing))
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(articlePartEntity), New EntityColumnData(itemEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New EntityColumnData(articlePartEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectUsingFromSubqueryWithJoinsThatIsUsingExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim labelEn = Me.ModelFactory.CreateLabel("", 1, English)
      Dim item = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues()
      item.IntColumn = article.Id

      InsertItems(article, labelEn, item)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns)
                             End Function).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.IsNull(result.Label)
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(itemEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())
        Dim itemEntity = db.Model.GetEntity(item.GetType())

        Dim result = db.From(Function(c)
                               Return c.From(Of Article).
                                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                                        SelectAll()
                             End Function).
                        Join(Of ItemWithAllSupportedValues)(Function(j) j.T1.Id = j.T2.IntColumn).As(Function(x) x.Tag).
                        SelectAll().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.IsNull(result.Label)
        Assert.AreEqual(item, result.Tag)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity), New EntityColumnData(itemEntity))
        EnsureColumnsPresence(GetSubqueryColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExcludeUsingExcludeNonRequiredColumnsBehavior()
      Dim article = Me.ModelFactory.CreateArticle(1)
      Dim labelEn = Me.ModelFactory.CreateLabel("", 1, English)

      InsertItems(article, labelEn)

      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())

        ' ensure relationship is defined
        Assert.IsNotNull(articleEntity.GetRelationshipNavigation(NameOf(article.Label)))

        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        SelectAll(SelectColumnsBehavior.ExcludeNonRequiredColumns).ExcludeT2().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.IsNull(result.Label)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using

      ' same as above, but assume behavior is not explicitly set
      Using db = CreateDbContext()
        Dim articleEntity = db.Model.GetEntity(article.GetType())
        Dim labelEntity = db.Model.GetEntity(labelEn.GetType())

        ' ensure relationship is defined
        Assert.IsNotNull(articleEntity.GetRelationshipNavigation(NameOf(article.Label)))

        Dim result = db.From(Of Article).
                        Join(Of Label)(Function(j) j.T1.Id = j.T2.Id).
                        SelectAll().ExcludeT2().FirstOrDefault()

        Assert.AreEqual(article, result)
        Assert.IsNull(result.Label)

        Dim sql = db.GetLastCommandText()

        EnsureColumnsPresence(GetColumnsPartFromQuery(sql), New EntityColumnData(articleEntity))
      End Using
    End Sub

    Private Sub EnsureColumnsPresence(sqlColumnsPart As String, ParamArray items As ColumnDataBase())
      Dim columns = sqlColumnsPart.Split(",")

      Assert.AreEqual(items.Sum(Function(x) x.GetColumnCount()), columns.Length)

      Dim i = 0

      For Each item In items
        Dim expected = item.GetExpectedColumnNames()
        Dim notExpected = item.GetNotExpectedColumnNames()

        If expected.Any() AndAlso notExpected.Any() Then
          Throw New NotSupportedException()
        End If

        If expected.Any() Then
          For Each column In expected
            Assert.IsTrue(columns(i).Contains(column))
            i += 1
          Next
        ElseIf notExpected.Any() Then
          For Each column In notExpected
            Assert.IsFalse(columns(i).Contains(column))
            i += 1
          Next
        Else
          i += item.GetColumnCount()
        End If
      Next
    End Sub

    Private Shared Function GetColumnsPartFromQuery(sql As String) As String
      Dim startToken = "SELECT"
      Dim endToken = "FROM"

      Dim startIndex = sql.IndexOf(startToken)
      Dim endIndex = sql.IndexOf(endToken)

      Return sql.Substring(startIndex + startToken.Length, endIndex - startIndex - startToken.Length)
    End Function

    Private Shared Function GetSubqueryColumnsPartFromQuery(sql As String) As String
      Dim startToken = "SELECT"
      Dim endToken = "FROM"

      Dim trimIndex = sql.IndexOf(endToken)

      sql = sql.Substring(trimIndex + endToken.Length)

      Dim startIndex = sql.IndexOf(startToken)
      Dim endIndex = sql.IndexOf(endToken)

      Return sql.Substring(startIndex + startToken.Length, endIndex - startIndex - startToken.Length)
    End Function

  End Class
End Namespace
