using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NortheastMegabuck.Controls;
public partial class SocialSecurityNumberControl : UserControl
{
    /// <summary>
    ///
    /// </summary>
    public event EventHandler<EventArgs>? ValueChanged;

    /// <summary>
    ///
    /// </summary>
    public SocialSecurityNumberControl()
    {
        InitializeComponent();
    }

    /// <summary>
    ///
    /// </summary>
    public bool ReadOnly
    {
        get => ssnText.ReadOnly;
        set => ssnText.ReadOnly = value;
    }

    /// <summary>
    ///
    /// </summary>
    public void Clear()
    {
        Value = string.Empty;
        ssnText.Clear();
    }

    private string _value = string.Empty;

    /// <summary>
    ///
    /// </summary>
    public string Value
    {
        get => _value;
        set
        {
            _value = value;

            if (string.IsNullOrWhiteSpace(_value))
            {
                ssnText.PasswordChar = '\0';
                verifyLink.Enabled = false;
            }
            else
            {
                verifyLink.Enabled = true;
                ssnText.PasswordChar = '*';
                ssnText.Text = @"000000000";
            }
        }
    }

    private void SSNText_Leave(object sender, EventArgs e)
        => Value = ssnText.Text.Encrypt();

    private void SSNText_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ssnText.Text))
        {
            ssnText.PasswordChar = '\0';
        }

        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    private void VerifyButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var form = new Dialogs.ConfirmSensitiveInfoDialog("SSN");

        if (form.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        MessageBox.Show(this, $"SSN {(Encryption.ValuesMatch(Value, form.EncryptedValue) ? string.Empty : "Not ")}Matches", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
