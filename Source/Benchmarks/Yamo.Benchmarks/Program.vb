Imports BenchmarkDotNet.Running

Module Program

  Sub Main()
    Dim benchmarks = {
      BenchmarkConverter.TypeToBenchmarks(GetType(Select1Record)),
      BenchmarkConverter.TypeToBenchmarks(GetType(Select500RecordsOneByOne)),
      BenchmarkConverter.TypeToBenchmarks(GetType(SelectListOf1000Records)),
      BenchmarkConverter.TypeToBenchmarks(GetType(SelectListOf1000RecordsWith1To1Join)),
      BenchmarkConverter.TypeToBenchmarks(GetType(SelectListOf1000RecordsWith1ToNJoin))
    }

    Dim summary = BenchmarkRunner.Run(benchmarks)

    'BenchmarkSwitcher.FromAssembly(GetType(Program).Assembly).RunAllJoined()
  End Sub

End Module
