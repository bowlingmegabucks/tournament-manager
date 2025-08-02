
namespace BowlingMegabucks.TournamentManager.Squads.Retrieve;
internal sealed partial class Form 
    : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;
    private readonly IServiceProvider _services;
    private readonly short _gamesPerSquad;

    public TournamentId TournamentId { get; }

    public Form(IServiceProvider services, TournamentId tournamentId, short gamesPerSquad)
    {
        InitializeComponent();

        _services = services;
        _presenter = new(this, services);
        TournamentId = tournamentId;
        _gamesPerSquad = gamesPerSquad;

        _ = _presenter.ExecuteAsync(default);
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
        using var form = new Portal.Form(_services, TournamentId, squadsGrid.SelectedSquad!.Id, _gamesPerSquad, squadsGrid.SelectedSquad.SquadDate, squadsGrid.SelectedSquad.Complete);

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
        => await _presenter.AddSquadAsync(default).ConfigureAwait(true);

    public SquadId? AddSquad(TournamentId tournamentId)
    {
        using var form = new Add.Form(_services, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Squad.Id : null;
    }

    public async Task RefreshSquadsAsync(CancellationToken cancellationToken)
        => await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(true);
}
