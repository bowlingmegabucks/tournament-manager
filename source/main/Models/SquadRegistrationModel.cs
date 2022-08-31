
namespace NortheastMegabuck.Models;
internal class SquadRegistration
{
    public RegistrationId RegistrationId { get; }

    public SquadId SquadId { get; }

    public Bowler Bowler { get; }

    public Division Division { get; }

    public string LaneAssignment { get; }

    public SquadRegistration(Database.Entities.SquadRegistration squadRegistration)
    {
        RegistrationId = squadRegistration.RegistrationId;
        SquadId = squadRegistration.SquadId;

        Bowler = new Bowler(squadRegistration.Registration.Bowler);
        Division = new Division(squadRegistration.Registration.Division);

        LaneAssignment = squadRegistration.LaneAssignment;
    }
}
