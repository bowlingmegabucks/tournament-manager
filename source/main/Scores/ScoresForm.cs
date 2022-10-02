
namespace NortheastMegabuck.Scores;
public partial class Form : System.Windows.Forms.Form
{
    private readonly IConfiguration _config;
    private readonly SquadId _squadId;

    public Form(IConfiguration config, SquadId squadId, short numberOfGames)
    {
        InitializeComponent();

        _config = config;
        _squadId = squadId;

        scoresGrid.GenerateGameColumns(numberOfGames);
    }
}
