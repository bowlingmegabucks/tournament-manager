
namespace NortheastMegabuck.Models;
internal class SquadRegistration
{
    public RegistrationId RegistrationId { get; }

    public SquadId SquadId { get; }

    public Bowler Bowler { get; }

    public Division Division { get; }

    public string LaneAssignment { get; }

    public int? Average { get; }

    public int Handicap { get; }

    public SquadRegistration(Database.Entities.SquadRegistration squadRegistration)
        : this(squadRegistration, new HandicapCalculator()) { }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="squadRegistration"></param>
    /// <param name="handicapCalculator"></param>
    internal SquadRegistration(Database.Entities.SquadRegistration squadRegistration, IHandicapCalculator handicapCalculator)
    {
        RegistrationId = squadRegistration.RegistrationId;
        SquadId = squadRegistration.SquadId;

        Bowler = new Bowler(squadRegistration.Registration.Bowler);
        Division = new Division(squadRegistration.Registration.Division);

        LaneAssignment = squadRegistration.LaneAssignment;

        Average = squadRegistration.Registration.Average;
        Handicap = handicapCalculator.Calculate(squadRegistration.Registration);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal SquadRegistration()
    {
        Bowler = new Bowler();
        Division = new Division();

        LaneAssignment = string.Empty;
    }
}
