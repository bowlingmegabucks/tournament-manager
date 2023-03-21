using System.ComponentModel;

namespace NortheastMegabuck.Controls;
public partial class PersonNameControl : UserControl
{
    public PersonNameControl()
    {
        InitializeComponent();
    }

    public string First
    {
        get => firstNameText.Text;
        set => firstNameText.Text = value;
    }

    private void FirstNameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(First))
        {
            personNameErrorProvider.SetError(firstNameText, "First name is required");
        }
    }

    public string MiddleInitial
    {
        get => middleInitialText.Text;
        set => middleInitialText.Text = value;
    }

    public string Last
    {
        get => lastNameText.Text;
        set => lastNameText.Text = value;
    }

    private void LastNameText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Last))
        {
            personNameErrorProvider.SetError(lastNameText, "Last name is required");
        }
    }

    public string Suffix
    {
        get => suffixText.Text;
        set => suffixText.Text = value;
    }
}
