namespace Robotico.Domain.Tests;

/// <summary>Sample value object for tests.</summary>
public sealed class SampleValueObject : ValueObject
{
    public int A { get; init; }
    public string B { get; init; } = string.Empty;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return A;
        yield return B;
    }
}
