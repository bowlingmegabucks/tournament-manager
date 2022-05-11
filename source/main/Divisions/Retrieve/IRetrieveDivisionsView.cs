namespace NewEnglandClassic.Divisions.Retrieve;

internal interface IView : NewEnglandClassic.IView
{
    Guid TournamentId { get; }

    void DisplayError(string message);

    void Disable();

    void BindDivisions(IEnumerable<IViewModel> divisions);
}
