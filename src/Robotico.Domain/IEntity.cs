namespace Robotico.Domain;

/// <summary>
/// Marks an entity with an identifier of type <typeparamref name="TId"/>. Entities have identity; equality is by Id, not by attribute values.
/// </summary>
/// <remarks>
/// <para><b>When to use</b>: Use for domain objects that have a stable identity over time (e.g. Order, User, AggregateRoot). Prefer <see cref="ValueObject"/> when the object is defined only by its attributes.</para>
/// <para><b>Identity</b>: <typeparamref name="TId"/> must be non-null (e.g. <see cref="Guid"/>, <see cref="int"/>, or a value-object id type).</para>
/// </remarks>
/// <typeparam name="TId">The type of the entity identifier (must be notnull).</typeparam>
public interface IEntity<TId>
    where TId : notnull
{
    /// <summary>Gets the unique identifier of the entity.</summary>
    TId Id { get; }
}
