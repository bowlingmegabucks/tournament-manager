
using System.Text;

namespace NortheastMegabuck.Tournaments.Results;
internal partial class AtLarge : System.Windows.Forms.Form, IView
{
    private readonly Dictionary<TabPage, string> _toSpreadsheet;
    internal string ToSpreadsheet()
        => _toSpreadsheet[divisionsTabControl.SelectedTab];

    public AtLarge(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        Id = tournamentId;

        _toSpreadsheet = new Dictionary<TabPage, string>();

        _ = new Presenter(config, this).AtLargeAsync(default);
    }

    public TournamentId Id { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(string divisionName, IEnumerable<IAtLargeViewModel> results)
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

        foreach (var score in results )
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
}
