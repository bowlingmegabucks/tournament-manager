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
            this.NumericCashRatio = new NewEnglandClassic.Controls.NumericControl();
            this.LabelCashRatio = new NewEnglandClassic.Controls.LabelControl();
            this.NumericFinalsRatio = new NewEnglandClassic.Controls.NumericControl();
            this.LabelFinalsRatio = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderSquad = new System.Windows.Forms.ErrorProvider(this.components);
            this.DatePickerSquadDate = new System.Windows.Forms.DateTimePicker();
            this.LabelDate = new NewEnglandClassic.Controls.LabelControl();
            this.NumericMaxPerPair = new NewEnglandClassic.Controls.NumericControl();
            this.LabelMaxPerPair = new NewEnglandClassic.Controls.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCashRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericFinalsRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSquad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaxPerPair)).BeginInit();
            this.SuspendLayout();
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
            this.NumericCashRatio.TabIndex = 2;
            this.NumericCashRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericCashRatio.Validating += new System.ComponentModel.CancelEventHandler(this.NumericCashRatio_Validating);
            this.NumericCashRatio.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelCashRatio
            // 
            this.LabelCashRatio.AutoSize = true;
            this.LabelCashRatio.Bold = false;
            this.LabelCashRatio.Location = new System.Drawing.Point(189, 68);
            this.LabelCashRatio.Name = "LabelCashRatio";
            this.LabelCashRatio.Required = false;
            this.LabelCashRatio.Size = new System.Drawing.Size(117, 19);
            this.LabelCashRatio.TabIndex = 111;
            this.LabelCashRatio.TabStop = false;
            this.LabelCashRatio.Text = "Cash Ratio:";
            // 
            // NumericFinalsRatio
            // 
            this.NumericFinalsRatio.DecimalPlaces = 1;
            this.NumericFinalsRatio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericFinalsRatio.Location = new System.Drawing.Point(3, 94);
            this.NumericFinalsRatio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericFinalsRatio.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.NumericFinalsRatio.Name = "NumericFinalsRatio";
            this.NumericFinalsRatio.Size = new System.Drawing.Size(131, 27);
            this.NumericFinalsRatio.TabIndex = 1;
            this.NumericFinalsRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericFinalsRatio.Validating += new System.ComponentModel.CancelEventHandler(this.NumericFinalsRatio_Validating);
            this.NumericFinalsRatio.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelFinalsRatio
            // 
            this.LabelFinalsRatio.AutoSize = true;
            this.LabelFinalsRatio.Bold = false;
            this.LabelFinalsRatio.Location = new System.Drawing.Point(3, 68);
            this.LabelFinalsRatio.Name = "LabelFinalsRatio";
            this.LabelFinalsRatio.Required = false;
            this.LabelFinalsRatio.Size = new System.Drawing.Size(126, 19);
            this.LabelFinalsRatio.TabIndex = 110;
            this.LabelFinalsRatio.TabStop = false;
            this.LabelFinalsRatio.Text = "Finals Ratio:";
            // 
            // ErrorProviderSquad
            // 
            this.ErrorProviderSquad.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderSquad.ContainerControl = this;
            // 
            // DatePickerSquadDate
            // 
            this.DatePickerSquadDate.CustomFormat = "MM/dd/yyyy    hh:mm tt";
            this.DatePickerSquadDate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DatePickerSquadDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePickerSquadDate.Location = new System.Drawing.Point(3, 28);
            this.DatePickerSquadDate.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.DatePickerSquadDate.MaxDate = new System.DateTime(2200, 12, 31, 0, 0, 0, 0);
            this.DatePickerSquadDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DatePickerSquadDate.Name = "DatePickerSquadDate";
            this.DatePickerSquadDate.Size = new System.Drawing.Size(217, 27);
            this.DatePickerSquadDate.TabIndex = 0;
            this.DatePickerSquadDate.Validating += new System.ComponentModel.CancelEventHandler(this.DatePickerSquadDate_Validating);
            this.DatePickerSquadDate.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelDate
            // 
            this.LabelDate.AutoSize = true;
            this.LabelDate.Bold = false;
            this.LabelDate.Location = new System.Drawing.Point(3, 3);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Required = true;
            this.LabelDate.Size = new System.Drawing.Size(60, 19);
            this.LabelDate.TabIndex = 113;
            this.LabelDate.TabStop = false;
            this.LabelDate.Text = "Date:";
            // 
            // NumericMaxPerPair
            // 
            this.NumericMaxPerPair.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericMaxPerPair.Location = new System.Drawing.Point(3, 160);
            this.NumericMaxPerPair.Margin = new System.Windows.Forms.Padding(3, 4, 3, 10);
            this.NumericMaxPerPair.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericMaxPerPair.Name = "NumericMaxPerPair";
            this.NumericMaxPerPair.Size = new System.Drawing.Size(131, 27);
            this.NumericMaxPerPair.TabIndex = 3;
            this.NumericMaxPerPair.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericMaxPerPair.Validating += new System.ComponentModel.CancelEventHandler(this.NumericMaxPerPair_Validating);
            this.NumericMaxPerPair.Validated += new System.EventHandler(this.Controls_Validated);
            // 
            // LabelMaxPerPair
            // 
            this.LabelMaxPerPair.AutoSize = true;
            this.LabelMaxPerPair.Bold = false;
            this.LabelMaxPerPair.Location = new System.Drawing.Point(3, 134);
            this.LabelMaxPerPair.Name = "LabelMaxPerPair";
            this.LabelMaxPerPair.Required = true;
            this.LabelMaxPerPair.Size = new System.Drawing.Size(132, 19);
            this.LabelMaxPerPair.TabIndex = 115;
            this.LabelMaxPerPair.TabStop = false;
            this.LabelMaxPerPair.Text = "Max Per Pair:";
            // 
            // SquadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NumericMaxPerPair);
            this.Controls.Add(this.LabelMaxPerPair);
            this.Controls.Add(this.DatePickerSquadDate);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.NumericCashRatio);
            this.Controls.Add(this.LabelCashRatio);
            this.Controls.Add(this.NumericFinalsRatio);
            this.Controls.Add(this.LabelFinalsRatio);
            this.Name = "SquadControl";
            this.Size = new System.Drawing.Size(357, 226);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.SquadControl_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.NumericCashRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericFinalsRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSquad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericMaxPerPair)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Controls.NumericControl NumericCashRatio;
    private Controls.LabelControl LabelCashRatio;
    private Controls.NumericControl NumericFinalsRatio;
    private Controls.LabelControl LabelFinalsRatio;
    private ErrorProvider ErrorProviderSquad;
    private DateTimePicker DatePickerSquadDate;
    private Controls.LabelControl LabelDate;
    private Controls.NumericControl NumericMaxPerPair;
    private Controls.LabelControl LabelMaxPerPair;
}
