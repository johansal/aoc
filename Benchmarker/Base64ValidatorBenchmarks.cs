using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarker;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Base64ValidatorBenchmarks {
    [Params("asdGHJ987=","asd^IUY02+", "asdGHJ987=iuy816OIP+nkwSOQ015/asdGHJ987=iuy816OIP+nkwSOQ015/")]
    public string TestString { get; set; } = "";
    private static readonly Base64Validator Validator = new();
    
    [Benchmark]
    public void Validate_Naive()
    {
        Validator.Validate_Naive(TestString);
    }
    [Benchmark]
    public void Validate_Span()
    {
        Validator.Validate_Span(TestString);
    }
    [Benchmark]
    public void Validate()
    {
        Base64Validator.Validate(TestString);
    }
}