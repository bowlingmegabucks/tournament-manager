namespace NortheastMegabuck.Bowlers.Update;

internal partial class UpdateForm
    : System.Windows.Forms.Form, IView
{
    private readonly Presenter _presenter;

    /// <summary>
    /// Add Registration from Tournament Portal
    /// </summary>
    /// <param name="presenter"></param>
    /// <param name="bowlerId"></param>
    public UpdateForm(Presenter presenter, BowlerId bowlerId)
    {
        InitializeComponent();

        _presenter = presenter;
        _ = presenter.LoadAsync(bowlerId, new CancellationToken());
    }

    public IViewModel Bowler
        => bowlerControl;

    public void OkToClose()
        => DialogResult = DialogResult.OK;

    public void Bind(Retrieve.IViewModel viewModel)
    {
        bowlerControl.Id = viewModel.Id;
        bowlerControl.FirstName = viewModel.FirstName;
        bowlerControl.MiddleInitial = viewModel.MiddleInitial;
        bowlerControl.LastName = viewModel.LastName;
        bowlerControl.Suffix = viewModel.Suffix;
        bowlerControl.StreetAddress = viewModel.Street;
        bowlerControl.CityAddress = viewModel.City;
        bowlerControl.StateAddress = viewModel.State;
        bowlerControl.ZipCode = viewModel.ZipCode;
        bowlerControl.EmailAddress = viewModel.Email;
        bowlerControl.DateOfBirth = viewModel.DateOfBirth;
        bowlerControl.PhoneNumber = viewModel.PhoneNumber;
        bowlerControl.USBCId = viewModel.USBCId;
        bowlerControl.Gender = viewModel.Gender;
        bowlerControl.SocialSecurityNumber = viewModel.SSN;
    }

    public void Disable()
    {
        bowlerControl.Enabled = false;

        saveButton.Enabled = false;
        cancelButton.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayErrors(IEnumerable<string> messages)
        => MessageBox.Show(string.Join(Environment.NewLine, messages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(new CancellationToken()).ConfigureAwait(true);
}
