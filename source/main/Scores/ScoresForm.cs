namespace BowlingMegabucks.TournamentManager.Scores;
internal partial class Form : System.Windows.Forms.Form, IView, Update.IView
{
    private readonly Presenter _presenter;
    private readonly IServiceProvider _services;

    private readonly short _numberOfGames;
    private readonly bool _complete;

    public SquadId SquadId { get; }

    public Form(IServiceProvider services, SquadId squadId, short numberOfGames, bool complete)
    {
        InitializeComponent();

        _presenter = new Presenter(this, services);
        _services = services;

        _numberOfGames = numberOfGames;
        SquadId = squadId;
        _complete = complete;

        scoresGrid.GenerateGameColumns(numberOfGames);
        scoresGrid.SetSquadId(squadId);
    }

    private void Form_Load(object sender, EventArgs e)
    {
        _ = _presenter.LoadAsync(default);

        if (_complete)
        {
            pasteScoresFromClipboardLinkLabel.Enabled = false;
            saveButton.Enabled = false;
        }
    }

    IEnumerable<IViewModel> Update.IView.Scores
        => scoresGrid.GetScores();

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void Disable()
    {
        pasteScoresFromClipboardLinkLabel.Enabled = false;
        saveButton.Enabled = false;
    }

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void BindLaneAssignments(IEnumerable<LaneAssignments.IViewModel> laneAssignments)
        => scoresGrid.Bind(laneAssignments.Select(assignment => new GridViewModel(assignment)));

    public void BindSquadScores(IEnumerable<IViewModel> squadScores)
        => scoresGrid.FillScores(squadScores);

    private void PasteScoresFromClipboardLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var data = Clipboard.GetText();

        if (string.IsNullOrWhiteSpace(data))
        {
            MessageBox.Show("No data");

            return;
        }

        var scoresByBowler = data.Split('\r');

        var squadScores = scoresByBowler.Select(bowlerScores => new GridViewModel(bowlerScores, _numberOfGames));

        scoresGrid.FillScores(squadScores);
    }

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        UseWaitCursor = true;

        await new Update.Presenter(this, _services).ExecuteAsync(default).ConfigureAwait(true);

        UseWaitCursor = false;
    }

    public void OkToClose()
        => DialogResult = DialogResult.OK;
}
