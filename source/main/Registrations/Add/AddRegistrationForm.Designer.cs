namespace NewEnglandClassic.Registrations.Add;

partial class Form
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.BowlerControl = new NewEnglandClassic.Contols.BowlerControl();
            this.ComboBoxDivisions = new System.Windows.Forms.ComboBox();
            this.LabelDivision = new NewEnglandClassic.Controls.LabelControl();
            this.ErrorProviderRegistration = new System.Windows.Forms.ErrorProvider(this.components);
            this.NumericAverage = new NewEnglandClassic.Controls.NumericControl();
            this.LabelAverage = new NewEnglandClassic.Controls.LabelControl();
            this.GroupboxSquads = new System.Windows.Forms.GroupBox();
            this.FlowLayoutPanelSquads = new System.Windows.Forms.FlowLayoutPanel();
            this.GroupboxSweepers = new System.Windows.Forms.GroupBox();
            this.FlowLayoutPanelSweepers = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderRegistration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericAverage)).BeginInit();
            this.GroupboxSquads.SuspendLayout();
            this.GroupboxSweepers.SuspendLayout();
            this.SuspendLayout();
            // 
            // BowlerControl
            // 
            this.BowlerControl.CityAddress = "";
            this.BowlerControl.DateOfBirth = null;
            this.BowlerControl.EmailAddress = "";
            this.BowlerControl.FirstName = "";
            this.BowlerControl.Gender = NewEnglandClassic.Models.Gender.Male;
            this.BowlerControl.Id = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.BowlerControl.LastName = "";
            this.BowlerControl.Location = new System.Drawing.Point(2, 12);
            this.BowlerControl.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.BowlerControl.MiddleInitial = "";
            this.BowlerControl.Name = "BowlerControl";
            this.BowlerControl.PhoneNumber = "";
            this.BowlerControl.Size = new System.Drawing.Size(597, 313);
            this.BowlerControl.StateAddress = "AL";
            this.BowlerControl.StreetAddress = "";
            this.BowlerControl.Suffix = "";
            this.BowlerControl.TabIndex = 0;
            this.BowlerControl.USBCId = "";
            this.BowlerControl.ZipCode = "";
            // 
            // ComboBoxDivisions
            // 
            this.ComboBoxDivisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDivisions.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBoxDivisions.FormattingEnabled = true;
            this.ComboBoxDivisions.Location = new System.Drawing.Point(5, 362);
            this.ComboBoxDivisions.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.ComboBoxDivisions.Name = "ComboBoxDivisions";
            this.ComboBoxDivisions.Size = new System.Drawing.Size(232, 26);
            this.ComboBoxDivisions.TabIndex = 1;
            this.ComboBoxDivisions.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxDivisions_Validating);
            // 
            // LabelDivision
            // 
            this.LabelDivision.AutoSize = true;
            this.LabelDivision.Bold = false;
            this.LabelDivision.Location = new System.Drawing.Point(5, 337);
            this.LabelDivision.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.LabelDivision.Name = "LabelDivision";
            this.LabelDivision.Required = true;
            this.LabelDivision.Size = new System.Drawing.Size(96, 19);
            this.LabelDivision.TabIndex = 28;
            this.LabelDivision.TabStop = false;
            this.LabelDivision.Text = "Division:";
            // 
            // ErrorProviderRegistration
            // 
            this.ErrorProviderRegistration.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderRegistration.ContainerControl = this.BowlerControl;
            // 
            // NumericAverage
            // 
            this.NumericAverage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericAverage.Location = new System.Drawing.Point(300, 361);
            this.NumericAverage.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericAverage.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.NumericAverage.Name = "NumericAverage";
            this.NumericAverage.Size = new System.Drawing.Size(99, 27);
            this.NumericAverage.TabIndex = 2;
            this.NumericAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelAverage
            // 
            this.LabelAverage.AutoSize = true;
            this.LabelAverage.Bold = false;
            this.LabelAverage.Location = new System.Drawing.Point(300, 337);
            this.LabelAverage.Name = "LabelAverage";
            this.LabelAverage.Required = true;
            this.LabelAverage.Size = new System.Drawing.Size(87, 19);
            this.LabelAverage.TabIndex = 111;
            this.LabelAverage.TabStop = false;
            this.LabelAverage.Text = "Average:";
            // 
            // GroupboxSquads
            // 
            this.GroupboxSquads.Controls.Add(this.FlowLayoutPanelSquads);
            this.GroupboxSquads.Location = new System.Drawing.Point(617, 12);
            this.GroupboxSquads.Name = "GroupboxSquads";
            this.GroupboxSquads.Size = new System.Drawing.Size(276, 220);
            this.GroupboxSquads.TabIndex = 112;
            this.GroupboxSquads.TabStop = false;
            this.GroupboxSquads.Text = "Squads";
            // 
            // FlowLayoutPanelSquads
            // 
            this.FlowLayoutPanelSquads.AutoScroll = true;
            this.FlowLayoutPanelSquads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanelSquads.Location = new System.Drawing.Point(3, 19);
            this.FlowLayoutPanelSquads.Name = "FlowLayoutPanelSquads";
            this.FlowLayoutPanelSquads.Size = new System.Drawing.Size(270, 198);
            this.FlowLayoutPanelSquads.TabIndex = 115;
            // 
            // GroupboxSweepers
            // 
            this.GroupboxSweepers.Controls.Add(this.FlowLayoutPanelSweepers);
            this.GroupboxSweepers.Location = new System.Drawing.Point(617, 238);
            this.GroupboxSweepers.Name = "GroupboxSweepers";
            this.GroupboxSweepers.Size = new System.Drawing.Size(276, 150);
            this.GroupboxSweepers.TabIndex = 0;
            this.GroupboxSweepers.TabStop = false;
            this.GroupboxSweepers.Text = "Sweepers";
            // 
            // FlowLayoutPanelSweepers
            // 
            this.FlowLayoutPanelSweepers.AutoScroll = true;
            this.FlowLayoutPanelSweepers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanelSweepers.Location = new System.Drawing.Point(3, 19);
            this.FlowLayoutPanelSweepers.Name = "FlowLayoutPanelSweepers";
            this.FlowLayoutPanelSweepers.Size = new System.Drawing.Size(270, 128);
            this.FlowLayoutPanelSweepers.TabIndex = 116;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(12, 394);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 114;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonSave
            // 
            this.ButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSave.Location = new System.Drawing.Point(818, 394);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 113;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // Form
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(909, 428);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.GroupboxSweepers);
            this.Controls.Add(this.GroupboxSquads);
            this.Controls.Add(this.NumericAverage);
            this.Controls.Add(this.LabelAverage);
            this.Controls.Add(this.LabelDivision);
            this.Controls.Add(this.ComboBoxDivisions);
            this.Controls.Add(this.BowlerControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register Bowler";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderRegistration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericAverage)).EndInit();
            this.GroupboxSquads.ResumeLayout(false);
            this.GroupboxSweepers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Contols.BowlerControl BowlerControl;
    private ComboBox ComboBoxDivisions;
    private Controls.LabelControl LabelDivision;
    private ErrorProvider ErrorProviderRegistration;
    private Controls.NumericControl NumericAverage;
    private Controls.LabelControl LabelAverage;
    private GroupBox GroupboxSquads;
    private GroupBox GroupboxSweepers;
    private Button ButtonCancel;
    private Button ButtonSave;
    private FlowLayoutPanel FlowLayoutPanelSquads;
    private FlowLayoutPanel FlowLayoutPanelSweepers;
}