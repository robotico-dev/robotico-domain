namespace Robotico.Domain.Tests;

/// <summary>Another value object type for tests (same component shape as SampleValueObject; used to assert type must match for equality).</summary>
public sealed class OtherValueObject : ValueObject
{
    public int A { get; init; }
    public string B { get; init; } = string.Empty;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return A;
        yield return B;
    }
}
