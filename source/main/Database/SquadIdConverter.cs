using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NewEnglandClassic.Database;
internal class SquadIdConverter : ValueConverter<SquadId, Guid>
{
    public SquadIdConverter() : base(id=> id.Value, value=> new SquadId(value))
    {

    }
}
