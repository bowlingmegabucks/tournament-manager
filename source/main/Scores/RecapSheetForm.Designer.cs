namespace NortheastMegabuck.Scores;

partial class RecapSheetForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;
	
	private void InitializeComponent()
	{
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.recapsTrackBar = new System.Windows.Forms.TrackBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.gamesFlowPanelLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.headerPictureBox = new System.Windows.Forms.PictureBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.divisionLabel = new System.Windows.Forms.Label();
            this.opposingSignatureLabel = new System.Windows.Forms.Label();
            this.bowlerSignatureLabel = new System.Windows.Forms.Label();
            this.buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recapsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.recapsTrackBar);
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Controls.Add(this.printButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 471);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(771, 34);
            this.buttonsPanel.TabIndex = 0;
            // 
            // recapsTrackBar
            // 
            this.recapsTrackBar.Location = new System.Drawing.Point(84, 3);
            this.recapsTrackBar.Name = "recapsTrackBar";
            this.recapsTrackBar.Size = new System.Drawing.Size(594, 45);
            this.recapsTrackBar.TabIndex = 2;
            this.recapsTrackBar.Scroll += new System.EventHandler(this.RecapsTrackBar_Scroll);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(684, 8);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(3, 8);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(75, 23);
            this.printButton.TabIndex = 0;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // gamesFlowPanelLayout
            // 
            this.gamesFlowPanelLayout.Dock = System.Windows.Forms.DockStyle.Right;
            this.gamesFlowPanelLayout.Location = new System.Drawing.Point(352, 0);
            this.gamesFlowPanelLayout.Name = "gamesFlowPanelLayout";
            this.gamesFlowPanelLayout.Size = new System.Drawing.Size(419, 471);
            this.gamesFlowPanelLayout.TabIndex = 1;
            // 
            // headerPictureBox
            // 
            this.headerPictureBox.Image = global::NortheastMegabuck.Properties.Resources.NMT_Header;
            this.headerPictureBox.Location = new System.Drawing.Point(0, 0);
            this.headerPictureBox.Name = "headerPictureBox";
            this.headerPictureBox.Size = new System.Drawing.Size(352, 80);
            this.headerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.headerPictureBox.TabIndex = 2;
            this.headerPictureBox.TabStop = false;
            // 
            // dateLabel
            // 
            this.dateLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateLabel.Location = new System.Drawing.Point(12, 83);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(100, 23);
            this.dateLabel.TabIndex = 3;
            this.dateLabel.Text = "88/88/8888";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timeLabel
            // 
            this.timeLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeLabel.Location = new System.Drawing.Point(246, 83);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(100, 23);
            this.timeLabel.TabIndex = 4;
            this.timeLabel.Text = "88:88 WW";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nameLabel.Location = new System.Drawing.Point(12, 106);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(334, 40);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "Joe Bowler";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // divisionLabel
            // 
            this.divisionLabel.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.divisionLabel.Location = new System.Drawing.Point(12, 146);
            this.divisionLabel.Name = "divisionLabel";
            this.divisionLabel.Size = new System.Drawing.Size(334, 40);
            this.divisionLabel.TabIndex = 6;
            this.divisionLabel.Text = "Divsion";
            this.divisionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // opposingSignatureLabel
            // 
            this.opposingSignatureLabel.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.opposingSignatureLabel.Location = new System.Drawing.Point(3, 445);
            this.opposingSignatureLabel.Name = "opposingSignatureLabel";
            this.opposingSignatureLabel.Size = new System.Drawing.Size(349, 23);
            this.opposingSignatureLabel.TabIndex = 7;
            this.opposingSignatureLabel.Text = "Opposing Bowler\'s Signature";
            this.opposingSignatureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bowlerSignatureLabel
            // 
            this.bowlerSignatureLabel.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bowlerSignatureLabel.Location = new System.Drawing.Point(3, 373);
            this.bowlerSignatureLabel.Name = "bowlerSignatureLabel";
            this.bowlerSignatureLabel.Size = new System.Drawing.Size(349, 23);
            this.bowlerSignatureLabel.TabIndex = 8;
            this.bowlerSignatureLabel.Text = "Bowler\'s Signature";
            this.bowlerSignatureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RecapSheetForm
            // 
            this.ClientSize = new System.Drawing.Size(771, 505);
            this.Controls.Add(this.bowlerSignatureLabel);
            this.Controls.Add(this.opposingSignatureLabel);
            this.Controls.Add(this.divisionLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.headerPictureBox);
            this.Controls.Add(this.gamesFlowPanelLayout);
            this.Controls.Add(this.buttonsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RecapSheetForm";
            this.Text = "Recap Sheets";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RecapSheetForm_Paint);
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recapsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerPictureBox)).EndInit();
            this.ResumeLayout(false);

	}

	private Panel buttonsPanel;
	private Button printButton;
	private Button cancelButton;
	private TrackBar recapsTrackBar;
	private FlowLayoutPanel gamesFlowPanelLayout;

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

		_pen.Dispose();

		base.Dispose(disposing);
	}

	private PictureBox headerPictureBox;
	private Label dateLabel;
	private Label timeLabel;
	private Label nameLabel;
	private Label divisionLabel;
	private Label opposingSignatureLabel;
	private Label bowlerSignatureLabel;
}