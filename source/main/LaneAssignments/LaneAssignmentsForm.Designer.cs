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
            this.laneAssignmentGroupBox.SuspendLayout();
            this.unassignedRegistrationsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // laneAssignmentGroupBox
            // 
            this.laneAssignmentGroupBox.Controls.Add(this.laneAssignmentFlowLayoutPanel);
            this.laneAssignmentGroupBox.Location = new System.Drawing.Point(12, 12);
            this.laneAssignmentGroupBox.Name = "laneAssignmentGroupBox";
            this.laneAssignmentGroupBox.Size = new System.Drawing.Size(850, 940);
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
            this.laneAssignmentFlowLayoutPanel.Size = new System.Drawing.Size(844, 918);
            this.laneAssignmentFlowLayoutPanel.TabIndex = 1;
            // 
            // unassignedRegistrationsGroupBox
            // 
            this.unassignedRegistrationsGroupBox.Controls.Add(this.unassignedRegistrationsFlowLayoutPanel);
            this.unassignedRegistrationsGroupBox.Location = new System.Drawing.Point(868, 12);
            this.unassignedRegistrationsGroupBox.Name = "unassignedRegistrationsGroupBox";
            this.unassignedRegistrationsGroupBox.Size = new System.Drawing.Size(435, 937);
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
            this.unassignedRegistrationsFlowLayoutPanel.Size = new System.Drawing.Size(429, 915);
            this.unassignedRegistrationsFlowLayoutPanel.TabIndex = 2;
            this.unassignedRegistrationsFlowLayoutPanel.WrapContents = false;
            // 
            // addToRegistrationButton
            // 
            this.addToRegistrationButton.Location = new System.Drawing.Point(15, 958);
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
            this.newRegistrationButton.Location = new System.Drawing.Point(733, 955);
            this.newRegistrationButton.Name = "newRegistrationButton";
            this.newRegistrationButton.Size = new System.Drawing.Size(123, 29);
            this.newRegistrationButton.TabIndex = 6;
            this.newRegistrationButton.Text = "New Registration";
            this.laneAssignmentToolTip.SetToolTip(this.newRegistrationButton, "Add bowler to squad who has NOT bowled in the tournament");
            this.newRegistrationButton.UseVisualStyleBackColor = true;
            this.newRegistrationButton.Click += new System.EventHandler(this.NewRegistrationButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1537, 1013);
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
            this.ResumeLayout(false);

    }

    #endregion

    private GroupBox laneAssignmentGroupBox;
    private FlowLayoutPanel laneAssignmentFlowLayoutPanel;
    private GroupBox unassignedRegistrationsGroupBox;
    private FlowLayoutPanel unassignedRegistrationsFlowLayoutPanel;
    private Button addToRegistrationButton;
    private ToolTip laneAssignmentToolTip;
    private Button newRegistrationButton;
}