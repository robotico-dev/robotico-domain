using Robotico.Domain;

namespace Robotico.Domain.Benchmarks;

/// <summary>
/// Concrete <see cref="ValueObject"/> used only in equality benchmarks.
/// </summary>
public sealed class BenchmarkValueObject : ValueObject
{
    /// <summary>First component.</summary>
    public int X { get; init; }

    /// <summary>Second component.</summary>
    public string Y { get; init; } = string.Empty;

    /// <inheritdoc />
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}
