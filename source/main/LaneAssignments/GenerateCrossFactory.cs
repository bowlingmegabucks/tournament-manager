using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.LaneAssignments;
internal class GenerateCrossFactory : IGenerateCrossFactory
{
    public IGenerate Execute(bool staggeredSkip)
        => staggeredSkip ? new StaggeredSkip() : new SameSkip();
}

internal interface IGenerateCrossFactory
{
    IGenerate Execute(bool staggeredSkip);
}