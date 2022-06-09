
namespace NewEnglandClassic.Contols;
internal partial class SquadsGrid
#if DEBUG
    : MiddleGrid
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
internal class MiddleGrid : Controls.DataGrid<Squads.IViewModel>
{
    public MiddleGrid()
    {

    }
}
#endif