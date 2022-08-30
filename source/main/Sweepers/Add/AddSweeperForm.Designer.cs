namespace NortheastMegabuck.Sweepers.Add;

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
            this.newSweeper = new NortheastMegabuck.Contols.SweeperControl();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newSweeper
            // 
            this.newSweeper.AutoSize = true;
            this.newSweeper.CashRatio = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.newSweeper.Complete = false;
            this.newSweeper.Date = new System.DateTime(2022, 6, 10, 20, 43, 13, 850);
            this.newSweeper.EntryFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.newSweeper.Games = ((short)(0));
            this.newSweeper.Location = new System.Drawing.Point(12, 12);
            this.newSweeper.MaxPerPair = ((short)(0));
            this.newSweeper.Name = "newSweeper";
            this.newSweeper.NumberOfLanes = ((short)(1));
            this.newSweeper.Size = new System.Drawing.Size(546, 516);
            this.newSweeper.StartingLane = ((short)(1));
            this.newSweeper.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 534);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(483, 534);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Form
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(578, 567);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.newSweeper);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Sweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Contols.SweeperControl newSweeper;
    private Button cancelButton;
    private Button saveButton;
}