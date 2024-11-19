namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal sealed class GenerateFactory
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
