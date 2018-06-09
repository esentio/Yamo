Public Class Test

  Public Shared Function GetInt32Value() As Int32
    Return 1
  End Function

  Public Function GetInt32Value2() As Int32
    Return 2
  End Function

  Public Function GetInt32Value3(value As Int32) As Int32
    Return 3
  End Function

  Public Class InnerTest
    Public Shared Function GetInt32Value() As Int32
      Return 4
    End Function

    Public Function GetInt32Value2() As Int32
      Return 5
    End Function
  End Class

  Public Sub TestWhere()
    Dim db = New MyContext()

    Dim now = DateTime.Now
    Dim aaa = "aaa"
    Dim r = 5
    Dim it = New InnerTest

    db.From(Of Blog).Where(Function(b) b.Rating = r)

    db.From(Of Blog).Where(Function(b) b.Rating = GetInt32Value())
    db.From(Of Blog).Where(Function(b) b.Rating = Test.GetInt32Value())

    db.From(Of Blog).Where(Function(b) b.Rating = GetInt32Value2())
    db.From(Of Blog).Where(Function(b) b.Rating = Me.GetInt32Value2())

    db.From(Of Blog).Where(Function(b) b.Rating = it.GetInt32Value2())


    'db.From(Of Blog).Where(Function(b) b.PublishDate < DateTime.Now)
    'db.From(Of Blog).Where(Function(b) b.PublishDate < now)
    'db.From(Of Blog).Where(Function(b) b.Title < aaa)
    'db.From(Of Blog).Where(Function(b) b.Rating < r)
    'db.From(Of Blog).Where(Function(b) b.Rating < 1234.56D)
    'db.From(Of Blog).Where(Function(b) 2 < b.Rating AndAlso b.Rating < 5)
    'db.From(Of Blog).Where(Function(b) b.Id = Guid.Empty)

  End Sub

End Class
