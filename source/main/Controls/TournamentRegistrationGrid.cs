namespace NortheastMegabuck.Controls;
public partial class TournamentRegistrationGrid
#if DEBUG
    : TournamentRegistrationMiddleGrid
#else
    : Controls.DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
#endif
{
    public TournamentRegistrationGrid()
    {
        InitializeComponent();
    }
}

#if DEBUG
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class TournamentRegistrationMiddleGrid : Controls.DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
{
    public TournamentRegistrationMiddleGrid()
    {

    }
}
#endif
