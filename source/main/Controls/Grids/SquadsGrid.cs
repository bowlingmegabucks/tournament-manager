
namespace BowlingMegabucks.TournamentManager.Controls.Grids;
internal partial class SquadsGrid
#if DEBUG
    : SquadMiddleGrid
#else
    : DataGrid<Squads.IViewModel>
#endif
{
    public SquadsGrid()
    {
        InitializeComponent();
    }

    public Squads.IViewModel? SelectedSquad
        => SelectedRow;
}

#if DEBUG
internal class SquadMiddleGrid : DataGrid<Squads.IViewModel>
{
    public SquadMiddleGrid()
    {

    }
}
#endif