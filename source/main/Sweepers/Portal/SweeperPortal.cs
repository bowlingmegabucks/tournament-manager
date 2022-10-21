
namespace NortheastMegabuck.Sweepers.Portal;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    private readonly SquadId _id;
    private readonly TournamentId _tournamentId;
    private readonly short _numberOfGames;
    private readonly DateTime _squadDate;
    
    public bool Complete { private get; set; }

    public Form(IConfiguration config, TournamentId tournamentId, SquadId id, short numberOfGames, DateTime squadDate, bool complete)
    {
        InitializeComponent();

        _config = config;
        _id = id;
        _tournamentId = tournamentId;
        _numberOfGames = numberOfGames;
        _squadDate = squadDate;

        Complete = complete;

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

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void LaneAssignmentsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new LaneAssignments.Form(_config,_tournamentId, _id, StartingLane,NumberOfLanes,MaxPerPair, _numberOfGames, _squadDate);

        form.ShowDialog(this);
    }

    private void ScoresMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Scores.Form(_config, _id, _numberOfGames);

        form.ShowDialog(this);
    }

    private void ResultsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Results.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void CompleteMenuItem_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Complete();

    public bool Confirm(string message)
        => MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;
}
