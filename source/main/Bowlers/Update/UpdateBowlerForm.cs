namespace NortheastMegabuck.Bowlers.Update;

internal partial class UpdateForm
    : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    /// <summary>
    /// Add Registration from Tournament Portal
    /// </summary>
    /// <param name="config"></param>
    /// <param name="bowlerId"></param>
    public UpdateForm(IConfiguration config, BowlerId bowlerId)
    {
        InitializeComponent();

        _config = config;

        _ = new Presenter(_config, this).LoadAsync(bowlerId, new CancellationToken());
    }

    public IViewModel Bowler
        => bowlerControl;

    public void OkToClose()
        => DialogResult = DialogResult.OK;

    public void Bind(Retrieve.IViewModel bowler)
    {
        bowlerControl.Id = bowler.Id;
        bowlerControl.FirstName = bowler.FirstName;
        bowlerControl.MiddleInitial = bowler.MiddleInitial;
        bowlerControl.LastName = bowler.LastName;
        bowlerControl.Suffix = bowler.Suffix;
        bowlerControl.StreetAddress = bowler.Street;
        bowlerControl.CityAddress = bowler.City;
        bowlerControl.StateAddress = bowler.State;
        bowlerControl.ZipCode = bowler.ZipCode;
        bowlerControl.EmailAddress = bowler.Email;
        bowlerControl.DateOfBirth = bowler.DateOfBirth;
        bowlerControl.PhoneNumber = bowler.PhoneNumber;
        bowlerControl.USBCId = bowler.USBCId;
        bowlerControl.Gender = bowler.Gender;
        bowlerControl.SocialSecurityNumber = bowler.SSN;
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
        => await new Presenter(_config, this).ExecuteAsync(new CancellationToken()).ConfigureAwait(true);
}
