
using System.Drawing.Printing;
using QuestPDF.Fluent;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NortheastMegabuck.Sweepers.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    internal string ToSpreadsheet { get; private set; }

    private IEnumerable<IViewModel> _results = new List<IViewModel>();
    private readonly DateTime? _sweeperDate;

    public Form(IConfiguration config, SquadId squadId, DateTime sweeperDate) : this()
    {
        _sweeperDate = sweeperDate;
        _ = new Presenter(config, this).ExecuteAsync(squadId, default);
    }

    public Form(IConfiguration config, TournamentId tournamentId) : this()
    {
        _sweeperDate = null;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
        Text = "Super Sweeper Results";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        _ = new Presenter(config, this).ExecuteAsync(tournamentId, default);
    }

    private Form()
    {
        InitializeComponent();

        ToSpreadsheet = string.Empty;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(ICollection<IViewModel> results)
    {
        _results = results;

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

    private void FileSaveAsPDFMenuItem_Click(object sender, EventArgs e)
    {
        var title = _sweeperDate.HasValue ? "Sweeper Results" : "Super Sweeper Results";
        var report = new SweeperResultReport(title, _sweeperDate, _results.ToList());
        report.GeneratePDF();
    }
}
