
namespace NortheastMegabuck.Scores;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    
    public SquadId SquadId { get; }

    public Form(IConfiguration config, SquadId squadId, short numberOfGames)
    {
        InitializeComponent();

        _config = config;
        SquadId = squadId;

        scoresGrid.GenerateGameColumns(numberOfGames);

        new Presenter(config, this).LoadLaneAssignments();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void Disable()
    {

    }

    public void BindLaneAssignments(IEnumerable<LaneAssignments.IViewModel> laneAssignments)
        => scoresGrid.Bind(laneAssignments.Select(assignment => new ViewModel(assignment)));
}
