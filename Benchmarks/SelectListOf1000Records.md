``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1645 (21H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|                  Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Rank |   Gen 0 |   Gen 1 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|--------:|-----:|--------:|--------:|----------:|
|    &#39;Yamo (using query)&#39; |  3.620 ms | 0.0100 ms | 0.0089 ms |  1.00 |    0.01 |    1 | 35.1563 | 15.6250 |    194 KB |
|                    Yamo |  3.621 ms | 0.0305 ms | 0.0300 ms |  1.00 |    0.00 |    1 | 35.1563 | 11.7188 |    194 KB |
|                  Dapper |  3.663 ms | 0.0063 ms | 0.0059 ms |  1.01 |    0.01 |    2 | 62.5000 | 19.5313 |    317 KB |
| &#39;EF Core (no tracking)&#39; |  4.725 ms | 0.0807 ms | 0.1805 ms |  1.36 |    0.06 |    3 |       - |       - |    484 KB |
|               &#39;EF Core&#39; | 10.803 ms | 0.2097 ms | 0.1961 ms |  2.98 |    0.05 |    4 |       - |       - |  1,584 KB |
