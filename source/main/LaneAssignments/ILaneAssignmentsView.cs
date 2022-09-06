using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.LaneAssignments;
internal interface IView
{
    SquadId SquadId { get; }

    int StartingLane { get; }

    int NumberOfLanes { get; }

    int MaxPerPair { get; }

    void BuildLanes(IEnumerable<string> lanes);
}
