using BenchmarkDotNet.Attributes;
using Robotico.Domain;

namespace Robotico.Domain.Benchmarks;

public sealed class BenchmarkValueObject : ValueObject
{
    public int X { get; init; }
    public string Y { get; init; } = string.Empty;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class ValueObjectEqualityBenchmarks
{
    private static readonly BenchmarkValueObject A = new() { X = 1, Y = "a" };
    private static readonly BenchmarkValueObject B = new() { X = 1, Y = "a" };
    private static readonly BenchmarkValueObject C = new() { X = 2, Y = "b" };

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
