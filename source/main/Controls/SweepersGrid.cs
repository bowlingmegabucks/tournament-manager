
namespace NewEnglandClassic.Contols;
internal partial class SweepersGrid
#if DEBUG
    : SweeperMiddleGrid
#else
    : Controls.DataGrid<Sweepers.IViewModel>
#endif
{ 
    public SweepersGrid()
    {
        InitializeComponent();
    }

    public Sweepers.IViewModel? SelectedSquad
        => SelectedRow;
}

#if DEBUG
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
internal class SweeperMiddleGrid : Controls.DataGrid<Sweepers.IViewModel>
{
    public SweeperMiddleGrid()
    {

    }
}
#endif