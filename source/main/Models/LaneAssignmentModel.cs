namespace NortheastMegabuck.Models;
internal class LaneAssignment
{
    public RegistrationId RegistrationId { get; init; }

    public SquadId SquadId { get; init; }

    public Bowler Bowler { get; init; }

    public Division Division { get; init; }

    public bool? SuperSweeper { get; init; }

    public string Position { get; init; }

    public int? Average { get; init; }

    public int Handicap { get; init; }

    public LaneAssignment(Database.Entities.SquadRegistration squadRegistration)
        : this(squadRegistration, new HandicapCalculator()) { }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="squadRegistration"></param>
    /// <param name="handicapCalculator"></param>
    internal LaneAssignment(Database.Entities.SquadRegistration squadRegistration, IHandicapCalculator handicapCalculator)
    {
        RegistrationId = squadRegistration.RegistrationId;
        SquadId = squadRegistration.SquadId;

        Bowler = new Bowler(squadRegistration.Registration.Bowler);
        Division = new Division(squadRegistration.Registration.Division);

        Position = squadRegistration.LaneAssignment;
        Average = squadRegistration.Registration.Average;

        Handicap = squadRegistration.Squad is Database.Entities.TournamentSquad
            ? handicapCalculator.Calculate(squadRegistration.Registration)
            : ((squadRegistration.Squad as Database.Entities.SweeperSquad)!.Divisions.SingleOrDefault(division => division.DivisionId == Division.Id)?.BonusPinsPerGame).GetValueOrDefault(0);

        SuperSweeper = squadRegistration.Squad is Database.Entities.SweeperSquad
            ? squadRegistration.Registration.SuperSweeper : null;

    }

    /// <summary>
    /// Unit Test Model Constructor
    /// </summary>
    internal LaneAssignment()
    {
        Bowler = new Bowler();
        Division = new Division();
        Position = string.Empty;
    }
}
