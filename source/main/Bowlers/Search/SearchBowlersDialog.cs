using System.ComponentModel;

namespace BowlingMegabucks.TournamentManager.Bowlers.Search;
internal sealed partial class Dialog 
    : Form, IView
{
    private readonly Presenter _presenter;
    private readonly IEnumerable<SquadId> _registrationsWithoutSquads;
    private readonly TournamentId? _registeredInTournament;
    private readonly TournamentId? _notRegisteredInTournament;

    public Dialog(IServiceProvider services, bool allowNewBowler) : this(services, allowNewBowler, null, [])
    { }

    public Dialog(IServiceProvider services, bool allowNewBowler, TournamentId? registeredInTournament, IEnumerable<SquadId> registrationWithoutSquad) : this(services, allowNewBowler, registeredInTournament, registrationWithoutSquad, null)
    { }

    private Dialog(IServiceProvider services, bool allowNewBowler, TournamentId? registeredInTournament, IEnumerable<SquadId> registrationWithoutSquad, TournamentId? notRegisteredInTournament)
    {
        InitializeComponent();

        SelectedBowlerId = null;

        _presenter = new Presenter(this, services);

        _registrationsWithoutSquads = registrationWithoutSquad;
        _registeredInTournament = registeredInTournament;
        _notRegisteredInTournament = notRegisteredInTournament;

        newBowlerButton.Visible = allowNewBowler;
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BowlerId? SelectedBowlerId { get; private set; }

    public Models.BowlerSearchCriteria SearchCriteria
        => new()
        {
            FirstName = firstNameText.Text,
            LastName = lastNameText.Text,
            EmailAddress = emailText.Text,
            WithoutRegistrationOnSquads = _registrationsWithoutSquads,
            RegisteredInTournament = _registeredInTournament,
            NotRegisteredInTournament = _notRegisteredInTournament
        };

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void BindResults(IEnumerable<IViewModel> bowlers)
        => searchResultsGrid.Bind(bowlers);

    private void SelectButton_Click(object sender, EventArgs e)
        => SelectedBowlerId = searchResultsGrid.SelectedBowler!.Id;

    private void SearchResultsGrid_GridRowDoubleClicked(object sender, Controls.Grids.GridRowDoubleClickEventArgs e)
    {
        SelectButton_Click(sender, e);
        DialogResult = DialogResult.OK;
    }

    private void NewButton_Click(object sender, EventArgs e)
        => SelectedBowlerId = BowlerId.Empty;

    private void CancelButton_Click(object sender, EventArgs e)
        => SelectedBowlerId = null;

    private async void SearchButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
