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
            this.GroupboxDivisions = new System.Windows.Forms.GroupBox();
            this.PanelDivisions = new System.Windows.Forms.Panel();
            this.SweeperDivisions = new NewEnglandClassic.Contols.SweeperDivisionsControl();
            this.DatePickerSweeperDate = new System.Windows.Forms.DateTimePicker();
            this.LabelDate = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderSweeper = new System.Windows.Forms.ErrorProvider(this.components);
            this.NumericEntryFee = new NewEnglandClassic.Controls.NumericControl();
            this.LabelEntryFee = new NewEnglandClassic.Controls.LabelControl();
            this.NumericGames = new NewEnglandClassic.Controls.NumericControl();
            this.LabelGames = new NewEnglandClassic.Controls.LabelControl();
            this.NumericCashRatio = new NewEnglandClassic.Controls.NumericControl();
            this.LabelCashRatio = new NewEnglandClassic.Controls.LabelControl();
            this.NumericMaxPerPair = new NewEnglandClassic.Controls.NumericControl();
            this.LabelMaxPerPair = new NewEnglandClassic.Controls.LabelControl();
            this.NumericNumberOfLanes = new NewEnglandClassic.Controls.NumericControl();
            this.LabelNumberOfLanes = new NewEnglandClassic.Controls.LabelControl();
            this.NumericStartingLane = new NewEnglandClassic.Controls.NumericControl();
            this.LabelStartingLane = new NewEnglandClassic.Controls.LabelControl();
            this.GroupboxDivisions.SuspendLayout();
            this.PanelDivisions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSweeper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEntryFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericGames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCashRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaxPerPair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericNumberOfLanes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartingLane)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupboxDivisions
            // 
            this.GroupboxDivisions.AutoSize = true;
            this.GroupboxDivisions.Controls.Add(this.PanelDivisions);
            this.GroupboxDivisions.Location = new System.Drawing.Point(3, 199);
            this.GroupboxDivisions.Name = "GroupboxDivisions";
            this.GroupboxDivisions.Size = new System.Drawing.Size(540, 314);
            this.GroupboxDivisions.TabIndex = 7;
            this.GroupboxDivisions.TabStop = false;
            this.GroupboxDivisions.Text = "Division Bonus Pins";
            // 
            // PanelDivisions
            // 
            this.PanelDivisions.AutoScroll = true;
            this.PanelDivisions.AutoSize = true;
            this.PanelDivisions.Controls.Add(this.SweeperDivisions);
            this.PanelDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDivisions.Location = new System.Drawing.Point(3, 19);
            this.PanelDivisions.Name = "PanelDivisions";
            this.PanelDivisions.Size = new System.Drawing.Size(534, 292);
            this.PanelDivisions.TabIndex = 0;
            // 
            // SweeperDivisions
            // 
            this.SweeperDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SweeperDivisions.Location = new System.Drawing.Point(0, 0);
            this.SweeperDivisions.Name = "SweeperDivisions";
            this.SweeperDivisions.Size = new System.Drawing.Size(534, 292);
            this.SweeperDivisions.TabIndex = 1;
            // 
            // DatePickerSweeperDate
            // 
            this.DatePickerSweeperDate.CustomFormat = "MM/dd/yyyy    hh:mm tt";
            this.DatePickerSweeperDate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DatePickerSweeperDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePickerSweeperDate.Location = new System.Drawing.Point(3, 28);
            this.DatePickerSweeperDate.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.DatePickerSweeperDate.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.DatePickerSweeperDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePickerSweeperDate.Name = "DatePickerSweeperDate";
            this.DatePickerSweeperDate.Size = new System.Drawing.Size(217, 27);
            this.DatePickerSweeperDate.TabIndex = 0;
            this.DatePickerSweeperDate.Validating += new System.ComponentModel.CancelEventHandler(this.DatePickerSquadDate_Validating);
            this.DatePickerSweeperDate.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // LabelDate
            // 
            this.LabelDate.AutoSize = true;
            this.LabelDate.Bold = false;
            this.LabelDate.Location = new System.Drawing.Point(3, 3);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Required = true;
            this.LabelDate.Size = new System.Drawing.Size(60, 19);
            this.LabelDate.TabIndex = 115;
            this.LabelDate.TabStop = false;
            this.LabelDate.Text = "Date:";
            // 
            // ErrorProviderSweeper
            // 
            this.ErrorProviderSweeper.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderSweeper.ContainerControl = this;
            // 
            // NumericEntryFee
            // 
            this.NumericEntryFee.DecimalPlaces = 2;
            this.NumericEntryFee.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericEntryFee.Location = new System.Drawing.Point(350, 31);
            this.NumericEntryFee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericEntryFee.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.NumericEntryFee.Name = "NumericEntryFee";
            this.NumericEntryFee.Size = new System.Drawing.Size(131, 27);
            this.NumericEntryFee.TabIndex = 1;
            this.NumericEntryFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericEntryFee.Validating += new System.ComponentModel.CancelEventHandler(this.NumericEntryFee_Validating);
            this.NumericEntryFee.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // LabelEntryFee
            // 
            this.LabelEntryFee.AutoSize = true;
            this.LabelEntryFee.Bold = false;
            this.LabelEntryFee.Location = new System.Drawing.Point(350, 5);
            this.LabelEntryFee.Name = "LabelEntryFee";
            this.LabelEntryFee.Required = true;
            this.LabelEntryFee.Size = new System.Drawing.Size(105, 19);
            this.LabelEntryFee.TabIndex = 117;
            this.LabelEntryFee.TabStop = false;
            this.LabelEntryFee.Text = "Entry Fee:";
            // 
            // NumericGames
            // 
            this.NumericGames.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericGames.Location = new System.Drawing.Point(3, 94);
            this.NumericGames.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericGames.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericGames.Name = "NumericGames";
            this.NumericGames.Size = new System.Drawing.Size(131, 27);
            this.NumericGames.TabIndex = 2;
            this.NumericGames.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericGames.Validating += new System.ComponentModel.CancelEventHandler(this.NumericGames_Validating);
            this.NumericGames.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // LabelGames
            // 
            this.LabelGames.AutoSize = true;
            this.LabelGames.Bold = false;
            this.LabelGames.Location = new System.Drawing.Point(3, 68);
            this.LabelGames.Name = "LabelGames";
            this.LabelGames.Required = true;
            this.LabelGames.Size = new System.Drawing.Size(69, 19);
            this.LabelGames.TabIndex = 119;
            this.LabelGames.TabStop = false;
            this.LabelGames.Text = "Games:";
            // 
            // NumericCashRatio
            // 
            this.NumericCashRatio.DecimalPlaces = 1;
            this.NumericCashRatio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericCashRatio.Location = new System.Drawing.Point(189, 94);
            this.NumericCashRatio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericCashRatio.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.NumericCashRatio.Name = "NumericCashRatio";
            this.NumericCashRatio.Size = new System.Drawing.Size(131, 27);
            this.NumericCashRatio.TabIndex = 3;
            this.NumericCashRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericCashRatio.Validating += new System.ComponentModel.CancelEventHandler(this.NumericCashRatio_Validating);
            this.NumericCashRatio.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // LabelCashRatio
            // 
            this.LabelCashRatio.AutoSize = true;
            this.LabelCashRatio.Bold = false;
            this.LabelCashRatio.Location = new System.Drawing.Point(189, 68);
            this.LabelCashRatio.Name = "LabelCashRatio";
            this.LabelCashRatio.Required = true;
            this.LabelCashRatio.Size = new System.Drawing.Size(114, 19);
            this.LabelCashRatio.TabIndex = 121;
            this.LabelCashRatio.TabStop = false;
            this.LabelCashRatio.Text = "Cash Ratio:";
            // 
            // NumericMaxPerPair
            // 
            this.NumericMaxPerPair.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMaxPerPair.Location = new System.Drawing.Point(350, 159);
            this.NumericMaxPerPair.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericMaxPerPair.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericMaxPerPair.Name = "NumericMaxPerPair";
            this.NumericMaxPerPair.Size = new System.Drawing.Size(131, 27);
            this.NumericMaxPerPair.TabIndex = 6;
            this.NumericMaxPerPair.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMaxPerPair.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMaxPerPair_Validating);
            this.NumericMaxPerPair.Validated += new System.EventHandler(this.SweeperControl_Validated);
            // 
            // LabelMaxPerPair
            // 
            this.LabelMaxPerPair.AutoSize = true;
            this.LabelMaxPerPair.Bold = false;
            this.LabelMaxPerPair.Location = new System.Drawing.Point(350, 133);
            this.LabelMaxPerPair.Name = "LabelMaxPerPair";
            this.LabelMaxPerPair.Required = true;
            this.LabelMaxPerPair.Size = new System.Drawing.Size(132, 19);
            this.LabelMaxPerPair.TabIndex = 123;
            this.LabelMaxPerPair.TabStop = false;
            this.LabelMaxPerPair.Text = "Max Per Pair:";
            // 
            // NumericNumberOfLanes
            // 
            this.NumericNumberOfLanes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericNumberOfLanes.Location = new System.Drawing.Point(189, 159);
            this.NumericNumberOfLanes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericNumberOfLanes.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericNumberOfLanes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericNumberOfLanes.Name = "NumericNumberOfLanes";
            this.NumericNumberOfLanes.Size = new System.Drawing.Size(131, 27);
            this.NumericNumberOfLanes.TabIndex = 5;
            this.NumericNumberOfLanes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericNumberOfLanes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LabelNumberOfLanes
            // 
            this.LabelNumberOfLanes.AutoSize = true;
            this.LabelNumberOfLanes.Bold = false;
            this.LabelNumberOfLanes.Location = new System.Drawing.Point(167, 133);
            this.LabelNumberOfLanes.Name = "LabelNumberOfLanes";
            this.LabelNumberOfLanes.Required = true;
            this.LabelNumberOfLanes.Size = new System.Drawing.Size(159, 19);
            this.LabelNumberOfLanes.TabIndex = 1003;
            this.LabelNumberOfLanes.TabStop = false;
            this.LabelNumberOfLanes.Text = "Number of Lanes:";
            // 
            // NumericStartingLane
            // 
            this.NumericStartingLane.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericStartingLane.Location = new System.Drawing.Point(3, 159);
            this.NumericStartingLane.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericStartingLane.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericStartingLane.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericStartingLane.Name = "NumericStartingLane";
            this.NumericStartingLane.Size = new System.Drawing.Size(131, 27);
            this.NumericStartingLane.TabIndex = 4;
            this.NumericStartingLane.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericStartingLane.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LabelStartingLane
            // 
            this.LabelStartingLane.AutoSize = true;
            this.LabelStartingLane.Bold = false;
            this.LabelStartingLane.Location = new System.Drawing.Point(3, 133);
            this.LabelStartingLane.Name = "LabelStartingLane";
            this.LabelStartingLane.Required = true;
            this.LabelStartingLane.Size = new System.Drawing.Size(141, 19);
            this.LabelStartingLane.TabIndex = 1002;
            this.LabelStartingLane.TabStop = false;
            this.LabelStartingLane.Text = "Starting Lane:";
            // 
            // SweeperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.NumericNumberOfLanes);
            this.Controls.Add(this.LabelNumberOfLanes);
            this.Controls.Add(this.NumericStartingLane);
            this.Controls.Add(this.LabelStartingLane);
            this.Controls.Add(this.NumericMaxPerPair);
            this.Controls.Add(this.LabelMaxPerPair);
            this.Controls.Add(this.NumericCashRatio);
            this.Controls.Add(this.LabelCashRatio);
            this.Controls.Add(this.NumericGames);
            this.Controls.Add(this.LabelGames);
            this.Controls.Add(this.NumericEntryFee);
            this.Controls.Add(this.LabelEntryFee);
            this.Controls.Add(this.DatePickerSweeperDate);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.GroupboxDivisions);
            this.Name = "SweeperControl";
            this.Size = new System.Drawing.Size(558, 528);
            this.GroupboxDivisions.ResumeLayout(false);
            this.GroupboxDivisions.PerformLayout();
            this.PanelDivisions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSweeper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEntryFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericGames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCashRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaxPerPair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericNumberOfLanes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartingLane)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private GroupBox GroupboxDivisions;
    private Panel PanelDivisions;
    private SweeperDivisionsControl SweeperDivisions;
    private DateTimePicker DatePickerSweeperDate;
    private Controls.LabelControl LabelDate;
    private ErrorProvider ErrorProviderSweeper;
    private Controls.NumericControl NumericEntryFee;
    private Controls.LabelControl LabelEntryFee;
    private Controls.NumericControl NumericGames;
    private Controls.LabelControl LabelGames;
    private Controls.NumericControl NumericCashRatio;
    private Controls.LabelControl LabelCashRatio;
    private Controls.NumericControl NumericMaxPerPair;
    private Controls.LabelControl LabelMaxPerPair;
    private Controls.NumericControl NumericNumberOfLanes;
    private Controls.LabelControl LabelNumberOfLanes;
    private Controls.NumericControl NumericStartingLane;
    private Controls.LabelControl LabelStartingLane;
}
