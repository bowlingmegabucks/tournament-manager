using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StronglyTypedIds;

#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
#pragma warning disable CA1036 // Override methods on structs should be used instead of operators

namespace NortheastMegabuck;

/// <summary>
/// Strongly-typed identifier for a Bowler entity.
/// </summary>
[StronglyTypedId]
public partial struct BowlerId { }

/// <summary>
/// Generates unique <see cref="BowlerId"/> values for EF Core entities.
/// </summary>
public class BowlerIdValueGenerator : ValueGenerator<BowlerId>
{
    /// <inheritdoc />
    public override bool GeneratesTemporaryValues
        => false;

    /// <summary>
    /// Generates a new <see cref="BowlerId"/> value.
    /// </summary>
    /// <param name="entry">The entity entry for which the value is being generated.</param>
    /// <returns>A new <see cref="BowlerId"/>.</returns>
    public override BowlerId Next(EntityEntry entry)
        => BowlerId.New();
}

/// <summary>
/// Strongly-typed identifier for a Registration entity.
/// </summary>
[StronglyTypedId]
public partial struct RegistrationId { }

/// <summary>
/// Generates unique <see cref="RegistrationId"/> values for EF Core entities.
/// </summary>
public class RegistrationIdValueGenerator : ValueGenerator<RegistrationId>
{
    /// <inheritdoc />
    public override bool GeneratesTemporaryValues
        => false;

    /// <summary>
    /// Generates a new <see cref="RegistrationId"/> value.
    /// </summary>
    /// <param name="entry">The entity entry for which the value is being generated.</param>
    /// <returns>A new <see cref="RegistrationId"/>.</returns>
    public override RegistrationId Next(EntityEntry entry)
        => RegistrationId.New();
}

/// <summary>
/// Strongly-typed identifier for a Squad entity.
/// </summary>
[StronglyTypedId]
public partial struct SquadId { }

/// <summary>
/// Generates unique <see cref="SquadId"/> values for EF Core entities.
/// </summary>
public class SquadIdValueGenerator : ValueGenerator<SquadId>
{
    /// <inheritdoc />
    public override bool GeneratesTemporaryValues
        => false;

    /// <summary>
    /// Generates a new <see cref="SquadId"/> value.
    /// </summary>
    /// <param name="entry">The entity entry for which the value is being generated.</param>
    /// <returns>A new <see cref="SquadId"/>.</returns>
    public override SquadId Next(EntityEntry entry)
        => SquadId.New();
}

/// <summary>
/// Strongly-typed identifier for a Tournament entity.
/// </summary>
[StronglyTypedId]
public partial struct TournamentId { }

/// <summary>
/// Generates unique <see cref="TournamentId"/> values for EF Core entities.
/// </summary>
public class TournamentIdValueGenerator : ValueGenerator<TournamentId>
{
    /// <inheritdoc />
    public override bool GeneratesTemporaryValues
        => false;

    /// <summary>
    /// Generates a new <see cref="TournamentId"/> value.
    /// </summary>
    /// <param name="entry">The entity entry for which the value is being generated.</param>
    /// <returns>A new <see cref="TournamentId"/>.</returns>
    public override TournamentId Next(EntityEntry entry)
        => TournamentId.New();
}

/// <summary>
/// Strongly-typed identifier for a Division entity.
/// </summary>
[StronglyTypedId]
public partial struct DivisionId { }

/// <summary>
/// Generates unique <see cref="DivisionId"/> values for EF Core entities.
/// </summary>
public class DivisionIdValueGenerator : ValueGenerator<DivisionId>
{
    /// <inheritdoc />
    public override bool GeneratesTemporaryValues
        => false;

    /// <summary>
    /// Generates a new <see cref="DivisionId"/> value.
    /// </summary>
    /// <param name="entry">The entity entry for which the value is being generated.</param>
    /// <returns>A new <see cref="DivisionId"/>.</returns>
    public override DivisionId Next(EntityEntry entry)
        => DivisionId.New();
}
