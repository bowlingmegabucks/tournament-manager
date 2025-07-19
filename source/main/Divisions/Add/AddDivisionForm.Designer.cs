namespace BowlingMegabucks.TournamentManager.Divisions.Add;

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
        cancelButton = new Button();
        saveButton = new Button();
        newDivision = new Controls.DivisionControl();
        SuspendLayout();
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 502);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 4;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(357, 502);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 3;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // newDivision
        // 
        newDivision.DivisionName = "";
        newDivision.Gender = null;
        newDivision.HandicapBase = null;
        newDivision.HandicapPercentage = null;
        newDivision.Location = new Point(1, 12);
        newDivision.MaximumAge = null;
        newDivision.MaximumAverage = null;
        newDivision.MaximumHandicapPerGame = null;
        newDivision.MinimumAge = null;
        newDivision.MinimumAverage = null;
        newDivision.Name = "newDivision";
        newDivision.Size = new Size(463, 484);
        newDivision.TabIndex = 5;
        // 
        // Form
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(476, 542);
        Controls.Add(newDivision);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "New Division";
        ResumeLayout(false);
    }

    #endregion
    private Button cancelButton;
    private Button saveButton;
    private Controls.DivisionControl newDivision;
}
