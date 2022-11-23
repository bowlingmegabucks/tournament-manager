
using System.Text;

namespace NortheastMegabuck.Squads.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IDictionary<TabPage, string> _toSpreadsheet;
    internal string ToSpreadsheet()
        => _toSpreadsheet[divisionsTabControl.SelectedTab];

    public Form(IConfiguration config, SquadId squadId)
    {
        InitializeComponent();

        SquadId = squadId;

        _toSpreadsheet = new Dictionary<TabPage, string>();

        new Presenter(config, this).Execute();
    }

    public SquadId SquadId { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(string divisionName, IEnumerable<IViewModel> scores)
    {
        var tabPage = new TabPage($"{divisionName}TabPage")
        {
            Text = divisionName
        };

        var panel = new FlowLayoutPanel()
        {
            Name = $"{divisionName}FlowLayoutPanel",
            Dock = DockStyle.Fill,
            AutoScroll= true
        };

        var toSpreadsheet = new StringBuilder();

        var advancers = scores.Where(score => score.Advancer).ToList();

        foreach (var advancer in advancers)
        {
            var control = new Controls.SquadResultsControl(advancer);

            toSpreadsheet.AppendLine(control.ToSpreadsheetRow);
            panel.Controls.Add(control);
        }

        if (advancers.Any())
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

        if (cashers.Any())
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
}
