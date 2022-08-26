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

        genderDropdown.DataSource = dictionary.ToList();
    }

    public DivisionId Id { get; set; }

    public short Number
    {
        get => Convert.ToInt16(numberText.Text);
        set => numberText.Text = value.ToString();
    }

    public string DivisionName
    {
        get => nameText.Text;
        set => nameText.Text = value;
    }

    private void NameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DivisionName))
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(nameText, "Division name is required");
        }
    }

    public TournamentId TournamentId { get; set; }

    public short? MinimumAge
    {
        get => minimumAgeValue.Value == 0 ? null : Convert.ToInt16(minimumAgeValue.Value);
        set => minimumAgeValue.Value = value ?? 0;
    }

    private void MinimumAgeValue_Validating(object sender, CancelEventArgs e)
    {
        if (MinimumAge.HasValue && MinimumAge.Value < 0)
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(minimumAgeValue, "Minimum age must be greater than or equal to 0");
        }
    }        

    public short? MaximumAge
    {
        get => maximumAgeValue.Value == 0 ? null : Convert.ToInt16(maximumAgeValue.Value);
        set => maximumAgeValue.Value = value ?? 0;
    }
    
    
    private void MaximumAgeValue_Validating(object sender, CancelEventArgs e)
    {
        if (MaximumAge.HasValue && MaximumAge.Value < 0)
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(maximumAgeValue, "Maximum age must be greater than or equal to 0");
        }
    } 

    public int? MinimumAverage
    {
        get => minimumAverageValue.Value == 0 ? null : Convert.ToInt32(minimumAverageValue.Value);
        set => minimumAverageValue.Value = value ?? 0;
    }

    private void MinimumAverage_Validating(object sender, CancelEventArgs e)
    {
        if (MinimumAverage.HasValue && (MinimumAverage.Value < 0 || MinimumAverage.Value > 300))
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(minimumAverageValue, "Minimum average must be between 0 and 300");
        }
    }

    public int? MaximumAverage
    {
        get => maximumAverageValue.Value == 0 ? null : Convert.ToInt32(maximumAverageValue.Value);
        set => maximumAverageValue.Value = value ?? 0;
    }

    private void MaximumAverage_Validating(object sender, CancelEventArgs e)
    {
        if (MaximumAverage.HasValue && (MaximumAverage.Value < 0 || MaximumAverage.Value > 300))
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(maximumAverageValue, "Maximum average must be between 0 and 300");
        }
    }    

    public decimal? HandicapPercentage
    {
        get => handicapPercentageValue.Value == 0 ? null : Convert.ToDecimal(handicapPercentageValue.Value);
        set => handicapPercentageValue.Value = value ?? 0;
    }

    private void HandicapPercentageValue_Validating(object sender, CancelEventArgs e)
    {
        if (HandicapPercentage.HasValue && HandicapPercentage.Value < 0)
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(handicapPercentageValue, "Handicap percentage must be greater than or equal to 0");
        }
    }

    public int? HandicapBase
    {
        get => handicapBaseValue.Value == 0 ? null : Convert.ToInt32(handicapBaseValue.Value);
        set => handicapBaseValue.Value = value ?? 0;
    }

    private void HandicapBaseValue_Validating(object sender, CancelEventArgs e)
    {
        if (HandicapBase.HasValue && (HandicapBase.Value < 0 || HandicapBase.Value > 300))
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(handicapBaseValue, "Handicap base must be between 0 and 300");
        }
    }

    public int? MaximumHandicapPerGame
    {
        get => maximumHandicapPerGameValue.Value == 0 ? null : Convert.ToInt32(maximumHandicapPerGameValue.Value);
        set => maximumHandicapPerGameValue.Value = value ?? 0;
    }

    private void MaximumHandicapPerGameValue_Validating(object sender, CancelEventArgs e)
    {
        if (MaximumHandicapPerGame.HasValue && MaximumHandicapPerGame.Value < 0)
        {
            e.Cancel = true;
            divisionErrorProvider.SetError(maximumHandicapPerGameValue, "Maximum handicap per game must be greater than 0");
        }
    }

    public Models.Gender? Gender
    {
        get => (int)genderDropdown.SelectedValue == -1 ? null : (Models.Gender)genderDropdown.SelectedValue;
        set => genderDropdown.SelectedValue = value.HasValue ? (int)value.Value : -1;
    }

    private void DivisionControl_Validated(object sender, EventArgs e)
        => divisionErrorProvider.SetError((Control)sender, string.Empty);
}
