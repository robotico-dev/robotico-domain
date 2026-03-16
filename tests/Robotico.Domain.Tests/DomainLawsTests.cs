namespace Robotico.Domain.Tests;

/// <summary>
/// Tests that Entity and ValueObject obey equality and hashing invariants (reflexive, symmetric, transitive, GetEqualityComponents contract).
/// </summary>
public sealed class DomainLawsTests
{
    // --- Entity laws: equality by Id ---

    [Fact]
    public void Entity_equality_is_reflexive()
    {
        Guid id = Guid.NewGuid();
        SampleEntity a = new(id, "A");
        Assert.True(a.Equals(a));
#pragma warning disable CS1718, CA1508 // Comparison to same variable / dead conditional - intentional reflexivity test
        Assert.True(a == a);
#pragma warning restore CS1718, CA1508
    }

    [Fact]
    public void Entity_equality_is_symmetric()
    {
        Guid id = Guid.NewGuid();
        SampleEntity a = new(id, "A");
        SampleEntity b = new(id, "B");
        Assert.True(a.Equals(b));
        Assert.True(b.Equals(a));
        Assert.True(a == b);
        Assert.True(b == a);
    }

    [Fact]
    public void Entity_equality_is_transitive()
    {
        Guid id = Guid.NewGuid();
        SampleEntity a = new(id, "A");
        SampleEntity b = new(id, "B");
        SampleEntity c = new(id, "C");
        Assert.True(a.Equals(b));
        Assert.True(b.Equals(c));
        Assert.True(a.Equals(c));
        Assert.True(a == b && b == c && a == c);
    }

    [Fact]
    public void Entity_equal_instances_have_equal_GetHashCode()
    {
        Guid id = Guid.NewGuid();
        SampleEntity a = new(id, "A");
        SampleEntity b = new(id, "B");
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // --- ValueObject laws: equality by GetEqualityComponents ---

    [Fact]
    public void ValueObject_equality_is_reflexive()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        Assert.True(a.Equals(a));
#pragma warning disable CS1718, CA1508 // Comparison to same variable / dead conditional - intentional reflexivity test
        Assert.True(a == a);
#pragma warning restore CS1718, CA1508
    }

    [Fact]
    public void ValueObject_equality_is_symmetric()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        SampleValueObject b = new() { A = 1, B = "x" };
        Assert.True(a.Equals(b));
        Assert.True(b.Equals(a));
        Assert.True(a == b);
        Assert.True(b == a);
    }

    [Fact]
    public void ValueObject_equality_is_transitive()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        SampleValueObject b = new() { A = 1, B = "x" };
        SampleValueObject c = new() { A = 1, B = "x" };
        Assert.True(a.Equals(b));
        Assert.True(b.Equals(c));
        Assert.True(a.Equals(c));
        Assert.True(a == b && b == c && a == c);
    }

    [Fact]
    public void ValueObject_same_components_imply_equal_and_same_GetHashCode()
    {
        SampleValueObject a = new() { A = 2, B = "y" };
        SampleValueObject b = new() { A = 2, B = "y" };
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ValueObject_different_components_imply_not_equal()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        SampleValueObject b = new() { A = 1, B = "z" };
        SampleValueObject c = new() { A = 2, B = "x" };
        Assert.NotEqual(a, b);
        Assert.NotEqual(a, c);
        Assert.False(a == b);
        Assert.False(a == c);
    }

    /// <summary>Equality requires exact runtime type match; same components but different derived type ⇒ not equal.</summary>
    [Fact]
    public void ValueObject_different_runtime_type_not_equal_even_with_same_components()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        OtherValueObject other = new() { A = 1, B = "x" };
        Assert.False(a.Equals(other));
        Assert.False(a == other);
        Assert.False(other.Equals(a));
    }

    [Fact]
    public void ValueObject_Equals_null_returns_false()
    {
        SampleValueObject a = new() { A = 1, B = "x" };
        Assert.False(a.Equals((ValueObject?)null));
        Assert.False(a.Equals((object?)null));
    }
}
