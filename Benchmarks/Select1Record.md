``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|                  Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |---------:|---------:|---------:|------:|--------:|-----:|-------:|------:|------:|----------:|
|                  Dapper | 112.9 μs |  0.15 μs |  0.13 μs |  0.89 |    0.00 |    1 | 1.2207 |     - |     - |   5.39 KB |
|                    Yamo | 126.6 μs |  0.74 μs |  0.62 μs |  1.00 |    0.00 |    2 | 1.7090 |     - |     - |    7.1 KB |
|    &#39;Yamo (using query)&#39; | 136.5 μs |  2.16 μs |  3.10 μs |  1.09 |    0.03 |    3 | 1.4648 |     - |     - |   6.98 KB |
| &#39;EF Core (no tracking)&#39; | 551.8 μs | 15.83 μs | 43.60 μs |  4.43 |    0.42 |    4 |      - |     - |     - |  36.45 KB |
|               &#39;EF Core&#39; | 644.1 μs | 21.24 μs | 59.21 μs |  5.31 |    0.64 |    5 |      - |     - |     - |  38.23 KB |
