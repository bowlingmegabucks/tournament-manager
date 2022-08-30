namespace NortheastMegabuck.Registrations.Add;

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
            this.bowlerControl = new NortheastMegabuck.Contols.BowlerControl();
            this.divisionsDropdown = new System.Windows.Forms.ComboBox();
            this.divisionLabel = new NortheastMegabuck.Controls.LabelControl();
            this.registrationErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.averageValue = new NortheastMegabuck.Controls.NumericControl();
            this.averageLabel = new NortheastMegabuck.Controls.LabelControl();
            this.squadsGroupbox = new System.Windows.Forms.GroupBox();
            this.squadsFlowPanelLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.sweepersGroupbox = new System.Windows.Forms.GroupBox();
            this.sweepersFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.superSweeperCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.registrationErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.averageValue)).BeginInit();
            this.squadsGroupbox.SuspendLayout();
            this.sweepersGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // bowlerControl
            // 
            this.bowlerControl.CityAddress = "";
            this.bowlerControl.DateOfBirth = null;
            this.bowlerControl.EmailAddress = "";
            this.bowlerControl.FirstName = "";
            this.bowlerControl.Gender = NortheastMegabuck.Models.Gender.Male;
            this.bowlerControl.LastName = "";
            this.bowlerControl.Location = new System.Drawing.Point(2, 12);
            this.bowlerControl.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.bowlerControl.MiddleInitial = "";
            this.bowlerControl.Name = "bowlerControl";
            this.bowlerControl.PhoneNumber = "";
            this.bowlerControl.Size = new System.Drawing.Size(597, 313);
            this.bowlerControl.StateAddress = "AL";
            this.bowlerControl.StreetAddress = "";
            this.bowlerControl.Suffix = "";
            this.bowlerControl.TabIndex = 0;
            this.bowlerControl.USBCId = "";
            this.bowlerControl.ZipCode = "";
            // 
            // divisionsDropdown
            // 
            this.divisionsDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.divisionsDropdown.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.divisionsDropdown.FormattingEnabled = true;
            this.divisionsDropdown.Location = new System.Drawing.Point(5, 362);
            this.divisionsDropdown.Margin = new System.Windows.Forms.Padding(15, 3, 15, 9);
            this.divisionsDropdown.Name = "divisionsDropdown";
            this.divisionsDropdown.Size = new System.Drawing.Size(232, 26);
            this.divisionsDropdown.TabIndex = 1;
            this.divisionsDropdown.Validating += new System.ComponentModel.CancelEventHandler(this.DivisionsDropDown_Validating);
            // 
            // divisionLabel
            // 
            this.divisionLabel.AutoSize = true;
            this.divisionLabel.Bold = false;
            this.divisionLabel.Location = new System.Drawing.Point(5, 337);
            this.divisionLabel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.divisionLabel.Name = "divisionLabel";
            this.divisionLabel.Required = true;
            this.divisionLabel.Size = new System.Drawing.Size(96, 19);
            this.divisionLabel.TabIndex = 28;
            this.divisionLabel.TabStop = false;
            this.divisionLabel.Text = "Division:";
            // 
            // registrationErrorProvider
            // 
            this.registrationErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.registrationErrorProvider.ContainerControl = this.bowlerControl;
            // 
            // averageValue
            // 
            this.averageValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.averageValue.Location = new System.Drawing.Point(300, 361);
            this.averageValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.averageValue.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.averageValue.Name = "averageValue";
            this.averageValue.Size = new System.Drawing.Size(99, 27);
            this.averageValue.TabIndex = 2;
            this.averageValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // averageLabel
            // 
            this.averageLabel.AutoSize = true;
            this.averageLabel.Bold = false;
            this.averageLabel.Location = new System.Drawing.Point(300, 337);
            this.averageLabel.Name = "averageLabel";
            this.averageLabel.Required = true;
            this.averageLabel.Size = new System.Drawing.Size(87, 19);
            this.averageLabel.TabIndex = 111;
            this.averageLabel.TabStop = false;
            this.averageLabel.Text = "Average:";
            // 
            // squadsGroupbox
            // 
            this.squadsGroupbox.Controls.Add(this.squadsFlowPanelLayout);
            this.squadsGroupbox.Location = new System.Drawing.Point(617, 12);
            this.squadsGroupbox.Name = "squadsGroupbox";
            this.squadsGroupbox.Size = new System.Drawing.Size(276, 220);
            this.squadsGroupbox.TabIndex = 112;
            this.squadsGroupbox.TabStop = false;
            this.squadsGroupbox.Text = "Squads";
            // 
            // squadsFlowPanelLayout
            // 
            this.squadsFlowPanelLayout.AutoScroll = true;
            this.squadsFlowPanelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadsFlowPanelLayout.Location = new System.Drawing.Point(3, 19);
            this.squadsFlowPanelLayout.Name = "squadsFlowPanelLayout";
            this.squadsFlowPanelLayout.Size = new System.Drawing.Size(270, 198);
            this.squadsFlowPanelLayout.TabIndex = 115;
            // 
            // sweepersGroupbox
            // 
            this.sweepersGroupbox.Controls.Add(this.sweepersFlowLayoutPanel);
            this.sweepersGroupbox.Location = new System.Drawing.Point(617, 238);
            this.sweepersGroupbox.Name = "sweepersGroupbox";
            this.sweepersGroupbox.Size = new System.Drawing.Size(276, 150);
            this.sweepersGroupbox.TabIndex = 0;
            this.sweepersGroupbox.TabStop = false;
            this.sweepersGroupbox.Text = "Sweepers";
            // 
            // sweepersFlowLayoutPanel
            // 
            this.sweepersFlowLayoutPanel.AutoScroll = true;
            this.sweepersFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sweepersFlowLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.sweepersFlowLayoutPanel.Name = "sweepersFlowLayoutPanel";
            this.sweepersFlowLayoutPanel.Size = new System.Drawing.Size(270, 128);
            this.sweepersFlowLayoutPanel.TabIndex = 116;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 394);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 114;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(818, 394);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 113;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // superSweeperCheckBox
            // 
            this.superSweeperCheckBox.AutoSize = true;
            this.superSweeperCheckBox.Location = new System.Drawing.Point(496, 366);
            this.superSweeperCheckBox.Name = "superSweeperCheckBox";
            this.superSweeperCheckBox.Size = new System.Drawing.Size(103, 19);
            this.superSweeperCheckBox.TabIndex = 3;
            this.superSweeperCheckBox.Text = "Super Sweeper";
            this.superSweeperCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(909, 428);
            this.Controls.Add(this.superSweeperCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.sweepersGroupbox);
            this.Controls.Add(this.squadsGroupbox);
            this.Controls.Add(this.averageValue);
            this.Controls.Add(this.averageLabel);
            this.Controls.Add(this.divisionLabel);
            this.Controls.Add(this.divisionsDropdown);
            this.Controls.Add(this.bowlerControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register Bowler";
            ((System.ComponentModel.ISupportInitialize)(this.registrationErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.averageValue)).EndInit();
            this.squadsGroupbox.ResumeLayout(false);
            this.sweepersGroupbox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Contols.BowlerControl bowlerControl;
    private ComboBox divisionsDropdown;
    private Controls.LabelControl divisionLabel;
    private ErrorProvider registrationErrorProvider;
    private Controls.NumericControl averageValue;
    private Controls.LabelControl averageLabel;
    private GroupBox squadsGroupbox;
    private GroupBox sweepersGroupbox;
    private Button cancelButton;
    private Button saveButton;
    private FlowLayoutPanel squadsFlowPanelLayout;
    private FlowLayoutPanel sweepersFlowLayoutPanel;
    private CheckBox superSweeperCheckBox;
}