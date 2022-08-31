using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StronglyTypedIds;

namespace NortheastMegabuck;

[StronglyTypedId]
public partial struct BowlerId { }

internal class BowlerIdValueGenerator : ValueGenerator<BowlerId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override BowlerId Next(EntityEntry entry)
        => BowlerId.New();
}

[StronglyTypedId]
public partial struct RegistrationId { }

internal class RegistrationIdValueGenerator : ValueGenerator<RegistrationId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override RegistrationId Next(EntityEntry entry)
        => RegistrationId.New();
}

[StronglyTypedId]
public partial struct SquadId { }

internal class SquadIdValueGenerator : ValueGenerator<SquadId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override SquadId Next(EntityEntry entry)
        => SquadId.New();
}

[StronglyTypedId]
public partial struct TournamentId { }

internal class TournamentIdValueGenerator : ValueGenerator<TournamentId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override TournamentId Next(EntityEntry entry)
        => TournamentId.New();
}

[StronglyTypedId]
public partial struct DivisionId { }

internal class DivisionIdValueGenerator : ValueGenerator<DivisionId>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override DivisionId Next(EntityEntry entry)
        => DivisionId.New();
}
