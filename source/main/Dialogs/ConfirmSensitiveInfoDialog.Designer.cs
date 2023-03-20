namespace NortheastMegabuck.Dialogs;

partial class ConfirmSensitiveInfoDialog
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        cancelButton = new Button();
        okButton = new Button();
        sensitiveText = new TextBox();
        nameLabel = new Controls.LabelControl();
        errorProvider = new ErrorProvider(components);
        ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
        SuspendLayout();
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        cancelButton.Location = new Point(289, 69);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(93, 34);
        cancelButton.TabIndex = 2;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // okButton
        // 
        okButton.DialogResult = DialogResult.OK;
        okButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        okButton.Location = new Point(190, 69);
        okButton.Name = "okButton";
        okButton.Size = new Size(93, 34);
        okButton.TabIndex = 1;
        okButton.Text = "OK";
        okButton.UseVisualStyleBackColor = true;
        // 
        // sensitiveText
        // 
        sensitiveText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        sensitiveText.Location = new Point(12, 37);
        sensitiveText.MaxLength = 50;
        sensitiveText.Name = "sensitiveText";
        sensitiveText.PasswordChar = '*';
        sensitiveText.Size = new Size(370, 26);
        sensitiveText.TabIndex = 0;
        sensitiveText.Validating += SensitiveText_Validating;
        sensitiveText.Validated += SensitiveText_Validated;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Bold = false;
        nameLabel.Location = new Point(12, 12);
        nameLabel.Name = "nameLabel";
        nameLabel.Required = true;
        nameLabel.Size = new Size(96, 19);
        nameLabel.TabIndex = 12;
        nameLabel.TabStop = false;
        nameLabel.Text = "Password:";
        // 
        // errorProvider
        // 
        errorProvider.ContainerControl = this;
        // 
        // ConfirmSensitiveInfoDialog
        // 
        AcceptButton = okButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new Size(416, 120);
        Controls.Add(nameLabel);
        Controls.Add(sensitiveText);
        Controls.Add(cancelButton);
        Controls.Add(okButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ConfirmSensitiveInfoDialog";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Confirm";
        TopMost = true;
        Validating += ConfirmSensitiveInfoDialog_Validating;
        ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox sensitiveText;
    private Controls.LabelControl nameLabel;
    private ErrorProvider errorProvider;
}