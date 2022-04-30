namespace NewEnglandClassic.Tournaments.Retrieve;

internal interface IView
{
    void DisplayErrorMessage(string message);

    void DisableOpenTournament();
    
    void BindTournaments(ICollection<IViewModel> viewModels);

    Guid? CreateNewTournament();

    void OpenTournament(Guid id);
}
