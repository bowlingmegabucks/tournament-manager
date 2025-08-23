namespace BowlingMegabucks.TournamentManager.Domain;

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
}
