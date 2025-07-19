namespace BowlingMegabucks.TournamentManager.Controls;

partial class SelectSquadControl
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
            this.nameCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // nameCheckBox
            // 
            this.nameCheckBox.AutoSize = true;
            this.nameCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameCheckBox.Location = new System.Drawing.Point(0, 0);
            this.nameCheckBox.Name = "nameCheckBox";
            this.nameCheckBox.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.nameCheckBox.Size = new System.Drawing.Size(237, 45);
            this.nameCheckBox.TabIndex = 0;
            this.nameCheckBox.Text = "DisplayText";
            this.nameCheckBox.UseVisualStyleBackColor = true;
            // 
            // SelectSquadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameCheckBox);
            this.Name = "SelectSquadControl";
            this.Size = new System.Drawing.Size(237, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private CheckBox nameCheckBox;
}
