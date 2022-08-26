
namespace NewEnglandClassic.Models;
internal class Registration
{
    public RegistrationId Id { get; set; }

    public Bowler Bowler { get; set; }

    public Division Division { get; set; }

    public int? Average { get; set; }

    public IEnumerable<Squad> Squads { get; set; }

    public IEnumerable<Sweeper> Sweepers { get; set; }

    public bool SuperSweeper { get; set; }

    internal DateOnly TournamentStartDate { get; set; }

    internal int SweeperCount { get; set; }

    public Registration(BowlerId bowlerId, NewEnglandClassic.Divisions.Id divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
        : this(new Bowler { Id = bowlerId }, divisionId, squads, sweepers, superSweeper, average)
    { }

    public Registration(Bowler bowler, NewEnglandClassic.Divisions.Id divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
    {
        Bowler = bowler;
        Division = new Division { Id = divisionId };

        Squads = squads.Select(squadId=> new Squad { Id = squadId});
        Sweepers = sweepers.Select(sweeperId=> new Sweeper { Id = sweeperId });

        Average = average;
        SuperSweeper = superSweeper;
    }

    internal Registration(Database.Entities.Registration registration)
    {
        Id = registration.Id;
        Bowler = new Bowler(registration.Bowler);
        Division = new Division(registration.Division);
        Average = registration.Average;
        Squads = registration.Squads.Select(squadRegistration=> squadRegistration.Squad).OfType<Database.Entities.TournamentSquad>().Select(squad => new Squad(squad)).ToList();
        Sweepers = registration.Squads.Select(squadRegistration => squadRegistration.Squad).OfType<Database.Entities.SweeperSquad>().Select(sweeper => new Sweeper(sweeper)).ToList();
        SuperSweeper = registration.SuperSweeper;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal Registration()
    {
        Bowler = new Bowler();
        Division = new Division();

        Squads = Enumerable.Empty<Squad>();
        Sweepers = Enumerable.Empty<Sweeper>();
    }
}
