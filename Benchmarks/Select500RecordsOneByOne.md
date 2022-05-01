``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|                  Method |      Mean |    Error |    StdDev |    Median | Ratio | RatioSD | Rank |     Gen 0 |    Gen 1 | Allocated |
|------------------------ |----------:|---------:|----------:|----------:|------:|--------:|-----:|----------:|---------:|----------:|
|                  Dapper |  58.22 ms | 0.485 ms |  0.430 ms |  58.24 ms |  0.67 |    0.01 |    1 |         - |        - |      3 MB |
|    &#39;Yamo (using query)&#39; |  67.72 ms | 0.771 ms |  0.721 ms |  67.91 ms |  0.77 |    0.01 |    2 |  750.0000 | 125.0000 |      3 MB |
|                    Yamo |  87.51 ms | 0.810 ms |  0.796 ms |  87.51 ms |  1.00 |    0.00 |    3 | 1000.0000 | 166.6667 |      4 MB |
|               &#39;EF Core&#39; | 140.61 ms | 2.730 ms |  2.804 ms | 139.92 ms |  1.61 |    0.04 |    4 | 1000.0000 |        - |      6 MB |
| &#39;EF Core (no tracking)&#39; | 141.10 ms | 4.317 ms | 12.730 ms | 134.63 ms |  1.66 |    0.18 |    5 | 1000.0000 |        - |      5 MB |
