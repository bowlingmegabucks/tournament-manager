
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

    public short? Handicap
    {
        set => handicapLabel.Text = value.HasValue ? value.ToString() : "-";
    }
}
