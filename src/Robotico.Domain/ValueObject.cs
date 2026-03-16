namespace Robotico.Domain;

/// <summary>
/// Base type for value objects. Equality and hashing are based on <see cref="GetEqualityComponents"/>.
/// </summary>
/// <remarks>
/// <para><b>When to use</b>: Use for domain values that are defined by their attributes (e.g. Money, Address, DateRange). Two value objects with the same components are considered equal; use <see cref="IEntity{TId}"/> for objects with identity.</para>
/// <para><b>Implementation</b>: Override <see cref="GetEqualityComponents"/> and yield the fields that define equality. Use <c>yield return</c> for clarity and avoid allocating collections.</para>
/// </remarks>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>Returns the components used for equality and hashing. Override and yield all fields that define value equality.</summary>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public bool Equals(ValueObject? other) => Equals((object?)other);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        ValueObject other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode() =>
        GetEqualityComponents()
            .Aggregate(0, (hash, component) => HashCode.Combine(hash, component?.GetHashCode() ?? 0));

    /// <summary>Equality operator. Null-safe: both null is equal; one null is not equal to non-null.</summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);
}
