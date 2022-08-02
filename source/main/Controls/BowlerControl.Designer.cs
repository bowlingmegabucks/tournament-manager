namespace NewEnglandClassic.Contols;

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
            this.TextboxFirstName = new System.Windows.Forms.TextBox();
            this.ComboBoxGender = new System.Windows.Forms.ComboBox();
            this.DatePickerDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.TextboxMiddleInitial = new System.Windows.Forms.TextBox();
            this.TextboxSuffix = new System.Windows.Forms.TextBox();
            this.TextboxLastName = new System.Windows.Forms.TextBox();
            this.TextboxPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.TextboxUSBCId = new System.Windows.Forms.TextBox();
            this.TextboxCity = new System.Windows.Forms.TextBox();
            this.TextboxStreet = new System.Windows.Forms.TextBox();
            this.ComboBoxStateUS = new System.Windows.Forms.ComboBox();
            this.TextboxZipCodeUS = new System.Windows.Forms.MaskedTextBox();
            this.TextboxEmail = new System.Windows.Forms.TextBox();
            this.LabelFirstName = new NewEnglandClassic.Controls.LabelControl();
            this.LabelLastName = new NewEnglandClassic.Controls.LabelControl();
            this.LabelSuffix = new NewEnglandClassic.Controls.LabelControl();
            this.LabelMiddleInitial = new NewEnglandClassic.Controls.LabelControl();
            this.LabelStreet = new NewEnglandClassic.Controls.LabelControl();
            this.LabelCity = new NewEnglandClassic.Controls.LabelControl();
            this.LabelState = new NewEnglandClassic.Controls.LabelControl();
            this.LabelZip = new NewEnglandClassic.Controls.LabelControl();
            this.LabelEmail = new NewEnglandClassic.Controls.LabelControl();
            this.LabelDateOfBirth = new NewEnglandClassic.Controls.LabelControl();
            this.LabelGender = new NewEnglandClassic.Controls.LabelControl();
            this.LabelPhoneNumber = new NewEnglandClassic.Controls.LabelControl();
            this.LabelUSBCId = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderBowler = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderBowler)).BeginInit();
            this.SuspendLayout();
            // 
            // TextboxFirstName
            // 
            this.TextboxFirstName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxFirstName.Location = new System.Drawing.Point(3, 28);
            this.TextboxFirstName.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxFirstName.MaxLength = 20;
            this.TextboxFirstName.Name = "TextboxFirstName";
            this.TextboxFirstName.PlaceholderText = "Joseph";
            this.TextboxFirstName.Size = new System.Drawing.Size(132, 26);
            this.TextboxFirstName.TabIndex = 0;
            this.TextboxFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxFirstName_Validating);
            this.TextboxFirstName.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // ComboBoxGender
            // 
            this.ComboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxGender.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBoxGender.FormattingEnabled = true;
            this.ComboBoxGender.Location = new System.Drawing.Point(172, 280);
            this.ComboBoxGender.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.ComboBoxGender.Name = "ComboBoxGender";
            this.ComboBoxGender.Size = new System.Drawing.Size(98, 26);
            this.ComboBoxGender.TabIndex = 10;
            // 
            // DatePickerDateOfBirth
            // 
            this.DatePickerDateOfBirth.CustomFormat = "MM/dd/yyyy";
            this.DatePickerDateOfBirth.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DatePickerDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePickerDateOfBirth.Location = new System.Drawing.Point(3, 280);
            this.DatePickerDateOfBirth.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.DatePickerDateOfBirth.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DatePickerDateOfBirth.Name = "DatePickerDateOfBirth";
            this.DatePickerDateOfBirth.ShowCheckBox = true;
            this.DatePickerDateOfBirth.Size = new System.Drawing.Size(139, 26);
            this.DatePickerDateOfBirth.TabIndex = 9;
            // 
            // TextboxMiddleInitial
            // 
            this.TextboxMiddleInitial.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxMiddleInitial.Location = new System.Drawing.Point(506, 28);
            this.TextboxMiddleInitial.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxMiddleInitial.MaxLength = 3;
            this.TextboxMiddleInitial.Name = "TextboxMiddleInitial";
            this.TextboxMiddleInitial.PlaceholderText = "A";
            this.TextboxMiddleInitial.Size = new System.Drawing.Size(36, 26);
            this.TextboxMiddleInitial.TabIndex = 3;
            // 
            // TextboxSuffix
            // 
            this.TextboxSuffix.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxSuffix.Location = new System.Drawing.Point(342, 28);
            this.TextboxSuffix.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxSuffix.MaxLength = 3;
            this.TextboxSuffix.Name = "TextboxSuffix";
            this.TextboxSuffix.PlaceholderText = "IV";
            this.TextboxSuffix.Size = new System.Drawing.Size(51, 26);
            this.TextboxSuffix.TabIndex = 2;
            // 
            // TextboxLastName
            // 
            this.TextboxLastName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxLastName.Location = new System.Drawing.Point(165, 28);
            this.TextboxLastName.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxLastName.MaxLength = 25;
            this.TextboxLastName.Name = "TextboxLastName";
            this.TextboxLastName.PlaceholderText = "Smith";
            this.TextboxLastName.Size = new System.Drawing.Size(147, 26);
            this.TextboxLastName.TabIndex = 1;
            this.TextboxLastName.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxLastName_Validating);
            this.TextboxLastName.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // TextboxPhoneNumber
            // 
            this.TextboxPhoneNumber.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxPhoneNumber.Location = new System.Drawing.Point(300, 280);
            this.TextboxPhoneNumber.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxPhoneNumber.Mask = "(999) 000-0000";
            this.TextboxPhoneNumber.Name = "TextboxPhoneNumber";
            this.TextboxPhoneNumber.Size = new System.Drawing.Size(100, 26);
            this.TextboxPhoneNumber.TabIndex = 11;
            this.TextboxPhoneNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // TextboxUSBCId
            // 
            this.TextboxUSBCId.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxUSBCId.Location = new System.Drawing.Point(430, 280);
            this.TextboxUSBCId.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxUSBCId.MaxLength = 20;
            this.TextboxUSBCId.Name = "TextboxUSBCId";
            this.TextboxUSBCId.PlaceholderText = "1234-5678";
            this.TextboxUSBCId.Size = new System.Drawing.Size(112, 26);
            this.TextboxUSBCId.TabIndex = 12;
            this.TextboxUSBCId.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxUSBCId_Validating);
            this.TextboxUSBCId.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // TextboxCity
            // 
            this.TextboxCity.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxCity.Location = new System.Drawing.Point(3, 154);
            this.TextboxCity.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxCity.MaxLength = 50;
            this.TextboxCity.Name = "TextboxCity";
            this.TextboxCity.PlaceholderText = "Anytown";
            this.TextboxCity.Size = new System.Drawing.Size(184, 26);
            this.TextboxCity.TabIndex = 5;
            // 
            // TextboxStreet
            // 
            this.TextboxStreet.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxStreet.Location = new System.Drawing.Point(3, 91);
            this.TextboxStreet.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxStreet.MaxLength = 50;
            this.TextboxStreet.Name = "TextboxStreet";
            this.TextboxStreet.PlaceholderText = "123 Any St. Unit 456";
            this.TextboxStreet.Size = new System.Drawing.Size(539, 26);
            this.TextboxStreet.TabIndex = 4;
            // 
            // ComboBoxStateUS
            // 
            this.ComboBoxStateUS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStateUS.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBoxStateUS.FormattingEnabled = true;
            this.ComboBoxStateUS.Location = new System.Drawing.Point(217, 154);
            this.ComboBoxStateUS.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.ComboBoxStateUS.Name = "ComboBoxStateUS";
            this.ComboBoxStateUS.Size = new System.Drawing.Size(195, 26);
            this.ComboBoxStateUS.TabIndex = 6;
            // 
            // TextboxZipCodeUS
            // 
            this.TextboxZipCodeUS.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxZipCodeUS.Location = new System.Drawing.Point(442, 154);
            this.TextboxZipCodeUS.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxZipCodeUS.Mask = "00000-9999";
            this.TextboxZipCodeUS.Name = "TextboxZipCodeUS";
            this.TextboxZipCodeUS.Size = new System.Drawing.Size(100, 26);
            this.TextboxZipCodeUS.TabIndex = 7;
            this.TextboxZipCodeUS.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // TextboxEmail
            // 
            this.TextboxEmail.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxEmail.Location = new System.Drawing.Point(3, 217);
            this.TextboxEmail.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.TextboxEmail.MaxLength = 50;
            this.TextboxEmail.Name = "TextboxEmail";
            this.TextboxEmail.PlaceholderText = "sample@newenglandclassic.com";
            this.TextboxEmail.Size = new System.Drawing.Size(539, 26);
            this.TextboxEmail.TabIndex = 8;
            this.TextboxEmail.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxEmail_Validating);
            this.TextboxEmail.Validated += new System.EventHandler(this.Control_Validated);
            // 
            // LabelFirstName
            // 
            this.LabelFirstName.AutoSize = true;
            this.LabelFirstName.Bold = false;
            this.LabelFirstName.Location = new System.Drawing.Point(3, 3);
            this.LabelFirstName.Name = "LabelFirstName";
            this.LabelFirstName.Required = true;
            this.LabelFirstName.Size = new System.Drawing.Size(114, 19);
            this.LabelFirstName.TabIndex = 19;
            this.LabelFirstName.TabStop = false;
            this.LabelFirstName.Text = "First Name:";
            // 
            // LabelLastName
            // 
            this.LabelLastName.AutoSize = true;
            this.LabelLastName.Bold = false;
            this.LabelLastName.Location = new System.Drawing.Point(165, 3);
            this.LabelLastName.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelLastName.Name = "LabelLastName";
            this.LabelLastName.Required = true;
            this.LabelLastName.Size = new System.Drawing.Size(105, 19);
            this.LabelLastName.TabIndex = 20;
            this.LabelLastName.TabStop = false;
            this.LabelLastName.Text = "Last Name:";
            // 
            // LabelSuffix
            // 
            this.LabelSuffix.AutoSize = true;
            this.LabelSuffix.Bold = false;
            this.LabelSuffix.Location = new System.Drawing.Point(321, 3);
            this.LabelSuffix.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelSuffix.Name = "LabelSuffix";
            this.LabelSuffix.Required = false;
            this.LabelSuffix.Size = new System.Drawing.Size(72, 19);
            this.LabelSuffix.TabIndex = 21;
            this.LabelSuffix.TabStop = false;
            this.LabelSuffix.Text = "Suffix:";
            // 
            // LabelMiddleInitial
            // 
            this.LabelMiddleInitial.AutoSize = true;
            this.LabelMiddleInitial.Bold = false;
            this.LabelMiddleInitial.Location = new System.Drawing.Point(423, 3);
            this.LabelMiddleInitial.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelMiddleInitial.Name = "LabelMiddleInitial";
            this.LabelMiddleInitial.Required = false;
            this.LabelMiddleInitial.Size = new System.Drawing.Size(144, 19);
            this.LabelMiddleInitial.TabIndex = 22;
            this.LabelMiddleInitial.TabStop = false;
            this.LabelMiddleInitial.Text = "Middle Initial:";
            // 
            // LabelStreet
            // 
            this.LabelStreet.AutoSize = true;
            this.LabelStreet.Bold = false;
            this.LabelStreet.Location = new System.Drawing.Point(3, 66);
            this.LabelStreet.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelStreet.Name = "LabelStreet";
            this.LabelStreet.Required = false;
            this.LabelStreet.Size = new System.Drawing.Size(72, 19);
            this.LabelStreet.TabIndex = 23;
            this.LabelStreet.TabStop = false;
            this.LabelStreet.Text = "Street:";
            // 
            // LabelCity
            // 
            this.LabelCity.AutoSize = true;
            this.LabelCity.Bold = false;
            this.LabelCity.Location = new System.Drawing.Point(3, 129);
            this.LabelCity.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelCity.Name = "LabelCity";
            this.LabelCity.Required = false;
            this.LabelCity.Size = new System.Drawing.Size(72, 19);
            this.LabelCity.TabIndex = 24;
            this.LabelCity.TabStop = false;
            this.LabelCity.Text = "City:";
            // 
            // LabelState
            // 
            this.LabelState.AutoSize = true;
            this.LabelState.Bold = false;
            this.LabelState.Location = new System.Drawing.Point(217, 129);
            this.LabelState.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelState.Name = "LabelState";
            this.LabelState.Required = false;
            this.LabelState.Size = new System.Drawing.Size(72, 19);
            this.LabelState.TabIndex = 25;
            this.LabelState.TabStop = false;
            this.LabelState.Text = "State:";
            // 
            // LabelZip
            // 
            this.LabelZip.AutoSize = true;
            this.LabelZip.Bold = false;
            this.LabelZip.Location = new System.Drawing.Point(442, 129);
            this.LabelZip.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelZip.Name = "LabelZip";
            this.LabelZip.Required = false;
            this.LabelZip.Size = new System.Drawing.Size(90, 19);
            this.LabelZip.TabIndex = 26;
            this.LabelZip.TabStop = false;
            this.LabelZip.Text = "Zip Code:";
            // 
            // LabelEmail
            // 
            this.LabelEmail.AutoSize = true;
            this.LabelEmail.Bold = false;
            this.LabelEmail.Location = new System.Drawing.Point(3, 192);
            this.LabelEmail.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelEmail.Name = "LabelEmail";
            this.LabelEmail.Required = true;
            this.LabelEmail.Size = new System.Drawing.Size(69, 19);
            this.LabelEmail.TabIndex = 27;
            this.LabelEmail.TabStop = false;
            this.LabelEmail.Text = "Email:";
            // 
            // LabelDateOfBirth
            // 
            this.LabelDateOfBirth.AutoSize = true;
            this.LabelDateOfBirth.Bold = false;
            this.LabelDateOfBirth.Location = new System.Drawing.Point(3, 255);
            this.LabelDateOfBirth.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelDateOfBirth.Name = "LabelDateOfBirth";
            this.LabelDateOfBirth.Required = false;
            this.LabelDateOfBirth.Size = new System.Drawing.Size(135, 19);
            this.LabelDateOfBirth.TabIndex = 28;
            this.LabelDateOfBirth.TabStop = false;
            this.LabelDateOfBirth.Text = "Date Of Birth:";
            // 
            // LabelGender
            // 
            this.LabelGender.AutoSize = true;
            this.LabelGender.Bold = false;
            this.LabelGender.Location = new System.Drawing.Point(172, 255);
            this.LabelGender.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelGender.Name = "LabelGender";
            this.LabelGender.Required = false;
            this.LabelGender.Size = new System.Drawing.Size(72, 19);
            this.LabelGender.TabIndex = 29;
            this.LabelGender.TabStop = false;
            this.LabelGender.Text = "Gender:";
            // 
            // LabelPhoneNumber
            // 
            this.LabelPhoneNumber.AutoSize = true;
            this.LabelPhoneNumber.Bold = false;
            this.LabelPhoneNumber.Location = new System.Drawing.Point(274, 255);
            this.LabelPhoneNumber.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelPhoneNumber.Name = "LabelPhoneNumber";
            this.LabelPhoneNumber.Required = false;
            this.LabelPhoneNumber.Size = new System.Drawing.Size(126, 19);
            this.LabelPhoneNumber.TabIndex = 30;
            this.LabelPhoneNumber.TabStop = false;
            this.LabelPhoneNumber.Text = "Phone Number:";
            // 
            // LabelUSBCId
            // 
            this.LabelUSBCId.AutoSize = true;
            this.LabelUSBCId.Bold = false;
            this.LabelUSBCId.Location = new System.Drawing.Point(430, 255);
            this.LabelUSBCId.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelUSBCId.Name = "LabelUSBCId";
            this.LabelUSBCId.Required = true;
            this.LabelUSBCId.Size = new System.Drawing.Size(87, 19);
            this.LabelUSBCId.TabIndex = 31;
            this.LabelUSBCId.TabStop = false;
            this.LabelUSBCId.Text = "USBC Id:";
            // 
            // ErrorProviderBowler
            // 
            this.ErrorProviderBowler.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderBowler.ContainerControl = this;
            // 
            // BowlerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabelUSBCId);
            this.Controls.Add(this.LabelPhoneNumber);
            this.Controls.Add(this.LabelGender);
            this.Controls.Add(this.LabelDateOfBirth);
            this.Controls.Add(this.LabelEmail);
            this.Controls.Add(this.LabelZip);
            this.Controls.Add(this.LabelState);
            this.Controls.Add(this.LabelCity);
            this.Controls.Add(this.LabelStreet);
            this.Controls.Add(this.LabelMiddleInitial);
            this.Controls.Add(this.LabelSuffix);
            this.Controls.Add(this.LabelLastName);
            this.Controls.Add(this.LabelFirstName);
            this.Controls.Add(this.TextboxEmail);
            this.Controls.Add(this.TextboxCity);
            this.Controls.Add(this.TextboxStreet);
            this.Controls.Add(this.ComboBoxStateUS);
            this.Controls.Add(this.TextboxZipCodeUS);
            this.Controls.Add(this.ComboBoxGender);
            this.Controls.Add(this.DatePickerDateOfBirth);
            this.Controls.Add(this.TextboxMiddleInitial);
            this.Controls.Add(this.TextboxSuffix);
            this.Controls.Add(this.TextboxLastName);
            this.Controls.Add(this.TextboxPhoneNumber);
            this.Controls.Add(this.TextboxUSBCId);
            this.Controls.Add(this.TextboxFirstName);
            this.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.Name = "BowlerControl";
            this.Size = new System.Drawing.Size(597, 326);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.BowlerControl_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderBowler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox TextboxFirstName;
    private ComboBox ComboBoxGender;
    private DateTimePicker DatePickerDateOfBirth;
    private TextBox TextboxMiddleInitial;
    private TextBox TextboxSuffix;
    private TextBox TextboxLastName;
    private MaskedTextBox TextboxPhoneNumber;
    private TextBox TextboxUSBCId;
    private TextBox TextboxCity;
    private TextBox TextboxStreet;
    protected ComboBox ComboBoxStateUS;
    private MaskedTextBox TextboxZipCodeUS;
    private TextBox TextboxEmail;
    private Controls.LabelControl LabelFirstName;
    private Controls.LabelControl LabelLastName;
    private Controls.LabelControl LabelSuffix;
    private Controls.LabelControl LabelMiddleInitial;
    private Controls.LabelControl LabelStreet;
    private Controls.LabelControl LabelCity;
    private Controls.LabelControl LabelState;
    private Controls.LabelControl LabelZip;
    private Controls.LabelControl LabelEmail;
    private Controls.LabelControl LabelDateOfBirth;
    private Controls.LabelControl LabelGender;
    private Controls.LabelControl LabelPhoneNumber;
    private Controls.LabelControl LabelUSBCId;
    private ErrorProvider ErrorProviderBowler;
}
