
namespace NewEnglandClassic.Sweepers.Retrieve;
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

    public void BindSweepers(IEnumerable<IViewModel> sweepers)
        => SweepersGrid.Bind(sweepers);

    public void Disable()
    {
        ButtonAdd.Enabled = false;
        ButtonOpen.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

    private void ButtonOpen_Click(object sender, EventArgs e)
        => MessageBox.Show("Coming soon...");

    private void SweepersGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
        => ButtonOpen_Click(sender, e);

    private void ButtonAdd_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddSweeper();

    public Guid? AddSweeper(Guid tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Sweeper.Id : null;
    }

    public void RefreshSweepers()
        => new Presenter(_config, this).Execute();
}
