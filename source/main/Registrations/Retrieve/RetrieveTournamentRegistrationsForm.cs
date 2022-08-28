using System.Text;

namespace NewEnglandClassic.Registrations.Retrieve;
internal partial class RetrieveTournamentRegistrationsForm : Form, ITournamentRegistrationsView
{
    public RetrieveTournamentRegistrationsForm(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;

        new TournamentRegistrationsPresenter(this, config).Execute();
    }

    public TournamentId TournamentId { get; }

    public void BindRegistrations(IEnumerable<ITournamentRegistrationViewModel> registrations)
        => tournamentRegistrationsGrid.Bind(registrations);
    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    public void SetDivisionEntries(IDictionary<string, int> divisionEntries)
    {
        var entries = new StringBuilder();

        foreach (var entry in divisionEntries)
        {
            entries.AppendLine($"{entry.Key}: {entry.Value} Entries");
        }

        divisionEntriesLabel.Text = entries.ToString();
    }
    public void SetSquadEntries(IDictionary<string, int> squadEntries)
    {
        var entries = new StringBuilder();

        foreach (var entry in squadEntries)
        {
            entries.AppendLine($"{entry.Key}: {entry.Value} Entries");
        }

        squadEntriesLabel.Text = entries.ToString();
    }
    public void SetSweeperEntries(IDictionary<string, int> sweeperEntries)
    {
        var entries = new StringBuilder();

        foreach (var entry in sweeperEntries)
        {
            entries.AppendLine($"{entry.Key}: {entry.Value} Entries");
        }

        sweeperEntriesLabel.Text = entries.ToString();
    }
}
