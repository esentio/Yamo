Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithTableSourceTests
    Inherits BaseIntegrationTests

    Protected Const English As String = "en"

    Protected Const German As String = "de"

    Protected Const ArticleArchiveTableName As String = "ArticleArchive"

    Protected Const LabelArchiveTableName As String = "LabelArchive"

    Protected Const LinkedItemArchiveTableName As String = "LinkedItem"

    <TestMethod()>
    Public Overridable Sub SelectWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)
      Dim article4 = Me.ModelFactory.CreateArticle(4)

      InsertItemsToArchive(ArticleArchiveTableName, article1, article2, article3)

      Using db = CreateDbContext()
        Dim one = 1
        Dim four = 4

        Dim result = db.From(Of Article)($"(SELECT {Sql.Model.Columns(Of Article)} FROM ArticleArchive WHERE Id < {four})").
                        Where(Function(x) one < x.Id).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({article2, article3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItemsToArchive(ArticleArchiveTableName, article1, article2, article3)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ArticleArchive)).TableName

        Dim result = db.From(Of Article)(table).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({article1, article2, article3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      InsertItemsToArchive(ArticleArchiveTableName, article1, article2, article3)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article)("(SELECT {0} FROM ArticleArchive WHERE Id < {1})", Sql.Model.Columns(Of Article), 3).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({article1, article2}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithInnerJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)($"(SELECT {Sql.Model.Columns(Of Label)} FROM LabelArchive WHERE Language = {English})").
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithInnerJoinWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)(LabelArchiveTableName).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithInnerJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Join(Of Label)("(SELECT {0} FROM LabelArchive WHERE Language = {1})", Sql.Model.Columns(Of Label), English).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithLeftOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)($"(SELECT {Sql.Model.Columns(Of Label)} FROM LabelArchive WHERE Language = {English})").
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithLeftOuterJoinWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)(LabelArchiveTableName).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(4, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithLeftOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        LeftJoin(Of Label)("(SELECT {0} FROM LabelArchive WHERE Language = {1})", Sql.Model.Columns(Of Label), English).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithRightOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        RightJoin(Of Label)($"(SELECT {Sql.Model.Columns(Of Label)} FROM LabelArchive WHERE Language = {English})").
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithRightOuterJoinWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        RightJoin(Of Label)(LabelArchiveTableName).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithRightOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        RightJoin(Of Label)("(SELECT {0} FROM LabelArchive WHERE Language = {1})", Sql.Model.Columns(Of Label), English).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(2, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithFullOuterJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        FullJoin(Of Label)($"(SELECT {Sql.Model.Columns(Of Label)} FROM LabelArchive WHERE Language = {English})").
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithFullOuterJoinWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        FullJoin(Of Label)(LabelArchiveTableName).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(4, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithFullOuterJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        FullJoin(Of Label)("(SELECT {0} FROM LabelArchive WHERE Language = {1})", Sql.Model.Columns(Of Label), English).
                        On(Function(a, l) a.Id = l.Id).
                        SelectAll().ToList()

        Assert.AreEqual(3, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label Is Nothing))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithCrossJoinWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        CrossJoin(Of Label)($"(SELECT {Sql.Model.Columns(Of Label)} FROM LabelArchive WHERE Language = {English})").
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithCrossJoinWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        CrossJoin(Of Label)(LabelArchiveTableName).
                        SelectAll().ToList()

        Assert.AreEqual(9, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = German))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithCrossJoinWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3De = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3)
      InsertItemsToArchive(LabelArchiveTableName, label1En, label3En, label3De)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        CrossJoin(Of Label)("(SELECT {0} FROM LabelArchive WHERE Language = {1})", Sql.Model.Columns(Of Label), English).
                        SelectAll().ToList()

        Assert.AreEqual(6, result.Count)
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 1 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 2 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 1 AndAlso a.Label.Language = English))
        Assert.IsNotNull(result.SingleOrDefault(Function(a) a.Id = 3 AndAlso a.Label.Id = 3 AndAlso a.Label.Language = English))
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithMaximumAllowedRelationshipsWithSpecifiedTableNameUsingRawSqlString()
      Dim linkedItem1 = Me.ModelFactory.CreateLinkedItem(1, Nothing)
      Dim linkedItem2 = Me.ModelFactory.CreateLinkedItem(2, 1)
      Dim linkedItem3 = Me.ModelFactory.CreateLinkedItem(3, 2)
      Dim linkedItem4 = Me.ModelFactory.CreateLinkedItem(4, 3)
      Dim linkedItem5 = Me.ModelFactory.CreateLinkedItem(5, 4)
      Dim linkedItem6 = Me.ModelFactory.CreateLinkedItem(6, 5)
      Dim linkedItem7 = Me.ModelFactory.CreateLinkedItem(7, 6)
      Dim linkedItem8 = Me.ModelFactory.CreateLinkedItem(8, 7)
      Dim linkedItem9 = Me.ModelFactory.CreateLinkedItem(9, 8)
      Dim linkedItem10 = Me.ModelFactory.CreateLinkedItem(10, 9)
      Dim linkedItem11 = Me.ModelFactory.CreateLinkedItem(11, 10)
      Dim linkedItem12 = Me.ModelFactory.CreateLinkedItem(12, 11)
      Dim linkedItem13 = Me.ModelFactory.CreateLinkedItem(13, 12)
      Dim linkedItem14 = Me.ModelFactory.CreateLinkedItem(14, 13)
      Dim linkedItem15 = Me.ModelFactory.CreateLinkedItem(15, 14)

      InsertItemsToArchive(Of LinkedItem)(LinkedItemArchiveTableName, linkedItem1, linkedItem2, linkedItem3, linkedItem4, linkedItem5, linkedItem6, linkedItem7, linkedItem8, linkedItem9, linkedItem10, linkedItem11, linkedItem12, linkedItem13, linkedItem14, linkedItem15)

      Using db = CreateDbContext()
        Dim result = db.From(Of LinkedItem)(LinkedItemArchiveTableName).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T1.Id = j.T2.PreviousId.Value).As(Function(x) x.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T2.Id = j.T3.PreviousId.Value).As(Function(j) j.T2.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T3.Id = j.T4.PreviousId.Value).As(Function(j) j.T3.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T4.Id = j.T5.PreviousId.Value).As(Function(j) j.T4.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T5.Id = j.T6.PreviousId.Value).As(Function(j) j.T5.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T6.Id = j.T7.PreviousId.Value).As(Function(j) j.T6.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T7.Id = j.T8.PreviousId.Value).As(Function(j) j.T7.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T8.Id = j.T9.PreviousId.Value).As(Function(j) j.T8.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T9.Id = j.T10.PreviousId.Value).As(Function(j) j.T9.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T10.Id = j.T11.PreviousId.Value).As(Function(j) j.T10.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T11.Id = j.T12.PreviousId.Value).As(Function(j) j.T11.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T12.Id = j.T13.PreviousId.Value).As(Function(j) j.T12.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T13.Id = j.T14.PreviousId.Value).As(Function(j) j.T13.NextItem).
                        LeftJoin(Of LinkedItem)(LinkedItemArchiveTableName).On(Function(j) j.T14.Id = j.T15.PreviousId.Value).As(Function(j) j.T14.NextItem).
                        OrderBy(Function(j) j.T1.Id).
                        SelectAll().ToList()

        Assert.AreEqual(15, result.Count)

        Assert.AreEqual(linkedItem1, result(0))
        Assert.AreEqual(linkedItem2, result(0).NextItem)
        Assert.AreEqual(linkedItem3, result(0).NextItem.NextItem)
        Assert.AreEqual(linkedItem4, result(0).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem5, result(0).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem6, result(0).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem7, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem8, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem9, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(0).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem2, result(1))
        Assert.AreEqual(linkedItem3, result(1).NextItem)
        Assert.AreEqual(linkedItem4, result(1).NextItem.NextItem)
        Assert.AreEqual(linkedItem5, result(1).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem6, result(1).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem7, result(1).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem8, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem9, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(1).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem3, result(2))
        Assert.AreEqual(linkedItem4, result(2).NextItem)
        Assert.AreEqual(linkedItem5, result(2).NextItem.NextItem)
        Assert.AreEqual(linkedItem6, result(2).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem7, result(2).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem8, result(2).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem9, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(2).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem4, result(3))
        Assert.AreEqual(linkedItem5, result(3).NextItem)
        Assert.AreEqual(linkedItem6, result(3).NextItem.NextItem)
        Assert.AreEqual(linkedItem7, result(3).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem8, result(3).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem9, result(3).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(3).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem5, result(4))
        Assert.AreEqual(linkedItem6, result(4).NextItem)
        Assert.AreEqual(linkedItem7, result(4).NextItem.NextItem)
        Assert.AreEqual(linkedItem8, result(4).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem9, result(4).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(4).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(4).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(4).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(4).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(4).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(4).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(4).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem6, result(5))
        Assert.AreEqual(linkedItem7, result(5).NextItem)
        Assert.AreEqual(linkedItem8, result(5).NextItem.NextItem)
        Assert.AreEqual(linkedItem9, result(5).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(5).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(5).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(5).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(5).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(5).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(5).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(5).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem7, result(6))
        Assert.AreEqual(linkedItem8, result(6).NextItem)
        Assert.AreEqual(linkedItem9, result(6).NextItem.NextItem)
        Assert.AreEqual(linkedItem10, result(6).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(6).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(6).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(6).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(6).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(6).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(6).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem8, result(7))
        Assert.AreEqual(linkedItem9, result(7).NextItem)
        Assert.AreEqual(linkedItem10, result(7).NextItem.NextItem)
        Assert.AreEqual(linkedItem11, result(7).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(7).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(7).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(7).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(7).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(7).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem9, result(8))
        Assert.AreEqual(linkedItem10, result(8).NextItem)
        Assert.AreEqual(linkedItem11, result(8).NextItem.NextItem)
        Assert.AreEqual(linkedItem12, result(8).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(8).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(8).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(8).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(8).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem10, result(9))
        Assert.AreEqual(linkedItem11, result(9).NextItem)
        Assert.AreEqual(linkedItem12, result(9).NextItem.NextItem)
        Assert.AreEqual(linkedItem13, result(9).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(9).NextItem.NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(9).NextItem.NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(9).NextItem.NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem11, result(10))
        Assert.AreEqual(linkedItem12, result(10).NextItem)
        Assert.AreEqual(linkedItem13, result(10).NextItem.NextItem)
        Assert.AreEqual(linkedItem14, result(10).NextItem.NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(10).NextItem.NextItem.NextItem.NextItem)
        Assert.IsNull(result(10).NextItem.NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem12, result(11))
        Assert.AreEqual(linkedItem13, result(11).NextItem)
        Assert.AreEqual(linkedItem14, result(11).NextItem.NextItem)
        Assert.AreEqual(linkedItem15, result(11).NextItem.NextItem.NextItem)
        Assert.IsNull(result(11).NextItem.NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem13, result(12))
        Assert.AreEqual(linkedItem14, result(12).NextItem)
        Assert.AreEqual(linkedItem15, result(12).NextItem.NextItem)
        Assert.IsNull(result(12).NextItem.NextItem.NextItem)

        Assert.AreEqual(linkedItem14, result(13))
        Assert.AreEqual(linkedItem15, result(13).NextItem)
        Assert.IsNull(result(13).NextItem.NextItem)

        Assert.AreEqual(linkedItem15, result(14))
        Assert.IsNull(result(14).NextItem)
      End Using
    End Sub

  End Class
End Namespace
