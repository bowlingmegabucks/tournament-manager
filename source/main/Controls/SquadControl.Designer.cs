namespace NewEnglandClassic.Contols;

partial class SquadControl
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
            this.cashRatioValue = new NewEnglandClassic.Controls.NumericControl();
            this.cashRatioLabel = new NewEnglandClassic.Controls.LabelControl();
            this.finalsRatioValue = new NewEnglandClassic.Controls.NumericControl();
            this.finalsRatioLabel = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderSquad = new System.Windows.Forms.ErrorProvider(this.components);
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.dateLabel = new NewEnglandClassic.Controls.LabelControl();
            this.masPerPairValue = new NewEnglandClassic.Controls.NumericControl();
            this.maxPerPairLabel = new NewEnglandClassic.Controls.LabelControl();
            this.startingLaneValue = new NewEnglandClassic.Controls.NumericControl();
            this.startingLaneLabel = new NewEnglandClassic.Controls.LabelControl();
            this.numberOfLanesValue = new NewEnglandClassic.Controls.NumericControl();
            this.numberOfLanesLabel = new NewEnglandClassic.Controls.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cashRatioValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalsRatioValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSquad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masPerPairValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingLaneValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfLanesValue)).BeginInit();
            this.SuspendLayout();
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
            this.cashRatioValue.TabIndex = 2;
            this.cashRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cashRatioValue.Validating += new System.ComponentModel.CancelEventHandler(this.CashRatioValue_Validating);
            this.cashRatioValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // cashRatioLabel
            // 
            this.cashRatioLabel.AutoSize = true;
            this.cashRatioLabel.Bold = false;
            this.cashRatioLabel.Location = new System.Drawing.Point(189, 68);
            this.cashRatioLabel.Name = "cashRatioLabel";
            this.cashRatioLabel.Required = false;
            this.cashRatioLabel.Size = new System.Drawing.Size(117, 19);
            this.cashRatioLabel.TabIndex = 111;
            this.cashRatioLabel.TabStop = false;
            this.cashRatioLabel.Text = "Cash Ratio:";
            // 
            // finalsRatioValue
            // 
            this.finalsRatioValue.DecimalPlaces = 1;
            this.finalsRatioValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.finalsRatioValue.Location = new System.Drawing.Point(3, 94);
            this.finalsRatioValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.finalsRatioValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.finalsRatioValue.Name = "finalsRatioValue";
            this.finalsRatioValue.Size = new System.Drawing.Size(131, 27);
            this.finalsRatioValue.TabIndex = 1;
            this.finalsRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.finalsRatioValue.Validating += new System.ComponentModel.CancelEventHandler(this.FinalsRatioValue_Validating);
            this.finalsRatioValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // finalsRatioLabel
            // 
            this.finalsRatioLabel.AutoSize = true;
            this.finalsRatioLabel.Bold = false;
            this.finalsRatioLabel.Location = new System.Drawing.Point(3, 68);
            this.finalsRatioLabel.Name = "finalsRatioLabel";
            this.finalsRatioLabel.Required = false;
            this.finalsRatioLabel.Size = new System.Drawing.Size(126, 19);
            this.finalsRatioLabel.TabIndex = 110;
            this.finalsRatioLabel.TabStop = false;
            this.finalsRatioLabel.Text = "Finals Ratio:";
            // 
            // ErrorProviderSquad
            // 
            this.ErrorProviderSquad.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderSquad.ContainerControl = this;
            // 
            // datePicker
            // 
            this.datePicker.CustomFormat = "MM/dd/yyyy    hh:mm tt";
            this.datePicker.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePicker.Location = new System.Drawing.Point(3, 28);
            this.datePicker.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.datePicker.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.datePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(217, 27);
            this.datePicker.TabIndex = 0;
            this.datePicker.Validating += new System.ComponentModel.CancelEventHandler(this.DatePicker_Validating);
            this.datePicker.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Bold = false;
            this.dateLabel.Location = new System.Drawing.Point(3, 3);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Required = true;
            this.dateLabel.Size = new System.Drawing.Size(60, 19);
            this.dateLabel.TabIndex = 113;
            this.dateLabel.TabStop = false;
            this.dateLabel.Text = "Date:";
            // 
            // masPerPairValue
            // 
            this.masPerPairValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.masPerPairValue.Location = new System.Drawing.Point(355, 160);
            this.masPerPairValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.masPerPairValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.masPerPairValue.Name = "masPerPairValue";
            this.masPerPairValue.Size = new System.Drawing.Size(131, 27);
            this.masPerPairValue.TabIndex = 5;
            this.masPerPairValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.masPerPairValue.Validating += new System.ComponentModel.CancelEventHandler(this.MaxPerPairValue_Validating);
            this.masPerPairValue.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // maxPerPairLabel
            // 
            this.maxPerPairLabel.AutoSize = true;
            this.maxPerPairLabel.Bold = false;
            this.maxPerPairLabel.Location = new System.Drawing.Point(355, 134);
            this.maxPerPairLabel.Name = "maxPerPairLabel";
            this.maxPerPairLabel.Required = true;
            this.maxPerPairLabel.Size = new System.Drawing.Size(132, 19);
            this.maxPerPairLabel.TabIndex = 115;
            this.maxPerPairLabel.TabStop = false;
            this.maxPerPairLabel.Text = "Max Per Pair:";
            // 
            // startingLaneValue
            // 
            this.startingLaneValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startingLaneValue.Location = new System.Drawing.Point(3, 160);
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
            this.startingLaneValue.TabIndex = 3;
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
            this.startingLaneLabel.Location = new System.Drawing.Point(3, 134);
            this.startingLaneLabel.Name = "startingLaneLabel";
            this.startingLaneLabel.Required = true;
            this.startingLaneLabel.Size = new System.Drawing.Size(141, 19);
            this.startingLaneLabel.TabIndex = 117;
            this.startingLaneLabel.TabStop = false;
            this.startingLaneLabel.Text = "Starting Lane:";
            // 
            // numberOfLanesValue
            // 
            this.numberOfLanesValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numberOfLanesValue.Location = new System.Drawing.Point(189, 160);
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
            this.numberOfLanesValue.TabIndex = 4;
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
            this.numberOfLanesLabel.Location = new System.Drawing.Point(167, 134);
            this.numberOfLanesLabel.Name = "numberOfLanesLabel";
            this.numberOfLanesLabel.Required = true;
            this.numberOfLanesLabel.Size = new System.Drawing.Size(159, 19);
            this.numberOfLanesLabel.TabIndex = 119;
            this.numberOfLanesLabel.TabStop = false;
            this.numberOfLanesLabel.Text = "Number of Lanes:";
            // 
            // SquadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numberOfLanesValue);
            this.Controls.Add(this.numberOfLanesLabel);
            this.Controls.Add(this.startingLaneValue);
            this.Controls.Add(this.startingLaneLabel);
            this.Controls.Add(this.masPerPairValue);
            this.Controls.Add(this.maxPerPairLabel);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.cashRatioValue);
            this.Controls.Add(this.cashRatioLabel);
            this.Controls.Add(this.finalsRatioValue);
            this.Controls.Add(this.finalsRatioLabel);
            this.Name = "SquadControl";
            this.Size = new System.Drawing.Size(527, 215);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.SquadControl_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.cashRatioValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalsRatioValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSquad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masPerPairValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingLaneValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfLanesValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Controls.NumericControl cashRatioValue;
    private Controls.LabelControl cashRatioLabel;
    private Controls.NumericControl finalsRatioValue;
    private Controls.LabelControl finalsRatioLabel;
    private ErrorProvider ErrorProviderSquad;
    private DateTimePicker datePicker;
    private Controls.LabelControl dateLabel;
    private Controls.NumericControl masPerPairValue;
    private Controls.LabelControl maxPerPairLabel;
    private Controls.NumericControl numberOfLanesValue;
    private Controls.LabelControl numberOfLanesLabel;
    private Controls.NumericControl startingLaneValue;
    private Controls.LabelControl startingLaneLabel;
}
