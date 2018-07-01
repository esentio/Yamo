``` ini

BenchmarkDotNet=v0.10.12, OS=Windows 10.0.17134
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical cores and 4 physical cores
Frequency=2987306 Hz, Resolution=334.7498 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3110.0
  DefaultJob : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3110.0


```
|              Namespace |                      Type |                                      Method |         Mean |         Error |        StdDev |       Median | Scaled | ScaledSD | Rank |     Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|----------------------- |-------------------------- |-------------------------------------------- |-------------:|--------------:|--------------:|-------------:|-------:|---------:|-----:|----------:|---------:|---------:|-----------:|
| Yamo.Benchmarks.Dapper |           DapperBenchmark |                           &#39;Select 1 record&#39; |     106.9 us |     0.2174 us |     0.1815 us |     106.9 us |   1.00 |     0.00 |    1 |    0.9766 |        - |        - |    4.11 KB |
|   Yamo.Benchmarks.Yamo |             YamoBenchmark |                           &#39;Select 1 record&#39; |     129.1 us |     1.9922 us |     1.8635 us |     129.1 us |   1.21 |     0.02 |    2 |    1.2207 |        - |        - |    5.42 KB |
| Yamo.Benchmarks.EFCore | EFCoreNoTrackingBenchmark |                           &#39;Select 1 record&#39; |     392.8 us |     1.6377 us |     1.4518 us |     393.0 us |   3.67 |     0.01 |    3 |    4.3945 |        - |        - |   18.22 KB |
| Yamo.Benchmarks.EFCore |           EFCoreBenchmark |                           &#39;Select 1 record&#39; |     446.9 us |    12.0309 us |    14.3219 us |     440.7 us |   4.18 |     0.13 |    4 |    4.3945 |        - |        - |   19.92 KB |
|                        |                           |                                             |              |               |               |              |        |          |      |           |          |          |            |
| Yamo.Benchmarks.Dapper |           DapperBenchmark |             &#39;Select 500 records one by one&#39; |  53,162.3 us |   115.6122 us |   102.4872 us |  53,145.0 us |   1.00 |     0.00 |    1 |  500.0000 |        - |        - |  2058.7 KB |
|   Yamo.Benchmarks.Yamo |             YamoBenchmark |             &#39;Select 500 records one by one&#39; |  81,145.8 us | 1,656.9391 us | 4,833.3691 us |  80,411.4 us |   1.53 |     0.09 |    2 |  750.0000 |        - |        - | 3217.51 KB |
| Yamo.Benchmarks.EFCore | EFCoreNoTrackingBenchmark |             &#39;Select 500 records one by one&#39; | 147,334.8 us |   615.0721 us |   444.7374 us | 147,537.6 us |   2.77 |     0.01 |    3 |  937.5000 | 187.5000 |        - | 4041.07 KB |
| Yamo.Benchmarks.EFCore |           EFCoreBenchmark |             &#39;Select 500 records one by one&#39; | 153,831.1 us | 2,567.7171 us | 2,144.1599 us | 153,016.8 us |   2.89 |     0.04 |    4 | 1062.5000 | 312.5000 |        - |    4389 KB |
|                        |                           |                                             |              |               |               |              |        |          |      |           |          |          |            |
|   Yamo.Benchmarks.Yamo |             YamoBenchmark |               &#39;Select list of 1000 records&#39; |   3,775.2 us |    38.2593 us |    35.7878 us |   3,770.0 us |   0.98 |     0.01 |    1 |   35.1563 |   7.8125 |        - |  153.34 KB |
| Yamo.Benchmarks.Dapper |           DapperBenchmark |               &#39;Select list of 1000 records&#39; |   3,836.4 us |    48.3784 us |    45.2532 us |   3,831.9 us |   1.00 |     0.00 |    2 |   46.8750 |  11.7188 |        - |  222.57 KB |
| Yamo.Benchmarks.EFCore | EFCoreNoTrackingBenchmark |               &#39;Select list of 1000 records&#39; |   3,996.0 us |    14.4921 us |    13.5560 us |   3,989.9 us |   1.04 |     0.01 |    3 |   78.1250 |  23.4375 |        - |  368.62 KB |
| Yamo.Benchmarks.EFCore |           EFCoreBenchmark |               &#39;Select list of 1000 records&#39; |   6,492.9 us |    21.1705 us |    18.7671 us |   6,486.9 us |   1.69 |     0.02 |    4 |  148.4375 |  54.6875 |        - |     805 KB |
|                        |                           |                                             |              |               |               |              |        |          |      |           |          |          |            |
| Yamo.Benchmarks.Dapper |           DapperBenchmark | &#39;Select list of 1000 records with 1:1 join&#39; |   7,326.7 us |   406.1942 us |   451.4837 us |   7,139.2 us |   1.00 |     0.00 |    1 |   70.3125 |  31.2500 |        - |   395.2 KB |
|   Yamo.Benchmarks.Yamo |             YamoBenchmark | &#39;Select list of 1000 records with 1:1 join&#39; |   8,092.9 us |   250.3079 us |   734.1098 us |   7,764.5 us |   1.11 |     0.12 |    2 |   54.6875 |  23.4375 |        - |  340.94 KB |
| Yamo.Benchmarks.EFCore | EFCoreNoTrackingBenchmark | &#39;Select list of 1000 records with 1:1 join&#39; |  13,989.4 us |    34.1710 us |    31.9636 us |  13,984.9 us |   1.92 |     0.10 |    3 |  140.6250 | 125.0000 |        - |  927.11 KB |
| Yamo.Benchmarks.EFCore |           EFCoreBenchmark | &#39;Select list of 1000 records with 1:1 join&#39; |  31,426.6 us |    90.6025 us |    80.3168 us |  31,431.3 us |   4.30 |     0.23 |    4 |  375.0000 | 312.5000 |        - | 2406.77 KB |
|                        |                           |                                             |              |               |               |              |        |          |      |           |          |          |            |
| Yamo.Benchmarks.Dapper |           DapperBenchmark | &#39;Select list of 1000 records with 1:N join&#39; |  50,111.2 us |   117.0024 us |   103.7196 us |  50,082.2 us |   1.00 |     0.00 |    1 |  312.5000 | 250.0000 |        - | 2105.74 KB |
| Yamo.Benchmarks.EFCore | EFCoreNoTrackingBenchmark | &#39;Select list of 1000 records with 1:N join&#39; |  51,363.0 us | 2,304.7931 us | 2,743.6922 us |  49,783.4 us |   1.02 |     0.05 |    2 |  437.5000 | 375.0000 |  62.5000 |  2998.3 KB |
|   Yamo.Benchmarks.Yamo |             YamoBenchmark | &#39;Select list of 1000 records with 1:N join&#39; |  57,411.0 us | 1,331.9724 us | 3,843.0450 us |  56,880.1 us |   1.15 |     0.08 |    3 |  250.0000 | 125.0000 |  62.5000 |  1920.8 KB |
| Yamo.Benchmarks.EFCore |           EFCoreBenchmark | &#39;Select list of 1000 records with 1:N join&#39; | 110,265.0 us |   163.5517 us |   152.9863 us | 110,323.2 us |   2.20 |     0.01 |    4 | 1000.0000 | 937.5000 | 437.5000 |  6608.7 KB |
