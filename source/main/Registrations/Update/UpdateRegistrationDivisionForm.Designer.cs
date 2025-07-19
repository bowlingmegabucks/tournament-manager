namespace BowlingMegabucks.TournamentManager.Registrations.Update;

partial class UpdateRegistrationDivisionForm
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
        usbcIdLabel = new Controls.LabelControl();
        genderLabel = new Controls.LabelControl();
        dateOfBirthLabel = new Controls.LabelControl();
        genderDropdown = new ComboBox();
        dateOfBirthPicker = new DateTimePicker();
        usbcIdText = new TextBox();
        averageValue = new Controls.NumericControl();
        averageLabel = new Controls.LabelControl();
        divisionLabel = new Controls.LabelControl();
        divisionsDropdown = new ComboBox();
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
        // usbcIdLabel
        // 
        usbcIdLabel.AutoSize = true;
        usbcIdLabel.Location = new Point(309, 84);
        usbcIdLabel.Margin = new Padding(15, 3, 15, 3);
        usbcIdLabel.Name = "usbcIdLabel";
        usbcIdLabel.Required = false;
        usbcIdLabel.Size = new Size(117, 19);
        usbcIdLabel.TabIndex = 38;
        usbcIdLabel.TabStop = false;
        usbcIdLabel.Text = "USBC Id:";
        // 
        // genderLabel
        // 
        genderLabel.AutoSize = true;
        genderLabel.Location = new Point(181, 81);
        genderLabel.Margin = new Padding(15, 3, 15, 3);
        genderLabel.Name = "genderLabel";
        genderLabel.Required = false;
        genderLabel.Size = new Size(117, 19);
        genderLabel.TabIndex = 37;
        genderLabel.TabStop = false;
        genderLabel.Text = "Gender:";
        // 
        // dateOfBirthLabel
        // 
        dateOfBirthLabel.AutoSize = true;
        dateOfBirthLabel.Location = new Point(12, 81);
        dateOfBirthLabel.Margin = new Padding(15, 3, 15, 3);
        dateOfBirthLabel.Name = "dateOfBirthLabel";
        dateOfBirthLabel.Required = false;
        dateOfBirthLabel.Size = new Size(135, 19);
        dateOfBirthLabel.TabIndex = 36;
        dateOfBirthLabel.TabStop = false;
        dateOfBirthLabel.Text = "Date Of Birth:";
        // 
        // genderDropdown
        // 
        genderDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        genderDropdown.Font = new Font("Calibri", 11.25F);
        genderDropdown.FormattingEnabled = true;
        genderDropdown.Location = new Point(181, 106);
        genderDropdown.Margin = new Padding(15, 3, 15, 9);
        genderDropdown.Name = "genderDropdown";
        genderDropdown.Size = new Size(98, 26);
        genderDropdown.TabIndex = 2;
        // 
        // dateOfBirthPicker
        // 
        dateOfBirthPicker.CustomFormat = "MM/dd/yyyy";
        dateOfBirthPicker.Font = new Font("Calibri", 11.25F);
        dateOfBirthPicker.Format = DateTimePickerFormat.Custom;
        dateOfBirthPicker.Location = new Point(12, 106);
        dateOfBirthPicker.Margin = new Padding(15, 3, 15, 9);
        dateOfBirthPicker.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        dateOfBirthPicker.Name = "dateOfBirthPicker";
        dateOfBirthPicker.ShowCheckBox = true;
        dateOfBirthPicker.Size = new Size(139, 26);
        dateOfBirthPicker.TabIndex = 1;
        // 
        // usbcIdText
        // 
        usbcIdText.Font = new Font("Calibri", 11.25F);
        usbcIdText.Location = new Point(309, 109);
        usbcIdText.Margin = new Padding(15, 3, 15, 9);
        usbcIdText.MaxLength = 20;
        usbcIdText.Name = "usbcIdText";
        usbcIdText.PlaceholderText = "1234-5678";
        usbcIdText.Size = new Size(112, 26);
        usbcIdText.TabIndex = 3;
        // 
        // averageValue
        // 
        averageValue.Font = new Font("Segoe UI", 11F);
        averageValue.Location = new Point(262, 168);
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
        averageLabel.Location = new Point(262, 142);
        averageLabel.Name = "averageLabel";
        averageLabel.Required = true;
        averageLabel.Size = new Size(87, 19);
        averageLabel.TabIndex = 115;
        averageLabel.TabStop = false;
        averageLabel.Text = "Average:";
        // 
        // divisionLabel
        // 
        divisionLabel.AutoSize = true;
        divisionLabel.Location = new Point(12, 144);
        divisionLabel.Margin = new Padding(15, 3, 15, 3);
        divisionLabel.Name = "divisionLabel";
        divisionLabel.Required = true;
        divisionLabel.Size = new Size(96, 19);
        divisionLabel.TabIndex = 114;
        divisionLabel.TabStop = false;
        divisionLabel.Text = "Division:";
        // 
        // divisionsDropdown
        // 
        divisionsDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        divisionsDropdown.Font = new Font("Calibri", 11.25F);
        divisionsDropdown.FormattingEnabled = true;
        divisionsDropdown.Location = new Point(12, 169);
        divisionsDropdown.Margin = new Padding(15, 3, 15, 9);
        divisionsDropdown.Name = "divisionsDropdown";
        divisionsDropdown.Size = new Size(232, 26);
        divisionsDropdown.TabIndex = 4;
        divisionsDropdown.Validating += DivisionsDropDown_Validating;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 237);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 117;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(503, 237);
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
        // UpdateRegistrationDivisionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(598, 272);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(averageValue);
        Controls.Add(averageLabel);
        Controls.Add(divisionLabel);
        Controls.Add(divisionsDropdown);
        Controls.Add(personName);
        Controls.Add(usbcIdLabel);
        Controls.Add(genderLabel);
        Controls.Add(dateOfBirthLabel);
        Controls.Add(genderDropdown);
        Controls.Add(dateOfBirthPicker);
        Controls.Add(usbcIdText);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UpdateRegistrationDivisionForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Update Registration Division";
        Validating += UpdateRegistrationDivisionForm_Validating;
        Validated += Control_Validated;
        ((System.ComponentModel.ISupportInitialize)averageValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.PersonNameControl personName;
    private Controls.LabelControl usbcIdLabel;
    private Controls.LabelControl genderLabel;
    private Controls.LabelControl dateOfBirthLabel;
    private ComboBox genderDropdown;
    private DateTimePicker dateOfBirthPicker;
    private TextBox usbcIdText;
    private Controls.NumericControl averageValue;
    private Controls.LabelControl averageLabel;
    private Controls.LabelControl divisionLabel;
    private ComboBox divisionsDropdown;
    private Button cancelButton;
    private Button saveButton;
    private ErrorProvider registrationErrorProvider;
}