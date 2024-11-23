
using NortheastMegabuck.Divisions;
using NortheastMegabuck.Models;
using NortheastMegabuck.Registrations.Retrieve;

namespace NortheastMegabuck.Registrations.Update;

internal interface IView
{
    RegistrationId RegistrationId { get; }
    DivisionId DivisionId { get; }
    Gender? Gender { get; }
    string UsbcId { get; }
    DateOnly? DateOfBirth { get; }
    int? Average { get; }

    void BindBowler(Bowlers.Retrieve.IViewModel viewModel);

    void BindDivisions(IEnumerable<IViewModel> divisions);

    void BindRegistration(ITournamentRegistrationViewModel tournamentRegistrationViewModel);

    void Disable();

    void DisplayError(string message);

    void DisplayMessage(string message);
    void KeepOpen();
    void OkToClose();
}
