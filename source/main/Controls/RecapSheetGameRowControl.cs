
namespace NortheastMegabuck.Controls;
public partial class RecapSheetGameRowControl : UserControl
{
    public RecapSheetGameRowControl()
    {
        InitializeComponent();

        laneLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        handicapLabel.Text = string.Empty;
        totalLabel.Text = string.Empty;
    }

    public short? Handicap
    {
        set => handicapLabel.Text = value.HasValue ? value.ToString() : "-";
    }

    public string LaneAssignment
    {
        set => laneLabel.Text = value;
    }
}
