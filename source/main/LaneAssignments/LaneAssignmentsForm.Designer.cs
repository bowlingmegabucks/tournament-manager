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
            this.components = new System.ComponentModel.Container();
            this.laneAssignmentGroupBox = new System.Windows.Forms.GroupBox();
            this.laneAssignmentFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.unassignedRegistrationsGroupBox = new System.Windows.Forms.GroupBox();
            this.unassignedRegistrationsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addToRegistrationButton = new System.Windows.Forms.Button();
            this.laneAssignmentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.newRegistrationButton = new System.Windows.Forms.Button();
            this.generateRecapSheetsButton = new System.Windows.Forms.Button();
            this.copyAssignmentsToClipboardLinkLabel = new System.Windows.Forms.LinkLabel();
            this.laneSkipGroupBox = new System.Windows.Forms.GroupBox();
            this.sameSkipRadioButton = new System.Windows.Forms.RadioButton();
            this.staggeredSkipRadioButton = new System.Windows.Forms.RadioButton();
            this.laneAssignmentContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteLaneAssignmentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAssignmentsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.laneAssignmentGroupBox.SuspendLayout();
            this.unassignedRegistrationsGroupBox.SuspendLayout();
            this.laneSkipGroupBox.SuspendLayout();
            this.laneAssignmentContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // laneAssignmentGroupBox
            // 
            this.laneAssignmentGroupBox.Controls.Add(this.laneAssignmentFlowLayoutPanel);
            this.laneAssignmentGroupBox.Location = new System.Drawing.Point(12, 12);
            this.laneAssignmentGroupBox.Name = "laneAssignmentGroupBox";
            this.laneAssignmentGroupBox.Size = new System.Drawing.Size(850, 740);
            this.laneAssignmentGroupBox.TabIndex = 0;
            this.laneAssignmentGroupBox.TabStop = false;
            this.laneAssignmentGroupBox.Text = "Assigned";
            // 
            // laneAssignmentFlowLayoutPanel
            // 
            this.laneAssignmentFlowLayoutPanel.AutoScroll = true;
            this.laneAssignmentFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laneAssignmentFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.laneAssignmentFlowLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.laneAssignmentFlowLayoutPanel.Name = "laneAssignmentFlowLayoutPanel";
            this.laneAssignmentFlowLayoutPanel.Size = new System.Drawing.Size(844, 718);
            this.laneAssignmentFlowLayoutPanel.TabIndex = 1;
            // 
            // unassignedRegistrationsGroupBox
            // 
            this.unassignedRegistrationsGroupBox.Controls.Add(this.unassignedRegistrationsFlowLayoutPanel);
            this.unassignedRegistrationsGroupBox.Location = new System.Drawing.Point(868, 12);
            this.unassignedRegistrationsGroupBox.Name = "unassignedRegistrationsGroupBox";
            this.unassignedRegistrationsGroupBox.Size = new System.Drawing.Size(435, 737);
            this.unassignedRegistrationsGroupBox.TabIndex = 0;
            this.unassignedRegistrationsGroupBox.TabStop = false;
            this.unassignedRegistrationsGroupBox.Text = "Registrations";
            // 
            // unassignedRegistrationsFlowLayoutPanel
            // 
            this.unassignedRegistrationsFlowLayoutPanel.AutoScroll = true;
            this.unassignedRegistrationsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unassignedRegistrationsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.unassignedRegistrationsFlowLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.unassignedRegistrationsFlowLayoutPanel.Name = "unassignedRegistrationsFlowLayoutPanel";
            this.unassignedRegistrationsFlowLayoutPanel.Size = new System.Drawing.Size(429, 715);
            this.unassignedRegistrationsFlowLayoutPanel.TabIndex = 2;
            this.unassignedRegistrationsFlowLayoutPanel.WrapContents = false;
            // 
            // addToRegistrationButton
            // 
            this.addToRegistrationButton.Location = new System.Drawing.Point(15, 755);
            this.addToRegistrationButton.Name = "addToRegistrationButton";
            this.addToRegistrationButton.Size = new System.Drawing.Size(123, 29);
            this.addToRegistrationButton.TabIndex = 5;
            this.addToRegistrationButton.Text = "Add to Registration";
            this.laneAssignmentToolTip.SetToolTip(this.addToRegistrationButton, "Add bowler to squad who has already bowled in the tournament");
            this.addToRegistrationButton.UseVisualStyleBackColor = true;
            this.addToRegistrationButton.Click += new System.EventHandler(this.AddToRegistrationButton_Click);
            // 
            // newRegistrationButton
            // 
            this.newRegistrationButton.Location = new System.Drawing.Point(733, 752);
            this.newRegistrationButton.Name = "newRegistrationButton";
            this.newRegistrationButton.Size = new System.Drawing.Size(123, 29);
            this.newRegistrationButton.TabIndex = 6;
            this.newRegistrationButton.Text = "New Registration";
            this.laneAssignmentToolTip.SetToolTip(this.newRegistrationButton, "Add bowler to squad who has NOT bowled in the tournament");
            this.newRegistrationButton.UseVisualStyleBackColor = true;
            this.newRegistrationButton.Click += new System.EventHandler(this.NewRegistrationButton_Click);
            // 
            // generateRecapSheetsButton
            // 
            this.generateRecapSheetsButton.Location = new System.Drawing.Point(1309, 97);
            this.generateRecapSheetsButton.Name = "generateRecapSheetsButton";
            this.generateRecapSheetsButton.Size = new System.Drawing.Size(161, 29);
            this.generateRecapSheetsButton.TabIndex = 9;
            this.generateRecapSheetsButton.Text = "Generate Recap Sheets";
            this.generateRecapSheetsButton.UseVisualStyleBackColor = true;
            this.generateRecapSheetsButton.Click += new System.EventHandler(this.GenerateRecapSheetsButton_Click);
            // 
            // copyAssignmentsToClipboardLinkLabel
            // 
            this.copyAssignmentsToClipboardLinkLabel.AutoSize = true;
            this.copyAssignmentsToClipboardLinkLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.copyAssignmentsToClipboardLinkLabel.Location = new System.Drawing.Point(1082, 752);
            this.copyAssignmentsToClipboardLinkLabel.Name = "copyAssignmentsToClipboardLinkLabel";
            this.copyAssignmentsToClipboardLinkLabel.Size = new System.Drawing.Size(218, 20);
            this.copyAssignmentsToClipboardLinkLabel.TabIndex = 7;
            this.copyAssignmentsToClipboardLinkLabel.TabStop = true;
            this.copyAssignmentsToClipboardLinkLabel.Text = "Copy Assignments to Clipboard";
            this.copyAssignmentsToClipboardLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CopyAssignmentsToClipboardLinkLabel_LinkClicked);
            // 
            // laneSkipGroupBox
            // 
            this.laneSkipGroupBox.Controls.Add(this.sameSkipRadioButton);
            this.laneSkipGroupBox.Controls.Add(this.staggeredSkipRadioButton);
            this.laneSkipGroupBox.Location = new System.Drawing.Point(1309, 12);
            this.laneSkipGroupBox.Name = "laneSkipGroupBox";
            this.laneSkipGroupBox.Size = new System.Drawing.Size(161, 79);
            this.laneSkipGroupBox.TabIndex = 8;
            this.laneSkipGroupBox.TabStop = false;
            this.laneSkipGroupBox.Text = "Lane Skip";
            // 
            // sameSkipRadioButton
            // 
            this.sameSkipRadioButton.AutoSize = true;
            this.sameSkipRadioButton.Location = new System.Drawing.Point(6, 22);
            this.sameSkipRadioButton.Name = "sameSkipRadioButton";
            this.sameSkipRadioButton.Size = new System.Drawing.Size(79, 19);
            this.sameSkipRadioButton.TabIndex = 9;
            this.sameSkipRadioButton.Text = "Same Skip";
            this.sameSkipRadioButton.UseVisualStyleBackColor = true;
            // 
            // staggeredSkipRadioButton
            // 
            this.staggeredSkipRadioButton.AutoSize = true;
            this.staggeredSkipRadioButton.Checked = true;
            this.staggeredSkipRadioButton.Location = new System.Drawing.Point(6, 47);
            this.staggeredSkipRadioButton.Name = "staggeredSkipRadioButton";
            this.staggeredSkipRadioButton.Size = new System.Drawing.Size(103, 19);
            this.staggeredSkipRadioButton.TabIndex = 10;
            this.staggeredSkipRadioButton.TabStop = true;
            this.staggeredSkipRadioButton.Text = "Staggered Skip";
            this.staggeredSkipRadioButton.UseVisualStyleBackColor = true;
            // 
            // laneAssignmentContextMenuStrip
            // 
            this.laneAssignmentContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteLaneAssignmentMenuItem});
            this.laneAssignmentContextMenuStrip.Name = "laneAssignmentContextMenuStrip";
            this.laneAssignmentContextMenuStrip.Size = new System.Drawing.Size(183, 26);
            // 
            // deleteLaneAssignmentMenuItem
            // 
            this.deleteLaneAssignmentMenuItem.Name = "deleteLaneAssignmentMenuItem";
            this.deleteLaneAssignmentMenuItem.Size = new System.Drawing.Size(182, 22);
            this.deleteLaneAssignmentMenuItem.Text = "Remove from Squad";
            this.deleteLaneAssignmentMenuItem.Click += new System.EventHandler(this.DeleteLaneAssignmentMenuItem_Click);
            // 
            // refreshAssignmentsLinkLabel
            // 
            this.refreshAssignmentsLinkLabel.AutoSize = true;
            this.refreshAssignmentsLinkLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.refreshAssignmentsLinkLabel.Location = new System.Drawing.Point(871, 775);
            this.refreshAssignmentsLinkLabel.Name = "refreshAssignmentsLinkLabel";
            this.refreshAssignmentsLinkLabel.Size = new System.Drawing.Size(58, 20);
            this.refreshAssignmentsLinkLabel.TabIndex = 10;
            this.refreshAssignmentsLinkLabel.TabStop = true;
            this.refreshAssignmentsLinkLabel.Text = "Refresh";
            this.refreshAssignmentsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RefreshAssignmentsLinkLabel_LinkClicked);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 796);
            this.Controls.Add(this.generateRecapSheetsButton);
            this.Controls.Add(this.laneSkipGroupBox);
            this.Controls.Add(this.copyAssignmentsToClipboardLinkLabel);
            this.Controls.Add(this.newRegistrationButton);
            this.Controls.Add(this.addToRegistrationButton);
            this.Controls.Add(this.unassignedRegistrationsGroupBox);
            this.Controls.Add(this.laneAssignmentGroupBox);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lane Assignments";
            this.laneAssignmentGroupBox.ResumeLayout(false);
            this.unassignedRegistrationsGroupBox.ResumeLayout(false);
            this.laneSkipGroupBox.ResumeLayout(false);
            this.laneSkipGroupBox.PerformLayout();
            this.laneAssignmentContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
}