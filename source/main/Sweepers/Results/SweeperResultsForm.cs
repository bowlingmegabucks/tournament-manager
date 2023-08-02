
namespace NortheastMegabuck.Sweepers.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    internal string ToSpreadsheet { get; private set; }

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

        ToSpreadsheet = string.Empty;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(ICollection<IViewModel> results)
    {
        var cashers = results.Where(result => result.Casher);
        var nonCashers = results.Where(result => !result.Casher);

        var casherResultControls = cashers.Select(casher => new Controls.SweeperResultsControl(casher)).ToArray();
        var nonCasherResultControls = nonCashers.Select(nonCasher => new Controls.SweeperResultsControl(nonCasher)).ToArray();

        resultsFlowLayoutPanel.Controls.AddRange(casherResultControls);
        resultsFlowLayoutPanel.Controls.Add(new Controls.SweeperResultsControl());
        resultsFlowLayoutPanel.Controls.AddRange(nonCasherResultControls);

        var toSpreadsheet = new System.Text.StringBuilder();

        foreach (var casherControl in casherResultControls)
        {
            toSpreadsheet.AppendLine(casherControl.ToSpreadsheetRow);
        }

        foreach (var nonCasherControl in nonCasherResultControls)
        {
            toSpreadsheet.AppendLine(nonCasherControl.ToSpreadsheetRow);
        }
        
        ToSpreadsheet = toSpreadsheet.ToString();
    }

    private void CopyToClipboardLabel_Click(object sender, EventArgs e) 
        => Clipboard.SetText(ToSpreadsheet);
}
