namespace NortheastMegabuck.Tournaments.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public Form(IConfiguration config)
    {
        InitializeComponent();

        newTournament.Start = new DateOnly(DateTime.Now.Year, 1, 1);
        newTournament.End = new DateOnly(DateTime.Now.Year, 12, 31);

        _config = config;
    }

    public bool IsValid()
        => ValidateChildren();

    public IViewModel Tournament
        => newTournament;

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void OkToClose()
        => DialogResult = DialogResult.OK;

    public void DisplayErrors(IEnumerable<string> errorMessages)
        => MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private async void SaveButton_Click(object sender, EventArgs e)
        => await new Presenter(_config, this).ExecuteAsync(default).ConfigureAwait(true);
}
