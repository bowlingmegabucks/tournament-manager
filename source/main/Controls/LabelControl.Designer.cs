
namespace NortheastMegabuck.Controls;

/// <summary>
/// 
/// </summary>
partial class LabelControl
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
            this.PanelLabel = new System.Windows.Forms.Panel();
            this.LabelText = new System.Windows.Forms.Label();
            this.PanelRequired = new System.Windows.Forms.Panel();
            this.LabelAsterisk = new System.Windows.Forms.Label();
            this.PanelLabel.SuspendLayout();
            this.PanelRequired.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelLabel
            // 
            this.PanelLabel.AutoSize = true;
            this.PanelLabel.Controls.Add(this.LabelText);
            this.PanelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelLabel.Location = new System.Drawing.Point(0, 0);
            this.PanelLabel.Name = "PanelLabel";
            this.PanelLabel.Size = new System.Drawing.Size(136, 20);
            this.PanelLabel.TabIndex = 0;
            // 
            // LabelText
            // 
            this.LabelText.AutoSize = true;
            this.LabelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelText.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelText.Location = new System.Drawing.Point(0, 0);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(117, 19);
            this.LabelText.TabIndex = 0;
            this.LabelText.Text = "LabelControl";
            this.LabelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelText.TextChanged += new System.EventHandler(this.LabelText_TextChanged);
            // 
            // PanelRequired
            // 
            this.PanelRequired.AutoSize = true;
            this.PanelRequired.Controls.Add(this.LabelAsterisk);
            this.PanelRequired.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelRequired.Location = new System.Drawing.Point(120, 0);
            this.PanelRequired.Name = "PanelRequired";
            this.PanelRequired.Size = new System.Drawing.Size(16, 20);
            this.PanelRequired.TabIndex = 0;
            this.PanelRequired.VisibleChanged += new System.EventHandler(this.PanelRequired_VisibleChanged);
            // 
            // LabelAsterisk
            // 
            this.LabelAsterisk.AutoSize = true;
            this.LabelAsterisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelAsterisk.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelAsterisk.ForeColor = System.Drawing.Color.Red;
            this.LabelAsterisk.Location = new System.Drawing.Point(0, 0);
            this.LabelAsterisk.Name = "LabelAsterisk";
            this.LabelAsterisk.Size = new System.Drawing.Size(16, 18);
            this.LabelAsterisk.TabIndex = 0;
            this.LabelAsterisk.Text = "*";
            // 
            // LabelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.PanelRequired);
            this.Controls.Add(this.PanelLabel);
            this.Name = "LabelControl";
            this.Size = new System.Drawing.Size(136, 20);
            this.PanelLabel.ResumeLayout(false);
            this.PanelLabel.PerformLayout();
            this.PanelRequired.ResumeLayout(false);
            this.PanelRequired.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel PanelLabel;
    private System.Windows.Forms.Label LabelText;
    private System.Windows.Forms.Panel PanelRequired;
    private System.Windows.Forms.Label LabelAsterisk;
}
