Imports Yamo.Test
Imports Yamo.Test.Model

Namespace Tests

  <TestClass()>
  Public Class DataTests
    Inherits Yamo.Test.Tests.DataTests

    Protected Overrides Function CreateTestEnvironment() As ITestEnvironment
      Return SQLiteTestEnvironment.Create()
    End Function

    Protected Overrides Sub Insert(item As ItemWithAllSupportedValues)
      ' use explicit SQL rather than built-in insert support (that is tested elsewhere)

      Dim sql As FormattableString = $"
      INSERT INTO ItemWithAllSupportedValues
        (Id
        ,UniqueidentifierColumn
        ,UniqueidentifierColumnNull
        ,Nvarchar50Column
        ,Nvarchar50ColumnNull
        ,NvarcharMaxColumn
        ,NvarcharMaxColumnNull
        ,BitColumn
        ,BitColumnNull
        ,SmallintColumn
        ,SmallintColumnNull
        ,IntColumn
        ,IntColumnNull
        ,BigintColumn
        ,BigintColumnNull
        ,RealColumn
        ,RealColumnNull
        ,FloatColumn
        ,FloatColumnNull
        ,Numeric10and3Column
        ,Numeric10and3ColumnNull
        ,Numeric15and0Column
        ,Numeric15and0ColumnNull
        ,DatetimeColumn
        ,DatetimeColumnNull
        ,Varbinary50Column
        ,Varbinary50ColumnNull
        ,VarbinaryMaxColumn
        ,VarbinaryMaxColumnNull)
      VALUES
        ({item.Id}
        ,{item.UniqueidentifierColumn}
        ,{item.UniqueidentifierColumnNull}
        ,{item.Nvarchar50Column}
        ,{item.Nvarchar50ColumnNull}
        ,{item.NvarcharMaxColumn}
        ,{item.NvarcharMaxColumnNull}
        ,{item.BitColumn}
        ,{item.BitColumnNull}
        ,{item.SmallintColumn}
        ,{item.SmallintColumnNull}
        ,{item.IntColumn}
        ,{item.IntColumnNull}
        ,{item.BigintColumn}
        ,{item.BigintColumnNull}
        ,{item.RealColumn}
        ,{item.RealColumnNull}
        ,{item.FloatColumn}
        ,{item.FloatColumnNull}
        ,{item.Numeric10and3Column}
        ,{item.Numeric10and3ColumnNull}
        ,{item.Numeric15and0Column}
        ,{item.Numeric15and0ColumnNull}
        ,{item.DatetimeColumn}
        ,{item.DatetimeColumnNull}
        ,{item.Varbinary50Column}
        ,{item.Varbinary50ColumnNull}
        ,{item.VarbinaryMaxColumn}
        ,{item.VarbinaryMaxColumnNull}
      )"

      Using db = CreateDbContext()
        db.Execute(sql)
      End Using
    End Sub

  End Class
End Namespace