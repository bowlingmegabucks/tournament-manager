namespace NortheastMegabuck.Sweepers.Retrieve;

internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;
    private readonly IServiceProvider _services;

    public TournamentId TournamentId { get; }

    public Form(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        _presenter = new(this, services);
        _services = services;
        TournamentId = tournamentId;

        _ = _presenter.ExecuteAsync(default);
    }

    public void BindSweepers(IEnumerable<IViewModel> squads)
        => sweepersGrid.Bind(squads);

    public void Disable()
    {
        addButton.Enabled = false;
        openButton.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private void OpenButton_Click(object sender, EventArgs e)
    {
        using var form = new Portal.Form(_services, TournamentId, sweepersGrid.SelectedSweeper!.Id, sweepersGrid.SelectedSweeper.Games, sweepersGrid.SelectedSweeper.Date, sweepersGrid.SelectedSweeper.Complete);

        if (!form.IsDisposed)
        {
            Hide();

            form.ShowDialog(this);

            Close();
        }
    }

    private void SweepersGrid_GridRowDoubleClicked(object sender, Controls.Grids.GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private async void AddButton_Click(object sender, EventArgs e)
        => await _presenter.AddSweeperAsync(default).ConfigureAwait(true);

    public SquadId? AddSweeper(TournamentId tournamentId)
    {
        using var form = new Add.Form(_services, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Sweeper.Id : null;
    }

    public async Task RefreshSweepersAsync(CancellationToken cancellationToken)
        => await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(true);
}
