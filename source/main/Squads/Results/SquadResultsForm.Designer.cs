namespace NortheastMegabuck.Squads.Results;

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
            this.divisionsTabControl = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // divisionsTabControl
            // 
            this.divisionsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisionsTabControl.Location = new System.Drawing.Point(0, 0);
            this.divisionsTabControl.Name = "divisionsTabControl";
            this.divisionsTabControl.SelectedIndex = 0;
            this.divisionsTabControl.Size = new System.Drawing.Size(650, 798);
            this.divisionsTabControl.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 798);
            this.Controls.Add(this.divisionsTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Squad Results";
            this.ResumeLayout(false);

    }

    #endregion

    private TabControl divisionsTabControl;
}