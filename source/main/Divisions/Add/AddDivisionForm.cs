namespace NewEnglandClassic.Divisions.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    
    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();
        
        _config = config;
        
        Division.TournamentId = tournamentId;
        

        new Presenter(config, this).GetNextDivisionNumber();
        
        DivisionNew.Focus();
    }

    public bool IsValid()
        => ValidateChildren();

    public IViewModel Division
        => DivisionNew;
    
    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void DisplayErrors(IEnumerable<string> errorMessages)
        => MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void ButtonSave_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
