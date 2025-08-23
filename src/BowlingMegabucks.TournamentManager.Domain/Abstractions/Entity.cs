namespace BowlingMegabucks.TournamentManager.Domain;

public abstract class Entity<TId>
    where TId : struct, IEquatable<TId>
{
    public TId Id { get; }

    protected Entity(TId id)
    {
        Id = id;
    }
}
