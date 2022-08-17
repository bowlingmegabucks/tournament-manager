
namespace NewEnglandClassic.Models;
internal class Registration
{
    public RegistrationId Id { get; set; }

    public Bowler Bowler { get; set; }

    public Division Division { get; set; }

    public int? Average { get; set; }

    public IEnumerable<SquadId> Squads { get; set; }

    public IEnumerable<SquadId> Sweepers { get; set; }

    public bool SuperSweeper { get; set; }

    internal DateOnly TournamentStartDate { get; set; }

    internal int SweeperCount { get; set; }

    public Registration(BowlerId bowlerId, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
        : this(new Bowler { Id = bowlerId }, divisionId, squads, sweepers, superSweeper, average)
    { }

    public Registration(Bowler bowler, DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
    {
        Bowler = bowler;
        Division = new Division { Id = divisionId };

        Squads = squads;
        Sweepers = sweepers;

        Average = average;
        SuperSweeper = superSweeper;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Registration()
    {
        Bowler = new Bowler();
        Division = new Division();

        Squads = Enumerable.Empty<SquadId>();
        Sweepers = Enumerable.Empty<SquadId>();
    }
}
