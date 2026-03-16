namespace Robotico.Domain;

/// <summary>
/// Base type for entities. Implements <see cref="IEntity{TId}"/> with equality and hashing by <see cref="Id"/>.
/// </summary>
/// <remarks>
/// <para><b>When to use</b>: Use for domain objects with identity (e.g. Order, User). Two entities with the same <see cref="Id"/> are considered equal. Prefer <see cref="ValueObject"/> when the object is defined only by its attributes.</para>
/// </remarks>
/// <typeparam name="TId">The type of the entity identifier (must be notnull).</typeparam>
public abstract class Entity<TId> : IEntity<TId>
    where TId : notnull
{
    /// <inheritdoc />
    public TId Id { get; }

    /// <summary>Initializes the entity with the given identifier.</summary>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null.</exception>
    protected Entity(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Id = id;
    }

    /// <inheritdoc />
    /// <remarks>Returns false when <paramref name="obj"/> is null; otherwise compares by <see cref="Id"/> (same Id implies equality).</remarks>
    public override bool Equals(object? obj) => obj is Entity<TId> other && Id.Equals(other.Id);

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>Equality operator. Null-safe.</summary>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
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
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);
}
