using System.ComponentModel;

namespace NortheastMegabuck.Controls;
public partial class BowlerControl : UserControl, Bowlers.Add.IViewModel
{
    private static readonly IDictionary<string, string> _states = new Dictionary<string, string>
    {
        {"", ""},
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

        dateOfBirthPicker.MaxDate = new DateTime(DateTime.Today.Year - 1, 12, 31, 0, 0, 0, DateTimeKind.Unspecified);
        dateOfBirthPicker.Value = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
        dateOfBirthPicker.Checked = false;

        var genders = Enum.GetNames<Models.Gender>().ToDictionary(e => (int)Enum.Parse<Models.Gender>(e), e => e);

        genderDropdown.DataSource = genders.ToList();
        genderDropdown.DisplayMember = "Value";
        genderDropdown.ValueMember = "Key";
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BowlerId Id { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FirstName
    {
        get => personName.First;
        set => personName.First = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string MiddleInitial
    {
        get => personName.MiddleInitial;
        set => personName.MiddleInitial = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string LastName
    {
        get => personName.Last;
        set => personName.Last = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Suffix
    {
        get => personName.Suffix;
        set => personName.Suffix = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StreetAddress
    {
        get => streetText.Text;
        set => streetText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CityAddress
    {
        get => cityText.Text;
        set => cityText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StateAddress
    {
        get => stateDropdown.SelectedValue?.ToString() ?? string.Empty;
        set => stateDropdown.SelectedValue = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ZipCode
    {
        get => zipCodeText.Text;
        set => zipCodeText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string PhoneNumber
    {
        get => phoneNumberText.Text;
        set => phoneNumberText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string USBCId
    {
        get => usbcIdText.Text;
        set => usbcIdText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateOnly? DateOfBirth
    {
        get => dateOfBirthPicker.Checked ? DateOnly.FromDateTime(dateOfBirthPicker.Value.Date) : null;
        set
        {
            if (value.HasValue)
            {
                dateOfBirthPicker.Checked = true;
                dateOfBirthPicker.Value = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 0, 0, 0, DateTimeKind.Unspecified);
            }
            else
            {
                dateOfBirthPicker.Checked = false;
                dateOfBirthPicker.Value = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Models.Gender? Gender
    {
        get => genderDropdown.SelectedIndex == -1 ? null : (Models.Gender)genderDropdown.SelectedValue!;
        set => genderDropdown.SelectedValue = (int?)value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SocialSecurityNumber
    {
        get => socialSecurityNumberControl.Value;
        set => socialSecurityNumberControl.Value = value;
    }

    private void Control_Validated(object sender, EventArgs e)
        => bowlerErrorProvider.SetError((Control)sender, string.Empty);

    private void BowlerControl_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();
}