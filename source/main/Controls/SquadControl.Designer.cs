namespace BowlingMegabucks.TournamentManager.Controls;

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
        components = new System.ComponentModel.Container();
        cashRatioValue = new NumericControl();
        cashRatioLabel = new LabelControl();
        finalsRatioValue = new NumericControl();
        finalsRatioLabel = new LabelControl();
        squadErrorProvider = new ErrorProvider(components);
        datePicker = new DateTimePicker();
        dateLabel = new LabelControl();
        maxPerPairValue = new NumericControl();
        maxPerPairLabel = new LabelControl();
        startingLaneValue = new NumericControl();
        startingLaneLabel = new LabelControl();
        numberOfLanesValue = new NumericControl();
        numberOfLanesLabel = new LabelControl();
        entryFeeValue = new NumericControl();
        entryFeeLabel = new LabelControl();
        label1 = new Label();
        label2 = new Label();
        ((System.ComponentModel.ISupportInitialize)cashRatioValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)finalsRatioValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)squadErrorProvider).BeginInit();
        ((System.ComponentModel.ISupportInitialize)maxPerPairValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)startingLaneValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numberOfLanesValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)entryFeeValue).BeginInit();
        SuspendLayout();
        // 
        // cashRatioValue
        // 
        cashRatioValue.DecimalPlaces = 1;
        cashRatioValue.Font = new Font("Segoe UI", 11F);
        cashRatioValue.Location = new Point(191, 94);
        cashRatioValue.Margin = new Padding(3, 4, 3, 10);
        cashRatioValue.Maximum = new decimal(new int[] { 999, 0, 0, 65536 });
        cashRatioValue.Name = "cashRatioValue";
        cashRatioValue.Size = new Size(131, 27);
        cashRatioValue.TabIndex = 3;
        cashRatioValue.TextAlign = HorizontalAlignment.Right;
        cashRatioValue.Validating += CashRatioValue_Validating;
        cashRatioValue.Validated += Controls_Validated;
        // 
        // cashRatioLabel
        // 
        cashRatioLabel.AutoSize = true;
        cashRatioLabel.Location = new Point(191, 68);
        cashRatioLabel.Name = "cashRatioLabel";
        cashRatioLabel.Required = false;
        cashRatioLabel.Size = new Size(117, 19);
        cashRatioLabel.TabIndex = 111;
        cashRatioLabel.TabStop = false;
        cashRatioLabel.Text = "Cash Ratio:";
        // 
        // finalsRatioValue
        // 
        finalsRatioValue.DecimalPlaces = 1;
        finalsRatioValue.Font = new Font("Segoe UI", 11F);
        finalsRatioValue.Location = new Point(3, 94);
        finalsRatioValue.Margin = new Padding(3, 4, 3, 10);
        finalsRatioValue.Maximum = new decimal(new int[] { 999, 0, 0, 65536 });
        finalsRatioValue.Name = "finalsRatioValue";
        finalsRatioValue.Size = new Size(131, 27);
        finalsRatioValue.TabIndex = 2;
        finalsRatioValue.TextAlign = HorizontalAlignment.Right;
        finalsRatioValue.Validating += FinalsRatioValue_Validating;
        finalsRatioValue.Validated += Controls_Validated;
        // 
        // finalsRatioLabel
        // 
        finalsRatioLabel.AutoSize = true;
        finalsRatioLabel.Location = new Point(3, 68);
        finalsRatioLabel.Name = "finalsRatioLabel";
        finalsRatioLabel.Required = false;
        finalsRatioLabel.Size = new Size(126, 19);
        finalsRatioLabel.TabIndex = 110;
        finalsRatioLabel.TabStop = false;
        finalsRatioLabel.Text = "Finals Ratio:";
        // 
        // squadErrorProvider
        // 
        squadErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        squadErrorProvider.ContainerControl = this;
        // 
        // datePicker
        // 
        datePicker.CustomFormat = "MM/dd/yyyy    hh:mm tt";
        datePicker.Font = new Font("Segoe UI", 11F);
        datePicker.Format = DateTimePickerFormat.Custom;
        datePicker.Location = new Point(3, 28);
        datePicker.Margin = new Padding(3, 3, 3, 10);
        datePicker.MaxDate = new DateTime(2200, 12, 31, 0, 0, 0, 0);
        datePicker.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
        datePicker.Name = "datePicker";
        datePicker.Size = new Size(217, 27);
        datePicker.TabIndex = 0;
        datePicker.Validating += DatePicker_Validating;
        datePicker.Validated += Controls_Validated;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Location = new Point(3, 3);
        dateLabel.Name = "dateLabel";
        dateLabel.Required = true;
        dateLabel.Size = new Size(60, 19);
        dateLabel.TabIndex = 113;
        dateLabel.TabStop = false;
        dateLabel.Text = "Date:";
        // 
        // maxPerPairValue
        // 
        maxPerPairValue.Font = new Font("Segoe UI", 11F);
        maxPerPairValue.Location = new Point(355, 160);
        maxPerPairValue.Margin = new Padding(3, 4, 3, 10);
        maxPerPairValue.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        maxPerPairValue.Name = "maxPerPairValue";
        maxPerPairValue.Size = new Size(131, 27);
        maxPerPairValue.TabIndex = 6;
        maxPerPairValue.TextAlign = HorizontalAlignment.Right;
        maxPerPairValue.Validating += MaxPerPairValue_Validating;
        maxPerPairValue.Validated += Controls_Validated;
        // 
        // maxPerPairLabel
        // 
        maxPerPairLabel.AutoSize = true;
        maxPerPairLabel.Location = new Point(355, 134);
        maxPerPairLabel.Name = "maxPerPairLabel";
        maxPerPairLabel.Required = true;
        maxPerPairLabel.Size = new Size(132, 19);
        maxPerPairLabel.TabIndex = 115;
        maxPerPairLabel.TabStop = false;
        maxPerPairLabel.Text = "Max Per Pair:";
        // 
        // startingLaneValue
        // 
        startingLaneValue.Font = new Font("Segoe UI", 11F);
        startingLaneValue.Location = new Point(3, 160);
        startingLaneValue.Margin = new Padding(3, 4, 3, 10);
        startingLaneValue.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        startingLaneValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        startingLaneValue.Name = "startingLaneValue";
        startingLaneValue.Size = new Size(131, 27);
        startingLaneValue.TabIndex = 4;
        startingLaneValue.TextAlign = HorizontalAlignment.Right;
        startingLaneValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // startingLaneLabel
        // 
        startingLaneLabel.AutoSize = true;
        startingLaneLabel.Location = new Point(3, 134);
        startingLaneLabel.Name = "startingLaneLabel";
        startingLaneLabel.Required = true;
        startingLaneLabel.Size = new Size(141, 19);
        startingLaneLabel.TabIndex = 117;
        startingLaneLabel.TabStop = false;
        startingLaneLabel.Text = "Starting Lane:";
        // 
        // numberOfLanesValue
        // 
        numberOfLanesValue.Font = new Font("Segoe UI", 11F);
        numberOfLanesValue.Location = new Point(189, 160);
        numberOfLanesValue.Margin = new Padding(3, 4, 3, 10);
        numberOfLanesValue.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        numberOfLanesValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numberOfLanesValue.Name = "numberOfLanesValue";
        numberOfLanesValue.Size = new Size(131, 27);
        numberOfLanesValue.TabIndex = 5;
        numberOfLanesValue.TextAlign = HorizontalAlignment.Right;
        numberOfLanesValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // numberOfLanesLabel
        // 
        numberOfLanesLabel.AutoSize = true;
        numberOfLanesLabel.Location = new Point(167, 134);
        numberOfLanesLabel.Name = "numberOfLanesLabel";
        numberOfLanesLabel.Required = true;
        numberOfLanesLabel.Size = new Size(159, 19);
        numberOfLanesLabel.TabIndex = 119;
        numberOfLanesLabel.TabStop = false;
        numberOfLanesLabel.Text = "Number of Lanes:";
        // 
        // entryFeeValue
        // 
        entryFeeValue.DecimalPlaces = 2;
        entryFeeValue.Font = new Font("Segoe UI", 11F);
        entryFeeValue.Location = new Point(355, 31);
        entryFeeValue.Margin = new Padding(3, 4, 3, 10);
        entryFeeValue.Maximum = new decimal(new int[] { 99999, 0, 0, 131072 });
        entryFeeValue.Name = "entryFeeValue";
        entryFeeValue.Size = new Size(131, 27);
        entryFeeValue.TabIndex = 1;
        entryFeeValue.TextAlign = HorizontalAlignment.Right;
        // 
        // entryFeeLabel
        // 
        entryFeeLabel.AutoSize = true;
        entryFeeLabel.Location = new Point(355, 5);
        entryFeeLabel.Name = "entryFeeLabel";
        entryFeeLabel.Required = false;
        entryFeeLabel.Size = new Size(117, 19);
        entryFeeLabel.TabIndex = 121;
        entryFeeLabel.TabStop = false;
        entryFeeLabel.Text = "Entry Fee:";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(140, 95);
        label1.Name = "label1";
        label1.Size = new Size(26, 21);
        label1.TabIndex = 122;
        label1.Text = ": 1";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 12F);
        label2.Location = new Point(328, 95);
        label2.Name = "label2";
        label2.Size = new Size(26, 21);
        label2.TabIndex = 123;
        label2.Text = ": 1";
        // 
        // SquadControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(entryFeeValue);
        Controls.Add(entryFeeLabel);
        Controls.Add(numberOfLanesValue);
        Controls.Add(numberOfLanesLabel);
        Controls.Add(startingLaneValue);
        Controls.Add(startingLaneLabel);
        Controls.Add(maxPerPairValue);
        Controls.Add(maxPerPairLabel);
        Controls.Add(datePicker);
        Controls.Add(dateLabel);
        Controls.Add(cashRatioValue);
        Controls.Add(cashRatioLabel);
        Controls.Add(finalsRatioValue);
        Controls.Add(finalsRatioLabel);
        Name = "SquadControl";
        Size = new Size(527, 215);
        Validating += SquadControl_Validating;
        ((System.ComponentModel.ISupportInitialize)cashRatioValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)finalsRatioValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)squadErrorProvider).EndInit();
        ((System.ComponentModel.ISupportInitialize)maxPerPairValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)startingLaneValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)numberOfLanesValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)entryFeeValue).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.NumericControl cashRatioValue;
    private Controls.LabelControl cashRatioLabel;
    private Controls.NumericControl finalsRatioValue;
    private Controls.LabelControl finalsRatioLabel;
    private ErrorProvider squadErrorProvider;
    private DateTimePicker datePicker;
    private Controls.LabelControl dateLabel;
    private Controls.NumericControl maxPerPairValue;
    private Controls.LabelControl maxPerPairLabel;
    private Controls.NumericControl numberOfLanesValue;
    private Controls.LabelControl numberOfLanesLabel;
    private Controls.NumericControl startingLaneValue;
    private Controls.LabelControl startingLaneLabel;
    private NumericControl entryFeeValue;
    private LabelControl entryFeeLabel;
    private Label label2;
    private Label label1;
}
