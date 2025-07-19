using System.ComponentModel;
using BowlingMegabucks.TournamentManager.Registrations.Retrieve;

namespace BowlingMegabucks.TournamentManager.Registrations.Update;

internal partial class UpdateRegistrationDivisionForm
    : Form, IView
{
    private readonly UpdateRegistrationDivisionPresenter _presenter;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RegistrationId RegistrationId { get; private set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TournamentId TournamentId { get; private set; }

    public UpdateRegistrationDivisionForm(IServiceProvider services, TournamentId tournamentId, RegistrationId registrationId)
    {
        RegistrationId = registrationId;
        TournamentId = tournamentId;

        InitializeComponent();

        _presenter = new UpdateRegistrationDivisionPresenter(this, services);

        var genders = Models.Gender.ToDictionary();

        genderDropdown.DataSource = genders.ToList();
        genderDropdown.DisplayMember = "Value";
        genderDropdown.ValueMember = "Key";

        _ = _presenter.LoadAsync(tournamentId, registrationId, default);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? Average
    {
        get => averageValue.Value == 0 ? null : (int)averageValue.Value;
        set => averageValue.Value = value ?? 0;
    }

    public void BindDivisions(IEnumerable<Divisions.IViewModel> divisions)
    {
        divisionsDropdown.DataSource = divisions.ToList();

        divisionsDropdown.ValueMember = nameof(Divisions.IViewModel.Id);
        divisionsDropdown.DisplayMember = nameof(Divisions.IViewModel.DivisionName);
    }

    private void DivisionsDropDown_Validating(object sender, CancelEventArgs e)
    {
        if (divisionsDropdown.SelectedIndex == -1)
        {
            e.Cancel = true;
            registrationErrorProvider.SetError(divisionsDropdown, "Division is required");
        }
    }

    public void BindBowler(Bowlers.Retrieve.IViewModel viewModel)
    {
        personName.First = viewModel.FirstName;
        personName.MiddleInitial = viewModel.MiddleInitial;
        personName.Last = viewModel.LastName;
        personName.Suffix = viewModel.Suffix;

        Gender = viewModel.Gender;
        DateOfBirth = viewModel.DateOfBirth;
        UsbcId = viewModel.USBCId;
    }

    public void BindRegistration(ITournamentRegistrationViewModel tournamentRegistrationViewModel)
    {
        divisionsDropdown.SelectedValue = tournamentRegistrationViewModel.DivisionId;
        Average = tournamentRegistrationViewModel.Average;
    }

    public void Disable()
    {
        personName.Enabled = false;
        genderDropdown.Enabled = false;
        dateOfBirthPicker.Enabled = false;
        usbcIdText.Enabled = false;

        divisionsDropdown.Enabled = false;
        averageValue.Enabled = false;

        saveButton.Enabled = false;
    }

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public bool IsValid()
        => ValidateChildren();

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void OkToClose()
        => DialogResult = DialogResult.OK;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string UsbcId
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
        get => genderDropdown.SelectedIndex == -1 ? null : Models.Gender.FromValue((int)genderDropdown.SelectedValue!);
        set => genderDropdown.SelectedValue = value!.Value;
    }

    public DivisionId DivisionId
        => (DivisionId)divisionsDropdown.SelectedValue!;

    private void Control_Validated(object sender, EventArgs e)
        => registrationErrorProvider.SetError((Control)sender, string.Empty);

    private void UpdateRegistrationDivisionForm_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();

    private async void SaveButton_Click(object sender, EventArgs e)
        => await _presenter.ExecuteAsync(default).ConfigureAwait(true);
}
