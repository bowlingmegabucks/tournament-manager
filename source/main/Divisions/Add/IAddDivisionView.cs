using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Divisions.Add;
internal interface IView
{
    bool IsValid();

    IViewModel Division { get; }

    void KeepOpen();

    void DisplayErrors(IEnumerable<string> errors);

    void DisplayMessage(string message);

    void Close();
}
