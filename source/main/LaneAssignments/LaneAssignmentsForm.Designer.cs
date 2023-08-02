namespace NortheastMegabuck.LaneAssignments;

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
        laneAssignmentGroupBox = new GroupBox();
        laneAssignmentFlowLayoutPanel = new FlowLayoutPanel();
        unassignedRegistrationsGroupBox = new GroupBox();
        unassignedRegistrationsFlowLayoutPanel = new FlowLayoutPanel();
        addToRegistrationButton = new Button();
        laneAssignmentToolTip = new ToolTip(components);
        newRegistrationButton = new Button();
        generateRecapSheetsButton = new Button();
        copyAssignmentsToClipboardLinkLabel = new LinkLabel();
        laneSkipGroupBox = new GroupBox();
        sameSkipRadioButton = new RadioButton();
        staggeredSkipRadioButton = new RadioButton();
        laneAssignmentContextMenuStrip = new ContextMenuStrip(components);
        deleteLaneAssignmentMenuItem = new ToolStripMenuItem();
        refreshAssignmentsLinkLabel = new LinkLabel();
        divisionBreakdownHeader = new Label();
        entriesPerDivisionLabel = new Label();
        laneAssignmentGroupBox.SuspendLayout();
        unassignedRegistrationsGroupBox.SuspendLayout();
        laneSkipGroupBox.SuspendLayout();
        laneAssignmentContextMenuStrip.SuspendLayout();
        SuspendLayout();
        // 
        // laneAssignmentGroupBox
        // 
        laneAssignmentGroupBox.Controls.Add(laneAssignmentFlowLayoutPanel);
        laneAssignmentGroupBox.Location = new Point(12, 12);
        laneAssignmentGroupBox.Name = "laneAssignmentGroupBox";
        laneAssignmentGroupBox.Size = new Size(850, 740);
        laneAssignmentGroupBox.TabIndex = 0;
        laneAssignmentGroupBox.TabStop = false;
        laneAssignmentGroupBox.Text = "Assigned";
        // 
        // laneAssignmentFlowLayoutPanel
        // 
        laneAssignmentFlowLayoutPanel.AutoScroll = true;
        laneAssignmentFlowLayoutPanel.Dock = DockStyle.Fill;
        laneAssignmentFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        laneAssignmentFlowLayoutPanel.Location = new Point(3, 19);
        laneAssignmentFlowLayoutPanel.Name = "laneAssignmentFlowLayoutPanel";
        laneAssignmentFlowLayoutPanel.Size = new Size(844, 718);
        laneAssignmentFlowLayoutPanel.TabIndex = 1;
        // 
        // unassignedRegistrationsGroupBox
        // 
        unassignedRegistrationsGroupBox.Controls.Add(unassignedRegistrationsFlowLayoutPanel);
        unassignedRegistrationsGroupBox.Location = new Point(868, 12);
        unassignedRegistrationsGroupBox.Name = "unassignedRegistrationsGroupBox";
        unassignedRegistrationsGroupBox.Size = new Size(435, 737);
        unassignedRegistrationsGroupBox.TabIndex = 0;
        unassignedRegistrationsGroupBox.TabStop = false;
        unassignedRegistrationsGroupBox.Text = "Registrations";
        // 
        // unassignedRegistrationsFlowLayoutPanel
        // 
        unassignedRegistrationsFlowLayoutPanel.AutoScroll = true;
        unassignedRegistrationsFlowLayoutPanel.Dock = DockStyle.Fill;
        unassignedRegistrationsFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        unassignedRegistrationsFlowLayoutPanel.Location = new Point(3, 19);
        unassignedRegistrationsFlowLayoutPanel.Name = "unassignedRegistrationsFlowLayoutPanel";
        unassignedRegistrationsFlowLayoutPanel.Size = new Size(429, 715);
        unassignedRegistrationsFlowLayoutPanel.TabIndex = 2;
        unassignedRegistrationsFlowLayoutPanel.WrapContents = false;
        // 
        // addToRegistrationButton
        // 
        addToRegistrationButton.Location = new Point(15, 755);
        addToRegistrationButton.Name = "addToRegistrationButton";
        addToRegistrationButton.Size = new Size(123, 29);
        addToRegistrationButton.TabIndex = 5;
        addToRegistrationButton.Text = "Add to Registration";
        laneAssignmentToolTip.SetToolTip(addToRegistrationButton, "Add bowler to squad who has already bowled in the tournament");
        addToRegistrationButton.UseVisualStyleBackColor = true;
        addToRegistrationButton.Click += AddToRegistrationButton_Click;
        // 
        // newRegistrationButton
        // 
        newRegistrationButton.Location = new Point(733, 752);
        newRegistrationButton.Name = "newRegistrationButton";
        newRegistrationButton.Size = new Size(123, 29);
        newRegistrationButton.TabIndex = 6;
        newRegistrationButton.Text = "New Registration";
        laneAssignmentToolTip.SetToolTip(newRegistrationButton, "Add bowler to squad who has NOT bowled in the tournament");
        newRegistrationButton.UseVisualStyleBackColor = true;
        newRegistrationButton.Click += NewRegistrationButton_Click;
        // 
        // generateRecapSheetsButton
        // 
        generateRecapSheetsButton.Location = new Point(1309, 752);
        generateRecapSheetsButton.Name = "generateRecapSheetsButton";
        generateRecapSheetsButton.Size = new Size(239, 29);
        generateRecapSheetsButton.TabIndex = 9;
        generateRecapSheetsButton.Text = "Generate Recap Sheets";
        generateRecapSheetsButton.UseVisualStyleBackColor = true;
        generateRecapSheetsButton.Click += GenerateRecapSheetsButton_Click;
        // 
        // copyAssignmentsToClipboardLinkLabel
        // 
        copyAssignmentsToClipboardLinkLabel.AutoSize = true;
        copyAssignmentsToClipboardLinkLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        copyAssignmentsToClipboardLinkLabel.Location = new Point(1082, 752);
        copyAssignmentsToClipboardLinkLabel.Name = "copyAssignmentsToClipboardLinkLabel";
        copyAssignmentsToClipboardLinkLabel.Size = new Size(218, 20);
        copyAssignmentsToClipboardLinkLabel.TabIndex = 7;
        copyAssignmentsToClipboardLinkLabel.TabStop = true;
        copyAssignmentsToClipboardLinkLabel.Text = "Copy Assignments to Clipboard";
        copyAssignmentsToClipboardLinkLabel.LinkClicked += CopyAssignmentsToClipboardLinkLabel_LinkClicked;
        // 
        // laneSkipGroupBox
        // 
        laneSkipGroupBox.Controls.Add(sameSkipRadioButton);
        laneSkipGroupBox.Controls.Add(staggeredSkipRadioButton);
        laneSkipGroupBox.Location = new Point(1309, 667);
        laneSkipGroupBox.Name = "laneSkipGroupBox";
        laneSkipGroupBox.Size = new Size(239, 79);
        laneSkipGroupBox.TabIndex = 8;
        laneSkipGroupBox.TabStop = false;
        laneSkipGroupBox.Text = "Lane Skip";
        // 
        // sameSkipRadioButton
        // 
        sameSkipRadioButton.AutoSize = true;
        sameSkipRadioButton.Location = new Point(6, 22);
        sameSkipRadioButton.Name = "sameSkipRadioButton";
        sameSkipRadioButton.Size = new Size(79, 19);
        sameSkipRadioButton.TabIndex = 9;
        sameSkipRadioButton.Text = "Same Skip";
        sameSkipRadioButton.UseVisualStyleBackColor = true;
        // 
        // staggeredSkipRadioButton
        // 
        staggeredSkipRadioButton.AutoSize = true;
        staggeredSkipRadioButton.Checked = true;
        staggeredSkipRadioButton.Location = new Point(6, 47);
        staggeredSkipRadioButton.Name = "staggeredSkipRadioButton";
        staggeredSkipRadioButton.Size = new Size(103, 19);
        staggeredSkipRadioButton.TabIndex = 10;
        staggeredSkipRadioButton.TabStop = true;
        staggeredSkipRadioButton.Text = "Staggered Skip";
        staggeredSkipRadioButton.UseVisualStyleBackColor = true;
        // 
        // laneAssignmentContextMenuStrip
        // 
        laneAssignmentContextMenuStrip.Items.AddRange(new ToolStripItem[] { deleteLaneAssignmentMenuItem });
        laneAssignmentContextMenuStrip.Name = "laneAssignmentContextMenuStrip";
        laneAssignmentContextMenuStrip.Size = new Size(183, 26);
        // 
        // deleteLaneAssignmentMenuItem
        // 
        deleteLaneAssignmentMenuItem.Name = "deleteLaneAssignmentMenuItem";
        deleteLaneAssignmentMenuItem.Size = new Size(182, 22);
        deleteLaneAssignmentMenuItem.Text = "Remove from Squad";
        deleteLaneAssignmentMenuItem.Click += DeleteLaneAssignmentMenuItem_Click;
        // 
        // refreshAssignmentsLinkLabel
        // 
        refreshAssignmentsLinkLabel.AutoSize = true;
        refreshAssignmentsLinkLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        refreshAssignmentsLinkLabel.Location = new Point(871, 755);
        refreshAssignmentsLinkLabel.Name = "refreshAssignmentsLinkLabel";
        refreshAssignmentsLinkLabel.Size = new Size(58, 20);
        refreshAssignmentsLinkLabel.TabIndex = 10;
        refreshAssignmentsLinkLabel.TabStop = true;
        refreshAssignmentsLinkLabel.Text = "Refresh";
        refreshAssignmentsLinkLabel.LinkClicked += RefreshAssignmentsLinkLabel_LinkClicked;
        // 
        // divisionBreakdownHeader
        // 
        divisionBreakdownHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        divisionBreakdownHeader.Location = new Point(1315, 12);
        divisionBreakdownHeader.Name = "divisionBreakdownHeader";
        divisionBreakdownHeader.Size = new Size(233, 23);
        divisionBreakdownHeader.TabIndex = 11;
        divisionBreakdownHeader.Text = "Entries per Division";
        divisionBreakdownHeader.TextAlign = ContentAlignment.TopCenter;
        // 
        // entriesPerDivisionLabel
        // 
        entriesPerDivisionLabel.Location = new Point(1309, 35);
        entriesPerDivisionLabel.Name = "entriesPerDivisionLabel";
        entriesPerDivisionLabel.Size = new Size(239, 629);
        entriesPerDivisionLabel.TabIndex = 12;
        entriesPerDivisionLabel.Text = "label1";
        // 
        // Form
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1560, 796);
        Controls.Add(entriesPerDivisionLabel);
        Controls.Add(divisionBreakdownHeader);
        Controls.Add(refreshAssignmentsLinkLabel);
        Controls.Add(generateRecapSheetsButton);
        Controls.Add(laneSkipGroupBox);
        Controls.Add(copyAssignmentsToClipboardLinkLabel);
        Controls.Add(newRegistrationButton);
        Controls.Add(addToRegistrationButton);
        Controls.Add(unassignedRegistrationsGroupBox);
        Controls.Add(laneAssignmentGroupBox);
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Lane Assignments";
        laneAssignmentGroupBox.ResumeLayout(false);
        unassignedRegistrationsGroupBox.ResumeLayout(false);
        laneSkipGroupBox.ResumeLayout(false);
        laneSkipGroupBox.PerformLayout();
        laneAssignmentContextMenuStrip.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox laneAssignmentGroupBox;
    private FlowLayoutPanel laneAssignmentFlowLayoutPanel;
    private GroupBox unassignedRegistrationsGroupBox;
    private FlowLayoutPanel unassignedRegistrationsFlowLayoutPanel;
    private Button addToRegistrationButton;
    private ToolTip laneAssignmentToolTip;
    private Button newRegistrationButton;
    private LinkLabel copyAssignmentsToClipboardLinkLabel;
    private GroupBox laneSkipGroupBox;
    private RadioButton sameSkipRadioButton;
    private RadioButton staggeredSkipRadioButton;
    private Button generateRecapSheetsButton;
    private ContextMenuStrip laneAssignmentContextMenuStrip;
    private ToolStripMenuItem deleteLaneAssignmentMenuItem;
    private LinkLabel refreshAssignmentsLinkLabel;
    private Label divisionBreakdownHeader;
    private Label entriesPerDivisionLabel;
}