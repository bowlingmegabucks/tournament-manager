
using System.Globalization;

namespace NortheastMegabuck.Controls;
public partial class RecapSheetGameTotalControl : UserControl
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
