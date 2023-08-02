namespace NortheastMegabuck.Controls;

partial class SocialSecurityNumberControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ssnText = new MaskedTextBox();
        ssnLabel = new LabelControl();
        verifyLink = new LinkLabel();
        SuspendLayout();
        // 
        // ssnText
        // 
        ssnText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        ssnText.Location = new Point(3, 28);
        ssnText.Mask = "000-00-0000";
        ssnText.Name = "ssnText";
        ssnText.Size = new Size(100, 26);
        ssnText.TabIndex = 0;
        ssnText.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        ssnText.TextChanged += SSNText_TextChanged;
        ssnText.Leave += SSNText_Leave;
        // 
        // ssnLabel
        // 
        ssnLabel.AutoSize = true;
        ssnLabel.Bold = false;
        ssnLabel.Location = new Point(3, 3);
        ssnLabel.Name = "ssnLabel";
        ssnLabel.Required = false;
        ssnLabel.Size = new Size(117, 19);
        ssnLabel.TabIndex = 1;
        ssnLabel.TabStop = false;
        ssnLabel.Text = "SSN:";
        // 
        // verifyLink
        // 
        verifyLink.AutoSize = true;
        verifyLink.Location = new Point(3, 57);
        verifyLink.Name = "verifyLink";
        verifyLink.Size = new Size(36, 15);
        verifyLink.TabIndex = 2;
        verifyLink.TabStop = true;
        verifyLink.Text = "Verify";
        verifyLink.LinkClicked += VerifyButton_LinkClicked;
        // 
        // SocialSecurityNumberControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(verifyLink);
        Controls.Add(ssnLabel);
        Controls.Add(ssnText);
        Name = "SocialSecurityNumberControl";
        Size = new Size(126, 86);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.MaskedTextBox ssnText;
    private Controls.LabelControl ssnLabel;
    private System.Windows.Forms.LinkLabel verifyLink;
}
