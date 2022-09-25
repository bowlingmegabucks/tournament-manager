
using NortheastMegabuck.Controls;

namespace NortheastMegabuck.Sweepers.Retrieve;
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

    public void BindSweepers(IEnumerable<IViewModel> sweepers)
        => sweepersGrid.Bind(sweepers);

    public void Disable()
    {
        addButton.Enabled = false;
        openButton.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

    private void OpenButton_Click(object sender, EventArgs e)
    {
        using var form = new Portal.Form(_config, TournamentId, sweepersGrid.SelectedSweeper!.Id);

        if (!form.IsDisposed)
        {
            Hide();

            form.ShowDialog(this);

            Close();
        }
    }

    private void SweepersGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
        => OpenButton_Click(sender, e);

    private void AddButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddSweeper();

    public SquadId? AddSweeper(TournamentId tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Sweeper.Id : null;
    }

    public void RefreshSweepers()
        => new Presenter(_config, this).Execute();
}
