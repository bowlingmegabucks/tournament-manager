using System.ComponentModel;

namespace BowlingMegabucks.TournamentManager.Sweepers.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal string ToSpreadsheet { get; private set; }

    private readonly Presenter _presenter;

    private IEnumerable<IViewModel> _results = [];
    private readonly DateTime? _sweeperDate;

    public Form(IServiceProvider services, SquadId squadId, DateTime sweeperDate) 
        : this(services)
    {
        _sweeperDate = sweeperDate;
        _ = _presenter.ExecuteAsync(squadId, default);
    }

    public Form(IServiceProvider services, TournamentId tournamentId) 
        : this(services)
    {
        _sweeperDate = null;

#pragma warning disable CA1303 // Do not pass literals as localized parameters
        Text = "Super Sweeper Results";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        _ = _presenter.ExecuteAsync(tournamentId, default);
    }

    private Form(IServiceProvider services)
    {
        InitializeComponent();

        _presenter = new(this, services);

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
        var report = GenerateReport();
        report.GeneratePDF();
    }

    private void PrintMenuItem_Click(object sender, EventArgs e)
    {
        var report = GenerateReport();
        report.Print();
    }

    private ResultReportBase<IViewModel> GenerateReport()
    {
        var title = _sweeperDate.HasValue ? "Sweeper Results" : "Super Sweeper Results";
        return new SweeperResultReport(title, _sweeperDate, [.. _results]);
    }
}
