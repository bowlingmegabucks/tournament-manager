namespace NortheastMegabuck.Sweepers.Cut;

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
            this.resultsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // resultsFlowLayoutPanel
            // 
            this.resultsFlowLayoutPanel.AutoScroll = true;
            this.resultsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.resultsFlowLayoutPanel.Name = "resultsFlowLayoutPanel";
            this.resultsFlowLayoutPanel.Size = new System.Drawing.Size(579, 637);
            this.resultsFlowLayoutPanel.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 637);
            this.Controls.Add(this.resultsFlowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sweeper Cut";
            this.ResumeLayout(false);

    }

    #endregion

    private FlowLayoutPanel resultsFlowLayoutPanel;
}