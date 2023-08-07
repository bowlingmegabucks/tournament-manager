
namespace NortheastMegabuck.Divisions.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public TournamentId TournamentId { get; }

    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;
        _config = config;

        _ = new Presenter(config, this).ExecuteAsync(default);
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindDivisions(IEnumerable<IViewModel> divisions)
        => divisionsGrid.Bind(divisions);

    public void Disable()
        => addButton.Enabled = false;

    private async void AddButton_Click(object sender, EventArgs e)
        => await new Presenter(_config, this).AddDivisionAsync(default).ConfigureAwait(true);

    public NortheastMegabuck.DivisionId? AddDivision(TournamentId tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Division.Id : null;
    }

    public async Task RefreshDivisionsAsync(CancellationToken cancellationToken)
        => await new Presenter(_config, this).ExecuteAsync(cancellationToken).ConfigureAwait(true);
}
