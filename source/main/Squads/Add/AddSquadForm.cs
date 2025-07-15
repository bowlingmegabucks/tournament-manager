namespace NortheastMegabuck.Squads.Add;

internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;

    public Form(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        _presenter = new(this, services);

        newSquad.TournamentId = tournamentId;
        newSquad.Date = DateTime.Today;

        _ = _presenter.GetTournamentDetailsAsync(default);
    }

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void SetTournamentEntryFee(string entryFee)
        => tournamentEntryFeeValue.Text = entryFee;

    public void SetTournamentFinalsRatio(string ratio)
        => tournamentFinalsRatioValue.Text = ratio;

    public void SetTournamentCashRatio(string ratio)
        => tournamentCashRatioValue.Text = ratio;

    public IViewModel Squad
        => newSquad;

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
