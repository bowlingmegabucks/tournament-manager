
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace NortheastMegabuck.Tournaments.Seeding;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Dictionary<TabPage, string> _toSpreadsheet;

    private readonly Dictionary<string, IEnumerable<IViewModel>> _results = [];

    internal string ToSpreadsheet()
        => _toSpreadsheet[divisionsTabControl.SelectedTab];

    public Form(IConfiguration config, TournamentId id)
    {
        InitializeComponent();

        Id = id;

        _toSpreadsheet = [];

        _ = new Presenter(config, this).ExecuteAsync(default);
    }

    public TournamentId Id { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(string divisionName, ICollection<IViewModel> scores)
    {
        _results.Add(divisionName, scores);

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

        foreach (var score in scores.Where(s => s.Qualified))
        {
            var control = new Controls.TournamentSeedingControl(score);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        panel.Controls.Add(new Controls.TournamentSeedingControl());

        foreach (var score in scores.Where(s => !s.Qualified))
        {
            var control = new Controls.TournamentSeedingControl(score);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        _toSpreadsheet.Add(tabPage, toSpreadsheet.ToString());

        tabPage.Controls.Add(panel);
        divisionsTabControl.TabPages.Add(tabPage);
    }

    private void CopyToClipboardLabel_Click(object sender, EventArgs e)
        => Clipboard.SetText(ToSpreadsheet());

    private void FileSaveAsPdfMenuItem_Click(object sender, EventArgs e)
    {
        var report = GenerateReport();

        ResultReportBase<IViewModel>.GeneratePDF(report, "Tournament Seeding");
    }

    private MergedDocument GenerateReport()
    {
        var reports = _results.Select(result => new TournamentSeedingReport(result.Key, result.Value.ToList()));

        return Document.Merge(reports).UseOriginalPageNumbers();
    }

    private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var report = GenerateReport();

        ResultReportBase<IViewModel>.Print(report);
    }
}
