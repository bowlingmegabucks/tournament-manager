
namespace NortheastMegabuck.Squads.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public TournamentId TournamentId { get; }
    
    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;

        new Presenter(_config, this).Execute();
    }

    public void BindSquads(IEnumerable<IViewModel> squads)
        => squadsGrid.Bind(squads);

    public void Disable()
    {
        buttonAdd.Enabled = false;
        buttonOpen.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

    private void OpenButton_Click(object sender, EventArgs e)
        => MessageBox.Show("Coming soon...");

    private void SquadsGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private void AddButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddSquad();

    public SquadId? AddSquad(TournamentId tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Squad.Id : null;
    }

    public void RefreshSquads()
        => new Presenter(_config, this).Execute();
}
