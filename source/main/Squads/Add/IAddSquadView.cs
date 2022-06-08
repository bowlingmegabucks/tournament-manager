using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Squads.Add;

internal interface IView : NewEnglandClassic.IView
{
    void SetTournamentFinalsRatio(string ratio);

    void SetTournamentCashRatio(string ratio);

    IViewModel Squad { get; }

    void DisplayError(string message);
}
