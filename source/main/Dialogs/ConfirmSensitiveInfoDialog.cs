using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NortheastMegabuck.Dialogs;
public partial class ConfirmSensitiveInfoDialog : Form
{
    /// <summary>
    ///
    /// </summary>
    public ConfirmSensitiveInfoDialog() : this("Password")
    { }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sensitiveInfoName"></param>
    public ConfirmSensitiveInfoDialog(string sensitiveInfoName)
    {
        InitializeComponent();

        nameLabel.Text = $"{sensitiveInfoName}:";
    }

    private void SensitiveText_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(sensitiveText.Text))
        {
            e.Cancel = true;
            SetError(sensitiveText, "Field is Required");
        }
    }

    /// <summary>
    ///
    /// </summary>
    public string EncryptedValue
        => sensitiveText.Text.Encrypt();

    private void SensitiveText_Validated(object sender, EventArgs e)
        => ClearError(sensitiveText);

    private void ConfirmSensitiveInfoDialog_Validating(object sender, CancelEventArgs e)
        => e.Cancel = !ValidateChildren();

    private void SetError(Control control, string message)
    {
        errorProvider.SetError(control, message);

        control.BackColor = Color.Red;
        control.ForeColor = Color.White;
    }

    private void ClearError(Control control)
    {
        errorProvider.SetError(control, null);

        control.ForeColor = default;
        control.BackColor = default;
    }
}
