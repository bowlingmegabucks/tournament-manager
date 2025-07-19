
namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public class Registration
{
    /// <summary>
    /// 
    /// </summary>
    public RegistrationId Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Bowler Bowler { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Division Division { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? Average { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Squad> Squads { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Sweeper> Sweepers { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool SuperSweeper { get; set; }

    internal DateOnly TournamentStartDate { get; set; }

    internal int TournamentSweeperCount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowlerId"></param>
    /// <param name="divisionId"></param>
    /// <param name="squads"></param>
    /// <param name="sweepers"></param>
    /// <param name="superSweeper"></param>
    /// <param name="average"></param>
    public Registration(BowlerId bowlerId, BowlingMegabucks.TournamentManager.DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
        : this(new Bowler { Id = bowlerId }, divisionId, squads, sweepers, superSweeper, average)
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowler"></param>
    /// <param name="divisionId"></param>
    /// <param name="squads"></param>
    /// <param name="sweepers"></param>
    /// <param name="superSweeper"></param>
    /// <param name="average"></param>
    public Registration(Bowler bowler, BowlingMegabucks.TournamentManager.DivisionId divisionId, IEnumerable<SquadId> squads, IEnumerable<SquadId> sweepers, bool superSweeper, int? average)
    {
        Bowler = bowler;
        Division = new Division { Id = divisionId };

        Squads = squads.Select(squadId => new Squad { Id = squadId });
        Sweepers = sweepers.Select(sweeperId => new Sweeper { Id = sweeperId });

        Average = average;
        SuperSweeper = superSweeper;
    }

    internal Registration(Database.Entities.Registration registration)
    {
        Id = registration.Id;
        Bowler = new Bowler(registration.Bowler);
        Division = new Division(registration.Division);
        Average = registration.Average;
        Squads = registration.Squads.Select(squadRegistration => squadRegistration.Squad).OfType<Database.Entities.TournamentSquad>().Select(squad => new Squad(squad)).ToList();
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

        Squads = [];
        Sweepers = [];
    }
}
