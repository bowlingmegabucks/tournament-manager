namespace BowlingMegabucks.TournamentManager.Controls;

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
        components = new System.ComponentModel.Container();
        nameLabel = new LabelControl();
        tournamentErrorProvider = new ErrorProvider(components);
        nameText = new TextBox();
        startDateLabel = new LabelControl();
        startDatePicker = new DateTimePicker();
        endDatePicker = new DateTimePicker();
        endDateLabel = new LabelControl();
        entryFeeLabel = new LabelControl();
        entryFeeValue = new NumericControl();
        gamesLabel = new LabelControl();
        gamesValue = new NumericControl();
        finalsRatioLabel = new LabelControl();
        finalsRatioValue = new NumericControl();
        cashRatioValue = new NumericControl();
        cashRatioLabel = new LabelControl();
        bowlingCenterValue = new TextBox();
        bowlingCenterLabel = new LabelControl();
        CheckboxComplete = new CheckBox();
        superSweeperCashRatioValue = new NumericControl();
        labelSuperSweeperCashRatio = new LabelControl();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        ((System.ComponentModel.ISupportInitialize)tournamentErrorProvider).BeginInit();
        ((System.ComponentModel.ISupportInitialize)entryFeeValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gamesValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)finalsRatioValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)cashRatioValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)superSweeperCashRatioValue).BeginInit();
        SuspendLayout();
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(3, 3);
        nameLabel.Name = "nameLabel";
        nameLabel.Required = true;
        nameLabel.Size = new Size(60, 19);
        nameLabel.TabIndex = 101;
        nameLabel.TabStop = false;
        nameLabel.Text = "Name:";
        // 
        // tournamentErrorProvider
        // 
        tournamentErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        tournamentErrorProvider.ContainerControl = this;
        // 
        // nameText
        // 
        nameText.Font = new Font("Segoe UI", 11F);
        nameText.Location = new Point(3, 28);
        nameText.Margin = new Padding(3, 3, 3, 10);
        nameText.Name = "nameText";
        nameText.PlaceholderText = "Tournament Name";
        nameText.Size = new Size(317, 27);
        nameText.TabIndex = 0;
        nameText.Validating += TournamentText_Validating;
        nameText.Validated += Controls_Validated;
        // 
        // startDateLabel
        // 
        startDateLabel.AutoSize = true;
        startDateLabel.Location = new Point(3, 68);
        startDateLabel.Name = "startDateLabel";
        startDateLabel.Required = true;
        startDateLabel.Size = new Size(114, 19);
        startDateLabel.TabIndex = 102;
        startDateLabel.TabStop = false;
        startDateLabel.Text = "Start Date:";
        // 
        // startDatePicker
        // 
        startDatePicker.Font = new Font("Segoe UI", 11F);
        startDatePicker.Format = DateTimePickerFormat.Short;
        startDatePicker.Location = new Point(3, 93);
        startDatePicker.Margin = new Padding(3, 3, 3, 10);
        startDatePicker.MaxDate = new DateTime(2200, 12, 31, 0, 0, 0, 0);
        startDatePicker.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
        startDatePicker.Name = "startDatePicker";
        startDatePicker.Size = new Size(131, 27);
        startDatePicker.TabIndex = 1;
        startDatePicker.Validating += TournamentDates_Validating;
        startDatePicker.Validated += Controls_Validated;
        // 
        // endDatePicker
        // 
        endDatePicker.Font = new Font("Segoe UI", 11F);
        endDatePicker.Format = DateTimePickerFormat.Short;
        endDatePicker.Location = new Point(179, 93);
        endDatePicker.Margin = new Padding(3, 3, 3, 10);
        endDatePicker.MaxDate = new DateTime(2200, 12, 31, 0, 0, 0, 0);
        endDatePicker.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
        endDatePicker.Name = "endDatePicker";
        endDatePicker.Size = new Size(131, 27);
        endDatePicker.TabIndex = 2;
        endDatePicker.Validating += TournamentDates_Validating;
        endDatePicker.Validated += Controls_Validated;
        // 
        // endDateLabel
        // 
        endDateLabel.AutoSize = true;
        endDateLabel.Location = new Point(179, 68);
        endDateLabel.Name = "endDateLabel";
        endDateLabel.Required = true;
        endDateLabel.Size = new Size(96, 19);
        endDateLabel.TabIndex = 103;
        endDateLabel.TabStop = false;
        endDateLabel.Text = "End Date:";
        // 
        // entryFeeLabel
        // 
        entryFeeLabel.AutoSize = true;
        entryFeeLabel.Location = new Point(3, 133);
        entryFeeLabel.Name = "entryFeeLabel";
        entryFeeLabel.Required = true;
        entryFeeLabel.Size = new Size(105, 19);
        entryFeeLabel.TabIndex = 104;
        entryFeeLabel.TabStop = false;
        entryFeeLabel.Text = "Entry Fee:";
        // 
        // entryFeeValue
        // 
        entryFeeValue.DecimalPlaces = 2;
        entryFeeValue.Font = new Font("Segoe UI", 11F);
        entryFeeValue.Location = new Point(3, 159);
        entryFeeValue.Margin = new Padding(3, 4, 3, 10);
        entryFeeValue.Maximum = new decimal(new int[] { 99999, 0, 0, 131072 });
        entryFeeValue.Name = "entryFeeValue";
        entryFeeValue.Size = new Size(131, 27);
        entryFeeValue.TabIndex = 3;
        entryFeeValue.TextAlign = HorizontalAlignment.Right;
        entryFeeValue.Validating += EntryFeeValue_Validating;
        entryFeeValue.Validated += Controls_Validated;
        // 
        // gamesLabel
        // 
        gamesLabel.AutoSize = true;
        gamesLabel.Location = new Point(179, 133);
        gamesLabel.Name = "gamesLabel";
        gamesLabel.Required = true;
        gamesLabel.Size = new Size(69, 19);
        gamesLabel.TabIndex = 105;
        gamesLabel.TabStop = false;
        gamesLabel.Text = "Games:";
        // 
        // gamesValue
        // 
        gamesValue.Font = new Font("Segoe UI", 11F);
        gamesValue.Location = new Point(179, 159);
        gamesValue.Margin = new Padding(3, 4, 3, 10);
        gamesValue.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        gamesValue.Name = "gamesValue";
        gamesValue.Size = new Size(131, 27);
        gamesValue.TabIndex = 4;
        gamesValue.TextAlign = HorizontalAlignment.Right;
        gamesValue.Validating += GamesValue_Validating;
        gamesValue.Validated += Controls_Validated;
        // 
        // finalsRatioLabel
        // 
        finalsRatioLabel.AutoSize = true;
        finalsRatioLabel.Location = new Point(3, 199);
        finalsRatioLabel.Name = "finalsRatioLabel";
        finalsRatioLabel.Required = true;
        finalsRatioLabel.Size = new Size(132, 19);
        finalsRatioLabel.TabIndex = 106;
        finalsRatioLabel.TabStop = false;
        finalsRatioLabel.Text = "Finals Ratio:";
        // 
        // finalsRatioValue
        // 
        finalsRatioValue.DecimalPlaces = 1;
        finalsRatioValue.Font = new Font("Segoe UI", 11F);
        finalsRatioValue.Location = new Point(6, 225);
        finalsRatioValue.Margin = new Padding(3, 4, 3, 10);
        finalsRatioValue.Maximum = new decimal(new int[] { 999, 0, 0, 65536 });
        finalsRatioValue.Name = "finalsRatioValue";
        finalsRatioValue.Size = new Size(123, 27);
        finalsRatioValue.TabIndex = 5;
        finalsRatioValue.TextAlign = HorizontalAlignment.Right;
        finalsRatioValue.Validating += FinalsRatioValue_Validating;
        finalsRatioValue.Validated += Controls_Validated;
        // 
        // cashRatioValue
        // 
        cashRatioValue.DecimalPlaces = 1;
        cashRatioValue.Font = new Font("Segoe UI", 11F);
        cashRatioValue.Location = new Point(179, 225);
        cashRatioValue.Margin = new Padding(3, 4, 3, 10);
        cashRatioValue.Maximum = new decimal(new int[] { 999, 0, 0, 65536 });
        cashRatioValue.Name = "cashRatioValue";
        cashRatioValue.Size = new Size(114, 27);
        cashRatioValue.TabIndex = 6;
        cashRatioValue.TextAlign = HorizontalAlignment.Right;
        cashRatioValue.Validating += CashRatioValue_Validating;
        cashRatioValue.Validated += Controls_Validated;
        // 
        // cashRatioLabel
        // 
        cashRatioLabel.AutoSize = true;
        cashRatioLabel.Location = new Point(179, 199);
        cashRatioLabel.Name = "cashRatioLabel";
        cashRatioLabel.Required = true;
        cashRatioLabel.Size = new Size(114, 19);
        cashRatioLabel.TabIndex = 107;
        cashRatioLabel.TabStop = false;
        cashRatioLabel.Text = "Cash Ratio:";
        // 
        // bowlingCenterValue
        // 
        bowlingCenterValue.Font = new Font("Segoe UI", 11F);
        bowlingCenterValue.Location = new Point(6, 356);
        bowlingCenterValue.Margin = new Padding(3, 3, 3, 10);
        bowlingCenterValue.Name = "bowlingCenterValue";
        bowlingCenterValue.PlaceholderText = "AMF Lanes";
        bowlingCenterValue.Size = new Size(317, 27);
        bowlingCenterValue.TabIndex = 8;
        bowlingCenterValue.Validating += BowlingCenterText_Validating;
        bowlingCenterValue.Validated += Controls_Validated;
        // 
        // bowlingCenterLabel
        // 
        bowlingCenterLabel.AutoSize = true;
        bowlingCenterLabel.Location = new Point(3, 331);
        bowlingCenterLabel.Name = "bowlingCenterLabel";
        bowlingCenterLabel.Required = true;
        bowlingCenterLabel.Size = new Size(150, 19);
        bowlingCenterLabel.TabIndex = 108;
        bowlingCenterLabel.TabStop = false;
        bowlingCenterLabel.Text = "Bowling Center:";
        // 
        // CheckboxComplete
        // 
        CheckboxComplete.AutoSize = true;
        CheckboxComplete.Location = new Point(242, 398);
        CheckboxComplete.Name = "CheckboxComplete";
        CheckboxComplete.Size = new Size(78, 19);
        CheckboxComplete.TabIndex = 109;
        CheckboxComplete.TabStop = false;
        CheckboxComplete.Text = "Complete";
        CheckboxComplete.UseVisualStyleBackColor = true;
        CheckboxComplete.Visible = false;
        // 
        // superSweeperCashRatioValue
        // 
        superSweeperCashRatioValue.DecimalPlaces = 1;
        superSweeperCashRatioValue.Font = new Font("Segoe UI", 11F);
        superSweeperCashRatioValue.Location = new Point(6, 291);
        superSweeperCashRatioValue.Margin = new Padding(3, 4, 3, 10);
        superSweeperCashRatioValue.Maximum = new decimal(new int[] { 999, 0, 0, 65536 });
        superSweeperCashRatioValue.Name = "superSweeperCashRatioValue";
        superSweeperCashRatioValue.Size = new Size(80, 27);
        superSweeperCashRatioValue.TabIndex = 7;
        superSweeperCashRatioValue.TextAlign = HorizontalAlignment.Right;
        superSweeperCashRatioValue.Validating += SuperSweeperCashRatioValue_Validating;
        superSweeperCashRatioValue.Validated += Controls_Validated;
        // 
        // labelSuperSweeperCashRatio
        // 
        labelSuperSweeperCashRatio.AutoSize = true;
        labelSuperSweeperCashRatio.Location = new Point(3, 265);
        labelSuperSweeperCashRatio.Name = "labelSuperSweeperCashRatio";
        labelSuperSweeperCashRatio.Required = true;
        labelSuperSweeperCashRatio.Size = new Size(240, 19);
        labelSuperSweeperCashRatio.TabIndex = 111;
        labelSuperSweeperCashRatio.TabStop = false;
        labelSuperSweeperCashRatio.Text = "Super Sweeper Cash Ratio:";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(92, 292);
        label1.Name = "label1";
        label1.Size = new Size(26, 21);
        label1.TabIndex = 112;
        label1.Text = ": 1";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 12F);
        label2.Location = new Point(135, 226);
        label2.Name = "label2";
        label2.Size = new Size(26, 21);
        label2.TabIndex = 113;
        label2.Text = ": 1";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI", 12F);
        label3.Location = new Point(299, 226);
        label3.Name = "label3";
        label3.Size = new Size(26, 21);
        label3.TabIndex = 114;
        label3.Text = ": 1";
        // 
        // TournamentControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(superSweeperCashRatioValue);
        Controls.Add(labelSuperSweeperCashRatio);
        Controls.Add(CheckboxComplete);
        Controls.Add(bowlingCenterValue);
        Controls.Add(bowlingCenterLabel);
        Controls.Add(cashRatioValue);
        Controls.Add(cashRatioLabel);
        Controls.Add(finalsRatioValue);
        Controls.Add(finalsRatioLabel);
        Controls.Add(gamesValue);
        Controls.Add(gamesLabel);
        Controls.Add(entryFeeValue);
        Controls.Add(entryFeeLabel);
        Controls.Add(endDatePicker);
        Controls.Add(endDateLabel);
        Controls.Add(startDatePicker);
        Controls.Add(startDateLabel);
        Controls.Add(nameText);
        Controls.Add(nameLabel);
        Name = "TournamentControl";
        Size = new Size(346, 395);
        Validating += TournamentControl_Validating;
        ((System.ComponentModel.ISupportInitialize)tournamentErrorProvider).EndInit();
        ((System.ComponentModel.ISupportInitialize)entryFeeValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)gamesValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)finalsRatioValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)cashRatioValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)superSweeperCashRatioValue).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.LabelControl nameLabel;
    private ErrorProvider tournamentErrorProvider;
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
    private NumericControl superSweeperCashRatioValue;
    private LabelControl labelSuperSweeperCashRatio;
    private Label label3;
    private Label label2;
    private Label label1;
}
