using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Models;
internal class AtLargeResults
{
    public Division Division { get; init; } = null!;

    public int Entries { get; init; }

    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerId> AdvancersWhoPreviouslyCashed { get; init; } = Enumerable.Empty<BowlerId>();
}
