namespace NewEnglandClassic.Squads.Retrieve;

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
            this.SquadsGrid = new NewEnglandClassic.Contols.SquadsGrid();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SquadsGrid
            // 
            this.SquadsGrid.AllowRowSelection = true;
            this.SquadsGrid.AlternateRowColors = true;
            this.SquadsGrid.Location = new System.Drawing.Point(12, 12);
            this.SquadsGrid.Name = "SquadsGrid";
            this.SquadsGrid.SelectedRowContextMenu = null;
            this.SquadsGrid.Size = new System.Drawing.Size(654, 377);
            this.SquadsGrid.TabIndex = 0;
            this.SquadsGrid.GridRowDoubleClicked += new System.EventHandler<NewEnglandClassic.Controls.GridRowDoubleClickEventArgs>(this.SquadsGrid_GridRowDoubleClicked);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(12, 395);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 4;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Location = new System.Drawing.Point(591, 395);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(75, 23);
            this.ButtonOpen.TabIndex = 5;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 463);
            this.Controls.Add(this.ButtonOpen);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.SquadsGrid);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tournament Squads";
            this.ResumeLayout(false);

    }

    #endregion

    private Contols.SquadsGrid SquadsGrid;
    private Button ButtonAdd;
    private Button ButtonOpen;
}