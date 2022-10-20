namespace NortheastMegabuck.Controls.Grids;

internal partial class TournamentsGrid
#if DEBUG   
    : TournamentMiddleGrid
#else
    : DataGrid<Tournaments.IViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();
    }
    
    public Tournaments.IViewModel? SelectedTournament
        => SelectedRow;
}

#if DEBUG
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
internal class TournamentMiddleGrid : DataGrid<Tournaments.IViewModel>
{
    public TournamentMiddleGrid()
    {

    }
}

#endif
