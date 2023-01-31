Imports BenchmarkDotNet.Attributes
Imports BenchmarkDotNet.Configs
Imports BenchmarkDotNet.Engines
Imports Microsoft.Data.SqlClient

'<SimpleJob(launchCount:=1, warmupCount:=3, targetCount:=5, invocationCount:=100, id:="QuickJob")>
'<SimpleJob(1, 1, 1, 1)>
<Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)>
<RankColumn()>
<MemoryDiagnoser()>
Public MustInherit Class BenchmarkBase

  Protected Property Connection As SqlConnection

  <GlobalSetup>
  Public Sub Setup()
    Me.Connection = New SqlConnection("Server=WIN10DEV01;Database=YamoTest;User Id=dbuser;Password=dbpassword;")
    Me.Connection.Open()
  End Sub

End Class
