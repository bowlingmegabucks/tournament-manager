namespace NortheastMegabuck.Scores;
public partial class Form : System.Windows.Forms.Form, IView, Update.IView
{
    private readonly IConfiguration _config;
    private readonly short _numberOfGames;
    private readonly bool _complete;
    
    public SquadId SquadId { get; }

    public Form(IConfiguration config, SquadId squadId, short numberOfGames, bool complete)
    {
        InitializeComponent();

        _config = config;
        _numberOfGames = numberOfGames;
        SquadId = squadId;
        _complete = complete; 

        scoresGrid.GenerateGameColumns(numberOfGames);
        scoresGrid.SetSquadId(squadId);
    }

    private void Form_Load(object sender, EventArgs e)
    {
        new Presenter(_config, this).Load();

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

    private void SaveButton_Click(object sender, EventArgs e)
    {
        UseWaitCursor = true;

        new Update.Presenter(_config, this).Execute();

        UseWaitCursor = false;
    }
}
