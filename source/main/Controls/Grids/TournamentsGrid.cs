namespace BowlingMegabucks.TournamentManager.Controls.Grids;

internal class TournamentMiddleGrid : DataGrid<Tournaments.IViewModel>
{
    public TournamentMiddleGrid()
    {

    }
}

internal sealed partial class TournamentsGrid : TournamentMiddleGrid
{
    public TournamentsGrid()
    {
        InitializeComponent();
    }

    public Tournaments.IViewModel? SelectedTournament
        => SelectedRow;
}
