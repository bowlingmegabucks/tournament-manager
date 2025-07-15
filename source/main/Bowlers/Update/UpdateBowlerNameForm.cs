
namespace NortheastMegabuck.Bowlers.Update;
internal partial class NameForm
    : Form, IBowlerNameView
{
    private readonly NamePresenter _presenter;
    public NameForm(IServiceProvider services, BowlerId id)
    {
        InitializeComponent();

        Id = id;
        _presenter = new(this, services);

        _ = _presenter.LoadAsync(default);
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

    public void OkToClose()
        => DialogResult = DialogResult.OK;

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
