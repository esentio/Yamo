```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26100.7623/24H2/2024Update/HudsonValley)
12th Gen Intel Core i7-12700K 3.61GHz, 1 CPU, 20 logical and 12 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v3


```
| Method                  | Mean     | Error     | StdDev    | Ratio | Rank | Gen0     | Gen1    | Allocated  | Alloc Ratio |
|------------------------ |---------:|----------:|----------:|------:|-----:|---------:|--------:|-----------:|------------:|
| Handcoded               | 1.180 ms | 0.0041 ms | 0.0038 ms |  0.92 |    1 |  31.2500 | 13.6719 |  399.64 KB |        0.99 |
| Yamo                    | 1.285 ms | 0.0064 ms | 0.0054 ms |  1.00 |    2 |  31.2500 | 13.6719 |  402.22 KB |        1.00 |
| &#39;Yamo (using query)&#39;    | 1.333 ms | 0.0045 ms | 0.0042 ms |  1.04 |    3 |  31.2500 | 13.6719 |  401.77 KB |        1.00 |
| &#39;EF Core (no tracking)&#39; | 1.380 ms | 0.0053 ms | 0.0042 ms |  1.07 |    4 |  46.8750 | 15.6250 |  639.99 KB |        1.59 |
| Dapper                  | 1.413 ms | 0.0059 ms | 0.0055 ms |  1.10 |    4 |  41.0156 | 19.5313 |  525.94 KB |        1.31 |
| &#39;EF Core&#39;               | 1.963 ms | 0.0116 ms | 0.0109 ms |  1.53 |    5 | 117.1875 | 39.0625 | 1590.19 KB |        3.95 |
