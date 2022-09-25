
namespace NortheastMegabuck.Squads.Portal;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    private readonly SquadId _id;
    private readonly TournamentId _tournamentId;
    public Form(IConfiguration config, TournamentId tournamentId, SquadId id)
    {
        InitializeComponent();

        _config = config;
        _id = id;
        _tournamentId = tournamentId;

        new Presenter(config, this).Load();
    }

    public void SetPortalTitle(string title)
        => Text = title;

    SquadId IView.Id
        => _id;

    public int StartingLane { private get; set; }

    public int NumberOfLanes { private get; set; }

    public int MaxPerPair { private get; set; }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private void LaneAssignmentsMenuItem_Click(object sender, EventArgs e)
    {
        //todo: get from presenter;
        using var form = new LaneAssignments.Form(_config,_tournamentId, _id, StartingLane,NumberOfLanes,MaxPerPair);

        form.ShowDialog(this);
    }
}
