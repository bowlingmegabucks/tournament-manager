namespace NortheastMegabuck.Controls;

partial class RecapSheetGameRowControl
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
            this.laneLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.handicapLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // laneLabel
            // 
            this.laneLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.laneLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.laneLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.laneLabel.Location = new System.Drawing.Point(0, 0);
            this.laneLabel.Name = "laneLabel";
            this.laneLabel.Size = new System.Drawing.Size(74, 48);
            this.laneLabel.TabIndex = 0;
            this.laneLabel.Text = "Lane";
            this.laneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreLabel
            // 
            this.scoreLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scoreLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.scoreLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.scoreLabel.Location = new System.Drawing.Point(74, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(135, 48);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Score";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHandicap
            // 
            this.handicapLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handicapLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.handicapLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.handicapLabel.Location = new System.Drawing.Point(209, 0);
            this.handicapLabel.Name = "labelHandicap";
            this.handicapLabel.Size = new System.Drawing.Size(75, 48);
            this.handicapLabel.TabIndex = 2;
            this.handicapLabel.Text = "Handicap";
            this.handicapLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTotal
            // 
            this.totalLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.totalLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.totalLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalLabel.Location = new System.Drawing.Point(284, 0);
            this.totalLabel.Name = "labelTotal";
            this.totalLabel.Size = new System.Drawing.Size(135, 48);
            this.totalLabel.TabIndex = 3;
            this.totalLabel.Text = "Total";
            this.totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecapSheetGameRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.handicapLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.laneLabel);
            this.Name = "RecapSheetGameRowControl";
            this.Size = new System.Drawing.Size(419, 48);
            this.ResumeLayout(false);

    }

    #endregion

    private Label laneLabel;
    private Label scoreLabel;
    private Label handicapLabel;
    private Label totalLabel;
}
