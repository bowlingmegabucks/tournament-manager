
namespace NewEnglandClassic.Squads.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public Guid TournamentId { get; }
    
    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;

        new Presenter(_config, this).Execute();
    }

    public void BindSquads(IEnumerable<IViewModel> squads)
        => SquadsGrid.Bind(squads);

    public void Disable()
    {
        ButtonAdd.Enabled = false;
        ButtonOpen.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

    private void ButtonOpen_Click(object sender, EventArgs e)
        => MessageBox.Show("Coming soon...");

    private void SquadsGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
        => ButtonOpen_Click(sender, e);

    private void ButtonAdd_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddSquad();

    public Guid? AddSquad(Guid tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Squad.Id : null;
    }

    public void RefreshSquads()
        => new Presenter(_config, this).Execute();
}
