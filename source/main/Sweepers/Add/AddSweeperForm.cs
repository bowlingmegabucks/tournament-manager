namespace NewEnglandClassic.Sweepers.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    
    public Guid TournamentId { get; }
    
    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;

        SweeperControl.Date = DateTime.Today;

        new Presenter(_config, this).GetDivisions();
    }

    public IViewModel Sweeper
        => SweeperControl;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void Disable()
        => ButtonSave.Enabled = false;

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
        => SweeperControl.BindDivisions(divisions);
}
