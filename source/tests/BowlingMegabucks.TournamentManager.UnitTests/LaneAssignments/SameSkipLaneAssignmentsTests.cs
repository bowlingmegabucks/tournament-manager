
namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments;

[TestFixture]
internal sealed class SameSkip : Generate
{
    protected override TournamentManager.LaneAssignments.IGenerate InstanciateInterface()
        => new TournamentManager.LaneAssignments.SameSkip();

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game2()
        => "10A";

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game3()
        => "17A";

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game4()
        => "26A";

    protected override string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game5()
        => "33A";

    protected override string StartLane35_CircleBack_Skip3_Game2()
        => "4A";

    protected override string StartLane35_CircleBack_Skip3_Game3()
        => "11A";

    protected override string StartLane35_CircleBack_Skip3_Game4()
        => "20A";

    protected override string StartLane35_CircleBack_Skip3_Game5()
        => "27A";
}
