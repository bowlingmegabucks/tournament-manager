
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

    public int Handicap
    {
        set => handicapLabel.Text = value> 0 ? value.ToString() : "-";
    }
}
