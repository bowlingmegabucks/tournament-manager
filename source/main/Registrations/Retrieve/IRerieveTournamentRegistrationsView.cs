using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Registrations.Retrieve;
internal interface ITournamentRegistrationsView
{
    TournamentId TournamentId { get; }

    void BindRegistrations(IEnumerable<ITournamentRegistrationViewModel> registrations);
    void DisplayError(string message);
    void SetDivisionEntries(IDictionary<string, int> divisionEntries);
    void SetSquadEntries(IDictionary<string, int> squadEntries);
    void SetSweeperEntries(IDictionary<string, int> sweeperEntries);
}
