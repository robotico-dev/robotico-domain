using BenchmarkDotNet.Attributes;

namespace Robotico.Domain.Benchmarks;

/// <summary>
/// Benchmarks for <see cref="BenchmarkValueObject"/> equality and hash.
/// </summary>
[MemoryDiagnoser]
[ShortRunJob]
public sealed class ValueObjectEqualityBenchmarks
{
    private static readonly BenchmarkValueObject A = new BenchmarkValueObject { X = 1, Y = "a" };
    private static readonly BenchmarkValueObject B = new BenchmarkValueObject { X = 1, Y = "a" };
    private static readonly BenchmarkValueObject C = new BenchmarkValueObject { X = 2, Y = "b" };

    [Benchmark(Baseline = true)]
    public bool ValueObject_Equals_same_components()
    {
        return A.Equals(B);
    }

    [Benchmark]
    public bool ValueObject_Equals_different_components()
    {
        return A.Equals(C);
    }

    [Benchmark]
    public int ValueObject_GetHashCode()
    {
        return A.GetHashCode();
    }

    [Benchmark]
    public bool ValueObject_operator_equals()
    {
        return A == B;
    }
}
