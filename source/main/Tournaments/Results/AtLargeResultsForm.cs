
namespace NortheastMegabuck.Tournaments.Results;
internal partial class AtLarge : System.Windows.Forms.Form, IView
{
    public AtLarge(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        Id = tournamentId;

        new Presenter(config, this).AtLarge();
    }

    public TournamentId Id { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(string divisionName, IEnumerable<IAtLargeViewModel> scores)
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

        foreach (var score in scores )
        {
            panel.Controls.Add(new Controls.AtLargeResultsControl(score));
        }

        tabPage.Controls.Add(panel);
        divisionsTabControl.TabPages.Add(tabPage);
    }
}
