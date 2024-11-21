
using NortheastMegabuck.Divisions;

namespace NortheastMegabuck.Registrations.Update;

internal interface IView
{
    void BindBowler(Bowlers.Retrieve.IViewModel viewModel);
    void BindDivisions(IEnumerable<IViewModel> divisions);
    void Disable();
    void DisplayError(string message);
}
