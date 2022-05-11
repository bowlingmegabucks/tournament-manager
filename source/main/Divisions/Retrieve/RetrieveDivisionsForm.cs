
namespace NewEnglandClassic.Divisions.Retrieve;
public partial class Form : System.Windows.Forms.Form, IView
{
    public Guid TournamentId { get; }

    public Form(IConfiguration config, Guid tournamentId)
    {
        InitializeComponent();

        TournamentId = tournamentId;

        new Presenter(config, this).Execute();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void BindDivisions(IEnumerable<IViewModel> divisions)
        => divisionsGrid1.Bind(divisions);
    
    public void Disable() => throw new NotImplementedException();
}
