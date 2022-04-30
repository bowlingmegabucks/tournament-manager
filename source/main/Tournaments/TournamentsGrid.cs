namespace NewEnglandClassic.Tournaments;
internal partial class TournamentsGrid
#if DEBUG   
    : MiddleGrid
#else
    : Controls.DataGrid<IViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();
    }

    public IViewModel? SelectedTournament
        => SelectedRow;
}

#if DEBUG
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
internal class MiddleGrid : Controls.DataGrid<IViewModel>
{
    public MiddleGrid()
    {

    }
}

#endif
