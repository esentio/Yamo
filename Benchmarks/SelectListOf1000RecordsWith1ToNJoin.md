``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|                  Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Rank |     Gen 0 |    Gen 1 | Allocated |
|------------------------ |---------:|---------:|---------:|------:|--------:|-----:|----------:|---------:|----------:|
| &#39;EF Core (no tracking)&#39; | 37.28 ms | 0.368 ms | 0.492 ms |  0.72 |    0.02 |    1 |         - |        - |      3 MB |
|                    Yamo | 52.17 ms | 0.811 ms | 0.758 ms |  1.00 |    0.00 |    2 |  300.0000 | 100.0000 |      2 MB |
|                  Dapper | 52.22 ms | 0.176 ms | 0.165 ms |  1.00 |    0.02 |    2 |  400.0000 | 100.0000 |      3 MB |
|               &#39;EF Core&#39; | 77.36 ms | 0.456 ms | 0.404 ms |  1.48 |    0.02 |    3 | 1000.0000 |        - |      9 MB |
