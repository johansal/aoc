using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarker;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AocBenchmarks {
    [Params("treb7uchet","4nineeightseven2")]
    public string TestString { get; set; } = "";
    
    [Benchmark]
    public void Day1_Part1()
    {
        day1.Program.FindCalibrationValues_part1(TestString);
    }
    [Benchmark]
    public void Day1_Part2()
    {
        day1.Program.FindCalibrationValues_part2(TestString);
    }
}