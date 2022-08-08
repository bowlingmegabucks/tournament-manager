namespace NewEnglandClassic.Tournaments.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    public Form(IConfiguration config)
    {
        InitializeComponent();

        _config = config;

        var presenter = new Presenter(config, this);

        presenter.Execute();
    }

    public void BindTournaments(ICollection<IViewModel> viewModels)
        => TournamentsGrid.Bind(viewModels);

    public void DisableOpenTournament() 
        => ButtonOpenTournament.Enabled = false;

    public void DisplayErrorMessage(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    
    public (TournamentId? id, string name) CreateNewTournament()
    {
        using var form = new Add.Form(_config);

        return form.ShowDialog() == DialogResult.OK ? (form.Tournament.Id, form.Tournament.TournamentName) : (null, string.Empty);
    }
    
    public void OpenTournament(TournamentId id, string tournamentName) 
    {   
        var portal = new Portal.Form(_config, id, tournamentName);

        Hide();

        portal.ShowDialog();

        Close();
    }

    private void TournamentsGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
        => ButtonOpenTournament_Click(sender, e);

    private void ButtonNew_Click(object sender, EventArgs e)
        => new Presenter(_config, this).NewTournament();

    private void ButtonOpenTournament_Click(object sender, EventArgs e) 
        => OpenTournament(TournamentsGrid.SelectedTournament!.Id, TournamentsGrid.SelectedTournament!.TournamentName);
}
