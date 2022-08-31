namespace NortheastMegabuck.Squads.Portal;

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
            this.portalMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // portalMenuStrip
            // 
            this.portalMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.portalMenuStrip.Name = "portalMenuStrip";
            this.portalMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.portalMenuStrip.TabIndex = 0;
            this.portalMenuStrip.Text = "menuStrip1";
            // 
            // SquadPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.portalMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.portalMenuStrip;
            this.MaximizeBox = false;
            this.Name = "SquadPortal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SquadPortal";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private MenuStrip portalMenuStrip;
}