using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace NortheastMegabuck.Controls;
public partial class SweeperDivisionControl : UserControl
{
    public DivisionId DivisionId { get; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? BonusPinsPerGame
    {
        get => bonusPinsPerGameValue.Value == 0 ? null : (int)bonusPinsPerGameValue.Value;
        set => bonusPinsPerGameValue.Value = value ?? 0;
    }

    public SweeperDivisionControl([NotNull] Divisions.IViewModel divisionViewModel)
    {
        InitializeComponent();

        DivisionId = divisionViewModel.Id;
        nameText.Text = divisionViewModel.DivisionName;
    }

    private void BonusPinsPerGameValue_Validating(object sender, CancelEventArgs e)
    {
        if (BonusPinsPerGame < 0)
        {
            e.Cancel = true;
            sweeperDivisionErrorProvider.SetError(bonusPinsPerGameValue, "Bonus pins per game must be greater than or equal to 0.");
        }
    }

    private void BonusPinsPerGameValue_Validated(object sender, EventArgs e)
        => sweeperDivisionErrorProvider.SetError(bonusPinsPerGameValue, string.Empty);
}
