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
        components = new System.ComponentModel.Container();
        bowlerControl = new Controls.BowlerControl();
        divisionsDropdown = new ComboBox();
        divisionLabel = new Controls.LabelControl();
        registrationErrorProvider = new ErrorProvider(components);
        averageValue = new Controls.NumericControl();
        averageLabel = new Controls.LabelControl();
        squadsGroupBox = new GroupBox();
        squadsFlowPanelLayout = new FlowLayoutPanel();
        sweepersGroupBox = new GroupBox();
        sweepersFlowLayoutPanel = new FlowLayoutPanel();
        cancelButton = new Button();
        saveButton = new Button();
        superSweeperCheckBox = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).BeginInit();
        ((System.ComponentModel.ISupportInitialize)averageValue).BeginInit();
        squadsGroupBox.SuspendLayout();
        sweepersGroupBox.SuspendLayout();
        SuspendLayout();
        // 
        // bowlerControl
        // 
        bowlerControl.CityAddress = "";
        bowlerControl.DateOfBirth = null;
        bowlerControl.EmailAddress = "";
        bowlerControl.FirstName = "";
        bowlerControl.Gender = Models.Gender.Male;
        bowlerControl.LastName = "";
        bowlerControl.Location = new Point(2, 12);
        bowlerControl.Margin = new Padding(15, 3, 15, 9);
        bowlerControl.MiddleInitial = "";
        bowlerControl.Name = "bowlerControl";
        bowlerControl.PhoneNumber = "";
        bowlerControl.Size = new Size(597, 376);
        bowlerControl.SocialSecurityNumber = "";
        bowlerControl.StateAddress = "";
        bowlerControl.StreetAddress = "";
        bowlerControl.Suffix = "";
        bowlerControl.TabIndex = 0;
        bowlerControl.USBCId = "";
        bowlerControl.ZipCode = "";
        // 
        // divisionsDropdown
        // 
        divisionsDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
        divisionsDropdown.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        divisionsDropdown.FormattingEnabled = true;
        divisionsDropdown.Location = new Point(2, 425);
        divisionsDropdown.Margin = new Padding(15, 3, 15, 9);
        divisionsDropdown.Name = "divisionsDropdown";
        divisionsDropdown.Size = new Size(232, 26);
        divisionsDropdown.TabIndex = 1;
        divisionsDropdown.Validating += DivisionsDropDown_Validating;
        // 
        // divisionLabel
        // 
        divisionLabel.AutoSize = true;
        divisionLabel.Bold = false;
        divisionLabel.Location = new Point(2, 400);
        divisionLabel.Margin = new Padding(15, 3, 15, 3);
        divisionLabel.Name = "divisionLabel";
        divisionLabel.Required = true;
        divisionLabel.Size = new Size(96, 19);
        divisionLabel.TabIndex = 28;
        divisionLabel.TabStop = false;
        divisionLabel.Text = "Division:";
        // 
        // registrationErrorProvider
        // 
        registrationErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        registrationErrorProvider.ContainerControl = bowlerControl;
        // 
        // averageValue
        // 
        averageValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        averageValue.Location = new Point(297, 424);
        averageValue.Margin = new Padding(3, 4, 30, 10);
        averageValue.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        averageValue.Name = "averageValue";
        averageValue.Size = new Size(99, 27);
        averageValue.TabIndex = 2;
        averageValue.TextAlign = HorizontalAlignment.Right;
        // 
        // averageLabel
        // 
        averageLabel.AutoSize = true;
        averageLabel.Bold = false;
        averageLabel.Location = new Point(297, 398);
        averageLabel.Name = "averageLabel";
        averageLabel.Required = true;
        averageLabel.Size = new Size(87, 19);
        averageLabel.TabIndex = 111;
        averageLabel.TabStop = false;
        averageLabel.Text = "Average:";
        // 
        // squadsGroupBox
        // 
        squadsGroupBox.Controls.Add(squadsFlowPanelLayout);
        squadsGroupBox.Location = new Point(617, 12);
        squadsGroupBox.Name = "squadsGroupBox";
        squadsGroupBox.Size = new Size(276, 260);
        squadsGroupBox.TabIndex = 112;
        squadsGroupBox.TabStop = false;
        squadsGroupBox.Text = "Squads";
        // 
        // squadsFlowPanelLayout
        // 
        squadsFlowPanelLayout.AutoScroll = true;
        squadsFlowPanelLayout.Dock = DockStyle.Fill;
        squadsFlowPanelLayout.Location = new Point(3, 19);
        squadsFlowPanelLayout.Name = "squadsFlowPanelLayout";
        squadsFlowPanelLayout.Size = new Size(270, 238);
        squadsFlowPanelLayout.TabIndex = 115;
        // 
        // sweepersGroupBox
        // 
        sweepersGroupBox.Controls.Add(sweepersFlowLayoutPanel);
        sweepersGroupBox.Location = new Point(617, 278);
        sweepersGroupBox.Name = "sweepersGroupBox";
        sweepersGroupBox.Size = new Size(276, 179);
        sweepersGroupBox.TabIndex = 0;
        sweepersGroupBox.TabStop = false;
        sweepersGroupBox.Text = "Sweepers";
        // 
        // sweepersFlowLayoutPanel
        // 
        sweepersFlowLayoutPanel.AutoScroll = true;
        sweepersFlowLayoutPanel.Dock = DockStyle.Fill;
        sweepersFlowLayoutPanel.Location = new Point(3, 19);
        sweepersFlowLayoutPanel.Name = "sweepersFlowLayoutPanel";
        sweepersFlowLayoutPanel.Size = new Size(270, 157);
        sweepersFlowLayoutPanel.TabIndex = 116;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(2, 463);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 114;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(808, 463);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 113;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // superSweeperCheckBox
        // 
        superSweeperCheckBox.AutoSize = true;
        superSweeperCheckBox.Location = new Point(493, 429);
        superSweeperCheckBox.Name = "superSweeperCheckBox";
        superSweeperCheckBox.Size = new Size(103, 19);
        superSweeperCheckBox.TabIndex = 3;
        superSweeperCheckBox.Text = "Super Sweeper";
        superSweeperCheckBox.UseVisualStyleBackColor = true;
        // 
        // Form
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(909, 494);
        Controls.Add(superSweeperCheckBox);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(sweepersGroupBox);
        Controls.Add(squadsGroupBox);
        Controls.Add(averageValue);
        Controls.Add(averageLabel);
        Controls.Add(divisionLabel);
        Controls.Add(divisionsDropdown);
        Controls.Add(bowlerControl);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Register Bowler";
        ((System.ComponentModel.ISupportInitialize)registrationErrorProvider).EndInit();
        ((System.ComponentModel.ISupportInitialize)averageValue).EndInit();
        squadsGroupBox.ResumeLayout(false);
        sweepersGroupBox.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.BowlerControl bowlerControl;
    private ComboBox divisionsDropdown;
    private Controls.LabelControl divisionLabel;
    private ErrorProvider registrationErrorProvider;
    private Controls.NumericControl averageValue;
    private Controls.LabelControl averageLabel;
    private GroupBox squadsGroupBox;
    private GroupBox sweepersGroupBox;
    private Button cancelButton;
    private Button saveButton;
    private FlowLayoutPanel squadsFlowPanelLayout;
    private FlowLayoutPanel sweepersFlowLayoutPanel;
    private CheckBox superSweeperCheckBox;
}