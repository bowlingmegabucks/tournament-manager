namespace NortheastMegabuck.Registrations.Retrieve;
internal interface ITournamentRegistrationsView
{
    TournamentId TournamentId { get; }

    void BindRegistrations(IEnumerable<ITournamentRegistrationViewModel> registrations);

    bool Confirm(string v);

    void DisplayError(string message);

    void RemoveRegistration(RegistrationId id);

    void SetDivisionEntries(IDictionary<string, int> divisionEntries);

    void SetSquadEntries(IDictionary<string, int> squadEntries);

    void SetSweeperEntries(IDictionary<string, int> sweeperEntries);

    void BindSquadDates(IDictionary<SquadId, string> squadDates);
}
