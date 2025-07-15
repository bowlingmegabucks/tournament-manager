
namespace NortheastMegabuck.Divisions.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;
    private readonly IServiceProvider _services;

    public TournamentId TournamentId { get; }

    public Form(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;
        _services = services;
        _presenter = new(this, services);

        _ = _presenter.ExecuteAsync(default);
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindDivisions(IEnumerable<IViewModel> divisions)
        => divisionsGrid.Bind(divisions);

    public void Disable()
        => addButton.Enabled = false;

    private async void AddButton_Click(object sender, EventArgs e)
        => await _presenter.AddDivisionAsync(default).ConfigureAwait(true);

    public NortheastMegabuck.DivisionId? AddDivision(TournamentId tournamentId)
    {
        using var form = new Add.Form(_services, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Division.Id : null;
    }

    public async Task RefreshDivisionsAsync(CancellationToken cancellationToken)
        => await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(true);
}
