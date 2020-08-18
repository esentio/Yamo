Imports BenchmarkDotNet.Running
Imports Microsoft.Data.SqlClient

Module Program

  Sub Main()
    'CreateData()

    'Dim benchmarks = {
    '  BenchmarkConverter.TypeToBenchmarks(GetType(Dapper.DapperBenchmark)),
    '  BenchmarkConverter.TypeToBenchmarks(GetType(EFCore.EFCoreBenchmark)),
    '  BenchmarkConverter.TypeToBenchmarks(GetType(EFCore.EFCoreNoTrackingBenchmark)),
    '  BenchmarkConverter.TypeToBenchmarks(GetType(Yamo.YamoBenchmark))
    '}

    'Dim summary = BenchmarkRunner.Run(benchmarks)

    'BenchmarkSwitcher.FromAssembly(GetType(Program).Assembly).RunAllJoined()

    Dim benchmarks = {
      GetType(Dapper.DapperBenchmark),
      GetType(EFCore.EFCoreBenchmark),
      GetType(EFCore.EFCoreNoTrackingBenchmark),
      GetType(Yamo.YamoBenchmark)
    }

    BenchmarkSwitcher.FromTypes(benchmarks).RunAllJoined()
  End Sub

  Private Sub CreateData()
    ' TODO: refactor to do proper initialization

    Dim connection = New SqlConnection("Server=localhost;Database=YamoTest;User Id=dbuser;Password=dbpassword;")
    connection.Open()

    Using db = New Yamo.YamoDbContext(connection)
      Dim now = DateTime.Now

      Dim k = 1

      For i = 1 To 1000
        Dim blog = New Model.Blog With {
            .Id = i,
            .Title = $"My awesome blog {i}",
            .Content = $"Lorem ipsum dolor sit amet {i}.",
            .Created = now.AddHours(i),
            .CreatedUserId = i,
            .Modified = now.AddHours(i * 2),
            .ModifiedUserId = i,
            .Deleted = Nothing,
            .DeletedUserId = Nothing
        }

        db.Insert(blog, useDbIdentityAndDefaults:=False)

        Dim user = New Model.User With {
            .Id = i,
            .Login = $"login{i}",
            .FirstName = $"first name {i}.",
            .LastName = $"last name {i}.",
            .Email = $"mail{i}@example.com"
        }

        db.Insert(user, useDbIdentityAndDefaults:=False)

        For j = 1 To 5
          Dim comment = New Model.Comment With {
            .Id = k,
            .BlogId = i,
            .Content = $"Lorem ipsum dolor sit amet {k}.",
            .Created = now.AddHours(k),
            .CreatedUserId = i
          }

          db.Insert(comment, useDbIdentityAndDefaults:=False)

          k += 1
        Next
      Next
    End Using

  End Sub

End Module
