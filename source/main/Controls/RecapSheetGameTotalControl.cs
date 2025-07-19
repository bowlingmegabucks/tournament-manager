
using System.Globalization;
using System.Runtime.Versioning;

namespace BowlingMegabucks.TournamentManager.Controls;

[SupportedOSPlatform("windows")]
internal partial class RecapSheetGameTotalControl : UserControl
{
    public RecapSheetGameTotalControl()
    {
        InitializeComponent();

        scratchScoreLabel.Text = string.Empty;
        handicapLabel.Text = string.Empty;
        handicapTotalLabel.Text = string.Empty;
    }

    public void SetHandicap(int handicap)
        => handicapLabel.Text = handicap > 0 ? handicap.ToString(CultureInfo.CurrentCulture) : "-";
}
