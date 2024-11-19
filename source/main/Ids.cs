using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StronglyTypedIds;

#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"

namespace NortheastMegabuck;

[StronglyTypedId]
internal partial struct BowlerId { }

internal class BowlerIdValueGenerator : ValueGenerator<BowlerId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override BowlerId Next(EntityEntry entry)
        => BowlerId.New();
}

[StronglyTypedId]
internal partial struct RegistrationId { }

internal class RegistrationIdValueGenerator : ValueGenerator<RegistrationId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override RegistrationId Next(EntityEntry entry)
        => RegistrationId.New();
}

[StronglyTypedId]
internal partial struct SquadId { }

internal class SquadIdValueGenerator : ValueGenerator<SquadId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override SquadId Next(EntityEntry entry)
        => SquadId.New();
}

[StronglyTypedId]
internal partial struct TournamentId { }

internal class TournamentIdValueGenerator : ValueGenerator<TournamentId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override TournamentId Next(EntityEntry entry)
        => TournamentId.New();
}

[StronglyTypedId]
internal partial struct DivisionId { }

internal class DivisionIdValueGenerator : ValueGenerator<DivisionId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override DivisionId Next(EntityEntry entry)
        => DivisionId.New();
}
