
namespace NortheastMegabuck.Tournaments.Seeding;
internal partial class Form : System.Windows.Forms.Form, IView
{
    public Form(IConfiguration config, TournamentId id)
    {
        InitializeComponent();

        Id = id;

        new Presenter(config, this).Execute();
    }

    public TournamentId Id { get; }

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

        foreach (var score in scores.Where(s=> s.Qualified))
        {
            panel.Controls.Add(new Controls.TournamentSeedingControl(score));
        }

        panel.Controls.Add(new Controls.TournamentSeedingControl());

        foreach (var score in scores.Where(s => !s.Qualified))
        {
            panel.Controls.Add(new Controls.TournamentSeedingControl(score));
        }

        tabPage.Controls.Add(panel);
        divisionsTabControl.TabPages.Add(tabPage);
    }
}
