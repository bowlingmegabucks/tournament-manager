namespace NortheastMegabuck.Bowlers.Update;

partial class NameForm
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
        personNameControl = new Controls.PersonNameControl();
        cancelButton = new Button();
        saveButton = new Button();
        SuspendLayout();
        // 
        // personNameControl
        // 
        personNameControl.First = "";
        personNameControl.Last = "";
        personNameControl.Location = new Point(12, 12);
        personNameControl.MiddleInitial = "";
        personNameControl.Name = "personNameControl";
        personNameControl.Size = new Size(566, 63);
        personNameControl.Suffix = "";
        personNameControl.TabIndex = 0;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 81);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 4;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(562, 81);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 3;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // NameForm
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(649, 116);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(personNameControl);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "NameForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Update Bowler Name";
        ResumeLayout(false);
    }

    #endregion

    private Controls.PersonNameControl personNameControl;
    private Button cancelButton;
    private Button saveButton;
}