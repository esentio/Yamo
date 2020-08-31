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

End Module
