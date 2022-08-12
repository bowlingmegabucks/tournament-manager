namespace NewEnglandClassic.Contols;

partial class TournamentControl
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
            this.nameLabel = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderTournament = new System.Windows.Forms.ErrorProvider(this.components);
            this.nameText = new System.Windows.Forms.TextBox();
            this.startDateLabel = new NewEnglandClassic.Controls.LabelControl();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateLabel = new NewEnglandClassic.Controls.LabelControl();
            this.entryFeeLabel = new NewEnglandClassic.Controls.LabelControl();
            this.entryFeeValue = new NewEnglandClassic.Controls.NumericControl();
            this.gamesLabel = new NewEnglandClassic.Controls.LabelControl();
            this.gamesValue = new NewEnglandClassic.Controls.NumericControl();
            this.finalsRatioLabel = new NewEnglandClassic.Controls.LabelControl();
            this.finalsRatioValue = new NewEnglandClassic.Controls.NumericControl();
            this.cashRatioValue = new NewEnglandClassic.Controls.NumericControl();
            this.cashRatioLabel = new NewEnglandClassic.Controls.LabelControl();
            this.bowlingCenterValue = new System.Windows.Forms.TextBox();
            this.bowlingCenterLabel = new NewEnglandClassic.Controls.LabelControl();
            this.CheckboxComplete = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderTournament)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFeeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalsRatioValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashRatioValue)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Bold = false;
            this.nameLabel.Location = new System.Drawing.Point(3, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Required = true;
            this.nameLabel.Size = new System.Drawing.Size(60, 19);
            this.nameLabel.TabIndex = 101;
            this.nameLabel.TabStop = false;
            this.nameLabel.Text = "Name:";
            // 
            // ErrorProviderTournament
            // 
            this.ErrorProviderTournament.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderTournament.ContainerControl = this;
            // 
            // nameText
            // 
            this.nameText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameText.Location = new System.Drawing.Point(3, 28);
            this.nameText.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.nameText.Name = "nameText";
            this.nameText.PlaceholderText = "Tournament Name";
            this.nameText.Size = new System.Drawing.Size(317, 27);
            this.nameText.TabIndex = 0;
            this.nameText.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentText_Validating);
            this.nameText.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Bold = false;
            this.startDateLabel.Location = new System.Drawing.Point(3, 68);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Required = true;
            this.startDateLabel.Size = new System.Drawing.Size(114, 19);
            this.startDateLabel.TabIndex = 102;
            this.startDateLabel.TabStop = false;
            this.startDateLabel.Text = "Start Date:";
            // 
            // startDatePicker
            // 
            this.startDatePicker.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(3, 93);
            this.startDatePicker.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.startDatePicker.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(131, 27);
            this.startDatePicker.TabIndex = 1;
            this.startDatePicker.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentDates_Validating);
            this.startDatePicker.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // endDatePicker
            // 
            this.endDatePicker.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDatePicker.Location = new System.Drawing.Point(189, 93);
            this.endDatePicker.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.endDatePicker.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.endDatePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(131, 27);
            this.endDatePicker.TabIndex = 2;
            this.endDatePicker.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentDates_Validating);
            this.endDatePicker.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Bold = false;
            this.endDateLabel.Location = new System.Drawing.Point(189, 68);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Required = true;
            this.endDateLabel.Size = new System.Drawing.Size(96, 19);
            this.endDateLabel.TabIndex = 103;
            this.endDateLabel.TabStop = false;
            this.endDateLabel.Text = "End Date:";
            // 
            // entryFeeLabel
            // 
            this.entryFeeLabel.AutoSize = true;
            this.entryFeeLabel.Bold = false;
            this.entryFeeLabel.Location = new System.Drawing.Point(3, 133);
            this.entryFeeLabel.Name = "entryFeeLabel";
            this.entryFeeLabel.Required = true;
            this.entryFeeLabel.Size = new System.Drawing.Size(105, 19);
            this.entryFeeLabel.TabIndex = 104;
            this.entryFeeLabel.TabStop = false;
            this.entryFeeLabel.Text = "Entry Fee:";
            // 
            // entryFeeValue
            // 
            this.entryFeeValue.DecimalPlaces = 2;
            this.entryFeeValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.entryFeeValue.Location = new System.Drawing.Point(3, 159);
            this.entryFeeValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.entryFeeValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.entryFeeValue.Name = "entryFeeValue";
            this.entryFeeValue.Size = new System.Drawing.Size(131, 27);
            this.entryFeeValue.TabIndex = 3;
            this.entryFeeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.entryFeeValue.Validating += new System.ComponentModel.CancelEventHandler(this.EntryFeeValue_Validating);
            this.entryFeeValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // gamesLabel
            // 
            this.gamesLabel.AutoSize = true;
            this.gamesLabel.Bold = false;
            this.gamesLabel.Location = new System.Drawing.Point(189, 133);
            this.gamesLabel.Name = "gamesLabel";
            this.gamesLabel.Required = true;
            this.gamesLabel.Size = new System.Drawing.Size(69, 19);
            this.gamesLabel.TabIndex = 105;
            this.gamesLabel.TabStop = false;
            this.gamesLabel.Text = "Games:";
            // 
            // gamesValue
            // 
            this.gamesValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gamesValue.Location = new System.Drawing.Point(189, 159);
            this.gamesValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.gamesValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.gamesValue.Name = "gamesValue";
            this.gamesValue.Size = new System.Drawing.Size(131, 27);
            this.gamesValue.TabIndex = 4;
            this.gamesValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gamesValue.Validating += new System.ComponentModel.CancelEventHandler(this.GamesValue_Validating);
            this.gamesValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // finalsRatioLabel
            // 
            this.finalsRatioLabel.AutoSize = true;
            this.finalsRatioLabel.Bold = false;
            this.finalsRatioLabel.Location = new System.Drawing.Point(3, 199);
            this.finalsRatioLabel.Name = "finalsRatioLabel";
            this.finalsRatioLabel.Required = false;
            this.finalsRatioLabel.Size = new System.Drawing.Size(126, 19);
            this.finalsRatioLabel.TabIndex = 106;
            this.finalsRatioLabel.TabStop = false;
            this.finalsRatioLabel.Text = "Finals Ratio:";
            // 
            // finalsRatioValue
            // 
            this.finalsRatioValue.DecimalPlaces = 1;
            this.finalsRatioValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.finalsRatioValue.Location = new System.Drawing.Point(3, 225);
            this.finalsRatioValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.finalsRatioValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.finalsRatioValue.Name = "finalsRatioValue";
            this.finalsRatioValue.Size = new System.Drawing.Size(131, 27);
            this.finalsRatioValue.TabIndex = 5;
            this.finalsRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.finalsRatioValue.Validating += new System.ComponentModel.CancelEventHandler(this.FinalsRatioValue_Validating);
            this.finalsRatioValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // cashRatioValue
            // 
            this.cashRatioValue.DecimalPlaces = 1;
            this.cashRatioValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cashRatioValue.Location = new System.Drawing.Point(189, 225);
            this.cashRatioValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.cashRatioValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.cashRatioValue.Name = "cashRatioValue";
            this.cashRatioValue.Size = new System.Drawing.Size(131, 27);
            this.cashRatioValue.TabIndex = 6;
            this.cashRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cashRatioValue.Validating += new System.ComponentModel.CancelEventHandler(this.CashRatioValue_Validating);
            this.cashRatioValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // cashRatioLabel
            // 
            this.cashRatioLabel.AutoSize = true;
            this.cashRatioLabel.Bold = false;
            this.cashRatioLabel.Location = new System.Drawing.Point(189, 199);
            this.cashRatioLabel.Name = "cashRatioLabel";
            this.cashRatioLabel.Required = true;
            this.cashRatioLabel.Size = new System.Drawing.Size(114, 19);
            this.cashRatioLabel.TabIndex = 107;
            this.cashRatioLabel.TabStop = false;
            this.cashRatioLabel.Text = "Cash Ratio:";
            // 
            // bowlingCenterValue
            // 
            this.bowlingCenterValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bowlingCenterValue.Location = new System.Drawing.Point(3, 290);
            this.bowlingCenterValue.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.bowlingCenterValue.Name = "bowlingCenterValue";
            this.bowlingCenterValue.PlaceholderText = "AMF Lanes";
            this.bowlingCenterValue.Size = new System.Drawing.Size(317, 27);
            this.bowlingCenterValue.TabIndex = 7;
            this.bowlingCenterValue.Validating += new System.ComponentModel.CancelEventHandler(this.BowlingCenterText_Validating);
            this.bowlingCenterValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // bowlingCenterLabel
            // 
            this.bowlingCenterLabel.AutoSize = true;
            this.bowlingCenterLabel.Bold = false;
            this.bowlingCenterLabel.Location = new System.Drawing.Point(0, 265);
            this.bowlingCenterLabel.Name = "bowlingCenterLabel";
            this.bowlingCenterLabel.Required = true;
            this.bowlingCenterLabel.Size = new System.Drawing.Size(150, 19);
            this.bowlingCenterLabel.TabIndex = 108;
            this.bowlingCenterLabel.TabStop = false;
            this.bowlingCenterLabel.Text = "Bowling Center:";
            // 
            // CheckboxComplete
            // 
            this.CheckboxComplete.AutoSize = true;
            this.CheckboxComplete.Location = new System.Drawing.Point(237, 330);
            this.CheckboxComplete.Name = "CheckboxComplete";
            this.CheckboxComplete.Size = new System.Drawing.Size(78, 19);
            this.CheckboxComplete.TabIndex = 109;
            this.CheckboxComplete.TabStop = false;
            this.CheckboxComplete.Text = "Complete";
            this.CheckboxComplete.UseVisualStyleBackColor = true;
            this.CheckboxComplete.Visible = false;
            // 
            // TournamentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CheckboxComplete);
            this.Controls.Add(this.bowlingCenterValue);
            this.Controls.Add(this.bowlingCenterLabel);
            this.Controls.Add(this.cashRatioValue);
            this.Controls.Add(this.cashRatioLabel);
            this.Controls.Add(this.finalsRatioValue);
            this.Controls.Add(this.finalsRatioLabel);
            this.Controls.Add(this.gamesValue);
            this.Controls.Add(this.gamesLabel);
            this.Controls.Add(this.entryFeeValue);
            this.Controls.Add(this.entryFeeLabel);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.nameLabel);
            this.Name = "TournamentControl";
            this.Size = new System.Drawing.Size(354, 330);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentControl_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderTournament)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFeeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalsRatioValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashRatioValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Controls.LabelControl nameLabel;
    private ErrorProvider ErrorProviderTournament;
    private CheckBox CheckboxComplete;
    private TextBox bowlingCenterValue;
    private Controls.LabelControl bowlingCenterLabel;
    private Controls.NumericControl cashRatioValue;
    private Controls.LabelControl cashRatioLabel;
    private Controls.NumericControl finalsRatioValue;
    private Controls.LabelControl finalsRatioLabel;
    private Controls.NumericControl gamesValue;
    private Controls.LabelControl gamesLabel;
    private Controls.NumericControl entryFeeValue;
    private Controls.LabelControl entryFeeLabel;
    private DateTimePicker endDatePicker;
    private Controls.LabelControl endDateLabel;
    private DateTimePicker startDatePicker;
    private Controls.LabelControl startDateLabel;
    private TextBox nameText;
}
