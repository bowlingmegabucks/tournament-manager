using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Squads.Portal;
internal interface IView
{
    void SetPortalTitle(string title);

    void DisplayError(string message);

    void Close();

    SquadId Id { get; }
}
