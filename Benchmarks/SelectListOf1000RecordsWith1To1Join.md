``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|                  Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Rank |   Gen 0 |   Gen 1 | Allocated |
|------------------------ |----------:|----------:|----------:|----------:|------:|--------:|-----:|--------:|--------:|----------:|
|                    Yamo |  6.113 ms | 0.0181 ms | 0.0169 ms |  6.115 ms |  1.00 |    0.00 |    1 | 70.3125 | 31.2500 |    465 KB |
|                  Dapper |  6.215 ms | 0.0151 ms | 0.0141 ms |  6.216 ms |  1.02 |    0.00 |    2 | 93.7500 | 46.8750 |    607 KB |
| &#39;EF Core (no tracking)&#39; |  7.623 ms | 0.1505 ms | 0.3272 ms |  7.571 ms |  1.26 |    0.03 |    3 |       - |       - |  1,155 KB |
|               &#39;EF Core&#39; | 19.965 ms | 0.7598 ms | 2.0540 ms | 20.705 ms |  3.58 |    0.40 |    4 |       - |       - |  3,378 KB |
