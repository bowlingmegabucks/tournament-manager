namespace BowlingMegabucks.TournamentManager.Divisions.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;

    public Form(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        _presenter = new(this, services);

        Division.TournamentId = tournamentId;

        _ = _presenter.GetNextDivisionNumberAsync(default);

        newDivision.Focus();
    }

    public bool IsValid()
        => ValidateChildren();

    public IViewModel Division
        => newDivision;

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void DisplayErrors(IEnumerable<string> errorMessages)
        => MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
