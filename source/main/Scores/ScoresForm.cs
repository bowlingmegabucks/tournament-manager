
namespace NortheastMegabuck.Scores;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    private readonly short _numberOfGames;
    
    public SquadId SquadId { get; }

    public Form(IConfiguration config, SquadId squadId, short numberOfGames)
    {
        InitializeComponent();

        _config = config;
        _numberOfGames = numberOfGames;
        SquadId = squadId;

        scoresGrid.GenerateGameColumns(numberOfGames);

        new Presenter(config, this).LoadLaneAssignments();
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void Disable()
    {
        pasteScoresFromClipboardLinkLabel.Enabled = false;
        saveButton.Enabled = false;
    }
        

    public void BindLaneAssignments(IEnumerable<LaneAssignments.IViewModel> laneAssignments)
        => scoresGrid.Bind(laneAssignments.Select(assignment => new ViewModel(assignment)));

    private void PasteScoresFromClipboardLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var data = Clipboard.GetText();

        if (string.IsNullOrWhiteSpace(data))
        {
            MessageBox.Show("No data");

            return;
        }

        var scoresByBowler = data.Split('\r');

        var squadScores = scoresByBowler.Select(bowlerScores => new ViewModel(bowlerScores, _numberOfGames));

        scoresGrid.LoadScores(squadScores);
    }
}
