
namespace NortheastMegabuck.Sweepers.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    public Form(IConfiguration config, SquadId squadId) : this()
    {
        new Presenter(config, this).Execute(squadId);
    }

    public Form(IConfiguration config, TournamentId tournamentId) : this()
    {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
        Text = "Super Sweeper Results";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        new Presenter(config, this).Execute(tournamentId);
    }

    private Form()
    {
        InitializeComponent();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(IEnumerable<IViewModel> results)
    {
        var cashers = results.Where(result => result.Casher);
        var nonCashers = results.Where(result => !result.Casher);

        resultsFlowLayoutPanel.Controls.AddRange(cashers.Select(casher => new Controls.SweeperResultsControl(casher)).ToArray());
        resultsFlowLayoutPanel.Controls.Add(new Controls.SweeperResultsControl());
        resultsFlowLayoutPanel.Controls.AddRange(nonCashers.Select(nonCasher => new Controls.SweeperResultsControl(nonCasher)).ToArray());
    }
}
