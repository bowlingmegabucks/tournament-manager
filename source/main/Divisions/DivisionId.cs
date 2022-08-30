using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StronglyTypedIds;

namespace NortheastMegabuck.Divisions;

[StronglyTypedId]
public partial struct Id { }

internal class IdValueGenerator : ValueGenerator<Id>
{
    public override bool GeneratesTemporaryValues
        => false;

    public override Id Next(EntityEntry entry)
        => Id.New();
}