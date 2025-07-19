
namespace BowlingMegabucks.TournamentManager.Controls.Grids;
internal partial class SearchBowlersGrid
#if DEBUG
    : SearchBowlersMiddleGrid
#else
    : DataGrid<Bowlers.Search.IViewModel>
#endif
{
    public SearchBowlersGrid()
    {
        InitializeComponent();
    }

    public Bowlers.Search.IViewModel? SelectedBowler
        => SelectedRow;
}

#if DEBUG
internal class SearchBowlersMiddleGrid : DataGrid<Bowlers.Search.IViewModel>
{
    public SearchBowlersMiddleGrid()
    {

    }
}
#endif