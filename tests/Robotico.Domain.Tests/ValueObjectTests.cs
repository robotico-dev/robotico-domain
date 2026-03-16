namespace Robotico.Domain.Tests;

/// <summary>ValueObject equality, GetHashCode, and null-safe operators.</summary>
public sealed class ValueObjectTests
{
    [Fact]
    public void ValueObject_equality_based_on_components()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        SampleValueObject b = new() { A = 1, B = "x" };
        SampleValueObject c = new() { A = 2, B = "x" };
        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void ValueObject_equal_components_have_equal_GetHashCode()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        SampleValueObject b = new() { A = 1, B = "x" };
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ValueObject_equality_operator_both_null_returns_true()
    {
        SampleValueObject? left = null;
        SampleValueObject? right = null;
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void ValueObject_equality_operator_left_null_returns_false()
    {
        SampleValueObject? left = null;
        SampleValueObject right = new() { A = 1, B = "x" };
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void ValueObject_equality_operator_right_null_returns_false()
    {
        SampleValueObject left = new() { A = 1, B = "x" };
        SampleValueObject? right = null;
        Assert.False(left == right);
        Assert.True(left != right);
    }
}
