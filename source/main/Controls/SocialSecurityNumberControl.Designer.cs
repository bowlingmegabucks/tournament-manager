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
        this.ssnText = new System.Windows.Forms.MaskedTextBox();
        this.ssnLabel = new NortheastMegabuck.Controls.LabelControl();
        this.verifyLink = new System.Windows.Forms.LinkLabel();
        this.SuspendLayout();
        // 
        // ssnText
        // 
        this.ssnText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.ssnText.Location = new System.Drawing.Point(3, 28);
        this.ssnText.Mask = "000-00-0000";
        this.ssnText.Name = "ssnText";
        this.ssnText.Size = new System.Drawing.Size(100, 26);
        this.ssnText.TabIndex = 0;
        this.ssnText.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
        this.ssnText.TextChanged += new System.EventHandler(this.SSNText_TextChanged);
        this.ssnText.Leave += new System.EventHandler(this.SSNText_Leave);
        // 
        // ssnLabel
        // 
        this.ssnLabel.AutoSize = true;
        this.ssnLabel.Bold = false;
        this.ssnLabel.Location = new System.Drawing.Point(3, 3);
        this.ssnLabel.Name = "ssnLabel";
        this.ssnLabel.Required = false;
        this.ssnLabel.Size = new System.Drawing.Size(117, 19);
        this.ssnLabel.TabIndex = 1;
        this.ssnLabel.TabStop = false;
        this.ssnLabel.Text = "SSN:";
        // 
        // verifyLink
        // 
        this.verifyLink.AutoSize = true;
        this.verifyLink.Location = new System.Drawing.Point(64, 57);
        this.verifyLink.Name = "verifyLink";
        this.verifyLink.Size = new System.Drawing.Size(36, 15);
        this.verifyLink.TabIndex = 2;
        this.verifyLink.TabStop = true;
        this.verifyLink.Text = "Verify";
        this.verifyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.VerifyButton_LinkClicked);
        // 
        // SocialSecurityNumberControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.verifyLink);
        this.Controls.Add(this.ssnLabel);
        this.Controls.Add(this.ssnText);
        this.Name = "SocialSecurityNumberControl";
        this.Size = new System.Drawing.Size(126, 86);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MaskedTextBox ssnText;
    private Controls.LabelControl ssnLabel;
    private System.Windows.Forms.LinkLabel verifyLink;
}
