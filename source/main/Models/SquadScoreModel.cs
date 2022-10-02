
namespace NortheastMegabuck.Models;
internal class SquadScore
{
    public SquadId SquadId { get; init; }

    public BowlerId BowlerId { get; init; }

    public short GameNumber { get; init; }

    public int Score { get; init; }
}
