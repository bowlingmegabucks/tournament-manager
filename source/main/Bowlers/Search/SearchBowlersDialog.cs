
namespace NewEnglandClassic.Bowlers.Search;
internal partial class Dialog : Form, IView
{
    private readonly IConfiguration _config; 
    public Dialog(IConfiguration config, bool allowNewBowler)
    {
        InitializeComponent();

        _config = config;
        SelectedBowlerId = null;

        newBowlerButton.Visible = allowNewBowler;
    }

    public BowlerId? SelectedBowlerId { get; private set; }

    public Models.BowlerSearchCriteria SearchCriteria
        => new()
        {
            FirstName = firstNameText.Text,
            LastName = lastNameText.Text,
            EmailAddress = emailText.Text
        };

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(IEnumerable<IViewModel> bowlers)
        => searchResultsGrid.Bind(bowlers);

    private void SelectButton_Click(object sender, EventArgs e) 
        => SelectedBowlerId = searchResultsGrid.SelectedBowler!.Id;

    private void SearchResultsGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
     => SelectButton_Click(sender, e);

    private void NewButton_Click(object sender, EventArgs e)
        => SelectedBowlerId = BowlerId.Empty;

    private void CancelButton_Click(object sender, EventArgs e)
        => SelectedBowlerId = null;

    private void SearchButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
