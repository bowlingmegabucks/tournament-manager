
namespace NortheastMegabuck.Divisions.Retrieve;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public TournamentId TournamentId { get; }

    public Form(IConfiguration config, TournamentId tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;
        _config = config;

        new Presenter(config, this).Execute();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindDivisions(IEnumerable<IViewModel> divisions)
        => divisionsGrid.Bind(divisions);

    public void Disable()
        => addButton.Enabled = false;

    private void AddButton_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddDivision();

    public NortheastMegabuck.Divisions.Id? AddDivision(TournamentId tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Division.Id : null;
    }

    public void RefreshDivisions()
        => new Presenter(_config, this).Execute();
}
