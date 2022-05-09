using System.ComponentModel;

namespace NewEnglandClassic.Contols;
public partial class DivisionControl : UserControl, Divisions.IViewModel
{
    public DivisionControl()
    {
        InitializeComponent();

        BindGenderDropdown();
    }

    private void BindGenderDropdown()
    {
        var dictionary = new Dictionary<int, string>
        {
            { -1, string.Empty },
            {(int)Models.Gender.Male, "Men" },
            {(int)Models.Gender.Female, "Women" }
        };

        ComboboxGender.DataSource = dictionary;
    }

    public Guid Id { get; set; }

    public short Number
    {
        get => Convert.ToInt16(TextboxNumber.Text);
        set => TextboxNumber.Text = value.ToString();
    }

    public string DivisionName
    {
        get => TextboxDivisionName.Text;
        set => TextboxDivisionName.Text = value;
    }

    private void TextboxDivisionName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DivisionName))
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(TextboxDivisionName, "Division name is required");
        }
    }

    public Guid TournamentId { get; set; }

    public short? MinimumAge
    {
        get => NumericMinimumAge.Value == 0 ? null : Convert.ToInt16(NumericMinimumAge.Value);
        set => NumericMinimumAge.Value = value ?? 0;
    }

    private void NumericMinimumAge_Validating(object sender, CancelEventArgs e)
    {
        if (MinimumAge.HasValue && MinimumAge.Value < 0)
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericMinimumAge, "Minimum age must be greater than or equal to 0");
        }
    }        

    public short? MaximumAge
    {
        get => NumericMaximumAge.Value == 0 ? null : Convert.ToInt16(NumericMaximumAge.Value);
        set => NumericMaximumAge.Value = value ?? 0;
    }
    
    
    private void NumericMaximumAge_Validating(object sender, CancelEventArgs e)
    {
        if (MaximumAge.HasValue && MaximumAge.Value < 0)
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericMaximumAge, "Maximum age must be greater than or equal to 0");
        }
    } 

    public int? MinimumAverage
    {
        get => NumericMinimumAverage.Value == 0 ? null : Convert.ToInt32(NumericMinimumAverage.Value);
        set => NumericMinimumAverage.Value = value ?? 0;
    }

    private void NumericMinimumAverage_Validating(object sender, CancelEventArgs e)
    {
        if (MinimumAverage.HasValue && (MinimumAverage.Value < 0 || MinimumAverage.Value > 300))
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericMinimumAverage, "Minimum average must be between 0 and 300");
        }
    }

    public int? MaximumAverage
    {
        get => NumericMaximumAverage.Value == 0 ? null : Convert.ToInt32(NumericMaximumAverage.Value);
        set => NumericMaximumAverage.Value = value ?? 0;
    }

    private void NumericMaximumAverage_Validating(object sender, CancelEventArgs e)
    {
        if (MaximumAverage.HasValue && (MaximumAverage.Value < 0 || MaximumAverage.Value > 300))
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericMaximumAverage, "Maximum average must be between 0 and 300");
        }
    }    

    public decimal? HandicapPercentage
    {
        get => NumericHandicapPercentage.Value == 0 ? null : Convert.ToDecimal(NumericHandicapPercentage.Value);
        set => NumericHandicapPercentage.Value = value ?? 0;
    }

    private void NumericHandicapPercentage_Validating(object sender, CancelEventArgs e)
    {
        if (HandicapPercentage.HasValue && HandicapPercentage.Value < 0)
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericHandicapPercentage, "Handicap percentage must be greater than or equal to 0");
        }
    }

    public int? HandicapBase
    {
        get => NumericHandicapBase.Value == 0 ? null : Convert.ToInt32(NumericHandicapBase.Value);
        set => NumericHandicapBase.Value = value ?? 0;
    }

    private void NumericHandicapBase_Validating(object sender, CancelEventArgs e)
    {
        if (HandicapBase.HasValue && (HandicapBase.Value < 0 || HandicapBase.Value > 300))
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericHandicapBase, "Handicap base must be between 0 and 300");
        }
    }

    public int? MaximumHandicapPerGame
    {
        get => NumericMaximumHandicapPerGame.Value == 0 ? null : Convert.ToInt32(NumericMaximumHandicapPerGame.Value);
        set => NumericMaximumHandicapPerGame.Value = value ?? 0;
    }

    private void NumericMaximumHandicapPerGame_Validating(object sender, CancelEventArgs e)
    {
        if (MaximumHandicapPerGame.HasValue && MaximumHandicapPerGame.Value < 0)
        {
            e.Cancel = true;
            ErrorProviderDivision.SetError(NumericMaximumHandicapPerGame, "Maximum handicap per game must be greater than 0");
        }
    }

    public Models.Gender? Gender
    {
        get => (int)ComboboxGender.SelectedValue == -1 ? null : (Models.Gender)ComboboxGender.SelectedValue;
        set => ComboboxGender.SelectedValue = value.HasValue ? (int)value.Value : -1;
    }

    private void DivisionControl_Validated(object sender, EventArgs e)
        => ErrorProviderDivision.SetError((Control)sender, string.Empty);
}
