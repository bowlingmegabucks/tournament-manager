using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NewEnglandClassic.Database;
internal class TournamentIdConverter : ValueConverter<TournamentId, Guid>
{
    public TournamentIdConverter() : base(id=> id.Value, value=> new TournamentId(value))
    {

    }
}
