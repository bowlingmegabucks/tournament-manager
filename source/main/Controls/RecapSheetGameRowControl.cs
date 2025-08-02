
using System.Globalization;
using System.Runtime.Versioning;

namespace BowlingMegabucks.TournamentManager.Controls;

[SupportedOSPlatform("windows")]
internal sealed partial class RecapSheetGameRowControl 
    : UserControl
{
    public RecapSheetGameRowControl()
    {
        InitializeComponent();

        laneLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        handicapLabel.Text = string.Empty;
        totalLabel.Text = string.Empty;
    }

    public void SetHandicap(int handicap)
        => handicapLabel.Text = handicap > 0 ? handicap.ToString(CultureInfo.CurrentCulture) : "-";

    public void SetLaneAssignment(string laneAssignment)
        => laneLabel.Text = laneAssignment;
}
