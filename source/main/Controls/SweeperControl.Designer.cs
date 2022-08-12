namespace NewEnglandClassic.Contols;

partial class SweeperControl
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
            this.divisionsGroupBox = new System.Windows.Forms.GroupBox();
            this.PanelDivisions = new System.Windows.Forms.Panel();
            this.sweeperDivisions = new NewEnglandClassic.Contols.SweeperDivisionsControl();
            this.squadDatePicker = new System.Windows.Forms.DateTimePicker();
            this.dateLabel = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderSweeper = new System.Windows.Forms.ErrorProvider(this.components);
            this.entryFeeValue = new NewEnglandClassic.Controls.NumericControl();
            this.entryFeeLabel = new NewEnglandClassic.Controls.LabelControl();
            this.gamesValue = new NewEnglandClassic.Controls.NumericControl();
            this.gamesLabel = new NewEnglandClassic.Controls.LabelControl();
            this.cashRatioValue = new NewEnglandClassic.Controls.NumericControl();
            this.cashRatioLabel = new NewEnglandClassic.Controls.LabelControl();
            this.maxPerPairValue = new NewEnglandClassic.Controls.NumericControl();
            this.maxPerPairLabel = new NewEnglandClassic.Controls.LabelControl();
            this.numberOfLanesValue = new NewEnglandClassic.Controls.NumericControl();
            this.numberOfLanesLabel = new NewEnglandClassic.Controls.LabelControl();
            this.startingLaneValue = new NewEnglandClassic.Controls.NumericControl();
            this.startingLaneLabel = new NewEnglandClassic.Controls.LabelControl();
            this.divisionsGroupBox.SuspendLayout();
            this.PanelDivisions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSweeper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFeeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashRatioValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPerPairValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfLanesValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingLaneValue)).BeginInit();
            this.SuspendLayout();
            // 
            // divisionsGroupBox
            // 
            this.divisionsGroupBox.AutoSize = true;
            this.divisionsGroupBox.Controls.Add(this.PanelDivisions);
            this.divisionsGroupBox.Location = new System.Drawing.Point(3, 199);
            this.divisionsGroupBox.Name = "divisionsGroupBox";
            this.divisionsGroupBox.Size = new System.Drawing.Size(540, 314);
            this.divisionsGroupBox.TabIndex = 7;
            this.divisionsGroupBox.TabStop = false;
            this.divisionsGroupBox.Text = "Division Bonus Pins";
            // 
            // PanelDivisions
            // 
            this.PanelDivisions.AutoScroll = true;
            this.PanelDivisions.AutoSize = true;
            this.PanelDivisions.Controls.Add(this.sweeperDivisions);
            this.PanelDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDivisions.Location = new System.Drawing.Point(3, 19);
            this.PanelDivisions.Name = "PanelDivisions";
            this.PanelDivisions.Size = new System.Drawing.Size(534, 292);
            this.PanelDivisions.TabIndex = 0;
            // 
            // sweeperDivisions
            // 
            this.sweeperDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sweeperDivisions.Location = new System.Drawing.Point(0, 0);
            this.sweeperDivisions.Name = "sweeperDivisions";
            this.sweeperDivisions.Size = new System.Drawing.Size(534, 292);
            this.sweeperDivisions.TabIndex = 1;
            // 
            // datePicker
            // 
            this.squadDatePicker.CustomFormat = "MM/dd/yyyy    hh:mm tt";
            this.squadDatePicker.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.squadDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.squadDatePicker.Location = new System.Drawing.Point(3, 28);
            this.squadDatePicker.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.squadDatePicker.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.squadDatePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.squadDatePicker.Name = "datePicker";
            this.squadDatePicker.Size = new System.Drawing.Size(217, 27);
            this.squadDatePicker.TabIndex = 0;
            this.squadDatePicker.Validating += new System.ComponentModel.CancelEventHandler(this.SquadDatePicker_Validating);
            this.squadDatePicker.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Bold = false;
            this.dateLabel.Location = new System.Drawing.Point(3, 3);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Required = true;
            this.dateLabel.Size = new System.Drawing.Size(60, 19);
            this.dateLabel.TabIndex = 115;
            this.dateLabel.TabStop = false;
            this.dateLabel.Text = "Date:";
            // 
            // ErrorProviderSweeper
            // 
            this.ErrorProviderSweeper.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderSweeper.ContainerControl = this;
            // 
            // entryFeeValue
            // 
            this.entryFeeValue.DecimalPlaces = 2;
            this.entryFeeValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.entryFeeValue.Location = new System.Drawing.Point(350, 31);
            this.entryFeeValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.entryFeeValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.entryFeeValue.Name = "entryFeeValue";
            this.entryFeeValue.Size = new System.Drawing.Size(131, 27);
            this.entryFeeValue.TabIndex = 1;
            this.entryFeeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.entryFeeValue.Validating += new System.ComponentModel.CancelEventHandler(this.EntryFeeValue_Validating);
            this.entryFeeValue.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // entryFeeLabel
            // 
            this.entryFeeLabel.AutoSize = true;
            this.entryFeeLabel.Bold = false;
            this.entryFeeLabel.Location = new System.Drawing.Point(350, 5);
            this.entryFeeLabel.Name = "entryFeeLabel";
            this.entryFeeLabel.Required = true;
            this.entryFeeLabel.Size = new System.Drawing.Size(105, 19);
            this.entryFeeLabel.TabIndex = 117;
            this.entryFeeLabel.TabStop = false;
            this.entryFeeLabel.Text = "Entry Fee:";
            // 
            // gamesValue
            // 
            this.gamesValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gamesValue.Location = new System.Drawing.Point(3, 94);
            this.gamesValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.gamesValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.gamesValue.Name = "gamesValue";
            this.gamesValue.Size = new System.Drawing.Size(131, 27);
            this.gamesValue.TabIndex = 2;
            this.gamesValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gamesValue.Validating += new System.ComponentModel.CancelEventHandler(this.GamesValue_Validating);
            this.gamesValue.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // gamesLabel
            // 
            this.gamesLabel.AutoSize = true;
            this.gamesLabel.Bold = false;
            this.gamesLabel.Location = new System.Drawing.Point(3, 68);
            this.gamesLabel.Name = "gamesLabel";
            this.gamesLabel.Required = true;
            this.gamesLabel.Size = new System.Drawing.Size(69, 19);
            this.gamesLabel.TabIndex = 119;
            this.gamesLabel.TabStop = false;
            this.gamesLabel.Text = "Games:";
            // 
            // cashRatioValue
            // 
            this.cashRatioValue.DecimalPlaces = 1;
            this.cashRatioValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cashRatioValue.Location = new System.Drawing.Point(189, 94);
            this.cashRatioValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.cashRatioValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.cashRatioValue.Name = "cashRatioValue";
            this.cashRatioValue.Size = new System.Drawing.Size(131, 27);
            this.cashRatioValue.TabIndex = 3;
            this.cashRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cashRatioValue.Validating += new System.ComponentModel.CancelEventHandler(this.CashRatioValue_Validating);
            this.cashRatioValue.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // cashRatioLabel
            // 
            this.cashRatioLabel.AutoSize = true;
            this.cashRatioLabel.Bold = false;
            this.cashRatioLabel.Location = new System.Drawing.Point(189, 68);
            this.cashRatioLabel.Name = "cashRatioLabel";
            this.cashRatioLabel.Required = true;
            this.cashRatioLabel.Size = new System.Drawing.Size(114, 19);
            this.cashRatioLabel.TabIndex = 121;
            this.cashRatioLabel.TabStop = false;
            this.cashRatioLabel.Text = "Cash Ratio:";
            // 
            // maxPerPairValue
            // 
            this.maxPerPairValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maxPerPairValue.Location = new System.Drawing.Point(350, 159);
            this.maxPerPairValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.maxPerPairValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.maxPerPairValue.Name = "maxPerPairValue";
            this.maxPerPairValue.Size = new System.Drawing.Size(131, 27);
            this.maxPerPairValue.TabIndex = 6;
            this.maxPerPairValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxPerPairValue.Validating += new System.ComponentModel.CancelEventHandler(this.MaxPerPairValue_Validating);
            this.maxPerPairValue.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // maxPerPairLabel
            // 
            this.maxPerPairLabel.AutoSize = true;
            this.maxPerPairLabel.Bold = false;
            this.maxPerPairLabel.Location = new System.Drawing.Point(350, 133);
            this.maxPerPairLabel.Name = "maxPerPairLabel";
            this.maxPerPairLabel.Required = true;
            this.maxPerPairLabel.Size = new System.Drawing.Size(132, 19);
            this.maxPerPairLabel.TabIndex = 123;
            this.maxPerPairLabel.TabStop = false;
            this.maxPerPairLabel.Text = "Max Per Pair:";
            // 
            // numberOfLanesValue
            // 
            this.numberOfLanesValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numberOfLanesValue.Location = new System.Drawing.Point(189, 159);
            this.numberOfLanesValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.numberOfLanesValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numberOfLanesValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfLanesValue.Name = "numberOfLanesValue";
            this.numberOfLanesValue.Size = new System.Drawing.Size(131, 27);
            this.numberOfLanesValue.TabIndex = 5;
            this.numberOfLanesValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numberOfLanesValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numberOfLanesLabel
            // 
            this.numberOfLanesLabel.AutoSize = true;
            this.numberOfLanesLabel.Bold = false;
            this.numberOfLanesLabel.Location = new System.Drawing.Point(167, 133);
            this.numberOfLanesLabel.Name = "numberOfLanesLabel";
            this.numberOfLanesLabel.Required = true;
            this.numberOfLanesLabel.Size = new System.Drawing.Size(159, 19);
            this.numberOfLanesLabel.TabIndex = 1003;
            this.numberOfLanesLabel.TabStop = false;
            this.numberOfLanesLabel.Text = "Number of Lanes:";
            // 
            // startingLaneValue
            // 
            this.startingLaneValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startingLaneValue.Location = new System.Drawing.Point(3, 159);
            this.startingLaneValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.startingLaneValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.startingLaneValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startingLaneValue.Name = "startingLaneValue";
            this.startingLaneValue.Size = new System.Drawing.Size(131, 27);
            this.startingLaneValue.TabIndex = 4;
            this.startingLaneValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingLaneValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startingLaneLabel
            // 
            this.startingLaneLabel.AutoSize = true;
            this.startingLaneLabel.Bold = false;
            this.startingLaneLabel.Location = new System.Drawing.Point(3, 133);
            this.startingLaneLabel.Name = "startingLaneLabel";
            this.startingLaneLabel.Required = true;
            this.startingLaneLabel.Size = new System.Drawing.Size(141, 19);
            this.startingLaneLabel.TabIndex = 1002;
            this.startingLaneLabel.TabStop = false;
            this.startingLaneLabel.Text = "Starting Lane:";
            // 
            // SweeperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.numberOfLanesValue);
            this.Controls.Add(this.numberOfLanesLabel);
            this.Controls.Add(this.startingLaneValue);
            this.Controls.Add(this.startingLaneLabel);
            this.Controls.Add(this.maxPerPairValue);
            this.Controls.Add(this.maxPerPairLabel);
            this.Controls.Add(this.cashRatioValue);
            this.Controls.Add(this.cashRatioLabel);
            this.Controls.Add(this.gamesValue);
            this.Controls.Add(this.gamesLabel);
            this.Controls.Add(this.entryFeeValue);
            this.Controls.Add(this.entryFeeLabel);
            this.Controls.Add(this.squadDatePicker);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.divisionsGroupBox);
            this.Name = "SweeperControl";
            this.Size = new System.Drawing.Size(558, 528);
            this.divisionsGroupBox.ResumeLayout(false);
            this.divisionsGroupBox.PerformLayout();
            this.PanelDivisions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSweeper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFeeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashRatioValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPerPairValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfLanesValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingLaneValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private GroupBox divisionsGroupBox;
    private Panel PanelDivisions;
    private SweeperDivisionsControl sweeperDivisions;
    private DateTimePicker squadDatePicker;
    private Controls.LabelControl dateLabel;
    private ErrorProvider ErrorProviderSweeper;
    private Controls.NumericControl entryFeeValue;
    private Controls.LabelControl entryFeeLabel;
    private Controls.NumericControl gamesValue;
    private Controls.LabelControl gamesLabel;
    private Controls.NumericControl cashRatioValue;
    private Controls.LabelControl cashRatioLabel;
    private Controls.NumericControl maxPerPairValue;
    private Controls.LabelControl maxPerPairLabel;
    private Controls.NumericControl numberOfLanesValue;
    private Controls.LabelControl numberOfLanesLabel;
    private Controls.NumericControl startingLaneValue;
    private Controls.LabelControl startingLaneLabel;
}
