``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|                  Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Rank |   Gen 0 |   Gen 1 | Gen 2 |  Allocated |
|------------------------ |----------:|----------:|----------:|----------:|------:|--------:|-----:|--------:|--------:|------:|-----------:|
|                    Yamo |  4.009 ms | 0.0392 ms | 0.0550 ms |  4.001 ms |  1.00 |    0.00 |    1 | 31.2500 |  7.8125 |     - |  193.88 KB |
|    &#39;Yamo (using query)&#39; |  4.080 ms | 0.0215 ms | 0.0201 ms |  4.079 ms |  1.01 |    0.02 |    2 | 31.2500 |  7.8125 |     - |  193.72 KB |
|                  Dapper |  4.107 ms | 0.0185 ms | 0.0173 ms |  4.114 ms |  1.02 |    0.02 |    2 | 62.5000 | 23.4375 |     - |   317.5 KB |
| &#39;EF Core (no tracking)&#39; |  4.944 ms | 0.1548 ms | 0.4539 ms |  4.949 ms |  1.34 |    0.07 |    3 |       - |       - |     - |   434.3 KB |
|               &#39;EF Core&#39; | 12.951 ms | 1.4363 ms | 4.2351 ms | 10.674 ms |  4.85 |    0.29 |    4 |       - |       - |     - | 1416.51 KB |
