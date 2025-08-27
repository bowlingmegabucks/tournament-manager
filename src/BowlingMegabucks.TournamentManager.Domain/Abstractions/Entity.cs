
namespace BowlingMegabucks.TournamentManager.Domain.Abstractions;

/// <summary>
/// Represents a domain entity with a unique identifier.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class Entity<TId>
    where TId : struct, IEquatable<TId>
{
    /// <summary>
    /// Gets the unique identifier for this entity.
    /// </summary>
    /// <value></value>
    public TId Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id"></param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// Two entities are considered equal if they are of the same type and have the same identifier.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns>
    /// <see langword="true"/> if the specified object is equal to the current entity; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj)
        => obj is Entity<TId> other && Id.Equals(other.Id);

    /// <summary>
    /// Returns the hash code for this entity.
    /// The hash code is based on the entity's identifier to ensure consistent behavior with equality comparisons.
    /// </summary>
    /// <returns>
    /// A hash code for the current entity, suitable for use in hashing algorithms and data structures like hash tables.
    /// </returns>
    public override int GetHashCode()
        => Id.GetHashCode();
}
