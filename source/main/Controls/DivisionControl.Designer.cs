namespace NewEnglandClassic.Contols;

partial class DivisionControl
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
            this.nameText = new System.Windows.Forms.TextBox();
            this.nameLabel = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderDivision = new System.Windows.Forms.ErrorProvider(this.components);
            this.numberLabel = new NewEnglandClassic.Controls.LabelControl();
            this.numberText = new System.Windows.Forms.TextBox();
            this.minimumAgeValue = new NewEnglandClassic.Controls.NumericControl();
            this.minimumAgeLabel = new NewEnglandClassic.Controls.LabelControl();
            this.maximumAgeValue = new NewEnglandClassic.Controls.NumericControl();
            this.maximumAgeLabel = new NewEnglandClassic.Controls.LabelControl();
            this.ageGroupBox = new System.Windows.Forms.GroupBox();
            this.averageGroupBox = new System.Windows.Forms.GroupBox();
            this.minimumAverageLabel = new NewEnglandClassic.Controls.LabelControl();
            this.maximumAverageValue = new NewEnglandClassic.Controls.NumericControl();
            this.minimumAverageValue = new NewEnglandClassic.Controls.NumericControl();
            this.maximumAverageLabel = new NewEnglandClassic.Controls.LabelControl();
            this.handicapGroupBox = new System.Windows.Forms.GroupBox();
            this.maximumHandicapPerGameLabel = new NewEnglandClassic.Controls.LabelControl();
            this.maximumHandicapPerGameValue = new NewEnglandClassic.Controls.NumericControl();
            this.handicapBaseLabel = new NewEnglandClassic.Controls.LabelControl();
            this.handicapBaseValue = new NewEnglandClassic.Controls.NumericControl();
            this.handicapPercentageLabel = new NewEnglandClassic.Controls.LabelControl();
            this.handicapPercentageValue = new NewEnglandClassic.Controls.NumericControl();
            this.genderLabel = new NewEnglandClassic.Controls.LabelControl();
            this.genderDropdown = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumAgeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximumAgeValue)).BeginInit();
            this.ageGroupBox.SuspendLayout();
            this.averageGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumAverageValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumAverageValue)).BeginInit();
            this.handicapGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumHandicapPerGameValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.handicapBaseValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.handicapPercentageValue)).BeginInit();
            this.SuspendLayout();
            // 
            // nameText
            // 
            this.nameText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameText.Location = new System.Drawing.Point(132, 28);
            this.nameText.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.nameText.Name = "nameText";
            this.nameText.PlaceholderText = "Division Description";
            this.nameText.Size = new System.Drawing.Size(299, 27);
            this.nameText.TabIndex = 0;
            this.nameText.Validating += new System.ComponentModel.CancelEventHandler(this.NameText_Validating);
            this.nameText.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Bold = false;
            this.nameLabel.Location = new System.Drawing.Point(132, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Required = true;
            this.nameLabel.Size = new System.Drawing.Size(60, 19);
            this.nameLabel.TabIndex = 103;
            this.nameLabel.TabStop = false;
            this.nameLabel.Text = "Name:";
            // 
            // ErrorProviderDivision
            // 
            this.ErrorProviderDivision.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderDivision.ContainerControl = this;
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Bold = false;
            this.numberLabel.Location = new System.Drawing.Point(9, 3);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Required = false;
            this.numberLabel.Size = new System.Drawing.Size(117, 19);
            this.numberLabel.TabIndex = 104;
            this.numberLabel.TabStop = false;
            this.numberLabel.Text = "Number:";
            // 
            // numberText
            // 
            this.numberText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numberText.Location = new System.Drawing.Point(9, 28);
            this.numberText.Margin = new System.Windows.Forms.Padding(3, 3, 30, 10);
            this.numberText.Name = "numberText";
            this.numberText.ReadOnly = true;
            this.numberText.Size = new System.Drawing.Size(72, 27);
            this.numberText.TabIndex = 900;
            this.numberText.TabStop = false;
            this.numberText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // minimumAgeValue
            // 
            this.minimumAgeValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minimumAgeValue.Location = new System.Drawing.Point(6, 48);
            this.minimumAgeValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.minimumAgeValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.minimumAgeValue.Name = "minimumAgeValue";
            this.minimumAgeValue.Size = new System.Drawing.Size(131, 27);
            this.minimumAgeValue.TabIndex = 0;
            this.minimumAgeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minimumAgeValue.Validating += new System.ComponentModel.CancelEventHandler(this.MinimumAgeValue_Validating);
            this.minimumAgeValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // minimumAgeLabel
            // 
            this.minimumAgeLabel.AutoSize = true;
            this.minimumAgeLabel.Bold = false;
            this.minimumAgeLabel.Location = new System.Drawing.Point(6, 22);
            this.minimumAgeLabel.Name = "minimumAgeLabel";
            this.minimumAgeLabel.Required = true;
            this.minimumAgeLabel.Size = new System.Drawing.Size(123, 19);
            this.minimumAgeLabel.TabIndex = 107;
            this.minimumAgeLabel.TabStop = false;
            this.minimumAgeLabel.Text = "Minimum Age:";
            // 
            // maximumAgeValue
            // 
            this.maximumAgeValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maximumAgeValue.Location = new System.Drawing.Point(170, 48);
            this.maximumAgeValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.maximumAgeValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.maximumAgeValue.Name = "maximumAgeValue";
            this.maximumAgeValue.Size = new System.Drawing.Size(131, 27);
            this.maximumAgeValue.TabIndex = 1;
            this.maximumAgeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maximumAgeValue.Validating += new System.ComponentModel.CancelEventHandler(this.MaximumAgeValue_Validating);
            this.maximumAgeValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // maximumAgeLabel
            // 
            this.maximumAgeLabel.AutoSize = true;
            this.maximumAgeLabel.Bold = false;
            this.maximumAgeLabel.Location = new System.Drawing.Point(170, 22);
            this.maximumAgeLabel.Name = "maximumAgeLabel";
            this.maximumAgeLabel.Required = true;
            this.maximumAgeLabel.Size = new System.Drawing.Size(123, 19);
            this.maximumAgeLabel.TabIndex = 109;
            this.maximumAgeLabel.TabStop = false;
            this.maximumAgeLabel.Text = "Maximum Age:";
            // 
            // ageGroupBox
            // 
            this.ageGroupBox.Controls.Add(this.minimumAgeLabel);
            this.ageGroupBox.Controls.Add(this.maximumAgeValue);
            this.ageGroupBox.Controls.Add(this.minimumAgeValue);
            this.ageGroupBox.Controls.Add(this.maximumAgeLabel);
            this.ageGroupBox.Location = new System.Drawing.Point(3, 68);
            this.ageGroupBox.Name = "ageGroupBox";
            this.ageGroupBox.Size = new System.Drawing.Size(428, 89);
            this.ageGroupBox.TabIndex = 1;
            this.ageGroupBox.TabStop = false;
            this.ageGroupBox.Text = "Age Requirements (Zero means none)";
            // 
            // averageGroupBox
            // 
            this.averageGroupBox.Controls.Add(this.minimumAverageLabel);
            this.averageGroupBox.Controls.Add(this.maximumAverageValue);
            this.averageGroupBox.Controls.Add(this.minimumAverageValue);
            this.averageGroupBox.Controls.Add(this.maximumAverageLabel);
            this.averageGroupBox.Location = new System.Drawing.Point(3, 163);
            this.averageGroupBox.Name = "averageGroupBox";
            this.averageGroupBox.Size = new System.Drawing.Size(428, 89);
            this.averageGroupBox.TabIndex = 2;
            this.averageGroupBox.TabStop = false;
            this.averageGroupBox.Text = "Average Requirements (Zero means none)";
            // 
            // minimumAverageLabel
            // 
            this.minimumAverageLabel.AutoSize = true;
            this.minimumAverageLabel.Bold = false;
            this.minimumAverageLabel.Location = new System.Drawing.Point(6, 22);
            this.minimumAverageLabel.Name = "minimumAverageLabel";
            this.minimumAverageLabel.Required = true;
            this.minimumAverageLabel.Size = new System.Drawing.Size(159, 19);
            this.minimumAverageLabel.TabIndex = 107;
            this.minimumAverageLabel.TabStop = false;
            this.minimumAverageLabel.Text = "Minimum Average:";
            // 
            // maximumAverageValue
            // 
            this.maximumAverageValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maximumAverageValue.Location = new System.Drawing.Point(171, 48);
            this.maximumAverageValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.maximumAverageValue.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.maximumAverageValue.Name = "maximumAverageValue";
            this.maximumAverageValue.Size = new System.Drawing.Size(131, 27);
            this.maximumAverageValue.TabIndex = 1;
            this.maximumAverageValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maximumAverageValue.Validating += new System.ComponentModel.CancelEventHandler(this.MaximumAverage_Validating);
            this.maximumAverageValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // minimumAverageValue
            // 
            this.minimumAverageValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minimumAverageValue.Location = new System.Drawing.Point(6, 48);
            this.minimumAverageValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.minimumAverageValue.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.minimumAverageValue.Name = "minimumAverageValue";
            this.minimumAverageValue.Size = new System.Drawing.Size(131, 27);
            this.minimumAverageValue.TabIndex = 0;
            this.minimumAverageValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minimumAverageValue.Validating += new System.ComponentModel.CancelEventHandler(this.MinimumAverage_Validating);
            this.minimumAverageValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // maximumAverageLabel
            // 
            this.maximumAverageLabel.AutoSize = true;
            this.maximumAverageLabel.Bold = false;
            this.maximumAverageLabel.Location = new System.Drawing.Point(171, 22);
            this.maximumAverageLabel.Name = "maximumAverageLabel";
            this.maximumAverageLabel.Required = true;
            this.maximumAverageLabel.Size = new System.Drawing.Size(159, 19);
            this.maximumAverageLabel.TabIndex = 109;
            this.maximumAverageLabel.TabStop = false;
            this.maximumAverageLabel.Text = "Maximum Average:";
            // 
            // handicapGroupBox
            // 
            this.handicapGroupBox.Controls.Add(this.maximumHandicapPerGameLabel);
            this.handicapGroupBox.Controls.Add(this.maximumHandicapPerGameValue);
            this.handicapGroupBox.Controls.Add(this.handicapBaseLabel);
            this.handicapGroupBox.Controls.Add(this.handicapBaseValue);
            this.handicapGroupBox.Controls.Add(this.handicapPercentageLabel);
            this.handicapGroupBox.Controls.Add(this.handicapPercentageValue);
            this.handicapGroupBox.Location = new System.Drawing.Point(3, 317);
            this.handicapGroupBox.Name = "handicapGroupBox";
            this.handicapGroupBox.Size = new System.Drawing.Size(428, 155);
            this.handicapGroupBox.TabIndex = 4;
            this.handicapGroupBox.TabStop = false;
            this.handicapGroupBox.Text = "Handicap (If Scratch all fields should be zero)";
            // 
            // maximumHandicapPerGameLabel
            // 
            this.maximumHandicapPerGameLabel.AutoSize = true;
            this.maximumHandicapPerGameLabel.Bold = false;
            this.maximumHandicapPerGameLabel.Location = new System.Drawing.Point(6, 88);
            this.maximumHandicapPerGameLabel.Name = "maximumHandicapPerGameLabel";
            this.maximumHandicapPerGameLabel.Required = true;
            this.maximumHandicapPerGameLabel.Size = new System.Drawing.Size(249, 19);
            this.maximumHandicapPerGameLabel.TabIndex = 113;
            this.maximumHandicapPerGameLabel.TabStop = false;
            this.maximumHandicapPerGameLabel.Text = "Maximum Handicap Per Game:";
            // 
            // maximumHandicapPerGameValue
            // 
            this.maximumHandicapPerGameValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maximumHandicapPerGameValue.Location = new System.Drawing.Point(6, 114);
            this.maximumHandicapPerGameValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.maximumHandicapPerGameValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.maximumHandicapPerGameValue.Name = "maximumHandicapPerGameValue";
            this.maximumHandicapPerGameValue.Size = new System.Drawing.Size(147, 27);
            this.maximumHandicapPerGameValue.TabIndex = 2;
            this.maximumHandicapPerGameValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maximumHandicapPerGameValue.Validating += new System.ComponentModel.CancelEventHandler(this.MaximumHandicapPerGameValue_Validating);
            this.maximumHandicapPerGameValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // handicapBaseLabel
            // 
            this.handicapBaseLabel.AutoSize = true;
            this.handicapBaseLabel.Bold = false;
            this.handicapBaseLabel.Location = new System.Drawing.Point(170, 22);
            this.handicapBaseLabel.Name = "handicapBaseLabel";
            this.handicapBaseLabel.Required = true;
            this.handicapBaseLabel.Size = new System.Drawing.Size(141, 19);
            this.handicapBaseLabel.TabIndex = 111;
            this.handicapBaseLabel.TabStop = false;
            this.handicapBaseLabel.Text = "Handicap Base:";
            // 
            // handicapBaseValue
            // 
            this.handicapBaseValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.handicapBaseValue.Location = new System.Drawing.Point(170, 48);
            this.handicapBaseValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.handicapBaseValue.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.handicapBaseValue.Name = "handicapBaseValue";
            this.handicapBaseValue.Size = new System.Drawing.Size(147, 27);
            this.handicapBaseValue.TabIndex = 1;
            this.handicapBaseValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.handicapBaseValue.Validating += new System.ComponentModel.CancelEventHandler(this.HandicapBaseValue_Validating);
            this.handicapBaseValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // handicapPercentageLabel
            // 
            this.handicapPercentageLabel.AutoSize = true;
            this.handicapPercentageLabel.Bold = false;
            this.handicapPercentageLabel.Location = new System.Drawing.Point(6, 22);
            this.handicapPercentageLabel.Name = "handicapPercentageLabel";
            this.handicapPercentageLabel.Required = true;
            this.handicapPercentageLabel.Size = new System.Drawing.Size(114, 19);
            this.handicapPercentageLabel.TabIndex = 109;
            this.handicapPercentageLabel.TabStop = false;
            this.handicapPercentageLabel.Text = "Handicap %:";
            // 
            // handicapPercentageValue
            // 
            this.handicapPercentageValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.handicapPercentageValue.Location = new System.Drawing.Point(6, 48);
            this.handicapPercentageValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.handicapPercentageValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.handicapPercentageValue.Name = "handicapPercentageValue";
            this.handicapPercentageValue.Size = new System.Drawing.Size(131, 27);
            this.handicapPercentageValue.TabIndex = 0;
            this.handicapPercentageValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.handicapPercentageValue.Validating += new System.ComponentModel.CancelEventHandler(this.HandicapPercentageValue_Validating);
            this.handicapPercentageValue.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Bold = false;
            this.genderLabel.Location = new System.Drawing.Point(9, 258);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Required = false;
            this.genderLabel.Size = new System.Drawing.Size(117, 19);
            this.genderLabel.TabIndex = 113;
            this.genderLabel.TabStop = false;
            this.genderLabel.Text = "Gender:";
            // 
            // genderDropdown
            // 
            this.genderDropdown.DisplayMember = "Value";
            this.genderDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderDropdown.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.genderDropdown.FormattingEnabled = true;
            this.genderDropdown.Location = new System.Drawing.Point(9, 283);
            this.genderDropdown.Name = "genderDropdown";
            this.genderDropdown.Size = new System.Drawing.Size(121, 28);
            this.genderDropdown.TabIndex = 3;
            this.genderDropdown.ValueMember = "Key";
            this.genderDropdown.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // DivisionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.genderDropdown);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.handicapGroupBox);
            this.Controls.Add(this.averageGroupBox);
            this.Controls.Add(this.ageGroupBox);
            this.Controls.Add(this.numberText);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.nameLabel);
            this.Name = "DivisionControl";
            this.Size = new System.Drawing.Size(463, 484);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumAgeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximumAgeValue)).EndInit();
            this.ageGroupBox.ResumeLayout(false);
            this.ageGroupBox.PerformLayout();
            this.averageGroupBox.ResumeLayout(false);
            this.averageGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumAverageValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumAverageValue)).EndInit();
            this.handicapGroupBox.ResumeLayout(false);
            this.handicapGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumHandicapPerGameValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.handicapBaseValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.handicapPercentageValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox nameText;
    private Controls.LabelControl nameLabel;
    private ErrorProvider ErrorProviderDivision;
    private TextBox numberText;
    private Controls.LabelControl numberLabel;
    private Controls.NumericControl minimumAgeValue;
    private Controls.LabelControl minimumAgeLabel;
    private GroupBox ageGroupBox;
    private Controls.NumericControl maximumAgeValue;
    private Controls.LabelControl maximumAgeLabel;
    private GroupBox averageGroupBox;
    private Controls.LabelControl minimumAverageLabel;
    private Controls.NumericControl maximumAverageValue;
    private Controls.NumericControl minimumAverageValue;
    private Controls.LabelControl maximumAverageLabel;
    private GroupBox handicapGroupBox;
    private Controls.LabelControl handicapPercentageLabel;
    private Controls.NumericControl handicapPercentageValue;
    private Controls.LabelControl handicapBaseLabel;
    private Controls.NumericControl handicapBaseValue;
    private Controls.LabelControl maximumHandicapPerGameLabel;
    private Controls.NumericControl maximumHandicapPerGameValue;
    private ComboBox genderDropdown;
    private Controls.LabelControl genderLabel;
}
