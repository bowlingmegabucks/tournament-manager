using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.LaneAssignments;
internal class GenerateFactory
{
    public IGenerate Execute(bool staggeredSkip)
        => staggeredSkip ? new StaggeredSkip() : new SameSkip();
}
