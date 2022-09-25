namespace NortheastMegabuck.Controls;

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
            this.components = new System.ComponentModel.Container();
            this.firstNameText = new System.Windows.Forms.TextBox();
            this.genderDropdown = new System.Windows.Forms.ComboBox();
            this.dateOfBirthPicker = new System.Windows.Forms.DateTimePicker();
            this.middleInitialText = new System.Windows.Forms.TextBox();
            this.suffixText = new System.Windows.Forms.TextBox();
            this.lastNameText = new System.Windows.Forms.TextBox();
            this.phoneNumberText = new System.Windows.Forms.MaskedTextBox();
            this.usbcIdText = new System.Windows.Forms.TextBox();
            this.cityText = new System.Windows.Forms.TextBox();
            this.streetText = new System.Windows.Forms.TextBox();
            this.stateDropdown = new System.Windows.Forms.ComboBox();
            this.zipCodeText = new System.Windows.Forms.MaskedTextBox();
            this.emailText = new System.Windows.Forms.TextBox();
            this.firstNameLabel = new NortheastMegabuck.Controls.LabelControl();
            this.lastNameLabel = new NortheastMegabuck.Controls.LabelControl();
            this.suffixLabel = new NortheastMegabuck.Controls.LabelControl();
            this.middleInitialLabel = new NortheastMegabuck.Controls.LabelControl();
            this.streetLabel = new NortheastMegabuck.Controls.LabelControl();
            this.cityLabel = new NortheastMegabuck.Controls.LabelControl();
            this.stateLabel = new NortheastMegabuck.Controls.LabelControl();
            this.zipCodeLabel = new NortheastMegabuck.Controls.LabelControl();
            this.emailLabel = new NortheastMegabuck.Controls.LabelControl();
            this.dateOfBirthLabel = new NortheastMegabuck.Controls.LabelControl();
            this.genderLabel = new NortheastMegabuck.Controls.LabelControl();
            this.phoneNumberLabel = new NortheastMegabuck.Controls.LabelControl();
            this.usbcIdLabel = new NortheastMegabuck.Controls.LabelControl();
            this.bowlerErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bowlerErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // firstNameText
            // 
            this.firstNameText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.firstNameText.Location = new System.Drawing.Point(3, 28);
            this.firstNameText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.firstNameText.MaxLength = 20;
            this.firstNameText.Name = "firstNameText";
            this.firstNameText.PlaceholderText = "Joseph";
            this.firstNameText.Size = new System.Drawing.Size(132, 26);
            this.firstNameText.TabIndex = 0;
            this.firstNameText.Validating += new System.ComponentModel.CancelEventHandler(this.FirstNameText_Validating);
            this.firstNameText.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // genderDropdown
            // 
            this.genderDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderDropdown.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.genderDropdown.FormattingEnabled = true;
            this.genderDropdown.Location = new System.Drawing.Point(172, 280);
            this.genderDropdown.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.genderDropdown.Name = "genderDropdown";
            this.genderDropdown.Size = new System.Drawing.Size(98, 26);
            this.genderDropdown.TabIndex = 10;
            // 
            // dateOfBirthPicker
            // 
            this.dateOfBirthPicker.CustomFormat = "MM/dd/yyyy";
            this.dateOfBirthPicker.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateOfBirthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateOfBirthPicker.Location = new System.Drawing.Point(3, 280);
            this.dateOfBirthPicker.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.dateOfBirthPicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateOfBirthPicker.Name = "dateOfBirthPicker";
            this.dateOfBirthPicker.ShowCheckBox = true;
            this.dateOfBirthPicker.Size = new System.Drawing.Size(139, 26);
            this.dateOfBirthPicker.TabIndex = 9;
            // 
            // middleInitialText
            // 
            this.middleInitialText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.middleInitialText.Location = new System.Drawing.Point(506, 28);
            this.middleInitialText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.middleInitialText.MaxLength = 3;
            this.middleInitialText.Name = "middleInitialText";
            this.middleInitialText.PlaceholderText = "A";
            this.middleInitialText.Size = new System.Drawing.Size(36, 26);
            this.middleInitialText.TabIndex = 3;
            // 
            // suffixText
            // 
            this.suffixText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.suffixText.Location = new System.Drawing.Point(342, 28);
            this.suffixText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.suffixText.MaxLength = 3;
            this.suffixText.Name = "suffixText";
            this.suffixText.PlaceholderText = "IV";
            this.suffixText.Size = new System.Drawing.Size(51, 26);
            this.suffixText.TabIndex = 2;
            // 
            // lastNameText
            // 
            this.lastNameText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lastNameText.Location = new System.Drawing.Point(165, 28);
            this.lastNameText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.lastNameText.MaxLength = 25;
            this.lastNameText.Name = "lastNameText";
            this.lastNameText.PlaceholderText = "Smith";
            this.lastNameText.Size = new System.Drawing.Size(147, 26);
            this.lastNameText.TabIndex = 1;
            this.lastNameText.Validating += new System.ComponentModel.CancelEventHandler(this.LastNameText_Validating);
            this.lastNameText.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // phoneNumberText
            // 
            this.phoneNumberText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.phoneNumberText.Location = new System.Drawing.Point(300, 280);
            this.phoneNumberText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.phoneNumberText.Mask = "(999) 000-0000";
            this.phoneNumberText.Name = "phoneNumberText";
            this.phoneNumberText.Size = new System.Drawing.Size(100, 26);
            this.phoneNumberText.TabIndex = 11;
            this.phoneNumberText.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // usbcIdText
            // 
            this.usbcIdText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.usbcIdText.Location = new System.Drawing.Point(430, 280);
            this.usbcIdText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.usbcIdText.MaxLength = 20;
            this.usbcIdText.Name = "usbcIdText";
            this.usbcIdText.PlaceholderText = "1234-5678";
            this.usbcIdText.Size = new System.Drawing.Size(112, 26);
            this.usbcIdText.TabIndex = 12;
            // 
            // cityText
            // 
            this.cityText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cityText.Location = new System.Drawing.Point(3, 154);
            this.cityText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.cityText.MaxLength = 50;
            this.cityText.Name = "cityText";
            this.cityText.PlaceholderText = "Anytown";
            this.cityText.Size = new System.Drawing.Size(184, 26);
            this.cityText.TabIndex = 5;
            // 
            // streetText
            // 
            this.streetText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.streetText.Location = new System.Drawing.Point(3, 91);
            this.streetText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.streetText.MaxLength = 50;
            this.streetText.Name = "streetText";
            this.streetText.PlaceholderText = "123 Any St. Unit 456";
            this.streetText.Size = new System.Drawing.Size(539, 26);
            this.streetText.TabIndex = 4;
            // 
            // stateDropdown
            // 
            this.stateDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateDropdown.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.stateDropdown.FormattingEnabled = true;
            this.stateDropdown.Location = new System.Drawing.Point(217, 154);
            this.stateDropdown.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.stateDropdown.Name = "stateDropdown";
            this.stateDropdown.Size = new System.Drawing.Size(195, 26);
            this.stateDropdown.TabIndex = 6;
            // 
            // zipCodeText
            // 
            this.zipCodeText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.zipCodeText.Location = new System.Drawing.Point(442, 154);
            this.zipCodeText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.zipCodeText.Mask = "00000-9999";
            this.zipCodeText.Name = "zipCodeText";
            this.zipCodeText.Size = new System.Drawing.Size(100, 26);
            this.zipCodeText.TabIndex = 7;
            this.zipCodeText.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // emailText
            // 
            this.emailText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.emailText.Location = new System.Drawing.Point(3, 217);
            this.emailText.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.emailText.MaxLength = 50;
            this.emailText.Name = "emailText";
            this.emailText.PlaceholderText = "sample@NortheastMegabuck.com";
            this.emailText.Size = new System.Drawing.Size(539, 26);
            this.emailText.TabIndex = 8;
            this.emailText.Validating += new System.ComponentModel.CancelEventHandler(this.EmailText_Validating);
            this.emailText.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Bold = false;
            this.firstNameLabel.Location = new System.Drawing.Point(3, 3);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Required = true;
            this.firstNameLabel.Size = new System.Drawing.Size(114, 19);
            this.firstNameLabel.TabIndex = 19;
            this.firstNameLabel.TabStop = false;
            this.firstNameLabel.Text = "First Name:";
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Bold = false;
            this.lastNameLabel.Location = new System.Drawing.Point(165, 3);
            this.lastNameLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Required = true;
            this.lastNameLabel.Size = new System.Drawing.Size(105, 19);
            this.lastNameLabel.TabIndex = 20;
            this.lastNameLabel.TabStop = false;
            this.lastNameLabel.Text = "Last Name:";
            // 
            // suffixLabel
            // 
            this.suffixLabel.AutoSize = true;
            this.suffixLabel.Bold = false;
            this.suffixLabel.Location = new System.Drawing.Point(321, 3);
            this.suffixLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.suffixLabel.Name = "suffixLabel";
            this.suffixLabel.Required = false;
            this.suffixLabel.Size = new System.Drawing.Size(117, 19);
            this.suffixLabel.TabIndex = 21;
            this.suffixLabel.TabStop = false;
            this.suffixLabel.Text = "Suffix:";
            // 
            // middleInitialLabel
            // 
            this.middleInitialLabel.AutoSize = true;
            this.middleInitialLabel.Bold = false;
            this.middleInitialLabel.Location = new System.Drawing.Point(423, 3);
            this.middleInitialLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.middleInitialLabel.Name = "middleInitialLabel";
            this.middleInitialLabel.Required = false;
            this.middleInitialLabel.Size = new System.Drawing.Size(144, 19);
            this.middleInitialLabel.TabIndex = 22;
            this.middleInitialLabel.TabStop = false;
            this.middleInitialLabel.Text = "Middle Initial:";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Bold = false;
            this.streetLabel.Location = new System.Drawing.Point(3, 66);
            this.streetLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Required = false;
            this.streetLabel.Size = new System.Drawing.Size(117, 19);
            this.streetLabel.TabIndex = 23;
            this.streetLabel.TabStop = false;
            this.streetLabel.Text = "Street:";
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Bold = false;
            this.cityLabel.Location = new System.Drawing.Point(3, 129);
            this.cityLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Required = false;
            this.cityLabel.Size = new System.Drawing.Size(117, 19);
            this.cityLabel.TabIndex = 24;
            this.cityLabel.TabStop = false;
            this.cityLabel.Text = "City:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Bold = false;
            this.stateLabel.Location = new System.Drawing.Point(217, 129);
            this.stateLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Required = false;
            this.stateLabel.Size = new System.Drawing.Size(117, 19);
            this.stateLabel.TabIndex = 25;
            this.stateLabel.TabStop = false;
            this.stateLabel.Text = "State:";
            // 
            // zipCodeLabel
            // 
            this.zipCodeLabel.AutoSize = true;
            this.zipCodeLabel.Bold = false;
            this.zipCodeLabel.Location = new System.Drawing.Point(442, 129);
            this.zipCodeLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.zipCodeLabel.Name = "zipCodeLabel";
            this.zipCodeLabel.Required = false;
            this.zipCodeLabel.Size = new System.Drawing.Size(117, 19);
            this.zipCodeLabel.TabIndex = 26;
            this.zipCodeLabel.TabStop = false;
            this.zipCodeLabel.Text = "Zip Code:";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Bold = false;
            this.emailLabel.Location = new System.Drawing.Point(3, 192);
            this.emailLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Required = true;
            this.emailLabel.Size = new System.Drawing.Size(69, 19);
            this.emailLabel.TabIndex = 27;
            this.emailLabel.TabStop = false;
            this.emailLabel.Text = "Email:";
            // 
            // dateOfBirthLabel
            // 
            this.dateOfBirthLabel.AutoSize = true;
            this.dateOfBirthLabel.Bold = false;
            this.dateOfBirthLabel.Location = new System.Drawing.Point(3, 255);
            this.dateOfBirthLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.dateOfBirthLabel.Name = "dateOfBirthLabel";
            this.dateOfBirthLabel.Required = false;
            this.dateOfBirthLabel.Size = new System.Drawing.Size(135, 19);
            this.dateOfBirthLabel.TabIndex = 28;
            this.dateOfBirthLabel.TabStop = false;
            this.dateOfBirthLabel.Text = "Date Of Birth:";
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Bold = false;
            this.genderLabel.Location = new System.Drawing.Point(172, 255);
            this.genderLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Required = false;
            this.genderLabel.Size = new System.Drawing.Size(117, 19);
            this.genderLabel.TabIndex = 29;
            this.genderLabel.TabStop = false;
            this.genderLabel.Text = "Gender:";
            // 
            // phoneNumberLabel
            // 
            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Bold = false;
            this.phoneNumberLabel.Location = new System.Drawing.Point(274, 255);
            this.phoneNumberLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Required = false;
            this.phoneNumberLabel.Size = new System.Drawing.Size(126, 19);
            this.phoneNumberLabel.TabIndex = 30;
            this.phoneNumberLabel.TabStop = false;
            this.phoneNumberLabel.Text = "Phone Number:";
            // 
            // usbcIdLabel
            // 
            this.usbcIdLabel.AutoSize = true;
            this.usbcIdLabel.Bold = false;
            this.usbcIdLabel.Location = new System.Drawing.Point(430, 255);
            this.usbcIdLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.usbcIdLabel.Name = "usbcIdLabel";
            this.usbcIdLabel.Required = false;
            this.usbcIdLabel.Size = new System.Drawing.Size(81, 19);
            this.usbcIdLabel.TabIndex = 31;
            this.usbcIdLabel.TabStop = false;
            this.usbcIdLabel.Text = "USBC Id:";
            // 
            // bowlerErrorProvider
            // 
            this.bowlerErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.bowlerErrorProvider.ContainerControl = this;
            // 
            // BowlerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.usbcIdLabel);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.dateOfBirthLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.zipCodeLabel);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.streetLabel);
            this.Controls.Add(this.middleInitialLabel);
            this.Controls.Add(this.suffixLabel);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameLabel);
            this.Controls.Add(this.emailText);
            this.Controls.Add(this.cityText);
            this.Controls.Add(this.streetText);
            this.Controls.Add(this.stateDropdown);
            this.Controls.Add(this.zipCodeText);
            this.Controls.Add(this.genderDropdown);
            this.Controls.Add(this.dateOfBirthPicker);
            this.Controls.Add(this.middleInitialText);
            this.Controls.Add(this.suffixText);
            this.Controls.Add(this.lastNameText);
            this.Controls.Add(this.phoneNumberText);
            this.Controls.Add(this.usbcIdText);
            this.Controls.Add(this.firstNameText);
            this.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.Name = "BowlerControl";
            this.Size = new System.Drawing.Size(597, 326);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.BowlerControl_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.bowlerErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox firstNameText;
    private ComboBox genderDropdown;
    private DateTimePicker dateOfBirthPicker;
    private TextBox middleInitialText;
    private TextBox suffixText;
    private TextBox lastNameText;
    private MaskedTextBox phoneNumberText;
    private TextBox usbcIdText;
    private TextBox cityText;
    private TextBox streetText;
    protected ComboBox stateDropdown;
    private MaskedTextBox zipCodeText;
    private TextBox emailText;
    private Controls.LabelControl firstNameLabel;
    private Controls.LabelControl lastNameLabel;
    private Controls.LabelControl suffixLabel;
    private Controls.LabelControl middleInitialLabel;
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
}
