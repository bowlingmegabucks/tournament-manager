
namespace NortheastMegabuck.Scores;
public partial class Form : System.Windows.Forms.Form
{
    private readonly SquadId _squadId;

    public Form(SquadId squadId, int numberOfGames)
    {
        InitializeComponent();

        _squadId = squadId;
        scoresGrid.GenerateGameColumns(numberOfGames);
    }
}
