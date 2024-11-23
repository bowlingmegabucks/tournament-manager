namespace NortheastMegabuck.Controls;

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
        components = new System.ComponentModel.Container();
        divisionsGroupBox = new GroupBox();
        PanelDivisions = new Panel();
        sweeperDivisions = new SweeperDivisionsControl();
        squadDatePicker = new DateTimePicker();
        dateLabel = new LabelControl();
        sweeperErrorProvider = new ErrorProvider(components);
        entryFeeValue = new NumericControl();
        entryFeeLabel = new LabelControl();
        gamesValue = new NumericControl();
        gamesLabel = new LabelControl();
        cashRatioValue = new NumericControl();
        cashRatioLabel = new LabelControl();
        maxPerPairValue = new NumericControl();
        maxPerPairLabel = new LabelControl();
        numberOfLanesValue = new NumericControl();
        numberOfLanesLabel = new LabelControl();
        startingLaneValue = new NumericControl();
        startingLaneLabel = new LabelControl();
        cashTo1Label = new LabelControl();
        divisionsGroupBox.SuspendLayout();
        PanelDivisions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)sweeperErrorProvider).BeginInit();
        ((System.ComponentModel.ISupportInitialize)entryFeeValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gamesValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)cashRatioValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)maxPerPairValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numberOfLanesValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)startingLaneValue).BeginInit();
        SuspendLayout();
        // 
        // divisionsGroupBox
        // 
        divisionsGroupBox.AutoSize = true;
        divisionsGroupBox.Controls.Add(PanelDivisions);
        divisionsGroupBox.Location = new Point(3, 199);
        divisionsGroupBox.Name = "divisionsGroupBox";
        divisionsGroupBox.Size = new Size(540, 314);
        divisionsGroupBox.TabIndex = 7;
        divisionsGroupBox.TabStop = false;
        divisionsGroupBox.Text = "Division Bonus Pins";
        // 
        // PanelDivisions
        // 
        PanelDivisions.AutoScroll = true;
        PanelDivisions.AutoSize = true;
        PanelDivisions.Controls.Add(sweeperDivisions);
        PanelDivisions.Dock = DockStyle.Fill;
        PanelDivisions.Location = new Point(3, 19);
        PanelDivisions.Name = "PanelDivisions";
        PanelDivisions.Size = new Size(534, 292);
        PanelDivisions.TabIndex = 0;
        // 
        // sweeperDivisions
        // 
        sweeperDivisions.Dock = DockStyle.Fill;
        sweeperDivisions.Location = new Point(0, 0);
        sweeperDivisions.Name = "sweeperDivisions";
        sweeperDivisions.Size = new Size(534, 292);
        sweeperDivisions.TabIndex = 1;
        // 
        // squadDatePicker
        // 
        squadDatePicker.CustomFormat = "MM/dd/yyyy    hh:mm tt";
        squadDatePicker.Font = new Font("Segoe UI", 11F);
        squadDatePicker.Format = DateTimePickerFormat.Custom;
        squadDatePicker.Location = new Point(3, 28);
        squadDatePicker.Margin = new Padding(3, 3, 3, 10);
        squadDatePicker.MaxDate = new DateTime(2200, 12, 31, 0, 0, 0, 0);
        squadDatePicker.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
        squadDatePicker.Name = "squadDatePicker";
        squadDatePicker.Size = new Size(217, 27);
        squadDatePicker.TabIndex = 0;
        squadDatePicker.Validating += SquadDatePicker_Validating;
        squadDatePicker.Validated += SweeperControl_Validated;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Location = new Point(3, 3);
        dateLabel.Name = "dateLabel";
        dateLabel.Required = true;
        dateLabel.Size = new Size(60, 19);
        dateLabel.TabIndex = 115;
        dateLabel.TabStop = false;
        dateLabel.Text = "Date:";
        // 
        // sweeperErrorProvider
        // 
        sweeperErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        sweeperErrorProvider.ContainerControl = this;
        // 
        // entryFeeValue
        // 
        entryFeeValue.DecimalPlaces = 2;
        entryFeeValue.Font = new Font("Segoe UI", 11F);
        entryFeeValue.Location = new Point(350, 31);
        entryFeeValue.Margin = new Padding(3, 4, 3, 10);
        entryFeeValue.Maximum = new decimal(new int[] { 99999, 0, 0, 131072 });
        entryFeeValue.Name = "entryFeeValue";
        entryFeeValue.Size = new Size(131, 27);
        entryFeeValue.TabIndex = 1;
        entryFeeValue.TextAlign = HorizontalAlignment.Right;
        entryFeeValue.Validating += EntryFeeValue_Validating;
        entryFeeValue.Validated += SweeperControl_Validated;
        // 
        // entryFeeLabel
        // 
        entryFeeLabel.AutoSize = true;
        entryFeeLabel.Location = new Point(350, 5);
        entryFeeLabel.Name = "entryFeeLabel";
        entryFeeLabel.Required = true;
        entryFeeLabel.Size = new Size(105, 19);
        entryFeeLabel.TabIndex = 117;
        entryFeeLabel.TabStop = false;
        entryFeeLabel.Text = "Entry Fee:";
        // 
        // gamesValue
        // 
        gamesValue.Font = new Font("Segoe UI", 11F);
        gamesValue.Location = new Point(3, 94);
        gamesValue.Margin = new Padding(3, 4, 3, 10);
        gamesValue.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        gamesValue.Name = "gamesValue";
        gamesValue.Size = new Size(131, 27);
        gamesValue.TabIndex = 2;
        gamesValue.TextAlign = HorizontalAlignment.Right;
        gamesValue.Validating += GamesValue_Validating;
        gamesValue.Validated += SweeperControl_Validated;
        // 
        // gamesLabel
        // 
        gamesLabel.AutoSize = true;
        gamesLabel.Location = new Point(3, 68);
        gamesLabel.Name = "gamesLabel";
        gamesLabel.Required = true;
        gamesLabel.Size = new Size(69, 19);
        gamesLabel.TabIndex = 119;
        gamesLabel.TabStop = false;
        gamesLabel.Text = "Games:";
        // 
        // cashRatioValue
        // 
        cashRatioValue.DecimalPlaces = 1;
        cashRatioValue.Font = new Font("Segoe UI", 11F);
        cashRatioValue.Location = new Point(189, 94);
        cashRatioValue.Margin = new Padding(3, 4, 3, 10);
        cashRatioValue.Maximum = new decimal(new int[] { 999, 0, 0, 65536 });
        cashRatioValue.Name = "cashRatioValue";
        cashRatioValue.Size = new Size(131, 27);
        cashRatioValue.TabIndex = 3;
        cashRatioValue.TextAlign = HorizontalAlignment.Right;
        cashRatioValue.Validating += CashRatioValue_Validating;
        cashRatioValue.Validated += SweeperControl_Validated;
        // 
        // cashRatioLabel
        // 
        cashRatioLabel.AutoSize = true;
        cashRatioLabel.Location = new Point(189, 68);
        cashRatioLabel.Name = "cashRatioLabel";
        cashRatioLabel.Required = true;
        cashRatioLabel.Size = new Size(114, 19);
        cashRatioLabel.TabIndex = 121;
        cashRatioLabel.TabStop = false;
        cashRatioLabel.Text = "Cash Ratio:";
        // 
        // maxPerPairValue
        // 
        maxPerPairValue.Font = new Font("Segoe UI", 11F);
        maxPerPairValue.Location = new Point(350, 159);
        maxPerPairValue.Margin = new Padding(3, 4, 3, 10);
        maxPerPairValue.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        maxPerPairValue.Name = "maxPerPairValue";
        maxPerPairValue.Size = new Size(131, 27);
        maxPerPairValue.TabIndex = 6;
        maxPerPairValue.TextAlign = HorizontalAlignment.Right;
        maxPerPairValue.Validating += MaxPerPairValue_Validating;
        maxPerPairValue.Validated += SweeperControl_Validated;
        // 
        // maxPerPairLabel
        // 
        maxPerPairLabel.AutoSize = true;
        maxPerPairLabel.Location = new Point(350, 133);
        maxPerPairLabel.Name = "maxPerPairLabel";
        maxPerPairLabel.Required = true;
        maxPerPairLabel.Size = new Size(132, 19);
        maxPerPairLabel.TabIndex = 123;
        maxPerPairLabel.TabStop = false;
        maxPerPairLabel.Text = "Max Per Pair:";
        // 
        // numberOfLanesValue
        // 
        numberOfLanesValue.Font = new Font("Segoe UI", 11F);
        numberOfLanesValue.Location = new Point(189, 159);
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
        numberOfLanesLabel.Location = new Point(167, 133);
        numberOfLanesLabel.Name = "numberOfLanesLabel";
        numberOfLanesLabel.Required = true;
        numberOfLanesLabel.Size = new Size(159, 19);
        numberOfLanesLabel.TabIndex = 1003;
        numberOfLanesLabel.TabStop = false;
        numberOfLanesLabel.Text = "Number of Lanes:";
        // 
        // startingLaneValue
        // 
        startingLaneValue.Font = new Font("Segoe UI", 11F);
        startingLaneValue.Location = new Point(3, 159);
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
        startingLaneLabel.Location = new Point(3, 133);
        startingLaneLabel.Name = "startingLaneLabel";
        startingLaneLabel.Required = true;
        startingLaneLabel.Size = new Size(141, 19);
        startingLaneLabel.TabIndex = 1002;
        startingLaneLabel.TabStop = false;
        startingLaneLabel.Text = "Starting Lane:";
        // 
        // cashTo1Label
        // 
        cashTo1Label.AutoSize = true;
        cashTo1Label.Location = new Point(326, 96);
        cashTo1Label.Name = "cashTo1Label";
        cashTo1Label.Required = false;
        cashTo1Label.Size = new Size(45, 19);
        cashTo1Label.TabIndex = 1005;
        cashTo1Label.TabStop = false;
        cashTo1Label.Text = ": 1";
        // 
        // SweeperControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        Controls.Add(cashTo1Label);
        Controls.Add(numberOfLanesValue);
        Controls.Add(numberOfLanesLabel);
        Controls.Add(startingLaneValue);
        Controls.Add(startingLaneLabel);
        Controls.Add(maxPerPairValue);
        Controls.Add(maxPerPairLabel);
        Controls.Add(cashRatioValue);
        Controls.Add(cashRatioLabel);
        Controls.Add(gamesValue);
        Controls.Add(gamesLabel);
        Controls.Add(entryFeeValue);
        Controls.Add(entryFeeLabel);
        Controls.Add(squadDatePicker);
        Controls.Add(dateLabel);
        Controls.Add(divisionsGroupBox);
        Name = "SweeperControl";
        Size = new Size(558, 528);
        divisionsGroupBox.ResumeLayout(false);
        divisionsGroupBox.PerformLayout();
        PanelDivisions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)sweeperErrorProvider).EndInit();
        ((System.ComponentModel.ISupportInitialize)entryFeeValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)gamesValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)cashRatioValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)maxPerPairValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)numberOfLanesValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)startingLaneValue).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox divisionsGroupBox;
    private Panel PanelDivisions;
    private SweeperDivisionsControl sweeperDivisions;
    private DateTimePicker squadDatePicker;
    private Controls.LabelControl dateLabel;
    private ErrorProvider sweeperErrorProvider;
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
    private LabelControl cashTo1Label;
}
