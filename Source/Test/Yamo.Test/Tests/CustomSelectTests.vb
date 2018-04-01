Imports Yamo.Test.Model

Namespace Tests

  Public MustInherit Class CustomSelectTests
    Inherits TestsBase

    Protected Const English As String = "en"

    Protected Const German As String = "ger"

    <TestMethod()>
    Public Overridable Sub CustomSelect1()
      Dim item1 = Me.ModelFactory.CreateItemWithAllSupportedValuesWithEmptyValues

      InsertItems(item1)

      ' TODO: SIP - implement

      Using db = CreateDbContext()
        Dim result1 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn, Item:=x)).
                         FirstOrDefault()

        Dim result2 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) New With {x.Nvarchar50Column, x.IntColumn, .Item = x}).
                         FirstOrDefault()

        Dim result3 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) (x.Nvarchar50Column, x.IntColumn, Item:=x)).
                         ToList()

        Dim result4 = db.From(Of ItemWithAllSupportedValues).
                         Select(Function(x) New With {x.Nvarchar50Column, x.IntColumn, .Item = x}).
                         ToList()

        'Dim a = result3(0).Nvarchar50Column

        Assert.Fail()

      End Using
    End Sub

    <TestMethod()>
    Public Overridable Sub CustomSelect2()
      Dim article1 = Me.ModelFactory.CreateArticle(1)
      Dim article2 = Me.ModelFactory.CreateArticle(2)
      Dim article3 = Me.ModelFactory.CreateArticle(3)

      Dim label1En = Me.ModelFactory.CreateLabel("", 1, English)
      Dim label3En = Me.ModelFactory.CreateLabel("", 3, English)
      Dim label3Ger = Me.ModelFactory.CreateLabel("", 3, German)

      InsertItems(article1, article2, article3, label1En, label3En, label3Ger)

      Using db = CreateDbContext()
        Dim result1 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(a, l) (Price:=a.Price, Artile:=a, Label:=l))

        Dim result2 = db.From(Of Article).
                         Join(Of Label)(Function(a, l) a.Id = l.Id).
                         Select(Function(j) (Price:=j.T1.Price, Artile:=j.T1, Label:=j.T2))


        Assert.Fail()

      End Using
    End Sub

  End Class
End Namespace
