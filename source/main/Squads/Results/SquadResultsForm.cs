
namespace NortheastMegabuck.Squads.Results;
internal partial class Form : System.Windows.Forms.Form, IView
{
    public Form(IConfiguration config, SquadId squadId)
    {
        InitializeComponent();

        SquadId = squadId;

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
            Dock = DockStyle.Fill
        };

        var advancers = scores.Where(score => score.Advancer).ToList();

        foreach (var advancer in advancers)
        {
            panel.Controls.Add(new Controls.SquadResultsControl(advancer));
        }

        if (advancers.Any())
        {
            panel.Controls.Add(new Controls.SquadResultsControl());
        }

        var cashers = scores.Where(score => score.Casher).ToList();

        foreach (var casher in cashers)
        {
            panel.Controls.Add(new Controls.SquadResultsControl(casher));
        }

        if (cashers.Any())
        {
            panel.Controls.Add(new Controls.SquadResultsControl());
        }

        var nonQualifiers = scores.Where(score => !(score.Advancer || score.Casher)).ToList();

        foreach (var nonQualifier in nonQualifiers)
        {
            panel.Controls.Add(new Controls.SquadResultsControl(nonQualifier));
        }

        tabPage.Controls.Add(panel);
        divisionsTabControl.TabPages.Add(tabPage);
    }
}
