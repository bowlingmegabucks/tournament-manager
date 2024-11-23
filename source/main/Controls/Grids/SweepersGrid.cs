
namespace NortheastMegabuck.Controls.Grids;
internal partial class SweepersGrid
#if DEBUG
    : SweeperMiddleGrid
#else
    : DataGrid<Sweepers.IViewModel>
#endif
{
    public SweepersGrid()
    {
        InitializeComponent();
    }

    public Sweepers.IViewModel? SelectedSweeper
        => SelectedRow;
}

#if DEBUG
internal class SweeperMiddleGrid : DataGrid<Sweepers.IViewModel>
{
    public SweeperMiddleGrid()
    {

    }
}
#endif