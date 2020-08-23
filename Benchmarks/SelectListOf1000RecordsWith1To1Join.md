``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|                  Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Rank |    Gen 0 |   Gen 1 | Gen 2 |  Allocated |
|------------------------ |----------:|----------:|----------:|------:|--------:|-----:|---------:|--------:|------:|-----------:|
|                    Yamo |  6.572 ms | 0.0617 ms | 0.0577 ms |  1.00 |    0.00 |    1 |  78.1250 | 39.0625 |     - |  464.71 KB |
|                  Dapper |  6.614 ms | 0.1257 ms | 0.1398 ms |  1.00 |    0.03 |    1 | 101.5625 | 46.8750 |     - |   607.5 KB |
| &#39;EF Core (no tracking)&#39; |  7.917 ms | 0.2396 ms | 0.6478 ms |  1.16 |    0.08 |    2 |        - |       - |     - |  832.96 KB |
|               &#39;EF Core&#39; | 31.282 ms | 1.9656 ms | 5.7957 ms |  5.70 |    0.99 |    3 |        - |       - |     - | 3141.84 KB |
