namespace NortheastMegabuck.Registrations.Update;

partial class UpdateRegistrationAverageForm
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
        personName = new Controls.PersonNameControl();
        averageValue = new Controls.NumericControl();
        averageLabel = new Controls.LabelControl();
        cancelButton = new Button();
        saveButton = new Button();
        registrationErrorProvider = new ErrorProvider(components);
        ((System.ComponentModel.ISupportInitialize)averageValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).BeginInit();
        SuspendLayout();
        // 
        // personName
        // 
        personName.Location = new Point(12, 12);
        personName.Name = "personName";
        personName.Size = new Size(566, 63);
        personName.TabIndex = 0;
        personName.ReadOnly = true;
        // 
        // averageValue
        // 
        averageValue.Font = new Font("Segoe UI", 11F);
        averageValue.Location = new Point(12, 107);
        averageValue.Margin = new Padding(3, 4, 30, 10);
        averageValue.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        averageValue.Name = "averageValue";
        averageValue.Size = new Size(99, 27);
        averageValue.TabIndex = 5;
        averageValue.TextAlign = HorizontalAlignment.Right;
        // 
        // averageLabel
        // 
        averageLabel.AutoSize = true;
        averageLabel.Location = new Point(12, 81);
        averageLabel.Name = "averageLabel";
        averageLabel.Required = true;
        averageLabel.Size = new Size(87, 19);
        averageLabel.TabIndex = 115;
        averageLabel.TabStop = false;
        averageLabel.Text = "Average:";
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 163);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 117;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.DialogResult = DialogResult.OK;
        saveButton.Location = new Point(494, 163);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 116;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // registrationErrorProvider
        // 
        registrationErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        registrationErrorProvider.ContainerControl = this;
        // 
        // UpdateRegistrationAverageForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(598, 199);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(averageValue);
        Controls.Add(averageLabel);
        Controls.Add(personName);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UpdateRegistrationAverageForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Update Registration Average";
        Validating += UpdateRegistrationDivisionForm_Validating;
        Validated += Control_Validated;
        ((System.ComponentModel.ISupportInitialize)averageValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.PersonNameControl personName;
    private Controls.NumericControl averageValue;
    private Controls.LabelControl averageLabel;
    private Button cancelButton;
    private Button saveButton;
    private ErrorProvider registrationErrorProvider;
}