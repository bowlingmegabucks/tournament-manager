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
            this.laneAssignmentGroupbox = new System.Windows.Forms.GroupBox();
            this.laneAssignmentFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.unassignedRegistrationsGroupbox = new System.Windows.Forms.GroupBox();
            this.unassignedRegistrationsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.newRegistrationButton = new System.Windows.Forms.Button();
            this.laneAssignmentGroupbox.SuspendLayout();
            this.unassignedRegistrationsGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // laneAssignmentGroupbox
            // 
            this.laneAssignmentGroupbox.Controls.Add(this.laneAssignmentFlowLayoutPanel);
            this.laneAssignmentGroupbox.Location = new System.Drawing.Point(12, 12);
            this.laneAssignmentGroupbox.Name = "laneAssignmentGroupbox";
            this.laneAssignmentGroupbox.Size = new System.Drawing.Size(850, 940);
            this.laneAssignmentGroupbox.TabIndex = 0;
            this.laneAssignmentGroupbox.TabStop = false;
            this.laneAssignmentGroupbox.Text = "Assigned";
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
            // unassignedRegistrationsGroupbox
            // 
            this.unassignedRegistrationsGroupbox.Controls.Add(this.unassignedRegistrationsFlowLayoutPanel);
            this.unassignedRegistrationsGroupbox.Location = new System.Drawing.Point(868, 12);
            this.unassignedRegistrationsGroupbox.Name = "unassignedRegistrationsGroupbox";
            this.unassignedRegistrationsGroupbox.Size = new System.Drawing.Size(435, 937);
            this.unassignedRegistrationsGroupbox.TabIndex = 0;
            this.unassignedRegistrationsGroupbox.TabStop = false;
            this.unassignedRegistrationsGroupbox.Text = "Registrations";
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
            // newRegistrationButton
            // 
            this.newRegistrationButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.newRegistrationButton.Location = new System.Drawing.Point(15, 958);
            this.newRegistrationButton.Name = "newRegistrationButton";
            this.newRegistrationButton.Size = new System.Drawing.Size(123, 29);
            this.newRegistrationButton.TabIndex = 4;
            this.newRegistrationButton.Text = "New Registration";
            this.newRegistrationButton.UseVisualStyleBackColor = true;
            this.newRegistrationButton.Click += new System.EventHandler(this.NewRegistrationButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1537, 1013);
            this.Controls.Add(this.newRegistrationButton);
            this.Controls.Add(this.unassignedRegistrationsGroupbox);
            this.Controls.Add(this.laneAssignmentGroupbox);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lane Assignments";
            this.laneAssignmentGroupbox.ResumeLayout(false);
            this.unassignedRegistrationsGroupbox.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private GroupBox laneAssignmentGroupbox;
    private FlowLayoutPanel laneAssignmentFlowLayoutPanel;
    private GroupBox unassignedRegistrationsGroupbox;
    private FlowLayoutPanel unassignedRegistrationsFlowLayoutPanel;
    private Button newRegistrationButton;
}