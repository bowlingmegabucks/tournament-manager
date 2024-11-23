using System.ComponentModel;
using NortheastMegabuck.Registrations.Retrieve;

namespace NortheastMegabuck.Registrations.Update;

internal partial class UpdateRegistrationAverageForm
    : Form, IAverageView
{
    private readonly IConfiguration _config;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RegistrationId RegistrationId { get; private set; }

    public UpdateRegistrationAverageForm(IConfiguration config, RegistrationId registrationId)
    {
        _config = config;
        RegistrationId = registrationId;

        InitializeComponent();

        _ = new UpdateRegistrationAveragePresenter(config, this).LoadAsync(registrationId, default);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? Average
    {
        get => averageValue.Value == 0 ? null : (int)averageValue.Value;
        set => averageValue.Value = value ?? 0;
    }

    public void BindBowler(Bowlers.Retrieve.IViewModel viewModel)
    {
        personName.First = viewModel.FirstName;
        personName.MiddleInitial = viewModel.MiddleInitial;
        personName.Last = viewModel.LastName;
        personName.Suffix = viewModel.Suffix;
    }

    public void BindRegistration(ITournamentRegistrationViewModel tournamentRegistrationViewModel)
        => Average = tournamentRegistrationViewModel.Average;

    public void Disable()
    {
        personName.Enabled = false;
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

    private void Control_Validated(object sender, EventArgs e)
        => registrationErrorProvider.SetError((Control)sender, string.Empty);

    private void UpdateRegistrationDivisionForm_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();

    private async void SaveButton_Click(object sender, EventArgs e)
        => await new UpdateRegistrationAveragePresenter(_config, this).ExecuteAsync(default).ConfigureAwait(true);
}
