using System.Globalization;
using System.Text;

namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;
internal sealed partial class RetrieveTournamentRegistrationsForm : Form, ITournamentRegistrationsView
{
    private readonly IServiceProvider _services;
    private readonly TournamentRegistrationsPresenter _tournamentRegistrationsPresenter;

    public RetrieveTournamentRegistrationsForm(IServiceProvider services, TournamentId tournamentId)
    {
        InitializeComponent();

        tournamentRegistrationsGrid.SelectedRowContextMenu = registrationGridContextMenu;

        _services = services;
        _tournamentRegistrationsPresenter = new(this, services);

        TournamentId = tournamentId;

        _ = _tournamentRegistrationsPresenter.ExecuteAsync(default);
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

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void RemoveRegistration(RegistrationId id)
        => tournamentRegistrationsGrid.Remove(id);

    private async void DeleteMenuItem_Click(object sender, EventArgs e)
    {
        var registration = tournamentRegistrationsGrid.SelectedRegistration;

        await _tournamentRegistrationsPresenter.DeleteAsync(registration.Id, default).ConfigureAwait(true);
    }

    private void UpdateBowlerNameMenuItem_Click(object sender, EventArgs e)
    {
        var registration = tournamentRegistrationsGrid.SelectedRegistration;

        _tournamentRegistrationsPresenter.UpdateBowlerName(registration.BowlerId);
    }

    public string? UpdateBowlerName(BowlerId id)
    {
        using var form = new Bowlers.Update.NameForm(_services, id);

        return form.ShowDialog(this) == DialogResult.OK ? form.FullName : null;
    }

    public void UpdateBowlerName(string bowlerName)
        => tournamentRegistrationsGrid.SelectedRegistration.BowlerName = bowlerName;

    public void UpdateBowlerSuperSweeper(RegistrationId id)
        => tournamentRegistrationsGrid.SelectedRegistration.SuperSweeperEntered = true;

    private async void AddSuperSweeperMenuItem_Click(object sender, EventArgs e)
    {
        var registration = tournamentRegistrationsGrid.SelectedRegistration;

        await _tournamentRegistrationsPresenter.AddSuperSweeperAsync(registration.Id, default).ConfigureAwait(true);
    }

    private async void ChangeDivisionMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Update.UpdateRegistrationDivisionForm(_services, TournamentId, tournamentRegistrationsGrid.SelectedRegistration.Id);

#pragma warning disable S6966
        var result = form.ShowDialog(this);
#pragma warning restore S6966

        if (result == DialogResult.OK)
        {
            await _tournamentRegistrationsPresenter.ExecuteAsync(default).ConfigureAwait(true);
        }
    }

    private void ChangeAverageMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Update.UpdateRegistrationAverageForm(_services, tournamentRegistrationsGrid.SelectedRegistration.Id);

        form.ShowDialog(this);
    }

    private void FilterText_TextChanged(object sender, EventArgs e)
        => tournamentRegistrationsGrid.Filter(filterText.Text);

    private void UpdateBowlerInfoMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Bowlers.Update.UpdateForm(_services, tournamentRegistrationsGrid.SelectedRegistration.BowlerId);

        form.ShowDialog(this);
    }
}