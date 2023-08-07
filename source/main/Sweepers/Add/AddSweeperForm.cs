namespace NortheastMegabuck.Sweepers.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public TournamentId TournamentId { get; }

    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;

        newSweeper.Date = DateTime.Today;
        newSweeper.TournamentId = tournamentId;

        _ = new Presenter(_config, this).GetDivisionsAsync(default);
    }

    public IViewModel Sweeper
        => newSweeper;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void Disable()
        => saveButton.Enabled = false;

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
        => newSweeper.BindDivisions(divisions);

    public bool IsValid()
        => ValidateChildren();

    private async void SaveButton_Click(object sender, EventArgs e)
        => await new Presenter(_config, this).ExecuteAsync(default).ConfigureAwait(true);
}
