namespace NewEnglandClassic.Divisions.Retrieve;

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
            this.divisionsGrid1 = new NewEnglandClassic.Divisions.DivisionsGrid();
            this.SuspendLayout();
            // 
            // divisionsGrid1
            // 
            this.divisionsGrid1.AllowRowSelection = true;
            this.divisionsGrid1.AlternateRowColors = true;
            this.divisionsGrid1.Location = new System.Drawing.Point(35, 106);
            this.divisionsGrid1.Name = "divisionsGrid1";
            this.divisionsGrid1.SelectedRowContextMenu = null;
            this.divisionsGrid1.Size = new System.Drawing.Size(907, 377);
            this.divisionsGrid1.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 798);
            this.Controls.Add(this.divisionsGrid1);
            this.Name = "Form";
            this.Text = "RetrieveDivisionsForm";
            this.ResumeLayout(false);

    }

    #endregion

    private DivisionsGrid divisionsGrid1;
}