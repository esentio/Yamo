``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|                  Method |      Mean |    Error |    StdDev | Ratio | RatioSD | Rank |     Gen 0 |    Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|---------:|----------:|------:|--------:|-----:|----------:|---------:|------:|----------:|
|                  Dapper |  56.70 ms | 0.162 ms |  0.135 ms |  0.68 |    0.02 |    1 |  555.5556 | 111.1111 |     - |   2.64 MB |
|    &#39;Yamo (using query)&#39; |  65.93 ms | 0.505 ms |  0.472 ms |  0.79 |    0.03 |    2 |  750.0000 | 125.0000 |     - |   3.31 MB |
|                    Yamo |  84.15 ms | 1.660 ms |  2.380 ms |  1.00 |    0.00 |    3 | 1000.0000 | 142.8571 |     - |    4.2 MB |
|               &#39;EF Core&#39; | 147.72 ms | 1.186 ms |  1.109 ms |  1.77 |    0.06 |    4 | 1000.0000 |        - |     - |   7.25 MB |
| &#39;EF Core (no tracking)&#39; | 154.32 ms | 4.643 ms | 13.691 ms |  1.69 |    0.13 |    5 | 1000.0000 |        - |     - |   6.78 MB |
