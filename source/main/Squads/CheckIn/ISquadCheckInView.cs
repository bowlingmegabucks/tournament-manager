using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Squads.CheckIn;
internal interface IView
{
    int StartingLane { get; }

    int NumberOfLanes { get; }

    int MaxPerPair { get; }

    void BuildLanes(IEnumerable<string> lanes);
}
