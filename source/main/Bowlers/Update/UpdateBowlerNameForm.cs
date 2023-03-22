
namespace NortheastMegabuck.Bowlers.Update;
internal partial class NameForm : Form, IBowlerNameView
{
    public NameForm(IConfiguration config, BowlerId id)
    {
        InitializeComponent();

        Id = id;

        new NamePresenter(config, this).Load();
    }

    public BowlerId Id { get; }

    public void DisplayError(string message)
        => MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

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
}
