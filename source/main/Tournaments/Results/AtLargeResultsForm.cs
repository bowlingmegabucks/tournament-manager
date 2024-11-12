
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace NortheastMegabuck.Tournaments.Results;
internal partial class AtLarge : System.Windows.Forms.Form, IView
{
    private readonly Dictionary<TabPage, string> _toSpreadsheet;

    private readonly Dictionary<string, IEnumerable<IAtLargeViewModel>> _results = [];

    internal string ToSpreadsheet()
        => _toSpreadsheet[divisionsTabControl.SelectedTab];

    public AtLarge(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        Id = tournamentId;

        _toSpreadsheet = [];

        _ = new Presenter(config, this).AtLargeAsync(default);
    }

    public TournamentId Id { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(string divisionName, IEnumerable<IAtLargeViewModel> results)
    {
        _results.Add(divisionName, results);

        var tabPage = new TabPage($"{divisionName}TabPage")
        {
            Text = divisionName
        };

        var panel = new FlowLayoutPanel()
        {
            Name = $"{divisionName}FlowLayoutPanel",
            Dock = DockStyle.Fill,
            AutoScroll = true
        };

        var toSpreadsheet = new StringBuilder();

        foreach (var score in results)
        {
            var control = new Controls.AtLargeResultsControl(score);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        _toSpreadsheet.Add(tabPage, toSpreadsheet.ToString());

        tabPage.Controls.Add(panel);
        divisionsTabControl.TabPages.Add(tabPage);
    }

    private void CopyToClipboardLabel_Click(object sender, EventArgs e)
        => Clipboard.SetText(ToSpreadsheet());

    private void SaveAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var report = GenerateReport();

        ResultReportBase<IAtLargeViewModel>.GeneratePDF(report, "At Large Results");
    }

    private MergedDocument GenerateReport()
    {
        var reports = _results.Select(result => new AtLargeReport(result.Key, result.Value.ToList()));

        return Document.Merge(reports).UseOriginalPageNumbers();
    }

    private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var report = GenerateReport();

        ResultReportBase<IAtLargeViewModel>.Print(report);
    }
}
