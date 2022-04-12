Imports System.Data.Common
Imports Microsoft.Data.SqlClient

Module Program

  Public Connection As DbConnection

  Sub Main(args As String())
    Connection = New SqlConnection("Server=localhost;Database=YamoTest;User Id=dbuser;Password=dbpassword;")
    Connection.Open()

    Dim test = New Test
    test.TestWhere()

    Connection.Close()
    Connection.Dispose()
  End Sub

  Private Sub Test1()
    Using db = CreateContext()
      Dim list = db.From(Of Article).
                    Join(Function(c)
                           Return c.From(Of ArticleCategory).
                                    GroupBy(Function(x) x.ArticleId).
                                    Select(Function(x) (ArticleId:=x.ArticleId, CategoriesCount:=Yamo.Sql.Aggregate.Count()))
                         End Function).
                    On(Function(j) j.T1.Id = j.T2.ArticleId).
                    Where(Function(j) 2 < j.T2.CategoriesCount).
                    SelectAll().ToList()
    End Using
  End Sub

  Private Function CreateContext() As MyContext
    Return New MyContext()
  End Function

End Module
