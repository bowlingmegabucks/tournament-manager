
namespace NortheastMegabuck.Bowlers.Update;
internal partial class NameForm : Form, IBowlerNameView
{
    private readonly IConfiguration _config;
    public NameForm(IConfiguration config, BowlerId id)
    {
        InitializeComponent();

        Id = id;
        _config = config;

        new NamePresenter(config, this).Load();
    }

    public BowlerId Id { get; }

    INameViewModel IBowlerNameView.BowlerName
        => personNameControl;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayErrors(IEnumerable<string> messages)
        => DisplayError(string.Join(Environment.NewLine, messages));

    public void Bind(Retrieve.IViewModel viewModel)
    {
        personNameControl.First = viewModel.FirstName;
        personNameControl.MiddleInitial = viewModel.MiddleInitial;
        personNameControl.Last = viewModel.LastName;
        personNameControl.Suffix = viewModel.Suffix;
    }

    public string FullName
        => Models.PersonName.FullName(personNameControl.First, personNameControl.Last, personNameControl.Suffix);

    public void Disable()
    {
        personNameControl.Enabled = false;
        saveButton.Enabled = false;
    }

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void SaveButton_Click(object sender, EventArgs e)
        => new NamePresenter(_config, this).Execute();
}
