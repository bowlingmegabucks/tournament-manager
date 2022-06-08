namespace NewEnglandClassic.Squads.Add;

internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;    
    
    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        _config = config;
        SquadNew.TournamentId = tournamentId;

        new Presenter(config, this).GetTournamentRatios();
    }

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);    

    public void SetTournamentFinalsRatio(string ratio)
        => TextboxTournamentFinalsRatio.Text = ratio;

    public void SetTournamentCashRatio(string ratio)
        => TextboxTournamentCashRatio.Text = ratio;

    public IViewModel Squad
        => SquadNew;

    private void ButtonSave_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
