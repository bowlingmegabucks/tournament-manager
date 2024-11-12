
using System.Runtime.Versioning;

namespace NortheastMegabuck.Squads.Portal;

[SupportedOSPlatform("windows")]
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    private readonly SquadId _id;
    private readonly TournamentId _tournamentId;
    private readonly short _numberOfGames;
    private readonly DateTime _squadDate;

    private bool _complete;
    public void SetComplete(bool complete)
        => _complete = complete;

    public Form(IConfiguration config, TournamentId tournamentId, SquadId id, short numberOfGames, DateTime squadDate, bool complete)
    {
        InitializeComponent();

        _config = config;
        _id = id;
        _tournamentId = tournamentId;
        _numberOfGames = numberOfGames;
        _squadDate = squadDate;

        _ = new Presenter(config, this).LoadAsync(default);
        _complete = complete;
    }

    public void SetPortalTitle(string title)
        => Text = title;

    SquadId IView.Id
        => _id;

    private int _startingLane;
    public void SetStartingLane(int startingLane)
        => _startingLane = startingLane;

    private int _numberOfLanes;
    public void SetNumberOfLanes(int numberOfLanes)
        => _numberOfLanes = numberOfLanes;

    private int _maxPerPair;
    public void SetMaxPerPair(int maxPerPair)
        => _maxPerPair = maxPerPair;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private void LaneAssignmentsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new LaneAssignments.Form(_config, _tournamentId, _id, _startingLane, _numberOfLanes, _maxPerPair, _numberOfGames, _squadDate, _complete);

        form.ShowDialog(this);
    }

    private void ScoresMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Scores.Form(_config, _id, _numberOfGames, _complete);

        form.ShowDialog(this);
    }

    private void ResultsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Results.Form(_config, _id, _squadDate);

        form.ShowDialog(this);
    }

    public bool Confirm(string message)
        => MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    private async void CompleteMenuItem_Click(object sender, EventArgs e)
        => await new Presenter(_config, this).CompleteAsync(default).ConfigureAwait(false);
}
