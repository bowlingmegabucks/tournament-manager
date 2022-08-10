namespace NewEnglandClassic.Sweepers.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    
    public TournamentId TournamentId { get; }
    
    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;

        SweeperControl.Date = DateTime.Today;
        SweeperControl.TournamentId = tournamentId;

        new Presenter(_config, this).GetDivisions();
    }

    public IViewModel Sweeper
        => SweeperControl;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void Disable()
        => ButtonSave.Enabled = false;

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
        => SweeperControl.BindDivisions(divisions);

    public bool IsValid()
        => ValidateChildren();

    private void ButtonSave_Click(object sender, EventArgs e)
     => new Presenter(_config, this).Execute();
}
