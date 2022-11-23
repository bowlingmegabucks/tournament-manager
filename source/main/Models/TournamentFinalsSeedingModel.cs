
namespace NortheastMegabuck.Models;
internal class TournamentFinalsSeeding
{
    public Division Division { get; init; } = null!;

    public IEnumerable<BowlerSquadScore> Qualifiers { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> NonQualifiers { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerId> AtLargeCashers { get; init; } = Enumerable.Empty<BowlerId>();
}
