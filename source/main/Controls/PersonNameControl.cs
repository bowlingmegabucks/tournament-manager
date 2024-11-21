using System.ComponentModel;
using System.Runtime.Versioning;

namespace NortheastMegabuck.Controls;

[SupportedOSPlatform("windows")]
internal partial class PersonNameControl : UserControl, Bowlers.Update.INameViewModel
{
    public PersonNameControl()
    {
        InitializeComponent();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string First
    {
        get => firstNameText.Text;
        set => firstNameText.Text = value;
    }

    string Bowlers.Update.INameViewModel.FirstName
        => First;

    private void FirstNameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(First))
        {
            personNameErrorProvider.SetError(firstNameText, "First name is required");
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string MiddleInitial
    {
        get => middleInitialText.Text;
        set => middleInitialText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string Last
    {
        get => lastNameText.Text;
        set => lastNameText.Text = value;
    }

    string Bowlers.Update.INameViewModel.LastName
        => Last;

    private void LastNameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Last))
        {
            personNameErrorProvider.SetError(lastNameText, "Last name is required");
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public string Suffix
    {
        get => suffixText.Text;
        set => suffixText.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public bool ReadOnly
    {
        get => firstNameText.ReadOnly;
        set
        {
            firstNameText.ReadOnly = value;
            middleInitialText.ReadOnly = value;
            lastNameText.ReadOnly = value;
            suffixText.ReadOnly = value;
        }
    }
}
