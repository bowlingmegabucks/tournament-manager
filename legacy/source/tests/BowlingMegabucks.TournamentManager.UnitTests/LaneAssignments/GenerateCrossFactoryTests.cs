namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments;

[TestFixture]
internal sealed class GenerateFactory
{
    [Test]
    public void Execute_StaggeredSkipTrue_ReturnsStaggeredSkip()
    {
        var instance = new TournamentManager.LaneAssignments.GenerateCrossFactory().Execute(true);

        Assert.That(instance, Is.TypeOf<TournamentManager.LaneAssignments.StaggeredSkip>());
    }

    [Test]
    public void Execute_StaggeredSkipFalse_ReturnsSameSkip()
    {
        var instance = new TournamentManager.LaneAssignments.GenerateCrossFactory().Execute(false);

        Assert.That(instance, Is.TypeOf<TournamentManager.LaneAssignments.SameSkip>());
    }
}
