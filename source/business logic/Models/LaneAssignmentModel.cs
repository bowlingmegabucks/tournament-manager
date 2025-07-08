using NortheastMegabuck.Squads;

namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class LaneAssignment
{
    /// <summary>
    /// 
    /// </summary>
    public RegistrationId RegistrationId { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public SquadId SquadId { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public Bowler Bowler { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public Division Division { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public bool? SuperSweeper { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string Position { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public int? Average { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public int Handicap { get; init; }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="squadRegistration"></param>
    internal LaneAssignment(Database.Entities.SquadRegistration squadRegistration)
    {
        RegistrationId = squadRegistration.RegistrationId;
        SquadId = squadRegistration.SquadId;

        Bowler = new Bowler(squadRegistration.Registration.Bowler);
        Division = new Division(squadRegistration.Registration.Division);

        Position = squadRegistration.LaneAssignment;
        Average = squadRegistration.Registration.Average;

        Handicap = squadRegistration.Squad is Database.Entities.TournamentSquad
            ? HandicapCalculator.Calculate(squadRegistration.Registration)
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
