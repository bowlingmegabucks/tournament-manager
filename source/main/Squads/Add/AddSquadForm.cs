namespace NortheastMegabuck.Squads.Add;

internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;    
    
    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        _config = config;
        
        newSquad.TournamentId = tournamentId;
        newSquad.Date = DateTime.Today;

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
        => tournamentFinalsRatioValue.Text = ratio;

    public void SetTournamentCashRatio(string ratio)
        => tournamentCashRatioValue.Text = ratio;

    public IViewModel Squad
        => newSquad;

    private void SaveButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
