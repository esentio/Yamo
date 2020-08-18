Imports System.Data.SqlClient
Imports BenchmarkDotNet.Attributes
Imports BenchmarkDotNet.Configs
Imports BenchmarkDotNet.Engines
Imports Microsoft.Data.SqlClient

'<Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)>
'<SimpleJob(launchCount:=1, warmupCount:=3, targetCount:=5, invocationCount:=100, id:="QuickJob")>
'<SimpleJob(1, 1, 1, 1)>
<RankColumn()>
<MemoryDiagnoser()>
<GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)>
Public MustInherit Class BenchmarkBase

  Protected Property Connection As SqlConnection

  Public Overridable Sub Setup()
    Me.Connection = New SqlConnection("Server=localhost;Database=YamoTest;User Id=dbuser;Password=dbpassword;")
    Me.Connection.Open()
  End Sub

End Class
