﻿using System.ComponentModel;
using BowlingMegabucks.TournamentManager.Properties;

namespace BowlingMegabucks.TournamentManager.Controls;
internal sealed partial class SocialSecurityNumberControl 
    : UserControl
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                ssnText.Text = Resources.DummySsn;
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

        MessageBox.Show(this, $"SSN {(Encryption.ValuesMatch(Value, form.EncryptedValue) ? "Matches" : "Does Not Match ")}", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
