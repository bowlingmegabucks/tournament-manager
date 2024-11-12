
namespace NortheastMegabuck.Registrations.Retrieve;
internal class TournamentRegistrationViewModel : ITournamentRegistrationViewModel
{
    public RegistrationId Id { get; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public BowlerId BowlerId { get; }

    public string BowlerName { get; set; }

    public string DivisionName { get; }

    public IEnumerable<SquadId> SquadsEntered { get; }

    public short SquadsEnteredCount
        => (short)SquadsEntered.Count();

    public IEnumerable<SquadId> SweepersEntered { get; }

    public short SweepersEnteredCount
        => (short)SweepersEntered.Count();

    public bool SuperSweeperEntered { get; set; }

    public TournamentRegistrationViewModel(Models.Registration registration)
    {
        Id = registration.Id;
        FirstName = registration.Bowler.Name.First;
        LastName = registration.Bowler.Name.Last;
        BowlerName = registration.Bowler.ToString();
        BowlerId = registration.Bowler.Id;
        DivisionName = registration.Division.Name;
        SquadsEntered = registration.Squads.Select(squad => squad.Id).ToList();
        SweepersEntered = registration.Sweepers.Select(sweeper => sweeper.Id).ToList();
        SuperSweeperEntered = registration.SuperSweeper;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal TournamentRegistrationViewModel()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        BowlerName = string.Empty;
        DivisionName = string.Empty;
        SquadsEntered = Enumerable.Empty<SquadId>();
        SweepersEntered = Enumerable.Empty<SquadId>();
    }
}

public interface ITournamentRegistrationViewModel
{
    string FirstName { get; }

    string LastName { get; }
    RegistrationId Id { get; }

    BowlerId BowlerId { get; }

    string BowlerName { get; set; }

    string DivisionName { get; }

    IEnumerable<SquadId> SquadsEntered { get; }

    short SquadsEnteredCount { get; }

    IEnumerable<SquadId> SweepersEntered { get; }

    short SweepersEnteredCount { get; }

    bool SuperSweeperEntered { get; set; }
}