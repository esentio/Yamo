``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|                  Method |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Rank |  Gen 0 | Allocated |
|------------------------ |-----------:|----------:|----------:|-----------:|------:|--------:|-----:|-------:|----------:|
|                    Yamo |   121.5 μs |   0.96 μs |   0.90 μs |   121.6 μs |  1.00 |    0.00 |    1 | 1.4648 |      7 KB |
|    &#39;Yamo (using query)&#39; |   134.5 μs |   1.29 μs |   1.20 μs |   134.1 μs |  1.11 |    0.01 |    2 | 1.4648 |      7 KB |
|                  Dapper |   161.8 μs |   7.70 μs |  21.33 μs |   151.8 μs |  1.52 |    0.22 |    3 |      - |      8 KB |
|               &#39;EF Core&#39; | 1,185.2 μs |  48.91 μs | 143.45 μs | 1,184.6 μs | 10.20 |    1.00 |    4 |      - |    111 KB |
| &#39;EF Core (no tracking)&#39; | 2,271.1 μs | 332.67 μs | 975.66 μs | 2,806.9 μs |  9.08 |    1.19 |    5 |      - |    108 KB |
