
namespace NewEnglandClassic.Registrations.Retrieve;
internal class TournamentRegistrationViewModel : ITournamentRegistrationViewModel
{
    public RegistrationId Id { get; }

    public string BowlerName { get; }

    public string DivisionName { get; }

    public IEnumerable<SquadId> SquadsEntered { get; }

    public short SquadsEnteredCount
        => (short)SquadsEntered.Count();

    public IEnumerable<SquadId> SweepersEntered { get; }

    public short SweepersEnteredCount
        => (short)SweepersEntered.Count();

    public bool SuperSweeperEntered { get; }

    public TournamentRegistrationViewModel(Models.Registration registration)
    {
        Id = registration.Id;
        BowlerName = registration.Bowler.ToString();
        DivisionName = registration.Division.Name;
        SquadsEntered = registration.Squads.Select(squad => squad.Id).ToList();
        SweepersEntered = registration.Sweepers.Select(sweeper => sweeper.Id).ToList();
        SuperSweeperEntered = registration.SuperSweeper;
    }
}

internal interface ITournamentRegistrationViewModel
{
    RegistrationId Id { get; }

    string BowlerName { get; }

    string DivisionName { get; }

    IEnumerable<SquadId> SquadsEntered { get; }

    short SquadsEnteredCount { get; }

    IEnumerable<SquadId> SweepersEntered { get; }

    short SweepersEnteredCount { get; }
    bool SuperSweeperEntered { get; }
}