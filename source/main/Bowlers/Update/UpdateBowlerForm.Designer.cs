namespace NortheastMegabuck.Bowlers.Update;

partial class UpdateForm
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
        bowlerControl = new Controls.BowlerControl();
        registrationErrorProvider = new ErrorProvider(components);
        cancelButton = new Button();
        saveButton = new Button();
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).BeginInit();
        SuspendLayout();
        // 
        // bowlerControl
        // 
        bowlerControl.Location = new Point(2, 12);
        bowlerControl.Margin = new Padding(15, 3, 15, 9);
        bowlerControl.Name = "bowlerControl";
        bowlerControl.Size = new Size(597, 376);
        bowlerControl.TabIndex = 0;
        bowlerControl.LockDivisionFields = true;
        // 
        // registrationErrorProvider
        // 
        registrationErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        registrationErrorProvider.ContainerControl = bowlerControl;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(2, 400);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 114;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(486, 400);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 113;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // UpdateForm
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(573, 439);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(bowlerControl);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "UpdateForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Update Bowler";
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Controls.BowlerControl bowlerControl;
    private ErrorProvider registrationErrorProvider;
    private Button cancelButton;
    private Button saveButton;
}