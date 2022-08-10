using System.ComponentModel;

namespace NewEnglandClassic.Contols;
internal partial class BowlerControl : UserControl, Bowlers.Add.IViewModel
{
    private readonly static IDictionary<string, string> _states = new Dictionary<string, string>
    {
        {"AL", "Alabama" },
        {"AK", "Alaska" },
        {"AZ", "Arizona" },
        {"AR", "Arkansas" },
        {"CA", "California" },
        {"CO", "Colorado" },
        {"CT", "Connecticut" },
        {"DE", "Delaware" },
        {"FL", "Florida" },
        {"GA", "Georgia" },
        {"HI", "Hawaii" },
        {"ID", "Idaho" },
        {"IL", "Illinois" },
        {"IN", "Indiana" },
        {"IA", "Iowa" },
        {"KS", "Kansas" },
        {"KY", "Kentucky" },
        {"LA", "Louisiana" },
        {"ME", "Maine" },
        {"MD", "Maryland" },
        {"MA", "Massachusetts" },
        {"MI", "Michigan" },
        {"MN", "Minnesota" },
        {"MS", "Mississippi" },
        {"MO", "Missouri" },
        {"MT", "Montana" },
        {"NE", "Nebraska" },
        {"NV", "Nevada" },
        {"NH", "New Hampshire" },
        {"NJ", "New Jersey" },
        {"NM", "New Mexico" },
        {"NY", "New York" },
        {"NC", "North Carolina" },
        {"ND", "North Dakota" },
        {"OH", "Ohio" },
        {"OK", "Oklahoma" },
        {"OR", "Oregon" },
        {"PA", "Pennsylvania" },
        {"RI", "Rhode Island" },
        {"SC", "South Carolina" },
        {"SD", "South Dakota" },
        {"TN", "Tennessee" },
        {"TX", "Texas" },
        {"UT", "Utah" },
        {"VT", "Vermont" },
        {"VA", "Virginia" },
        {"WA", "Washington" },
        {"WV", "West Virginia" },
        {"WI", "Wisconsin" },
        {"WY", "Wyoming" }
    };
    
    public BowlerControl()
    {
        InitializeComponent();

        ComboBoxStateUS.DataSource = _states.ToList();
        ComboBoxStateUS.DisplayMember = "Value";
        ComboBoxStateUS.ValueMember = "Key";

        DatePickerDateOfBirth.MaxDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
        DatePickerDateOfBirth.Value = new DateTime(1900, 1, 1);
        DatePickerDateOfBirth.Checked = false;

        var genders = Enum.GetNames(typeof(Models.Gender)).ToDictionary(e => (int)Enum.Parse(typeof(Models.Gender), e), e => e);

        ComboBoxGender.DataSource = genders.ToList();
        ComboBoxGender.DisplayMember = "Value";
        ComboBoxGender.ValueMember = "Key";
    }

    public BowlerId Id { get; set; }

    public string FirstName
    {
        get => TextboxFirstName.Text;
        set => TextboxFirstName.Text = value;
    }

    private void TextboxFirstName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            ErrorProviderBowler.SetError(TextboxFirstName, "First name is required");
        }
    }

    public string MiddleInitial
    {
        get => TextboxMiddleInitial.Text;
        set => TextboxMiddleInitial.Text = value;
    }

    public string LastName
    {
        get => TextboxLastName.Text;
        set => TextboxLastName.Text = value;
    }

    private void TextboxLastName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LastName))
        {
            ErrorProviderBowler.SetError(TextboxLastName, "Last name is required");
        }
    }

    public string Suffix
    {
        get => TextboxSuffix.Text;
        set => TextboxSuffix.Text = value;
    }
    
    public string StreetAddress
    {
        get => TextboxStreet.Text;
        set => TextboxStreet.Text = value;
    }

    public string CityAddress
    {
        get => TextboxCity.Text;
        set => TextboxCity.Text = value;
    }
    
    public string StateAddress
    {
        get => ComboBoxStateUS.SelectedValue?.ToString() ?? string.Empty;
        set => ComboBoxStateUS.SelectedValue = value;
    }

    public string ZipCode
    {
        get => TextboxZipCodeUS.Text;
        set => TextboxZipCodeUS.Text = value;
    }

    public string EmailAddress
    {
        get => TextboxEmail.Text;
        set => TextboxEmail.Text = value;
    }

    private void TextboxEmail_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailAddress))
        {
            ErrorProviderBowler.SetError(TextboxEmail, "Email is required");
        }
    }

    public string PhoneNumber
    {
        get => TextboxPhoneNumber.Text;
        set => TextboxPhoneNumber.Text = value;
    }

    public string USBCId
    {
        get => TextboxUSBCId.Text;
        set => TextboxUSBCId.Text = value;
    }

    private void TextboxUSBCId_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(USBCId))
        {
            ErrorProviderBowler.SetError(TextboxFirstName, "USBC id is required");
        }
    }

    public DateOnly? DateOfBirth
    {
        get => DatePickerDateOfBirth.Checked ? DateOnly.FromDateTime(DatePickerDateOfBirth.Value.Date) : null;
        set
        {
            if (value.HasValue)
            {
                DatePickerDateOfBirth.Checked = true;
                DatePickerDateOfBirth.Value = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day);
            }
            else
            {
                DatePickerDateOfBirth.Checked = false;
                DatePickerDateOfBirth.Value = new DateTime(1900, 1, 1);
            }
        }
    }
    
    public Models.Gender? Gender
    {
        get => ComboBoxGender.SelectedIndex == -1 ? null: (Models.Gender)ComboBoxGender.SelectedValue;
        set => ComboBoxGender.SelectedValue = (int?)value ?? null;
    }
    
    private void Control_Validated(object sender, EventArgs e)
        => ErrorProviderBowler.SetError((Control)sender, string.Empty);

    private void BowlerControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}