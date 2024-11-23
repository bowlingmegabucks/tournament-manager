namespace NortheastMegabuck.Tournaments.Retrieve;

internal interface IView
{
    void DisplayErrorMessage(string message);

    void DisableOpenTournament();

    void BindTournaments(ICollection<IViewModel> viewModels);

    (TournamentId? id, string name, short gamesPerSquad) CreateNewTournament();

    void OpenTournament(TournamentId id, string tournamentName, short gamesPerSquad);
}
