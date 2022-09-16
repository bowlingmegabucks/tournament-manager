
namespace NortheastMegabuck.Contols;
public partial class SweeperDivisionsControl : UserControl
{
    public SweeperDivisionsControl()
    {
        InitializeComponent();
    }

    public IDictionary<DivisionId, int?> Divisions
        => sweeperDivisionsFlowLayoutPanel.Controls.OfType<SweeperDivisionControl>().ToDictionary(d => d.DivisionId, d => d.BonusPinsPerGame);
    
    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
    {
        foreach (var division in divisions)
        {
            sweeperDivisionsFlowLayoutPanel.Controls.Add(new SweeperDivisionControl(division)); 
        }
    }
}
