namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;
internal sealed partial class Form 
    : System.Windows.Forms.Form, IView
{
    private readonly IServiceProvider _services;
    private readonly Presenter _presenter;

    public Form(IServiceProvider services)
    {
        InitializeComponent();

        _services = services;

        _presenter = new(this, services);

        _ = _presenter.ExecuteAsync(default);
    }

    public void BindTournaments(ICollection<IViewModel> viewModels)
        => tournamentsGrid.Bind(viewModels);

    public void DisableOpenTournament()
        => openButton.Enabled = false;

    public void DisplayErrorMessage(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public (TournamentId? id, string name, short gamesPerSquad) CreateNewTournament()
    {
        using var form = new Add.Form(_services);

        return form.ShowDialog() == DialogResult.OK ? (form.Tournament.Id, form.Tournament.TournamentName, form.Tournament.Games) : (null, string.Empty, 0);
    }

    public void OpenTournament(TournamentId id, string tournamentName, short gamesPerSquad)
    {
        using var portal = new Portal.Form(_services, id, tournamentName, gamesPerSquad);

        Hide();

        portal.ShowDialog();

        Close();
    }

    private void TournamentsGrid_GridRowDoubleClicked(object sender, Controls.Grids.GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private void NewButton_Click(object sender, EventArgs e)
        => _presenter.NewTournament();

    private void OpenButton_Click(object sender, EventArgs e)
        => OpenTournament(tournamentsGrid.SelectedTournament!.Id, tournamentsGrid.SelectedTournament!.TournamentName, tournamentsGrid.SelectedTournament!.Games);
}
