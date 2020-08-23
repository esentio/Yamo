``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|                  Method |      Mean |    Error |   StdDev |    Median | Ratio | RatioSD | Rank |     Gen 0 |    Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|---------:|---------:|----------:|------:|--------:|-----:|----------:|---------:|------:|----------:|
| &#39;EF Core (no tracking)&#39; |  49.69 ms | 1.301 ms | 3.755 ms |  48.45 ms |  0.86 |    0.05 |    1 |         - |        - |     - |   2.75 MB |
|                  Dapper |  56.50 ms | 0.907 ms | 0.804 ms |  56.24 ms |  0.98 |    0.02 |    2 |  444.4444 | 222.2222 |     - |   2.96 MB |
|                    Yamo |  57.47 ms | 0.775 ms | 0.605 ms |  57.36 ms |  1.00 |    0.00 |    3 |  333.3333 | 111.1111 |     - |   2.45 MB |
|               &#39;EF Core&#39; | 103.01 ms | 1.944 ms | 2.080 ms | 102.48 ms |  1.80 |    0.05 |    4 | 1000.0000 |        - |     - |   8.17 MB |
