﻿
namespace BowlingMegabucks.TournamentManager.Controls.Grids;
internal sealed partial class SweepersGrid
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