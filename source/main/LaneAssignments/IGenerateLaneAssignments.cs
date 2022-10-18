
namespace NortheastMegabuck.LaneAssignments;
internal interface IGenerate
{
    IEnumerable<string> Execute(short startingLane, string letter, short games, IList<short> lanesUsed, short defaultSkip);
}
