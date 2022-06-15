
namespace NewEnglandClassic.Bowlers.Search;
internal partial class Dialog : Form, IView
{
    private readonly IConfiguration _config; 
    public Dialog(IConfiguration config, bool allowNewBowler)
    {
        InitializeComponent();

        _config = config;
        SelectedBowlerId = null;

        ButtonNew.Visible = allowNewBowler;
    }

    public Guid? SelectedBowlerId { get; private set; }

    public Models.BowlerSearchCriteria SearchCriteria
        => new()
        {
            FirstName = TextboxFirstName.Text,
            LastName = TextboxLastName.Text,
            EmailAddress = TextboxEmail.Text
        };

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindResults(IEnumerable<IViewModel> bowlers)
        => SearchResultsGrid.Bind(bowlers);

    private void ButtonSelect_Click(object sender, EventArgs e) 
        => SelectedBowlerId = SearchResultsGrid.SelectedBowler!.Id;

    private void SearchResultsGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
     => ButtonSelect_Click(sender, e);

    private void ButtonNew_Click(object sender, EventArgs e)
        => SelectedBowlerId = Guid.Empty;

    private void ButtonCancel_Click(object sender, EventArgs e)
        => SelectedBowlerId = null;

    private void ButtonSearch_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
