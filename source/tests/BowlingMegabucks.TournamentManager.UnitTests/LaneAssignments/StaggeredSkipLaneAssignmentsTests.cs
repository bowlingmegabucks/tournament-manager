
namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments;

[TestFixture]
internal sealed class Staggered : Generate
{
    protected override TournamentManager.LaneAssignments.IGenerate InstanciateInterface()
        => new TournamentManager.LaneAssignments.StaggeredSkip();

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game2()
        => "10A";

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game3()
        => "19A";

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game4()
        => "26A";

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game5()
        => "33A";

    protected override string StartLane35_CircleBack_Skip3_Game2()
        => "4A";

    protected override string StartLane35_CircleBack_Skip3_Game3()
        => "13A";

    protected override string StartLane35_CircleBack_Skip3_Game4()
        => "20A";

    protected override string StartLane35_CircleBack_Skip3_Game5()
        => "27A";
}
