
namespace NortheastMegabuck.Contols;
public partial class SearchBowlersGrid
#if DEBUG
    : SearchBowlersMiddleGrid
#else
    : Controls.DataGrid<Bowlers.Search.IViewModel>
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class SearchBowlersMiddleGrid : Controls.DataGrid<Bowlers.Search.IViewModel>
{
    public SearchBowlersMiddleGrid()
    {

    }
}
#endif