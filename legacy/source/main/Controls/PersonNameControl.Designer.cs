namespace BowlingMegabucks.TournamentManager.Controls;

partial class PersonNameControl
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
        components = new System.ComponentModel.Container();
        middleInitialLabel = new LabelControl();
        suffixLabel = new LabelControl();
        lastNameLabel = new LabelControl();
        firstNameLabel = new LabelControl();
        middleInitialText = new TextBox();
        suffixText = new TextBox();
        lastNameText = new TextBox();
        firstNameText = new TextBox();
        personNameErrorProvider = new ErrorProvider(components);
        ((System.ComponentModel.ISupportInitialize)personNameErrorProvider).BeginInit();
        SuspendLayout();
        // 
        // middleInitialLabel
        // 
        middleInitialLabel.AutoSize = true;
        middleInitialLabel.Bold = false;
        middleInitialLabel.Location = new Point(420, 3);
        middleInitialLabel.Margin = new Padding(15, 3, 15, 3);
        middleInitialLabel.Name = "middleInitialLabel";
        middleInitialLabel.Required = false;
        middleInitialLabel.Size = new Size(144, 19);
        middleInitialLabel.TabIndex = 30;
        middleInitialLabel.TabStop = false;
        middleInitialLabel.Text = "Middle Initial:";
        // 
        // suffixLabel
        // 
        suffixLabel.AutoSize = true;
        suffixLabel.Bold = false;
        suffixLabel.Location = new Point(318, 3);
        suffixLabel.Margin = new Padding(15, 3, 15, 3);
        suffixLabel.Name = "suffixLabel";
        suffixLabel.Required = false;
        suffixLabel.Size = new Size(117, 19);
        suffixLabel.TabIndex = 29;
        suffixLabel.TabStop = false;
        suffixLabel.Text = "Suffix:";
        // 
        // lastNameLabel
        // 
        lastNameLabel.AutoSize = true;
        lastNameLabel.Bold = false;
        lastNameLabel.Location = new Point(162, 3);
        lastNameLabel.Margin = new Padding(15, 3, 15, 3);
        lastNameLabel.Name = "lastNameLabel";
        lastNameLabel.Required = true;
        lastNameLabel.Size = new Size(105, 19);
        lastNameLabel.TabIndex = 28;
        lastNameLabel.TabStop = false;
        lastNameLabel.Text = "Last Name:";
        // 
        // firstNameLabel
        // 
        firstNameLabel.AutoSize = true;
        firstNameLabel.Bold = false;
        firstNameLabel.Location = new Point(0, 3);
        firstNameLabel.Name = "firstNameLabel";
        firstNameLabel.Required = true;
        firstNameLabel.Size = new Size(114, 19);
        firstNameLabel.TabIndex = 27;
        firstNameLabel.TabStop = false;
        firstNameLabel.Text = "First Name:";
        // 
        // middleInitialText
        // 
        middleInitialText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        middleInitialText.Location = new Point(503, 28);
        middleInitialText.Margin = new Padding(15, 3, 15, 9);
        middleInitialText.MaxLength = 3;
        middleInitialText.Name = "middleInitialText";
        middleInitialText.PlaceholderText = "A";
        middleInitialText.Size = new Size(36, 26);
        middleInitialText.TabIndex = 26;
        // 
        // suffixText
        // 
        suffixText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        suffixText.Location = new Point(339, 28);
        suffixText.Margin = new Padding(15, 3, 15, 9);
        suffixText.MaxLength = 3;
        suffixText.Name = "suffixText";
        suffixText.PlaceholderText = "IV";
        suffixText.Size = new Size(51, 26);
        suffixText.TabIndex = 25;
        // 
        // lastNameText
        // 
        lastNameText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        lastNameText.Location = new Point(162, 28);
        lastNameText.Margin = new Padding(15, 3, 15, 9);
        lastNameText.MaxLength = 25;
        lastNameText.Name = "lastNameText";
        lastNameText.PlaceholderText = "Smith";
        lastNameText.Size = new Size(147, 26);
        lastNameText.TabIndex = 24;
        lastNameText.Validating += LastNameText_Validating;
        // 
        // firstNameText
        // 
        firstNameText.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        firstNameText.Location = new Point(0, 28);
        firstNameText.Margin = new Padding(15, 3, 15, 9);
        firstNameText.MaxLength = 20;
        firstNameText.Name = "firstNameText";
        firstNameText.PlaceholderText = "Joseph";
        firstNameText.Size = new Size(132, 26);
        firstNameText.TabIndex = 23;
        firstNameText.Validating += FirstNameText_Validating;
        // 
        // personNameErrorProvider
        // 
        personNameErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        personNameErrorProvider.ContainerControl = this;
        // 
        // PersonNameControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(middleInitialLabel);
        Controls.Add(suffixLabel);
        Controls.Add(lastNameLabel);
        Controls.Add(firstNameLabel);
        Controls.Add(middleInitialText);
        Controls.Add(suffixText);
        Controls.Add(lastNameText);
        Controls.Add(firstNameText);
        Name = "PersonNameControl";
        Size = new Size(566, 63);
        ((System.ComponentModel.ISupportInitialize)personNameErrorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private LabelControl middleInitialLabel;
    private LabelControl suffixLabel;
    private LabelControl lastNameLabel;
    private LabelControl firstNameLabel;
    private TextBox middleInitialText;
    private TextBox suffixText;
    private TextBox lastNameText;
    private TextBox firstNameText;
    private ErrorProvider personNameErrorProvider;
}
