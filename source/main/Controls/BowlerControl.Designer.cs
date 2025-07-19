namespace BowlingMegabucks.TournamentManager.Controls;

partial class BowlerControl
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
        components = new System.ComponentModel.Container();
        genderDropdown = new ComboBox();
        dateOfBirthPicker = new DateTimePicker();
        phoneNumberText = new MaskedTextBox();
        usbcIdText = new TextBox();
        cityText = new TextBox();
        streetText = new TextBox();
        stateDropdown = new ComboBox();
        zipCodeText = new MaskedTextBox();
        emailText = new TextBox();
        streetLabel = new LabelControl();
        cityLabel = new LabelControl();
        stateLabel = new LabelControl();
        zipCodeLabel = new LabelControl();
        emailLabel = new LabelControl();
        dateOfBirthLabel = new LabelControl();
        genderLabel = new LabelControl();
        phoneNumberLabel = new LabelControl();
        usbcIdLabel = new LabelControl();
        bowlerErrorProvider = new ErrorProvider(components);
        socialSecurityNumberControl = new SocialSecurityNumberControl();
        personName = new PersonNameControl();
        ((System.ComponentModel.ISupportInitialize)bowlerErrorProvider).BeginInit();
        SuspendLayout();
        // 
        // genderDropdown
        // 
        genderDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        genderDropdown.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        genderDropdown.FormattingEnabled = true;
        genderDropdown.Location = new Point(3, 343);
        genderDropdown.Margin = new Padding(15, 3, 15, 9);
        genderDropdown.Name = "genderDropdown";
        genderDropdown.Size = new Size(98, 26);
        genderDropdown.TabIndex = 9;
        // 
        // dateOfBirthPicker
        // 
        dateOfBirthPicker.CustomFormat = "MM/dd/yyyy";
        dateOfBirthPicker.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        dateOfBirthPicker.Format = DateTimePickerFormat.Custom;
        dateOfBirthPicker.Location = new Point(3, 280);
        dateOfBirthPicker.Margin = new Padding(15, 3, 15, 9);
        dateOfBirthPicker.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        dateOfBirthPicker.Name = "dateOfBirthPicker";
        dateOfBirthPicker.ShowCheckBox = true;
        dateOfBirthPicker.Size = new Size(139, 26);
        dateOfBirthPicker.TabIndex = 6;
        // 
        // phoneNumberText
        // 
        phoneNumberText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        phoneNumberText.Location = new Point(194, 280);
        phoneNumberText.Margin = new Padding(15, 3, 15, 9);
        phoneNumberText.Mask = "(999) 000-0000";
        phoneNumberText.Name = "phoneNumberText";
        phoneNumberText.Size = new Size(100, 26);
        phoneNumberText.TabIndex = 7;
        phoneNumberText.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        // 
        // usbcIdText
        // 
        usbcIdText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        usbcIdText.Location = new Point(182, 343);
        usbcIdText.Margin = new Padding(15, 3, 15, 9);
        usbcIdText.MaxLength = 20;
        usbcIdText.Name = "usbcIdText";
        usbcIdText.PlaceholderText = "1234-5678";
        usbcIdText.Size = new Size(112, 26);
        usbcIdText.TabIndex = 10;
        // 
        // cityText
        // 
        cityText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        cityText.Location = new Point(3, 154);
        cityText.Margin = new Padding(15, 3, 15, 9);
        cityText.MaxLength = 50;
        cityText.Name = "cityText";
        cityText.PlaceholderText = "Anytown";
        cityText.Size = new Size(184, 26);
        cityText.TabIndex = 2;
        // 
        // streetText
        // 
        streetText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        streetText.Location = new Point(3, 91);
        streetText.Margin = new Padding(15, 3, 15, 9);
        streetText.MaxLength = 50;
        streetText.Name = "streetText";
        streetText.PlaceholderText = "123 Any St. Unit 456";
        streetText.Size = new Size(539, 26);
        streetText.TabIndex = 1;
        // 
        // stateDropdown
        // 
        stateDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        stateDropdown.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        stateDropdown.FormattingEnabled = true;
        stateDropdown.Location = new Point(217, 154);
        stateDropdown.Margin = new Padding(15, 3, 15, 9);
        stateDropdown.Name = "stateDropdown";
        stateDropdown.Size = new Size(195, 26);
        stateDropdown.TabIndex = 3;
        // 
        // zipCodeText
        // 
        zipCodeText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        zipCodeText.Location = new Point(442, 154);
        zipCodeText.Margin = new Padding(15, 3, 15, 9);
        zipCodeText.Mask = "00000-9999";
        zipCodeText.Name = "zipCodeText";
        zipCodeText.Size = new Size(100, 26);
        zipCodeText.TabIndex = 4;
        zipCodeText.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        // 
        // emailText
        // 
        emailText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        emailText.Location = new Point(3, 217);
        emailText.Margin = new Padding(15, 3, 15, 9);
        emailText.MaxLength = 50;
        emailText.Name = "emailText";
        emailText.PlaceholderText = "sample@BowlingMegabucks.TournamentManager.com";
        emailText.Size = new Size(539, 26);
        emailText.TabIndex = 5;
        emailText.Validating += EmailText_Validating;
        emailText.Validated += Control_Validated;
        // 
        // streetLabel
        // 
        streetLabel.AutoSize = true;
        streetLabel.Bold = false;
        streetLabel.Location = new Point(3, 66);
        streetLabel.Margin = new Padding(15, 3, 15, 3);
        streetLabel.Name = "streetLabel";
        streetLabel.Required = false;
        streetLabel.Size = new Size(117, 19);
        streetLabel.TabIndex = 23;
        streetLabel.TabStop = false;
        streetLabel.Text = "Street:";
        // 
        // cityLabel
        // 
        cityLabel.AutoSize = true;
        cityLabel.Bold = false;
        cityLabel.Location = new Point(3, 129);
        cityLabel.Margin = new Padding(15, 3, 15, 3);
        cityLabel.Name = "cityLabel";
        cityLabel.Required = false;
        cityLabel.Size = new Size(117, 19);
        cityLabel.TabIndex = 24;
        cityLabel.TabStop = false;
        cityLabel.Text = "City:";
        // 
        // stateLabel
        // 
        stateLabel.AutoSize = true;
        stateLabel.Bold = false;
        stateLabel.Location = new Point(217, 129);
        stateLabel.Margin = new Padding(15, 3, 15, 3);
        stateLabel.Name = "stateLabel";
        stateLabel.Required = false;
        stateLabel.Size = new Size(117, 19);
        stateLabel.TabIndex = 25;
        stateLabel.TabStop = false;
        stateLabel.Text = "State:";
        // 
        // zipCodeLabel
        // 
        zipCodeLabel.AutoSize = true;
        zipCodeLabel.Bold = false;
        zipCodeLabel.Location = new Point(442, 129);
        zipCodeLabel.Margin = new Padding(15, 3, 15, 3);
        zipCodeLabel.Name = "zipCodeLabel";
        zipCodeLabel.Required = false;
        zipCodeLabel.Size = new Size(117, 19);
        zipCodeLabel.TabIndex = 26;
        zipCodeLabel.TabStop = false;
        zipCodeLabel.Text = "Zip Code:";
        // 
        // emailLabel
        // 
        emailLabel.AutoSize = true;
        emailLabel.Bold = false;
        emailLabel.Location = new Point(3, 192);
        emailLabel.Margin = new Padding(15, 3, 15, 3);
        emailLabel.Name = "emailLabel";
        emailLabel.Required = true;
        emailLabel.Size = new Size(69, 19);
        emailLabel.TabIndex = 27;
        emailLabel.TabStop = false;
        emailLabel.Text = "Email:";
        // 
        // dateOfBirthLabel
        // 
        dateOfBirthLabel.AutoSize = true;
        dateOfBirthLabel.Bold = false;
        dateOfBirthLabel.Location = new Point(3, 255);
        dateOfBirthLabel.Margin = new Padding(15, 3, 15, 3);
        dateOfBirthLabel.Name = "dateOfBirthLabel";
        dateOfBirthLabel.Required = false;
        dateOfBirthLabel.Size = new Size(135, 19);
        dateOfBirthLabel.TabIndex = 28;
        dateOfBirthLabel.TabStop = false;
        dateOfBirthLabel.Text = "Date Of Birth:";
        // 
        // genderLabel
        // 
        genderLabel.AutoSize = true;
        genderLabel.Bold = false;
        genderLabel.Location = new Point(3, 318);
        genderLabel.Margin = new Padding(15, 3, 15, 3);
        genderLabel.Name = "genderLabel";
        genderLabel.Required = false;
        genderLabel.Size = new Size(117, 19);
        genderLabel.TabIndex = 29;
        genderLabel.TabStop = false;
        genderLabel.Text = "Gender:";
        // 
        // phoneNumberLabel
        // 
        phoneNumberLabel.AutoSize = true;
        phoneNumberLabel.Bold = false;
        phoneNumberLabel.Location = new Point(168, 255);
        phoneNumberLabel.Margin = new Padding(15, 3, 15, 3);
        phoneNumberLabel.Name = "phoneNumberLabel";
        phoneNumberLabel.Required = false;
        phoneNumberLabel.Size = new Size(126, 19);
        phoneNumberLabel.TabIndex = 30;
        phoneNumberLabel.TabStop = false;
        phoneNumberLabel.Text = "Phone Number:";
        // 
        // usbcIdLabel
        // 
        usbcIdLabel.AutoSize = true;
        usbcIdLabel.Bold = false;
        usbcIdLabel.Location = new Point(168, 318);
        usbcIdLabel.Margin = new Padding(15, 3, 15, 3);
        usbcIdLabel.Name = "usbcIdLabel";
        usbcIdLabel.Required = false;
        usbcIdLabel.Size = new Size(117, 19);
        usbcIdLabel.TabIndex = 31;
        usbcIdLabel.TabStop = false;
        usbcIdLabel.Text = "USBC Id:";
        // 
        // bowlerErrorProvider
        // 
        bowlerErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        bowlerErrorProvider.ContainerControl = this;
        // 
        // socialSecurityNumberControl
        // 
        socialSecurityNumberControl.Location = new Point(321, 250);
        socialSecurityNumberControl.Name = "socialSecurityNumberControl";
        socialSecurityNumberControl.ReadOnly = false;
        socialSecurityNumberControl.Size = new Size(130, 86);
        socialSecurityNumberControl.TabIndex = 8;
        socialSecurityNumberControl.Value = "";
        // 
        // personName
        // 
        personName.First = "";
        personName.Last = "";
        personName.Location = new Point(3, 3);
        personName.MiddleInitial = "";
        personName.Name = "personName";
        personName.Size = new Size(566, 63);
        personName.Suffix = "";
        personName.TabIndex = 0;
        // 
        // BowlerControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(personName);
        Controls.Add(socialSecurityNumberControl);
        Controls.Add(usbcIdLabel);
        Controls.Add(phoneNumberLabel);
        Controls.Add(genderLabel);
        Controls.Add(dateOfBirthLabel);
        Controls.Add(emailLabel);
        Controls.Add(zipCodeLabel);
        Controls.Add(stateLabel);
        Controls.Add(cityLabel);
        Controls.Add(streetLabel);
        Controls.Add(emailText);
        Controls.Add(cityText);
        Controls.Add(streetText);
        Controls.Add(stateDropdown);
        Controls.Add(zipCodeText);
        Controls.Add(genderDropdown);
        Controls.Add(dateOfBirthPicker);
        Controls.Add(phoneNumberText);
        Controls.Add(usbcIdText);
        Margin = new Padding(15, 3, 15, 9);
        Name = "BowlerControl";
        Size = new Size(573, 388);
        Validating += BowlerControl_Validating;
        ((System.ComponentModel.ISupportInitialize)bowlerErrorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private ComboBox genderDropdown;
    private DateTimePicker dateOfBirthPicker;
    private MaskedTextBox phoneNumberText;
    private TextBox usbcIdText;
    private TextBox cityText;
    private TextBox streetText;
    protected ComboBox stateDropdown;
    private MaskedTextBox zipCodeText;
    private TextBox emailText;
    private Controls.LabelControl streetLabel;
    private Controls.LabelControl cityLabel;
    private Controls.LabelControl stateLabel;
    private Controls.LabelControl zipCodeLabel;
    private Controls.LabelControl emailLabel;
    private Controls.LabelControl dateOfBirthLabel;
    private Controls.LabelControl genderLabel;
    private Controls.LabelControl phoneNumberLabel;
    private Controls.LabelControl usbcIdLabel;
    private ErrorProvider bowlerErrorProvider;
    private SocialSecurityNumberControl socialSecurityNumberControl;
    private PersonNameControl personName;
}
