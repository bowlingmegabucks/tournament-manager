
namespace NortheastMegabuck.Controls.Grids;
public partial class SweepersGrid
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class SweeperMiddleGrid : DataGrid<Sweepers.IViewModel>
{
    public SweeperMiddleGrid()
    {

    }
}
#endif