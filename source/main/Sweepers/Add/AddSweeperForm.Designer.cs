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
        newSweeper = new Controls.SweeperControl();
        cancelButton = new Button();
        saveButton = new Button();
        SuspendLayout();
        // 
        // newSweeper
        // 
        newSweeper.AutoSize = true;
        newSweeper.CashRatio = new decimal(new int[] { 0, 0, 0, 0 });
        newSweeper.Complete = false;
        newSweeper.Date = new DateTime(2022, 6, 10, 20, 43, 13, 850);
        newSweeper.EntryFee = new decimal(new int[] { 0, 0, 0, 0 });
        newSweeper.Games = 0;
        newSweeper.Location = new Point(12, 12);
        newSweeper.MaxPerPair = 0;
        newSweeper.Name = "newSweeper";
        newSweeper.NumberOfLanes = 1;
        newSweeper.Size = new Size(546, 516);
        newSweeper.StartingLane = 1;
        newSweeper.TabIndex = 0;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 534);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 4;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(483, 534);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 3;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // Form
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(578, 567);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(newSweeper);
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Add Sweeper";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.SweeperControl newSweeper;
    private Button cancelButton;
    private Button saveButton;
}