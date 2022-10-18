using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal class GenerateFactory
{
    [Test]
    public void Execute_StaggeredSkipTrue_ReturnsStaggeredSkip()
    {
        var instance = new NortheastMegabuck.LaneAssignments.GenerateCrossFactory().Execute(true);

        Assert.That(instance, Is.TypeOf<NortheastMegabuck.LaneAssignments.StaggeredSkip>());
    }

    [Test]
    public void Execute_StaggeredSkipFalse_ReturnsSameSkip()
    {
        var instance = new NortheastMegabuck.LaneAssignments.GenerateCrossFactory().Execute(false);

        Assert.That(instance, Is.TypeOf<NortheastMegabuck.LaneAssignments.SameSkip>());
    }
}
