
namespace BowlingMegabucks.TournamentManager.LaneAssignments;
internal interface IGenerate
{
    IEnumerable<string> Execute(short startingLane, string letter, short games, IList<short> lanesUsed, short defaultSkip);

    short DetermineSkip(int lanes);
}
