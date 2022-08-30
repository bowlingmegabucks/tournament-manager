
namespace NortheastMegabuck.Contols;
public partial class SquadsGrid
#if DEBUG
    : SquadMiddleGrid
#else
    : Controls.DataGrid<Squads.IViewModel>
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
public class SquadMiddleGrid : Controls.DataGrid<Squads.IViewModel>
{
    public SquadMiddleGrid()
    {

    }
}
#endif