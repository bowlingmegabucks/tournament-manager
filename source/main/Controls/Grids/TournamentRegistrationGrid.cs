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

    public void Remove(RegistrationId id)
    {
        var registration = Models.Single(model => model.Id == id);

        Remove(registration);
    }

    public Registrations.Retrieve.ITournamentRegistrationViewModel SelectedRegistration
        => SelectedRow!;
}

#if DEBUG
public class TournamentRegistrationMiddleGrid : DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
{
    public TournamentRegistrationMiddleGrid()
    {

    }
}
#endif
