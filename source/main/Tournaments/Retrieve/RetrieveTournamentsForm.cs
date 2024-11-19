namespace NortheastMegabuck.Tournaments.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    public Form(IConfiguration config)
    {
        InitializeComponent();

        _config = config;

        var presenter = new Presenter(config, this);

        _ = presenter.ExecuteAsync(default);
    }

    public void BindTournaments(ICollection<IViewModel> viewModels)
        => tournamentsGrid.Bind(viewModels);

    public void DisableOpenTournament()
        => openButton.Enabled = false;

    public void DisplayErrorMessage(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public (TournamentId? id, string name, short gamesPerSquad) CreateNewTournament()
    {
        using var form = new Add.Form(_config);

        return form.ShowDialog() == DialogResult.OK ? (form.Tournament.Id, form.Tournament.TournamentName, form.Tournament.Games) : (null, string.Empty, 0);
    }

    public void OpenTournament(TournamentId id, string tournamentName, short gamesPerSquad)
    {
        using var portal = new Portal.Form(_config, id, tournamentName, gamesPerSquad);

        Hide();

        portal.ShowDialog();

        Close();
    }

    private void TournamentsGrid_GridRowDoubleClicked(object sender, Controls.Grids.GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private void NewButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).NewTournament();

    private void OpenButton_Click(object sender, EventArgs e)
        => OpenTournament(tournamentsGrid.SelectedTournament!.Id, tournamentsGrid.SelectedTournament!.TournamentName, tournamentsGrid.SelectedTournament!.Games);
}
