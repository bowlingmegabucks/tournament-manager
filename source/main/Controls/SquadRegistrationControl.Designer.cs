namespace NortheastMegabuck.Controls;

partial class SquadRegistrationControl
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
            this.laneAssignmentLabel = new System.Windows.Forms.Label();
            this.bowlerNameLabel = new System.Windows.Forms.Label();
            this.divisionPanel = new System.Windows.Forms.Panel();
            this.divisionLabelText = new System.Windows.Forms.Label();
            this.divisionLabelValue = new System.Windows.Forms.Label();
            this.averagePanel = new System.Windows.Forms.Panel();
            this.averageLabelValue = new System.Windows.Forms.Label();
            this.averageLabelText = new System.Windows.Forms.Label();
            this.handicapPanel = new System.Windows.Forms.Panel();
            this.handicapLabelValue = new System.Windows.Forms.Label();
            this.handicapLabelText = new System.Windows.Forms.Label();
            this.divisionPanel.SuspendLayout();
            this.averagePanel.SuspendLayout();
            this.handicapPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // laneAssignmentLabel
            // 
            this.laneAssignmentLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.laneAssignmentLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.laneAssignmentLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.laneAssignmentLabel.Location = new System.Drawing.Point(0, 0);
            this.laneAssignmentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.laneAssignmentLabel.Name = "laneAssignmentLabel";
            this.laneAssignmentLabel.Size = new System.Drawing.Size(36, 36);
            this.laneAssignmentLabel.TabIndex = 0;
            this.laneAssignmentLabel.Text = "22B";
            this.laneAssignmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bowlerNameLabel
            // 
            this.bowlerNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.bowlerNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bowlerNameLabel.Location = new System.Drawing.Point(36, 0);
            this.bowlerNameLabel.Name = "bowlerNameLabel";
            this.bowlerNameLabel.Size = new System.Drawing.Size(210, 36);
            this.bowlerNameLabel.TabIndex = 1;
            this.bowlerNameLabel.Text = "Joe Bowler";
            this.bowlerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // divisionPanel
            // 
            this.divisionPanel.Controls.Add(this.divisionLabelValue);
            this.divisionPanel.Controls.Add(this.divisionLabelText);
            this.divisionPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.divisionPanel.Location = new System.Drawing.Point(246, 0);
            this.divisionPanel.Name = "divisionPanel";
            this.divisionPanel.Size = new System.Drawing.Size(166, 36);
            this.divisionPanel.TabIndex = 2;
            // 
            // divisionLabelText
            // 
            this.divisionLabelText.Dock = System.Windows.Forms.DockStyle.Top;
            this.divisionLabelText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.divisionLabelText.Location = new System.Drawing.Point(0, 0);
            this.divisionLabelText.Name = "divisionLabelText";
            this.divisionLabelText.Size = new System.Drawing.Size(166, 15);
            this.divisionLabelText.TabIndex = 0;
            this.divisionLabelText.Text = "Division:";
            // 
            // divisionLabelValue
            // 
            this.divisionLabelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisionLabelValue.Location = new System.Drawing.Point(0, 15);
            this.divisionLabelValue.Name = "divisionLabelValue";
            this.divisionLabelValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.divisionLabelValue.Size = new System.Drawing.Size(166, 21);
            this.divisionLabelValue.TabIndex = 1;
            this.divisionLabelValue.Text = "Under 215 Average";
            this.divisionLabelValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // averagePanel
            // 
            this.averagePanel.Controls.Add(this.averageLabelValue);
            this.averagePanel.Controls.Add(this.averageLabelText);
            this.averagePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.averagePanel.Location = new System.Drawing.Point(412, 0);
            this.averagePanel.Name = "averagePanel";
            this.averagePanel.Size = new System.Drawing.Size(71, 36);
            this.averagePanel.TabIndex = 3;
            // 
            // averageLabelValue
            // 
            this.averageLabelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.averageLabelValue.Location = new System.Drawing.Point(0, 15);
            this.averageLabelValue.Name = "averageLabelValue";
            this.averageLabelValue.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.averageLabelValue.Size = new System.Drawing.Size(71, 21);
            this.averageLabelValue.TabIndex = 1;
            this.averageLabelValue.Text = "888";
            this.averageLabelValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // averageLabelText
            // 
            this.averageLabelText.Dock = System.Windows.Forms.DockStyle.Top;
            this.averageLabelText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.averageLabelText.Location = new System.Drawing.Point(0, 0);
            this.averageLabelText.Name = "averageLabelText";
            this.averageLabelText.Size = new System.Drawing.Size(71, 15);
            this.averageLabelText.TabIndex = 0;
            this.averageLabelText.Text = "Average:";
            // 
            // handicapPanel
            // 
            this.handicapPanel.Controls.Add(this.handicapLabelValue);
            this.handicapPanel.Controls.Add(this.handicapLabelText);
            this.handicapPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.handicapPanel.Location = new System.Drawing.Point(483, 0);
            this.handicapPanel.Name = "handicapPanel";
            this.handicapPanel.Size = new System.Drawing.Size(70, 36);
            this.handicapPanel.TabIndex = 4;
            // 
            // handicapLabelValue
            // 
            this.handicapLabelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.handicapLabelValue.Location = new System.Drawing.Point(0, 15);
            this.handicapLabelValue.Name = "handicapLabelValue";
            this.handicapLabelValue.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.handicapLabelValue.Size = new System.Drawing.Size(70, 21);
            this.handicapLabelValue.TabIndex = 1;
            this.handicapLabelValue.Text = "20";
            this.handicapLabelValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // handicapLabelText
            // 
            this.handicapLabelText.Dock = System.Windows.Forms.DockStyle.Top;
            this.handicapLabelText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.handicapLabelText.Location = new System.Drawing.Point(0, 0);
            this.handicapLabelText.Name = "handicapLabelText";
            this.handicapLabelText.Size = new System.Drawing.Size(70, 15);
            this.handicapLabelText.TabIndex = 0;
            this.handicapLabelText.Text = "Handicap:";
            // 
            // SquadRegistrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.handicapPanel);
            this.Controls.Add(this.averagePanel);
            this.Controls.Add(this.divisionPanel);
            this.Controls.Add(this.bowlerNameLabel);
            this.Controls.Add(this.laneAssignmentLabel);
            this.Name = "SquadRegistrationControl";
            this.Size = new System.Drawing.Size(561, 36);
            this.divisionPanel.ResumeLayout(false);
            this.averagePanel.ResumeLayout(false);
            this.handicapPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private Label laneAssignmentLabel;
    private Label bowlerNameLabel;
    private Panel divisionPanel;
    private Label divisionLabelValue;
    private Label divisionLabelText;
    private Panel averagePanel;
    private Label averageLabelValue;
    private Label averageLabelText;
    private Panel handicapPanel;
    private Label handicapLabelValue;
    private Label handicapLabelText;
}
