using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarker;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Base64ValidatorBenchmarks {
    private const string ValidTestStr1 = "abc1";
    private static readonly Base64Validator Validator = new();
    
    [Benchmark]
    public void ValidateTestString()
    {
        Validator.Validate_Naive(ValidTestStr1);
    }
}