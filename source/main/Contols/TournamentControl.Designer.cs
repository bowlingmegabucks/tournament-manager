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
            this.LabelName = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderTournament = new System.Windows.Forms.ErrorProvider(this.components);
            this.TextboxTournament = new System.Windows.Forms.TextBox();
            this.LabelStart = new NewEnglandClassic.Controls.LabelControl();
            this.DatePickerStart = new System.Windows.Forms.DateTimePicker();
            this.DatePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.LabelEnd = new NewEnglandClassic.Controls.LabelControl();
            this.LabelEntryFee = new NewEnglandClassic.Controls.LabelControl();
            this.NumericEntryFee = new NewEnglandClassic.Controls.NumericControl();
            this.LabelGames = new NewEnglandClassic.Controls.LabelControl();
            this.NumericGames = new NewEnglandClassic.Controls.NumericControl();
            this.LabelFinalsRatio = new NewEnglandClassic.Controls.LabelControl();
            this.NumericFinalsRatio = new NewEnglandClassic.Controls.NumericControl();
            this.NumericCashRatio = new NewEnglandClassic.Controls.NumericControl();
            this.LabelCashRatio = new NewEnglandClassic.Controls.LabelControl();
            this.TextboxBowlingCenter = new System.Windows.Forms.TextBox();
            this.LabelBowlingCenter = new NewEnglandClassic.Controls.LabelControl();
            this.CheckboxComplete = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderTournament)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEntryFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericGames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericFinalsRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCashRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Bold = false;
            this.LabelName.Location = new System.Drawing.Point(3, 3);
            this.LabelName.Name = "LabelName";
            this.LabelName.Required = true;
            this.LabelName.Size = new System.Drawing.Size(60, 19);
            this.LabelName.TabIndex = 101;
            this.LabelName.TabStop = false;
            this.LabelName.Text = "Name:";
            // 
            // ErrorProviderTournament
            // 
            this.ErrorProviderTournament.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderTournament.ContainerControl = this;
            // 
            // TextboxTournament
            // 
            this.TextboxTournament.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxTournament.Location = new System.Drawing.Point(3, 28);
            this.TextboxTournament.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.TextboxTournament.Name = "TextboxTournament";
            this.TextboxTournament.PlaceholderText = "Tournament Name";
            this.TextboxTournament.Size = new System.Drawing.Size(317, 27);
            this.TextboxTournament.TabIndex = 0;
            this.TextboxTournament.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxTournament_Validating);
            this.TextboxTournament.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelStart
            // 
            this.LabelStart.AutoSize = true;
            this.LabelStart.Bold = false;
            this.LabelStart.Location = new System.Drawing.Point(3, 68);
            this.LabelStart.Name = "LabelStart";
            this.LabelStart.Required = true;
            this.LabelStart.Size = new System.Drawing.Size(114, 19);
            this.LabelStart.TabIndex = 102;
            this.LabelStart.TabStop = false;
            this.LabelStart.Text = "Start Date:";
            // 
            // DatePickerStart
            // 
            this.DatePickerStart.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DatePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatePickerStart.Location = new System.Drawing.Point(3, 93);
            this.DatePickerStart.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.DatePickerStart.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.DatePickerStart.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePickerStart.Name = "DatePickerStart";
            this.DatePickerStart.Size = new System.Drawing.Size(131, 27);
            this.DatePickerStart.TabIndex = 1;
            this.DatePickerStart.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentDates_Validating);
            this.DatePickerStart.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // DatePickerEnd
            // 
            this.DatePickerEnd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DatePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatePickerEnd.Location = new System.Drawing.Point(189, 93);
            this.DatePickerEnd.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.DatePickerEnd.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.DatePickerEnd.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePickerEnd.Name = "DatePickerEnd";
            this.DatePickerEnd.Size = new System.Drawing.Size(131, 27);
            this.DatePickerEnd.TabIndex = 2;
            this.DatePickerEnd.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentDates_Validating);
            this.DatePickerEnd.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelEnd
            // 
            this.LabelEnd.AutoSize = true;
            this.LabelEnd.Bold = false;
            this.LabelEnd.Location = new System.Drawing.Point(189, 68);
            this.LabelEnd.Name = "LabelEnd";
            this.LabelEnd.Required = true;
            this.LabelEnd.Size = new System.Drawing.Size(96, 19);
            this.LabelEnd.TabIndex = 103;
            this.LabelEnd.TabStop = false;
            this.LabelEnd.Text = "End Date:";
            // 
            // LabelEntryFee
            // 
            this.LabelEntryFee.AutoSize = true;
            this.LabelEntryFee.Bold = false;
            this.LabelEntryFee.Location = new System.Drawing.Point(3, 133);
            this.LabelEntryFee.Name = "LabelEntryFee";
            this.LabelEntryFee.Required = true;
            this.LabelEntryFee.Size = new System.Drawing.Size(105, 19);
            this.LabelEntryFee.TabIndex = 104;
            this.LabelEntryFee.TabStop = false;
            this.LabelEntryFee.Text = "Entry Fee:";
            // 
            // NumericEntryFee
            // 
            this.NumericEntryFee.DecimalPlaces = 2;
            this.NumericEntryFee.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericEntryFee.Location = new System.Drawing.Point(3, 159);
            this.NumericEntryFee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericEntryFee.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.NumericEntryFee.Name = "NumericEntryFee";
            this.NumericEntryFee.Size = new System.Drawing.Size(131, 27);
            this.NumericEntryFee.TabIndex = 3;
            this.NumericEntryFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericEntryFee.Validating += new System.ComponentModel.CancelEventHandler(this.NumericEntryFee_Validating);
            this.NumericEntryFee.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelGames
            // 
            this.LabelGames.AutoSize = true;
            this.LabelGames.Bold = false;
            this.LabelGames.Location = new System.Drawing.Point(189, 133);
            this.LabelGames.Name = "LabelGames";
            this.LabelGames.Required = true;
            this.LabelGames.Size = new System.Drawing.Size(69, 19);
            this.LabelGames.TabIndex = 105;
            this.LabelGames.TabStop = false;
            this.LabelGames.Text = "Games:";
            // 
            // NumericGames
            // 
            this.NumericGames.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericGames.Location = new System.Drawing.Point(189, 159);
            this.NumericGames.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericGames.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericGames.Name = "NumericGames";
            this.NumericGames.Size = new System.Drawing.Size(131, 27);
            this.NumericGames.TabIndex = 4;
            this.NumericGames.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericGames.Validating += new System.ComponentModel.CancelEventHandler(this.NumericGames_Validating);
            this.NumericGames.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelFinalsRatio
            // 
            this.LabelFinalsRatio.AutoSize = true;
            this.LabelFinalsRatio.Bold = false;
            this.LabelFinalsRatio.Location = new System.Drawing.Point(3, 199);
            this.LabelFinalsRatio.Name = "LabelFinalsRatio";
            this.LabelFinalsRatio.Required = false;
            this.LabelFinalsRatio.Size = new System.Drawing.Size(126, 19);
            this.LabelFinalsRatio.TabIndex = 106;
            this.LabelFinalsRatio.TabStop = false;
            this.LabelFinalsRatio.Text = "Finals Ratio:";
            // 
            // NumericFinalsRatio
            // 
            this.NumericFinalsRatio.DecimalPlaces = 1;
            this.NumericFinalsRatio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericFinalsRatio.Location = new System.Drawing.Point(3, 225);
            this.NumericFinalsRatio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericFinalsRatio.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.NumericFinalsRatio.Name = "NumericFinalsRatio";
            this.NumericFinalsRatio.Size = new System.Drawing.Size(131, 27);
            this.NumericFinalsRatio.TabIndex = 5;
            this.NumericFinalsRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericFinalsRatio.Validating += new System.ComponentModel.CancelEventHandler(this.NumericFinalsRatio_Validating);
            this.NumericFinalsRatio.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // NumericCashRatio
            // 
            this.NumericCashRatio.DecimalPlaces = 1;
            this.NumericCashRatio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericCashRatio.Location = new System.Drawing.Point(189, 225);
            this.NumericCashRatio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericCashRatio.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.NumericCashRatio.Name = "NumericCashRatio";
            this.NumericCashRatio.Size = new System.Drawing.Size(131, 27);
            this.NumericCashRatio.TabIndex = 6;
            this.NumericCashRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericCashRatio.Validating += new System.ComponentModel.CancelEventHandler(this.NumericCashRatio_Validating);
            this.NumericCashRatio.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelCashRatio
            // 
            this.LabelCashRatio.AutoSize = true;
            this.LabelCashRatio.Bold = false;
            this.LabelCashRatio.Location = new System.Drawing.Point(189, 199);
            this.LabelCashRatio.Name = "LabelCashRatio";
            this.LabelCashRatio.Required = true;
            this.LabelCashRatio.Size = new System.Drawing.Size(114, 19);
            this.LabelCashRatio.TabIndex = 107;
            this.LabelCashRatio.TabStop = false;
            this.LabelCashRatio.Text = "Cash Ratio:";
            // 
            // TextboxBowlingCenter
            // 
            this.TextboxBowlingCenter.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxBowlingCenter.Location = new System.Drawing.Point(3, 290);
            this.TextboxBowlingCenter.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.TextboxBowlingCenter.Name = "TextboxBowlingCenter";
            this.TextboxBowlingCenter.PlaceholderText = "AMF Lanes";
            this.TextboxBowlingCenter.Size = new System.Drawing.Size(317, 27);
            this.TextboxBowlingCenter.TabIndex = 7;
            this.TextboxBowlingCenter.Validating += new System.ComponentModel.CancelEventHandler(this.TextboxBowlingCenter_Validating);
            this.TextboxBowlingCenter.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelBowlingCenter
            // 
            this.LabelBowlingCenter.AutoSize = true;
            this.LabelBowlingCenter.Bold = false;
            this.LabelBowlingCenter.Location = new System.Drawing.Point(0, 265);
            this.LabelBowlingCenter.Name = "LabelBowlingCenter";
            this.LabelBowlingCenter.Required = true;
            this.LabelBowlingCenter.Size = new System.Drawing.Size(150, 19);
            this.LabelBowlingCenter.TabIndex = 108;
            this.LabelBowlingCenter.TabStop = false;
            this.LabelBowlingCenter.Text = "Bowling Center:";
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
            this.Controls.Add(this.TextboxBowlingCenter);
            this.Controls.Add(this.LabelBowlingCenter);
            this.Controls.Add(this.NumericCashRatio);
            this.Controls.Add(this.LabelCashRatio);
            this.Controls.Add(this.NumericFinalsRatio);
            this.Controls.Add(this.LabelFinalsRatio);
            this.Controls.Add(this.NumericGames);
            this.Controls.Add(this.LabelGames);
            this.Controls.Add(this.NumericEntryFee);
            this.Controls.Add(this.LabelEntryFee);
            this.Controls.Add(this.DatePickerEnd);
            this.Controls.Add(this.LabelEnd);
            this.Controls.Add(this.DatePickerStart);
            this.Controls.Add(this.LabelStart);
            this.Controls.Add(this.TextboxTournament);
            this.Controls.Add(this.LabelName);
            this.Name = "TournamentControl";
            this.Size = new System.Drawing.Size(354, 330);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.TournamentControl_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderTournament)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEntryFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericGames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericFinalsRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCashRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Controls.LabelControl LabelName;
    private ErrorProvider ErrorProviderTournament;
    private CheckBox CheckboxComplete;
    private TextBox TextboxBowlingCenter;
    private Controls.LabelControl LabelBowlingCenter;
    private Controls.NumericControl NumericCashRatio;
    private Controls.LabelControl LabelCashRatio;
    private Controls.NumericControl NumericFinalsRatio;
    private Controls.LabelControl LabelFinalsRatio;
    private Controls.NumericControl NumericGames;
    private Controls.LabelControl LabelGames;
    private Controls.NumericControl NumericEntryFee;
    private Controls.LabelControl LabelEntryFee;
    private DateTimePicker DatePickerEnd;
    private Controls.LabelControl LabelEnd;
    private DateTimePicker DatePickerStart;
    private Controls.LabelControl LabelStart;
    private TextBox TextboxTournament;
}
