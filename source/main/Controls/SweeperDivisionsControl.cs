
using System.Diagnostics.CodeAnalysis;

namespace NortheastMegabuck.Controls;
public partial class SweeperDivisionsControl : UserControl
{
    public SweeperDivisionsControl()
    {
        InitializeComponent();
    }

    public IDictionary<DivisionId, int?> Divisions
        => sweeperDivisionsFlowLayoutPanel.Controls.OfType<SweeperDivisionControl>().ToDictionary(d => d.DivisionId, d => d.BonusPinsPerGame);

    public void BindDivisions([NotNull] IEnumerable<Divisions.IViewModel> divisions)
    {
        foreach (var division in divisions)
        {
            sweeperDivisionsFlowLayoutPanel.Controls.Add(new SweeperDivisionControl(division)); 
        }
    }
}
