namespace NortheastMegabuck.Sweepers.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;

    public TournamentId TournamentId { get; }

    public Form(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        _presenter = new(this, services);
        TournamentId = tournamentId;

        newSweeper.Date = DateTime.Today;
        newSweeper.TournamentId = tournamentId;

        _ = _presenter.GetDivisionsAsync(default);
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
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
