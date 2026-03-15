using Robotico.Domain;
using Xunit;

namespace Robotico.Domain.Tests;

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

public sealed class DomainTests
{
    [Fact]
    public void ValueObject_equality_based_on_components()
    {
        var a = new SampleValueObject { A = 1, B = "x" };
        var b = new SampleValueObject { A = 1, B = "x" };
        var c = new SampleValueObject { A = 2, B = "x" };
        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.True(a == b);
        Assert.False(a != b);
    }
}
