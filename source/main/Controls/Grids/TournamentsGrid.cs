namespace BowlingMegabucks.TournamentManager.Controls.Grids;

#if DEBUG
internal class TournamentMiddleGrid : DataGrid<Tournaments.IViewModel>
{
    public TournamentMiddleGrid()
    {

    }
}

#endif

internal sealed partial class TournamentsGrid
#if DEBUG   
    : TournamentMiddleGrid
#else
    : DataGrid<Tournaments.IViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();
    }

    public Tournaments.IViewModel? SelectedTournament
        => SelectedRow;
}
