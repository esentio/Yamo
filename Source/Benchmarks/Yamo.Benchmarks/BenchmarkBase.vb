Imports System.Data.SqlClient
Imports BenchmarkDotNet.Attributes
Imports BenchmarkDotNet.Attributes.Columns
Imports BenchmarkDotNet.Attributes.Jobs
Imports BenchmarkDotNet.Configs
Imports BenchmarkDotNet.Engines

'<SimpleJob(launchCount:=1, warmupCount:=3, targetCount:=5, invocationCount:=100, id:="QuickJob")>
'<SimpleJob(1, 1, 1, 1)>
<RankColumn()>
<MemoryDiagnoser()>
<GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)>
<OrderProvider(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)>
Public MustInherit Class BenchmarkBase

  Protected Property Connection As SqlConnection

  Public Overridable Sub Setup()
    Connection = New SqlConnection("Server=localhost;Database=YamoTest;User Id=dbuser;Password=dbpassword;")
    Connection.Open()
  End Sub

End Class
