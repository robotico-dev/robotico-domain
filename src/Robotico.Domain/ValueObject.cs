namespace Robotico.Domain;

/// <summary>
/// Base type for value objects. Equality and hashing are based on <see cref="GetEqualityComponents"/>.
/// </summary>
/// <remarks>
/// <para><b>When to use</b>: Use for domain values that are defined by their attributes (e.g. Money, Address, DateRange). Two value objects with the same components are considered equal; use <see cref="IEntity{TId}"/> for objects with identity.</para>
/// <para><b>Implementation</b>: Override <see cref="GetEqualityComponents"/> and yield the fields that define equality. Use <c>yield return</c> for clarity and avoid allocating collections.</para>
/// <para><b>Contract</b>: Overrides must return a stable, ordered sequence of components. Two value objects are equal if and only if their component sequences are equal in order; equal component sequences must produce the same <see cref="GetHashCode"/>. Do not mutate components after construction.</para>
/// <para><b>Null and type</b>: <see cref="Equals(object?)"/> returns false when the other instance is null or when its runtime type differs from this instance's type (exact type match, not just derived).</para>
/// </remarks>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>Returns the components used for equality and hashing. Override and yield all fields that define value equality. Must be stable and consistent with <see cref="Equals(object?)"/> and <see cref="GetHashCode"/>.</summary>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public bool Equals(ValueObject? other) => Equals((object?)other);

    /// <inheritdoc />
    /// <remarks>Returns false if <paramref name="obj"/> is null or if the runtime type of <paramref name="obj"/> is not exactly this instance's type.</remarks>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        ValueObject other = (ValueObject)obj;
        IEnumerable<object?> ours = GetEqualityComponents();
        IEnumerable<object?> theirs = other.GetEqualityComponents();
        using IEnumerator<object?> e1 = ours.GetEnumerator();
        using IEnumerator<object?> e2 = theirs.GetEnumerator();
        while (e1.MoveNext())
        {
            if (!e2.MoveNext())
            {
                return false;
            }

            if (!Equals(e1.Current, e2.Current))
            {
                return false;
            }
        }

        return !e2.MoveNext();
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        int hash = 0;
        foreach (object? component in GetEqualityComponents())
        {
            hash = HashCode.Combine(hash, component?.GetHashCode() ?? 0);
        }

        return hash;
    }

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
