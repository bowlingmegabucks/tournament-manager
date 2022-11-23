namespace NortheastMegabuck.Controls;

partial class TournamentSeedingControl
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
            this.components = new System.ComponentModel.Container();
            this.seedLabel = new System.Windows.Forms.Label();
            this.bowlerNameLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.highGameLabel = new System.Windows.Forms.Label();
            this.scratchToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cashingPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cashingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // seedLabel
            // 
            this.seedLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.seedLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.seedLabel.Location = new System.Drawing.Point(0, 0);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(57, 33);
            this.seedLabel.TabIndex = 0;
            this.seedLabel.Text = "888";
            this.seedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bowlerNameLabel
            // 
            this.bowlerNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.bowlerNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bowlerNameLabel.Location = new System.Drawing.Point(57, 0);
            this.bowlerNameLabel.Name = "bowlerNameLabel";
            this.bowlerNameLabel.Size = new System.Drawing.Size(347, 33);
            this.bowlerNameLabel.TabIndex = 1;
            this.bowlerNameLabel.Text = "Bowler Name";
            this.bowlerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scoreLabel
            // 
            this.scoreLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.scoreLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.scoreLabel.Location = new System.Drawing.Point(404, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(57, 33);
            this.scoreLabel.TabIndex = 2;
            this.scoreLabel.Text = "8888";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // highGameLabel
            // 
            this.highGameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.highGameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.highGameLabel.Location = new System.Drawing.Point(461, 0);
            this.highGameLabel.Name = "highGameLabel";
            this.highGameLabel.Size = new System.Drawing.Size(57, 33);
            this.highGameLabel.TabIndex = 3;
            this.highGameLabel.Text = "888";
            this.highGameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cashingPictureBox
            // 
            this.cashingPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.cashingPictureBox.Image = global::NortheastMegabuck.Properties.Resources.DollarSign;
            this.cashingPictureBox.Location = new System.Drawing.Point(518, 0);
            this.cashingPictureBox.Name = "cashingPictureBox";
            this.cashingPictureBox.Size = new System.Drawing.Size(46, 33);
            this.cashingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cashingPictureBox.TabIndex = 5;
            this.cashingPictureBox.TabStop = false;
            // 
            // TournamentSeedingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cashingPictureBox);
            this.Controls.Add(this.highGameLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.bowlerNameLabel);
            this.Controls.Add(this.seedLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TournamentSeedingControl";
            this.Size = new System.Drawing.Size(568, 33);
            ((System.ComponentModel.ISupportInitialize)(this.cashingPictureBox)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private Label seedLabel;
    private Label bowlerNameLabel;
    private Label scoreLabel;
    private Label highGameLabel;
    private ToolTip scratchToolTip;
    private PictureBox cashingPictureBox;
}
