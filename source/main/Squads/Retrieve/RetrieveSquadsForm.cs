
namespace NewEnglandClassic.Squads.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    
    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        _config = config;

        new Presenter(_config, this).Execute(tournamentId);
    }

    public void BindSquads(IEnumerable<IViewModel> squads)
        => SquadsGrid.Bind(squads);

    public void Disable()
    {
        ButtonAdd.Enabled = false;
        ButtonOpen.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

    private void ButtonOpen_Click(object sender, EventArgs e)
        => throw new NotImplementedException();

    private void SquadsGrid_GridRowDoubleClicked(object sender, Controls.GridRowDoubleClickEventArgs e)
        => ButtonOpen_Click(sender, e);
}
