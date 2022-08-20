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

        stateDropdown.DataSource = _states.ToList();
        stateDropdown.DisplayMember = "Value";
        stateDropdown.ValueMember = "Key";

        dateOfBirthPicker.MaxDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
        dateOfBirthPicker.Value = new DateTime(1900, 1, 1);
        dateOfBirthPicker.Checked = false;

        var genders = Enum.GetNames(typeof(Models.Gender)).ToDictionary(e => (int)Enum.Parse(typeof(Models.Gender), e), e => e);

        genderDropdown.DataSource = genders.ToList();
        genderDropdown.DisplayMember = "Value";
        genderDropdown.ValueMember = "Key";
    }

    public BowlerId Id { get; set; }

    public string FirstName
    {
        get => firstNameText.Text;
        set => firstNameText.Text = value;
    }

    private void FirstNameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            bowlerErrorProvider.SetError(firstNameText, "First name is required");
        }
    }

    public string MiddleInitial
    {
        get => middleInitialText.Text;
        set => middleInitialText.Text = value;
    }

    public string LastName
    {
        get => lastNameText.Text;
        set => lastNameText.Text = value;
    }

    private void LastNameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LastName))
        {
            bowlerErrorProvider.SetError(lastNameText, "Last name is required");
        }
    }

    public string Suffix
    {
        get => suffixText.Text;
        set => suffixText.Text = value;
    }
    
    public string StreetAddress
    {
        get => streetText.Text;
        set => streetText.Text = value;
    }

    public string CityAddress
    {
        get => cityText.Text;
        set => cityText.Text = value;
    }
    
    public string StateAddress
    {
        get => stateDropdown.SelectedValue?.ToString() ?? string.Empty;
        set => stateDropdown.SelectedValue = value;
    }

    public string ZipCode
    {
        get => zipCodeText.Text;
        set => zipCodeText.Text = value;
    }

    public string EmailAddress
    {
        get => emailText.Text;
        set => emailText.Text = value;
    }

    private void EmailText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailAddress))
        {
            bowlerErrorProvider.SetError(emailText, "Email is required");
        }
    }

    public string PhoneNumber
    {
        get => phoneNumberText.Text;
        set => phoneNumberText.Text = value;
    }

    public string USBCId
    {
        get => usbcIdText.Text;
        set => usbcIdText.Text = value;
    }

    private void USBCIdText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(USBCId))
        {
            bowlerErrorProvider.SetError(firstNameText, "USBC id is required");
        }
    }

    public DateOnly? DateOfBirth
    {
        get => dateOfBirthPicker.Checked ? DateOnly.FromDateTime(dateOfBirthPicker.Value.Date) : null;
        set
        {
            if (value.HasValue)
            {
                dateOfBirthPicker.Checked = true;
                dateOfBirthPicker.Value = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day);
            }
            else
            {
                dateOfBirthPicker.Checked = false;
                dateOfBirthPicker.Value = new DateTime(1900, 1, 1);
            }
        }
    }
    
    public Models.Gender? Gender
    {
        get => genderDropdown.SelectedIndex == -1 ? null: (Models.Gender)genderDropdown.SelectedValue;
        set => genderDropdown.SelectedValue = (int?)value ?? null;
    }
    
    private void Control_Validated(object sender, EventArgs e)
        => bowlerErrorProvider.SetError((Control)sender, string.Empty);

    private void BowlerControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}