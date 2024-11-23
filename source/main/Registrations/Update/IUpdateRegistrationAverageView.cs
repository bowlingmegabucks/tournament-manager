using NortheastMegabuck.Registrations.Retrieve;

namespace NortheastMegabuck.Registrations.Update;

internal interface IAverageView
{
    RegistrationId RegistrationId { get; }

    int? Average { get; }

    void BindBowler(Bowlers.Retrieve.IViewModel viewModel);

    void BindRegistration(ITournamentRegistrationViewModel tournamentRegistrationViewModel);

    void Disable();

    void DisplayError(string message);

    void DisplayMessage(string message);

    void KeepOpen();
    void OkToClose();
}
