
namespace NortheastMegabuck.Bowlers.Search;
internal partial class Dialog : Form, IView
{
    private readonly IConfiguration _config;

    private readonly IEnumerable<SquadId> _registrationsWithoutSquads;
    private readonly TournamentId? _registeredInTournament;
    private readonly TournamentId? _notRegisteredInTournament;

    public Dialog(IConfiguration config, bool allowNewBowler) : this(config, allowNewBowler, null,Enumerable.Empty<SquadId>()) 
    { }

    public Dialog(IConfiguration config, bool allowNewBowler, TournamentId? registeredInTournament, IEnumerable<SquadId> registrationWithoutSquad) : this(config, allowNewBowler, registeredInTournament, registrationWithoutSquad, null) 
    { }

    private Dialog(IConfiguration config, bool allowNewBowler, TournamentId? registeredInTournament, IEnumerable<SquadId> registrationWithoutSquad, TournamentId? notRegisteredInTournament)
    {
        InitializeComponent();

        _config = config;
        SelectedBowlerId = null;

        _registrationsWithoutSquads = registrationWithoutSquad;
        _registeredInTournament = registeredInTournament;
        _notRegisteredInTournament = notRegisteredInTournament;

        newBowlerButton.Visible = allowNewBowler;
    }
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
        => await new Presenter(_config, this).ExecuteAsync(default).ConfigureAwait(true);
}
