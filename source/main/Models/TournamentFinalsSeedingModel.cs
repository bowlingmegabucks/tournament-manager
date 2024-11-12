
namespace NortheastMegabuck.Models;
internal class TournamentFinalsSeeding
{
    public Division Division { get; init; } = null!;

    public IEnumerable<BowlerSquadScore> Qualifiers { get; init; } = [];

    public IEnumerable<BowlerSquadScore> NonQualifiers { get; init; } = [];

    public IEnumerable<BowlerId> AtLargeCashers { get; init; } = [];
}
