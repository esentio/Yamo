Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class SelectWithSetTests
    Inherits BaseIntegrationTests

    <TestMethod()>
    Public Overridable Sub SelectWithUnionUsingSubquery()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 2).
                        SelectAll().
                        Union(Function(c)
                                Return c.From(Of Article).
                                         Where(Function(x) 2 <= x.Id AndAlso x.Id <= 3).
                                         OrderBy(Function(x) x.Id).
                                         SelectAll()
                              End Function).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithUnionUsingFormattableString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 3

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 2).
                        SelectAll().
                        Union($"SELECT {Sql.Model.Columns(Of Article)} FROM Article WHERE {filterFrom} <= Id AND Id <= {filterTo} ORDER BY Id").
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithUnionUsingRawSqlString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 3

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 2).
                        SelectAll().
                        Union("SELECT {0} FROM Article WHERE {1} <= Id AND Id <= {2} ORDER BY Id", Sql.Model.Columns(Of Article), filterFrom, filterTo).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithUnionAllUsingSubquery()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 2).
                        SelectAll().
                        UnionAll(Function(c)
                                   Return c.From(Of Article).
                                            Where(Function(x) 2 <= x.Id AndAlso x.Id <= 3).
                                            OrderBy(Function(x) x.Id).
                                            SelectAll()
                                 End Function).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(1), articles(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithUnionAllUsingFormattableString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 3

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 2).
                        SelectAll().
                        UnionAll($"SELECT {Sql.Model.Columns(Of Article)} FROM Article WHERE {filterFrom} <= Id AND Id <= {filterTo} ORDER BY Id").
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(1), articles(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithUnionAllUsingRawSqlString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 3

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 2).
                        SelectAll().
                        UnionAll("SELECT {0} FROM Article WHERE {1} <= Id AND Id <= {2} ORDER BY Id", Sql.Model.Columns(Of Article), filterFrom, filterTo).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(1), articles(2)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExceptUsingSubquery()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 5).
                        SelectAll().
                        Except(Function(c)
                                 Return c.From(Of Article).
                                          Where(Function(x) 2 <= x.Id AndAlso x.Id <= 4).
                                          OrderBy(Function(x) x.Id).
                                          SelectAll()
                               End Function).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExceptUsingFormattableString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 4

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 5).
                        SelectAll().
                        Except($"SELECT {Sql.Model.Columns(Of Article)} FROM Article WHERE {filterFrom} <= Id AND Id <= {filterTo} ORDER BY Id").
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithExceptUsingRawSqlString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 4

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 5).
                        SelectAll().
                        Except("SELECT {0} FROM Article WHERE {1} <= Id AND Id <= {2} ORDER BY Id", Sql.Model.Columns(Of Article), filterFrom, filterTo).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(4)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIntersectUsingSubquery()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 5).
                        SelectAll().
                        Intersect(Function(c)
                                    Return c.From(Of Article).
                                             Where(Function(x) 2 <= x.Id AndAlso x.Id <= 4).
                                             OrderBy(Function(x) x.Id).
                                             SelectAll()
                                  End Function).
                        ToList()

        CollectionAssert.AreEqual({articles(1), articles(2), articles(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIntersectUsingFormattableString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 4

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 5).
                        SelectAll().
                        Intersect($"SELECT {Sql.Model.Columns(Of Article)} FROM Article WHERE {filterFrom} <= Id AND Id <= {filterTo} ORDER BY Id").
                        ToList()

        CollectionAssert.AreEqual({articles(1), articles(2), articles(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithIntersectUsingRawSqlString()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Dim filterFrom = 2
      Dim filterTo = 4

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id <= 5).
                        SelectAll().
                        Intersect("SELECT {0} FROM Article WHERE {1} <= Id AND Id <= {2} ORDER BY Id", Sql.Model.Columns(Of Article), filterFrom, filterTo).
                        ToList()

        CollectionAssert.AreEqual({articles(1), articles(2), articles(3)}, result)
      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub SelectWithMultipleSetOperators()
      Dim articles = CreateArticles()

      InsertItems(articles)

      Using db = CreateDbContext()
        Dim result = db.From(Of Article).
                        Where(Function(x) x.Id = 3).
                        SelectAll().
                        UnionAll(Function(c)
                                   Return c.From(Of Article).
                                            Where(Function(x) x.Id = 2).
                                            SelectAll()
                                 End Function).
                        UnionAll(Function(c)
                                   Return c.From(Of Article).
                                            Where(Function(x) x.Id = 1).
                                            OrderBy(Function(x) x.Id).
                                            SelectAll()
                                 End Function).
                        ToList()

        CollectionAssert.AreEqual({articles(0), articles(1), articles(2)}, result)
      End Using
    End Sub

    Protected Overridable Function CreateArticles() As List(Of Article)
      Return New List(Of Article) From {
        Me.ModelFactory.CreateArticle(1, 10),
        Me.ModelFactory.CreateArticle(2, 20),
        Me.ModelFactory.CreateArticle(3, 30),
        Me.ModelFactory.CreateArticle(4, 40),
        Me.ModelFactory.CreateArticle(5, 50)
      }
    End Function

  End Class
End Namespace
