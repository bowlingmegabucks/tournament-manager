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
            this.TextboxDivisionName = new System.Windows.Forms.TextBox();
            this.LabelName = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderDivision = new System.Windows.Forms.ErrorProvider(this.components);
            this.LabelNumber = new NewEnglandClassic.Controls.LabelControl();
            this.TextboxNumber = new System.Windows.Forms.TextBox();
            this.NumericMinimumAge = new NewEnglandClassic.Controls.NumericControl();
            this.LabelMinimumAge = new NewEnglandClassic.Controls.LabelControl();
            this.NumericMaximumAge = new NewEnglandClassic.Controls.NumericControl();
            this.LabelMaximumAge = new NewEnglandClassic.Controls.LabelControl();
            this.GroupboxAge = new System.Windows.Forms.GroupBox();
            this.GroupboxAverage = new System.Windows.Forms.GroupBox();
            this.LabelMinimumAverage = new NewEnglandClassic.Controls.LabelControl();
            this.NumericMaximumAverage = new NewEnglandClassic.Controls.NumericControl();
            this.NumericMinimumAverage = new NewEnglandClassic.Controls.NumericControl();
            this.LabelMaximumAverage = new NewEnglandClassic.Controls.LabelControl();
            this.GroupboxHandicap = new System.Windows.Forms.GroupBox();
            this.LabelMaximumHandicapPerGame = new NewEnglandClassic.Controls.LabelControl();
            this.NumericMaximumHandicapPerGame = new NewEnglandClassic.Controls.NumericControl();
            this.LabelHandicapBase = new NewEnglandClassic.Controls.LabelControl();
            this.NumericHandicapBase = new NewEnglandClassic.Controls.NumericControl();
            this.LabelHandicapPercentage = new NewEnglandClassic.Controls.LabelControl();
            this.NumericHandicapPercentage = new NewEnglandClassic.Controls.NumericControl();
            this.LabelGender = new NewEnglandClassic.Controls.LabelControl();
            this.ComboboxGender = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMinimumAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaximumAge)).BeginInit();
            this.GroupboxAge.SuspendLayout();
            this.GroupboxAverage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaximumAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMinimumAverage)).BeginInit();
            this.GroupboxHandicap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaximumHandicapPerGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHandicapBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHandicapPercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // TextboxDivisionName
            // 
            this.TextboxDivisionName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxDivisionName.Location = new System.Drawing.Point(132, 28);
            this.TextboxDivisionName.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.TextboxDivisionName.Name = "TextboxDivisionName";
            this.TextboxDivisionName.PlaceholderText = "Division Description";
            this.TextboxDivisionName.Size = new System.Drawing.Size(299, 27);
            this.TextboxDivisionName.TabIndex = 0;
            this.TextboxDivisionName.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxDivisionName_Validating);
            this.TextboxDivisionName.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Bold = false;
            this.LabelName.Location = new System.Drawing.Point(132, 3);
            this.LabelName.Name = "LabelName";
            this.LabelName.Required = true;
            this.LabelName.Size = new System.Drawing.Size(60, 19);
            this.LabelName.TabIndex = 103;
            this.LabelName.TabStop = false;
            this.LabelName.Text = "Name:";
            // 
            // ErrorProviderDivision
            // 
            this.ErrorProviderDivision.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderDivision.ContainerControl = this;
            // 
            // LabelNumber
            // 
            this.LabelNumber.AutoSize = true;
            this.LabelNumber.Bold = false;
            this.LabelNumber.Location = new System.Drawing.Point(9, 3);
            this.LabelNumber.Name = "LabelNumber";
            this.LabelNumber.Required = false;
            this.LabelNumber.Size = new System.Drawing.Size(117, 19);
            this.LabelNumber.TabIndex = 104;
            this.LabelNumber.TabStop = false;
            this.LabelNumber.Text = "Number:";
            // 
            // TextboxNumber
            // 
            this.TextboxNumber.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxNumber.Location = new System.Drawing.Point(9, 28);
            this.TextboxNumber.Margin = new System.Windows.Forms.Padding(3, 3, 30, 10);
            this.TextboxNumber.Name = "TextboxNumber";
            this.TextboxNumber.ReadOnly = true;
            this.TextboxNumber.Size = new System.Drawing.Size(72, 27);
            this.TextboxNumber.TabIndex = 900;
            this.TextboxNumber.TabStop = false;
            this.TextboxNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NumericMinimumAge
            // 
            this.NumericMinimumAge.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMinimumAge.Location = new System.Drawing.Point(6, 48);
            this.NumericMinimumAge.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericMinimumAge.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NumericMinimumAge.Name = "NumericMinimumAge";
            this.NumericMinimumAge.Size = new System.Drawing.Size(131, 27);
            this.NumericMinimumAge.TabIndex = 0;
            this.NumericMinimumAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMinimumAge.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMinimumAge_Validating);
            this.NumericMinimumAge.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelMinimumAge
            // 
            this.LabelMinimumAge.AutoSize = true;
            this.LabelMinimumAge.Bold = false;
            this.LabelMinimumAge.Location = new System.Drawing.Point(6, 22);
            this.LabelMinimumAge.Name = "LabelMinimumAge";
            this.LabelMinimumAge.Required = true;
            this.LabelMinimumAge.Size = new System.Drawing.Size(123, 19);
            this.LabelMinimumAge.TabIndex = 107;
            this.LabelMinimumAge.TabStop = false;
            this.LabelMinimumAge.Text = "Minimum Age:";
            // 
            // NumericMaximumAge
            // 
            this.NumericMaximumAge.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMaximumAge.Location = new System.Drawing.Point(170, 48);
            this.NumericMaximumAge.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericMaximumAge.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NumericMaximumAge.Name = "NumericMaximumAge";
            this.NumericMaximumAge.Size = new System.Drawing.Size(131, 27);
            this.NumericMaximumAge.TabIndex = 1;
            this.NumericMaximumAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMaximumAge.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMaximumAge_Validating);
            this.NumericMaximumAge.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelMaximumAge
            // 
            this.LabelMaximumAge.AutoSize = true;
            this.LabelMaximumAge.Bold = false;
            this.LabelMaximumAge.Location = new System.Drawing.Point(170, 22);
            this.LabelMaximumAge.Name = "LabelMaximumAge";
            this.LabelMaximumAge.Required = true;
            this.LabelMaximumAge.Size = new System.Drawing.Size(123, 19);
            this.LabelMaximumAge.TabIndex = 109;
            this.LabelMaximumAge.TabStop = false;
            this.LabelMaximumAge.Text = "Maximum Age:";
            // 
            // GroupboxAge
            // 
            this.GroupboxAge.Controls.Add(this.LabelMinimumAge);
            this.GroupboxAge.Controls.Add(this.NumericMaximumAge);
            this.GroupboxAge.Controls.Add(this.NumericMinimumAge);
            this.GroupboxAge.Controls.Add(this.LabelMaximumAge);
            this.GroupboxAge.Location = new System.Drawing.Point(3, 68);
            this.GroupboxAge.Name = "GroupboxAge";
            this.GroupboxAge.Size = new System.Drawing.Size(428, 89);
            this.GroupboxAge.TabIndex = 1;
            this.GroupboxAge.TabStop = false;
            this.GroupboxAge.Text = "Age Requirements (Zero means none)";
            // 
            // GroupboxAverage
            // 
            this.GroupboxAverage.Controls.Add(this.LabelMinimumAverage);
            this.GroupboxAverage.Controls.Add(this.NumericMaximumAverage);
            this.GroupboxAverage.Controls.Add(this.NumericMinimumAverage);
            this.GroupboxAverage.Controls.Add(this.LabelMaximumAverage);
            this.GroupboxAverage.Location = new System.Drawing.Point(3, 163);
            this.GroupboxAverage.Name = "GroupboxAverage";
            this.GroupboxAverage.Size = new System.Drawing.Size(428, 89);
            this.GroupboxAverage.TabIndex = 2;
            this.GroupboxAverage.TabStop = false;
            this.GroupboxAverage.Text = "Average Requirements (Zero means none)";
            // 
            // LabelMinimumAverage
            // 
            this.LabelMinimumAverage.AutoSize = true;
            this.LabelMinimumAverage.Bold = false;
            this.LabelMinimumAverage.Location = new System.Drawing.Point(6, 22);
            this.LabelMinimumAverage.Name = "LabelMinimumAverage";
            this.LabelMinimumAverage.Required = true;
            this.LabelMinimumAverage.Size = new System.Drawing.Size(159, 19);
            this.LabelMinimumAverage.TabIndex = 107;
            this.LabelMinimumAverage.TabStop = false;
            this.LabelMinimumAverage.Text = "Minimum Average:";
            // 
            // NumericMaximumAverage
            // 
            this.NumericMaximumAverage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMaximumAverage.Location = new System.Drawing.Point(171, 48);
            this.NumericMaximumAverage.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericMaximumAverage.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.NumericMaximumAverage.Name = "NumericMaximumAverage";
            this.NumericMaximumAverage.Size = new System.Drawing.Size(131, 27);
            this.NumericMaximumAverage.TabIndex = 1;
            this.NumericMaximumAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMaximumAverage.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMaximumAverage_Validating);
            this.NumericMaximumAverage.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // NumericMinimumAverage
            // 
            this.NumericMinimumAverage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMinimumAverage.Location = new System.Drawing.Point(6, 48);
            this.NumericMinimumAverage.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericMinimumAverage.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.NumericMinimumAverage.Name = "NumericMinimumAverage";
            this.NumericMinimumAverage.Size = new System.Drawing.Size(131, 27);
            this.NumericMinimumAverage.TabIndex = 0;
            this.NumericMinimumAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMinimumAverage.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMinimumAverage_Validating);
            this.NumericMinimumAverage.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelMaximumAverage
            // 
            this.LabelMaximumAverage.AutoSize = true;
            this.LabelMaximumAverage.Bold = false;
            this.LabelMaximumAverage.Location = new System.Drawing.Point(171, 22);
            this.LabelMaximumAverage.Name = "LabelMaximumAverage";
            this.LabelMaximumAverage.Required = true;
            this.LabelMaximumAverage.Size = new System.Drawing.Size(159, 19);
            this.LabelMaximumAverage.TabIndex = 109;
            this.LabelMaximumAverage.TabStop = false;
            this.LabelMaximumAverage.Text = "Maximum Average:";
            // 
            // GroupboxHandicap
            // 
            this.GroupboxHandicap.Controls.Add(this.LabelMaximumHandicapPerGame);
            this.GroupboxHandicap.Controls.Add(this.NumericMaximumHandicapPerGame);
            this.GroupboxHandicap.Controls.Add(this.LabelHandicapBase);
            this.GroupboxHandicap.Controls.Add(this.NumericHandicapBase);
            this.GroupboxHandicap.Controls.Add(this.LabelHandicapPercentage);
            this.GroupboxHandicap.Controls.Add(this.NumericHandicapPercentage);
            this.GroupboxHandicap.Location = new System.Drawing.Point(3, 317);
            this.GroupboxHandicap.Name = "GroupboxHandicap";
            this.GroupboxHandicap.Size = new System.Drawing.Size(428, 155);
            this.GroupboxHandicap.TabIndex = 4;
            this.GroupboxHandicap.TabStop = false;
            this.GroupboxHandicap.Text = "Handicap (If Scratch all fields should be zero)";
            // 
            // LabelMaximumHandicapPerGame
            // 
            this.LabelMaximumHandicapPerGame.AutoSize = true;
            this.LabelMaximumHandicapPerGame.Bold = false;
            this.LabelMaximumHandicapPerGame.Location = new System.Drawing.Point(6, 88);
            this.LabelMaximumHandicapPerGame.Name = "LabelMaximumHandicapPerGame";
            this.LabelMaximumHandicapPerGame.Required = true;
            this.LabelMaximumHandicapPerGame.Size = new System.Drawing.Size(249, 19);
            this.LabelMaximumHandicapPerGame.TabIndex = 113;
            this.LabelMaximumHandicapPerGame.TabStop = false;
            this.LabelMaximumHandicapPerGame.Text = "Maximum Handicap Per Game:";
            // 
            // NumericMaximumHandicapPerGame
            // 
            this.NumericMaximumHandicapPerGame.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMaximumHandicapPerGame.Location = new System.Drawing.Point(6, 114);
            this.NumericMaximumHandicapPerGame.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericMaximumHandicapPerGame.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NumericMaximumHandicapPerGame.Name = "NumericMaximumHandicapPerGame";
            this.NumericMaximumHandicapPerGame.Size = new System.Drawing.Size(147, 27);
            this.NumericMaximumHandicapPerGame.TabIndex = 2;
            this.NumericMaximumHandicapPerGame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMaximumHandicapPerGame.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMaximumHandicapPerGame_Validating);
            this.NumericMaximumHandicapPerGame.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelHandicapBase
            // 
            this.LabelHandicapBase.AutoSize = true;
            this.LabelHandicapBase.Bold = false;
            this.LabelHandicapBase.Location = new System.Drawing.Point(170, 22);
            this.LabelHandicapBase.Name = "LabelHandicapBase";
            this.LabelHandicapBase.Required = true;
            this.LabelHandicapBase.Size = new System.Drawing.Size(141, 19);
            this.LabelHandicapBase.TabIndex = 111;
            this.LabelHandicapBase.TabStop = false;
            this.LabelHandicapBase.Text = "Handicap Base:";
            // 
            // NumericHandicapBase
            // 
            this.NumericHandicapBase.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericHandicapBase.Location = new System.Drawing.Point(170, 48);
            this.NumericHandicapBase.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericHandicapBase.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.NumericHandicapBase.Name = "NumericHandicapBase";
            this.NumericHandicapBase.Size = new System.Drawing.Size(147, 27);
            this.NumericHandicapBase.TabIndex = 1;
            this.NumericHandicapBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericHandicapBase.Validating += new System.ComponentModel.CancelEventHandler(this.NumericHandicapBase_Validating);
            this.NumericHandicapBase.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelHandicapPercentage
            // 
            this.LabelHandicapPercentage.AutoSize = true;
            this.LabelHandicapPercentage.Bold = false;
            this.LabelHandicapPercentage.Location = new System.Drawing.Point(6, 22);
            this.LabelHandicapPercentage.Name = "LabelHandicapPercentage";
            this.LabelHandicapPercentage.Required = true;
            this.LabelHandicapPercentage.Size = new System.Drawing.Size(114, 19);
            this.LabelHandicapPercentage.TabIndex = 109;
            this.LabelHandicapPercentage.TabStop = false;
            this.LabelHandicapPercentage.Text = "Handicap %:";
            // 
            // NumericHandicapPercentage
            // 
            this.NumericHandicapPercentage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericHandicapPercentage.Location = new System.Drawing.Point(6, 48);
            this.NumericHandicapPercentage.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericHandicapPercentage.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NumericHandicapPercentage.Name = "NumericHandicapPercentage";
            this.NumericHandicapPercentage.Size = new System.Drawing.Size(131, 27);
            this.NumericHandicapPercentage.TabIndex = 0;
            this.NumericHandicapPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericHandicapPercentage.Validating += new System.ComponentModel.CancelEventHandler(this.NumericHandicapPercentage_Validating);
            this.NumericHandicapPercentage.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // LabelGender
            // 
            this.LabelGender.AutoSize = true;
            this.LabelGender.Bold = false;
            this.LabelGender.Location = new System.Drawing.Point(9, 258);
            this.LabelGender.Name = "LabelGender";
            this.LabelGender.Required = false;
            this.LabelGender.Size = new System.Drawing.Size(117, 19);
            this.LabelGender.TabIndex = 113;
            this.LabelGender.TabStop = false;
            this.LabelGender.Text = "Gender:";
            // 
            // ComboboxGender
            // 
            this.ComboboxGender.DisplayMember = "Value";
            this.ComboboxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboboxGender.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboboxGender.FormattingEnabled = true;
            this.ComboboxGender.Location = new System.Drawing.Point(9, 283);
            this.ComboboxGender.Name = "ComboboxGender";
            this.ComboboxGender.Size = new System.Drawing.Size(121, 28);
            this.ComboboxGender.TabIndex = 3;
            this.ComboboxGender.ValueMember = "Key";
            this.ComboboxGender.Validated += new System.EventHandler(this.DivisionControl_Validated);
            // 
            // DivisionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComboboxGender);
            this.Controls.Add(this.LabelGender);
            this.Controls.Add(this.GroupboxHandicap);
            this.Controls.Add(this.GroupboxAverage);
            this.Controls.Add(this.GroupboxAge);
            this.Controls.Add(this.TextboxNumber);
            this.Controls.Add(this.LabelNumber);
            this.Controls.Add(this.TextboxDivisionName);
            this.Controls.Add(this.LabelName);
            this.Name = "DivisionControl";
            this.Size = new System.Drawing.Size(463, 484);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMinimumAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaximumAge)).EndInit();
            this.GroupboxAge.ResumeLayout(false);
            this.GroupboxAge.PerformLayout();
            this.GroupboxAverage.ResumeLayout(false);
            this.GroupboxAverage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaximumAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMinimumAverage)).EndInit();
            this.GroupboxHandicap.ResumeLayout(false);
            this.GroupboxHandicap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaximumHandicapPerGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHandicapBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHandicapPercentage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox TextboxDivisionName;
    private Controls.LabelControl LabelName;
    private ErrorProvider ErrorProviderDivision;
    private TextBox TextboxNumber;
    private Controls.LabelControl LabelNumber;
    private Controls.NumericControl NumericMinimumAge;
    private Controls.LabelControl LabelMinimumAge;
    private GroupBox GroupboxAge;
    private Controls.NumericControl NumericMaximumAge;
    private Controls.LabelControl LabelMaximumAge;
    private GroupBox GroupboxAverage;
    private Controls.LabelControl LabelMinimumAverage;
    private Controls.NumericControl NumericMaximumAverage;
    private Controls.NumericControl NumericMinimumAverage;
    private Controls.LabelControl LabelMaximumAverage;
    private GroupBox GroupboxHandicap;
    private Controls.LabelControl LabelHandicapPercentage;
    private Controls.NumericControl NumericHandicapPercentage;
    private Controls.LabelControl LabelHandicapBase;
    private Controls.NumericControl NumericHandicapBase;
    private Controls.LabelControl LabelMaximumHandicapPerGame;
    private Controls.NumericControl NumericMaximumHandicapPerGame;
    private ComboBox ComboboxGender;
    private Controls.LabelControl LabelGender;
}
