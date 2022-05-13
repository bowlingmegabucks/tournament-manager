
namespace NewEnglandClassic.Divisions.Retrieve;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;

    public Guid TournamentId { get; }

    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;
        _config = config;

        new Presenter(config, this).Execute();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindDivisions(IEnumerable<IViewModel> divisions)
        => divisionsGrid1.Bind(divisions);

    public void Disable() => throw new NotImplementedException();

    private void ButtonAdd_Click(object sender, EventArgs e)
        => new Presenter(_config, this).AddDivision();

    public Guid? AddDivision(Guid tournamentId)
    {
        using var form = new Add.Form(_config, tournamentId);

        return form.ShowDialog() == DialogResult.OK ? form.Division.Id : null;
    }

    public void RefreshDivisions()
        => new Presenter(_config, this).Execute();
}
