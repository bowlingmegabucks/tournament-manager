
namespace NortheastMegabuck.Controls.Grids;
public partial class SquadsGrid
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class SquadMiddleGrid : DataGrid<Squads.IViewModel>
{
    public SquadMiddleGrid()
    {

    }
}
#endif