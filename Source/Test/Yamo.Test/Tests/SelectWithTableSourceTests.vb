Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithTableSourceTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectWithSpecifiedTableNameUsingRawSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ArticleArchive)).TableName
        InsertItemsToArchive(db, table, article1, article2, article3)
      End Using

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ArticleArchive)).TableName

        Dim result = db.From(Of Article)(table).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({article1, article2, article3}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSpecifiedTableSourceUsingFormattableSqlString()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ArticleArchive)).TableName
        InsertItemsToArchive(db, table, article1, article2, article3)
      End Using

      Using db = CreateDbContext()
        Dim result = db.From(Of Article)($"(SELECT {Sql.Model.Columns(Of Article)} FROM ArticleArchive WHERE Id < 3)").
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({article1, article2}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithSpecifiedTableSourceUsingRawSqlStringWithParameters()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Using db = CreateDbContext()
        Dim table = db.Model.GetEntity(GetType(ArticleArchive)).TableName
        InsertItemsToArchive(db, table, article1, article2, article3)
      End Using

      Using db = CreateDbContext()
        ' order of columns in SELECT clause is important!
        Dim result = db.From(Of Article)("(SELECT Id, Price FROM ArticleArchive WHERE Id < {0})", 3).
                        SelectAll().ToList()

        CollectionAssert.AreEquivalent({article1, article2}, result)
      End Using
    End Sub

  End Class
End Namespace
