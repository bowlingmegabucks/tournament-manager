
using NortheastMegabuck.Divisions;
using NortheastMegabuck.Registrations.Retrieve;

namespace NortheastMegabuck.Registrations.Update;

internal interface IView
{
    void BindBowler(Bowlers.Retrieve.IViewModel viewModel);

    void BindDivisions(IEnumerable<IViewModel> divisions);

    void BindRegistration(ITournamentRegistrationViewModel tournamentRegistrationViewModel);

    void Disable();

    void DisplayError(string message);

    void DisplayMessage(string message);
}
