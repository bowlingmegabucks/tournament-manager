namespace BowlingMegabucks.TournamentManager.Tests.LaneAssignments;

[TestFixture]
internal sealed class GenerateFactory
{
    [Test]
    public void Execute_StaggeredSkipTrue_ReturnsStaggeredSkip()
    {
        var instance = new BowlingMegabucks.TournamentManager.LaneAssignments.GenerateCrossFactory().Execute(true);

        Assert.That(instance, Is.TypeOf<BowlingMegabucks.TournamentManager.LaneAssignments.StaggeredSkip>());
    }

    [Test]
    public void Execute_StaggeredSkipFalse_ReturnsSameSkip()
    {
        var instance = new BowlingMegabucks.TournamentManager.LaneAssignments.GenerateCrossFactory().Execute(false);

        Assert.That(instance, Is.TypeOf<BowlingMegabucks.TournamentManager.LaneAssignments.SameSkip>());
    }
}
