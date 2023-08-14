using System.Globalization;
using System.Text;

namespace NortheastMegabuck.Registrations.Retrieve;
internal partial class RetrieveTournamentRegistrationsForm : Form, ITournamentRegistrationsView
{
    private readonly IConfiguration _config;

    public RetrieveTournamentRegistrationsForm(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        _config = config;
        TournamentId = tournamentId;

        new TournamentRegistrationsPresenter(this, config).Execute(default);
    }

    public TournamentId TournamentId { get; }

    public void BindSquadDates(IDictionary<SquadId, string> squadDates)
        => tournamentRegistrationsGrid.AddSquadDates(squadDates);

    public void BindRegistrations(IEnumerable<ITournamentRegistrationViewModel> registrations)
        => tournamentRegistrationsGrid.Bind(registrations);

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void SetDivisionEntries(IDictionary<string, int> divisionEntries)
    {
        var entries = new StringBuilder();

        foreach (var entry in divisionEntries)
        {
            entries.AppendLine(CultureInfo.CurrentCulture, $"{entry.Key}: {entry.Value} Entries");
        }

        divisionEntriesLabel.Text = entries.ToString();
    }
    public void SetSquadEntries(IDictionary<string, int> squadEntries)
    {
        var entries = new StringBuilder();

        foreach (var entry in squadEntries)
        {
            entries.AppendLine(CultureInfo.CurrentCulture, $"{entry.Key}: {entry.Value} Entries");
        }

        squadEntriesLabel.Text = entries.ToString();
    }
    public void SetSweeperEntries(IDictionary<string, int> sweeperEntries)
    {
        var entries = new StringBuilder();

        foreach (var entry in sweeperEntries)
        {
            entries.AppendLine(CultureInfo.CurrentCulture, $"{entry.Key}: {entry.Value} Entries");
        }

        sweeperEntriesLabel.Text = entries.ToString();
    }

    public bool Confirm(string message)
        => MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;

    public void RemoveRegistration(RegistrationId id)
        => tournamentRegistrationsGrid.Remove(id);

    private async void DeleteMenuItem_Click(object sender, EventArgs e)
    {
        var registration = tournamentRegistrationsGrid.SelectedRegistration;

        await new TournamentRegistrationsPresenter(this, _config).DeleteAsync(registration.Id, default).ConfigureAwait(true);
    }

    private void UpdateBowlerNameMenuItem_Click(object sender, EventArgs e)
    {
        var registration = tournamentRegistrationsGrid.SelectedRegistration;

        new TournamentRegistrationsPresenter(this, _config).UpdateBowlerName(registration.BowlerId);
    }

    public string? UpdateBowlerName(BowlerId id)
    {
        using var form = new Bowlers.Update.NameForm(_config, id);

        return form.ShowDialog(this) == DialogResult.OK ? form.FullName : null;
    }

    public void UpdateBowlerName(string bowlerName)
        => tournamentRegistrationsGrid.SelectedRegistration.BowlerName = bowlerName;
}