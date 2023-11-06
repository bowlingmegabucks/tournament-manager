
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace NortheastMegabuck.Squads.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly Dictionary<TabPage, string> _toSpreadsheet;
    internal string ToSpreadsheet()
        => _toSpreadsheet[divisionsTabControl.SelectedTab];

    private readonly DateTime _bowlDate;
    private readonly Dictionary<KeyValuePair<string, bool>, IEnumerable<IViewModel>> _results = new();

    public Form(IConfiguration config, SquadId squadId, DateTime bowlDate)
    {
        InitializeComponent();

        SquadId = squadId;
        _bowlDate = bowlDate;

        _toSpreadsheet = new Dictionary<TabPage, string>();

        _ = new Presenter(config, this).ExecuteAsync(default);
    }

    public SquadId SquadId { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(string divisionName, bool isHandicap, ICollection<IViewModel> scores)
    {
        _results.Add(new KeyValuePair<string, bool>(divisionName, isHandicap), scores);

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

        var advancers = scores.Where(score => score.Advancer).ToList();

        foreach (var advancer in advancers)
        {
            var control = new Controls.SquadResultsControl(advancer);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        if (advancers.Count > 0)
        {
            panel.Controls.Add(new Controls.SquadResultsControl());
        }

        var cashers = scores.Where(score => score.Casher).ToList();

        foreach (var casher in cashers)
        {
            var control = new Controls.SquadResultsControl(casher);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        if (cashers.Count > 0)
        {
            panel.Controls.Add(new Controls.SquadResultsControl());
        }

        var nonQualifiers = scores.Where(score => !(score.Advancer || score.Casher)).ToList();

        foreach (var nonQualifier in nonQualifiers)
        {
            var control = new Controls.SquadResultsControl(nonQualifier);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        tabPage.Controls.Add(panel);
        divisionsTabControl.TabPages.Add(tabPage);

        _toSpreadsheet.Add(tabPage, toSpreadsheet.ToString());
    }

    private void CopyToClipboardLabel_Click(object sender, EventArgs e)
        => Clipboard.SetText(ToSpreadsheet());

    private void FileSaveAsPDFMenuItem_Click(object sender, EventArgs e)
    {
        //need to come back to handle handicap division
        var report = GenerateReport();

        ResultReportBase<IViewModel>.GeneratePDF(report);
    }

    private void FilePrintMenuItem_Click(object sender, EventArgs e)
    {
        var report = GenerateReport();

        ResultReportBase<IViewModel>.Print(report);
    }

    private MergedDocument GenerateReport()
    {
        var reports = _results.Select(result => new SquadResultReport(_bowlDate, result.Key.Key, result.Key.Value, result.Value.ToList())).ToList();

        return Document.Merge(reports).UseOriginalPageNumbers();
    }
}
