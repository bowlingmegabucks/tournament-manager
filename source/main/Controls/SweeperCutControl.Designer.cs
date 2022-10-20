namespace NortheastMegabuck.Controls;

partial class SweeperCutControl
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
            this.placeLabel = new System.Windows.Forms.Label();
            this.bowlerNameLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.highGameLabel = new System.Windows.Forms.Label();
            this.cashingPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cashingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // placeLabel
            // 
            this.placeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.placeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.placeLabel.Location = new System.Drawing.Point(0, 0);
            this.placeLabel.Name = "placeLabel";
            this.placeLabel.Size = new System.Drawing.Size(57, 42);
            this.placeLabel.TabIndex = 0;
            this.placeLabel.Text = "888";
            this.placeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bowlerNameLabel
            // 
            this.bowlerNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.bowlerNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bowlerNameLabel.Location = new System.Drawing.Point(57, 0);
            this.bowlerNameLabel.Name = "bowlerNameLabel";
            this.bowlerNameLabel.Size = new System.Drawing.Size(347, 42);
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
            this.scoreLabel.Size = new System.Drawing.Size(57, 42);
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
            this.highGameLabel.Size = new System.Drawing.Size(57, 42);
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
            this.cashingPictureBox.Size = new System.Drawing.Size(61, 42);
            this.cashingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cashingPictureBox.TabIndex = 4;
            this.cashingPictureBox.TabStop = false;
            // 
            // SweeperCutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cashingPictureBox);
            this.Controls.Add(this.highGameLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.bowlerNameLabel);
            this.Controls.Add(this.placeLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SweeperCutControl";
            this.Size = new System.Drawing.Size(581, 42);
            ((System.ComponentModel.ISupportInitialize)(this.cashingPictureBox)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private Label placeLabel;
    private Label bowlerNameLabel;
    private Label scoreLabel;
    private Label highGameLabel;
    private PictureBox cashingPictureBox;
}
