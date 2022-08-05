using System.ComponentModel;

namespace NewEnglandClassic.Contols;
internal partial class SweeperDivisionControl : UserControl
{
    public DivisionId DivisionId { get; }
    
    public int? BonusPinsPerGame
    {
        get => NumericBonusPinsPerGame.Value == 0 ? null : (int)NumericBonusPinsPerGame.Value;
        set => NumericBonusPinsPerGame.Value = value ?? 0;
    }
    
    public SweeperDivisionControl(Divisions.IViewModel divisionViewModel)
    {
        InitializeComponent();
        
        DivisionId = divisionViewModel.Id;
        TextboxDivisionName.Text = divisionViewModel.DivisionName;
    }

    private void NumericBonusPinsPerGame_Validating(object sender, CancelEventArgs e)
    {
        if (BonusPinsPerGame < 0)
        {
            e.Cancel = true;
            ErrorProviderSweeperDivision.SetError(NumericBonusPinsPerGame, "Bonus pins per game must be greater than or equal to 0.");
        }
    }

    private void NumericBonusPinsPerGame_Validated(object sender, EventArgs e) 
        => ErrorProviderSweeperDivision.SetError(NumericBonusPinsPerGame, string.Empty);
}
