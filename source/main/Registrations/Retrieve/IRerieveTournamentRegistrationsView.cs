namespace NortheastMegabuck.Registrations.Retrieve;
internal interface ITournamentRegistrationsView
{
    TournamentId TournamentId { get; }

    void BindRegistrations(IEnumerable<ITournamentRegistrationViewModel> registrations);

    bool Confirm(string message);

    void DisplayError(string message);

    void DisplayMessage(string message);

    void RemoveRegistration(RegistrationId id);

    void SetDivisionEntries(IDictionary<string, int> divisionEntries);

    void SetSquadEntries(IDictionary<string, int> squadEntries);

    void SetSweeperEntries(IDictionary<string, int> sweeperEntries);

    void BindSquadDates(IDictionary<SquadId, string> squadDates);
    
    string? UpdateBowlerName(BowlerId id);

    void UpdateBowlerName(string bowlerName);
    void UpdateBowlerSuperSweeper(RegistrationId id);
}
