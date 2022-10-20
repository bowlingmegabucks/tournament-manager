namespace NortheastMegabuck.Controls.Grids;
public partial class TournamentRegistrationGrid
#if DEBUG
    : TournamentRegistrationMiddleGrid
#else
    : DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
#endif
{
    public TournamentRegistrationGrid()
    {
        InitializeComponent();
    }
}

#if DEBUG
public class TournamentRegistrationMiddleGrid : DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
{
    public TournamentRegistrationMiddleGrid()
    {

    }
}
#endif
