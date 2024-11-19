
namespace NortheastMegabuck.Squads.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    private readonly short _gamesPerSquad;

    public TournamentId TournamentId { get; }

    public Form(IConfiguration config, TournamentId tournamentId, short gamesPerSquad)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;
        _gamesPerSquad = gamesPerSquad;

        _ = new Presenter(_config, this).ExecuteAsync(default);
    }

    public void BindSquads(IEnumerable<IViewModel> squads)
        => squadsGrid.Bind(squads);

    public void Disable()
    {
        buttonAdd.Enabled = false;
        buttonOpen.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private void OpenButton_Click(object sender, EventArgs e)
    {
        using var form = new Portal.Form(_config, TournamentId, squadsGrid.SelectedSquad!.Id, _gamesPerSquad, squadsGrid.SelectedSquad.Date, squadsGrid.SelectedSquad.Complete);

        if (!form.IsDisposed)
        {
            Hide();

            form.ShowDialog(this);

            Close();
        }
    }

    private void SquadsGrid_GridRowDoubleClicked(object sender, Controls.Grids.GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private async void AddButton_Click(object sender, EventArgs e)
        => await new Presenter(_config, this).AddSquadAsync(default).ConfigureAwait(true);

    public SquadId? AddSquad(TournamentId tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Squad.Id : null;
    }

    public async Task RefreshSquadsAsync(CancellationToken cancellationToken)
        => await new Presenter(_config, this).ExecuteAsync(cancellationToken).ConfigureAwait(true);
}
