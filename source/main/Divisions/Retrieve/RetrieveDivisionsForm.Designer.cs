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
            this.DivisionsGrid = new NewEnglandClassic.Controls.DivisionsGrid();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DivisionsGrid
            // 
            this.DivisionsGrid.AllowRowSelection = true;
            this.DivisionsGrid.AlternateRowColors = true;
            this.DivisionsGrid.Location = new System.Drawing.Point(12, 12);
            this.DivisionsGrid.Name = "DivisionsGrid";
            this.DivisionsGrid.SelectedRowContextMenu = null;
            this.DivisionsGrid.Size = new System.Drawing.Size(884, 377);
            this.DivisionsGrid.TabIndex = 0;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(12, 395);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 3;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 443);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.DivisionsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Divisions";
            this.ResumeLayout(false);

    }

    #endregion

    private Controls.DivisionsGrid DivisionsGrid;
    private Button ButtonAdd;
}