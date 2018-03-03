Public Class Test

  Public Shared Function GetGuidValue() As Guid
    Return Guid.NewGuid()
  End Function

  Public Function GetGuidValue2() As Guid
    Return Guid.NewGuid()
  End Function

  Public Function GetGuidValue3(value As Guid) As Guid
    Return Guid.NewGuid()
  End Function

  Public Class InnerTest
    Public Shared Function GetGuidValue() As Guid
      Return Guid.NewGuid()
    End Function

    Public Function GetGuidValue2() As Guid
      Return Guid.NewGuid()
    End Function
  End Class

  Public Sub TestWhere()
    Dim db = New MyContext()

    Dim now = DateTime.Now
    Dim aaa = "aaa"
    Dim r = 5
    Dim rn As Int32? = Nothing
    Dim it = New InnerTest

    'db.From(Of Blog).Where(Function(b) b.PublishDate < DateTime.Now)
    'db.From(Of Blog).Where(Function(b) b.PublishDate < now)
    'db.From(Of Blog).Where(Function(b) b.Title < aaa)
    'db.From(Of Blog).Where(Function(b) b.Rating < r)
    'db.From(Of Blog).Where(Function(b) b.Rating < 1234.56D)
    'db.From(Of Blog).Where(Function(b) 2 < b.Rating AndAlso b.Rating < 5)
    'db.From(Of Blog).Where(Function(b) b.Id = Guid.Empty)

    'db.From(Of Blog).Where(Function(b) b.Title = Nothing)
    'db.From(Of Blog).Where(Function(b) b.Title = "aaa")
    'db.From(Of Blog).Where(Function(b) b.Title <> "aaa")
    'db.From(Of Blog).Where(Function(b) b.Title < "aaa")
    'db.From(Of Blog).Where(Function(b) b.Title <= "aaa")
    'db.From(Of Blog).Where(Function(b) b.Title > "aaa")
    'db.From(Of Blog).Where(Function(b) b.Title >= "aaa")

    'db.From(Of Blog).Where(Function(b) b.Title.StartsWith("aaa1"))
    'db.From(Of Blog).Where(Function(b) b.Title.StartsWith(aaa))
    'db.From(Of Blog).Where(Function(b) b.Title.StartsWith(Nothing))
    'db.From(Of Blog).Where(Function(b) "aaa".StartsWith(b.Title))
    'db.From(Of Blog).Where(Function(b) b.Title.EndsWith("aaa"))
    'db.From(Of Blog).Where(Function(b) b.Title.Contains("aaa"))

    'db.From(Of Blog).Where(Function(b) b.Id = GetGuidValue3(Guid.Empty))
    'db.From(Of Blog).Where(Function(b) b.Id = it.GetGuidValue2())
    'db.From(Of Blog).Where(Function(b) b.Id = GetGuidValue2())
    'db.From(Of Blog).Where(Function(b) b.Id = InnerTest.GetGuidValue())
    'db.From(Of Blog).Where(Function(b) b.Id = GetGuidValue())
    'db.From(Of Blog).Where(Function(b) b.Id = Guid.NewGuid())

    Dim userId = 42

    'Dim x1 = db.From(Of Blog).Join(Of Comment)(Function(b, c) b.Id = c.BlogId).Where(Function(b) b.Id = Guid.NewGuid()).Select().ToList()
    'Dim x2 = db.From(Of Blog).Join(Of Comment)(Function(b, c) b.Id = c.BlogId And c.UserId = userId).Where(Function(b, c) b.Id = Guid.NewGuid() And c.Content = "").Select().ToList()
    'Dim x3 = db.From(Of Blog).CrossJoin(Of Comment).OrderBy(Function(b) b.PublishDate).ThenByDescending(Function(b, c) c.PublishDate).Select().ToList()
    'Dim y = db.From(Of Blog).Join(Of Comment)(Function(b, c) True).Where(Function(b) b.Table1.Id = Guid.NewGuid())

    'Dim x10 = db.From(Of Blog).Select().ToList()
    'Dim x11 = db.From(Of Blog).LeftJoin(Of Comment)(Function(b, c) b.Id = c.BlogId).Select().ToList()

    'Dim exp As Expressions.Expression(Of Action(Of Object, Object)) = Sub(a As Object, b As Object) DirectCast(a, Article).Label = DirectCast(b, Label)
    'Dim exp2 As Expressions.Expression(Of Action(Of Object, Object)) = Sub(a As Object, b As Object) CType(a, Article).Label = CType(b, Label)

    Dim exp As Linq.Expressions.Expression(Of Func(Of Object, Boolean)) = Function(a As Object) a Is Nothing


    Dim x12 = db.From(Of Article).
                 LeftJoin(Of Label)(Function(a, l) l.TableId = NameOf(Article) AndAlso a.Id = l.Id AndAlso l.Language = "en").
                 SelectAll().
                 ToList()

    Dim x13 = db.From(Of Article).
                 LeftJoin(Of ArticlePart)(Function(a, ap) a.Id = ap.ArticleId).
                 SelectAll().
                 ToList()


    'Dim f = Function(reader As IDataReader) As Blog
    '          Dim value = New Blog

    '          value.Id = reader.GetGuid(1)
    '          value.Title = reader.GetString(2)

    '          If reader.IsDBNull(3) Then
    '            value.ExtraRating = Nothing
    '          Else
    '            value.ExtraRating = reader.GetInt32(3)
    '          End If

    '          Return value
    '        End Function

    'Dim ff = Function(reader As IDataReader) 1
    'Dim ff As Expressions.Expression(Of Func(Of IDataReader, Blog)) = Function(reader As IDataReader) New Blog

    'Dim x = Expressions.Expression.Lambda(Function(reader As IDataReader) As Blog
    '                                        Return New Blog With {
    '                                          .Id = reader.GetGuid(1),
    '                                          .Title = reader.GetString(2)
    '                                        }
    '                                      End Function)

    'Func(Of IDataReader, Blog)

    'Dim x14 = db.ExecuteScalar(Of String)("select count(*) from article")
    'Dim x14 = db.ExecuteScalar(Of String)("select title from blog")
    'Dim x14 = db.ExecuteScalar(Of Int32?)("select extrarating from blog where extrarating is not null")


  End Sub

End Class
