namespace NortheastMegabuck.Controls;

partial class RecapSheetGameTotalControl
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
            this.scratchScoreLabel = new System.Windows.Forms.Label();
            this.handicapLabel = new System.Windows.Forms.Label();
            this.handicapTotalLabel = new System.Windows.Forms.Label();
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
            this.laneLabel.Text = "Totals";
            this.laneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scratchScoreLabel
            // 
            this.scratchScoreLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scratchScoreLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.scratchScoreLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.scratchScoreLabel.Location = new System.Drawing.Point(74, 0);
            this.scratchScoreLabel.Name = "scratchScoreLabel";
            this.scratchScoreLabel.Size = new System.Drawing.Size(135, 48);
            this.scratchScoreLabel.TabIndex = 1;
            this.scratchScoreLabel.Text = "Score";
            this.scratchScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // handicapLabel
            // 
            this.handicapLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handicapLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.handicapLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.handicapLabel.Location = new System.Drawing.Point(209, 0);
            this.handicapLabel.Name = "handicapLabel";
            this.handicapLabel.Size = new System.Drawing.Size(75, 48);
            this.handicapLabel.TabIndex = 2;
            this.handicapLabel.Text = "Handicap";
            this.handicapLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // handicapTotalLabel
            // 
            this.handicapTotalLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handicapTotalLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.handicapTotalLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.handicapTotalLabel.Location = new System.Drawing.Point(284, 0);
            this.handicapTotalLabel.Name = "handicapTotalLabel";
            this.handicapTotalLabel.Size = new System.Drawing.Size(135, 48);
            this.handicapTotalLabel.TabIndex = 3;
            this.handicapTotalLabel.Text = "Total";
            this.handicapTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecapSheetGameTotalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.handicapTotalLabel);
            this.Controls.Add(this.handicapLabel);
            this.Controls.Add(this.scratchScoreLabel);
            this.Controls.Add(this.laneLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RecapSheetGameTotalControl";
            this.Size = new System.Drawing.Size(419, 48);
            this.ResumeLayout(false);

    }

    #endregion

    private Label laneLabel;
    private Label scratchScoreLabel;
    private Label handicapLabel;
    private Label handicapTotalLabel;
}
