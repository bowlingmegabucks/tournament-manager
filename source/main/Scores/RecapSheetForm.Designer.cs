namespace BowlingMegabucks.TournamentManager.Scores;

partial class RecapSheetForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(RecapSheetForm));
        buttonsPanel = new Panel();
        recapsTrackBar = new TrackBar();
        cancelButton = new Button();
        printButton = new Button();
        gamesFlowPanelLayout = new FlowLayoutPanel();
        headerPictureBox = new PictureBox();
        dateLabel = new Label();
        timeLabel = new Label();
        nameLabel = new Label();
        divisionLabel = new Label();
        opposingSignatureLabel = new Label();
        bowlerSignatureLabel = new Label();
        scrollRecapsTimer = new System.Windows.Forms.Timer(components);
        recapPrintDocument = new System.Drawing.Printing.PrintDocument();
        recapsPrintDialog = new PrintDialog();
        pictureBox1 = new PictureBox();
        buttonsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)recapsTrackBar).BeginInit();
        gamesFlowPanelLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)headerPictureBox).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // buttonsPanel
        // 
        buttonsPanel.Controls.Add(recapsTrackBar);
        buttonsPanel.Controls.Add(cancelButton);
        buttonsPanel.Controls.Add(printButton);
        buttonsPanel.Dock = DockStyle.Bottom;
        buttonsPanel.Location = new Point(0, 471);
        buttonsPanel.Name = "buttonsPanel";
        buttonsPanel.Size = new Size(771, 34);
        buttonsPanel.TabIndex = 0;
        // 
        // recapsTrackBar
        // 
        recapsTrackBar.Location = new Point(84, 3);
        recapsTrackBar.Name = "recapsTrackBar";
        recapsTrackBar.Size = new Size(594, 45);
        recapsTrackBar.TabIndex = 2;
        recapsTrackBar.Scroll += RecapsTrackBar_Scroll;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(684, 8);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 1;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += CancelButton_Click;
        // 
        // printButton
        // 
        printButton.Location = new Point(3, 8);
        printButton.Name = "printButton";
        printButton.Size = new Size(75, 23);
        printButton.TabIndex = 0;
        printButton.Text = "Print";
        printButton.UseVisualStyleBackColor = true;
        printButton.Click += PrintButton_Click;
        // 
        // gamesFlowPanelLayout
        // 
        gamesFlowPanelLayout.Controls.Add(pictureBox1);
        gamesFlowPanelLayout.Location = new Point(352, 0);
        gamesFlowPanelLayout.Name = "gamesFlowPanelLayout";
        gamesFlowPanelLayout.Size = new Size(419, 471);
        gamesFlowPanelLayout.TabIndex = 1;
        // 
        // headerPictureBox
        // 
        headerPictureBox.Image = (Image)resources.GetObject("headerPictureBox.Image");
        headerPictureBox.Location = new Point(0, 0);
        headerPictureBox.Name = "headerPictureBox";
        headerPictureBox.Size = new Size(352, 175);
        headerPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        headerPictureBox.TabIndex = 2;
        headerPictureBox.TabStop = false;
        // 
        // dateLabel
        // 
        dateLabel.Font = new Font("Calibri", 12F);
        dateLabel.Location = new Point(12, 178);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new Size(100, 23);
        dateLabel.TabIndex = 3;
        dateLabel.Text = "88/88/8888";
        dateLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // timeLabel
        // 
        timeLabel.Font = new Font("Calibri", 12F);
        timeLabel.Location = new Point(246, 178);
        timeLabel.Name = "timeLabel";
        timeLabel.Size = new Size(100, 23);
        timeLabel.TabIndex = 4;
        timeLabel.Text = "88:88 WW";
        timeLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // nameLabel
        // 
        nameLabel.Font = new Font("Calibri", 15F, FontStyle.Bold);
        nameLabel.Location = new Point(12, 201);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(334, 40);
        nameLabel.TabIndex = 5;
        nameLabel.Text = "Joe Bowler";
        nameLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // divisionLabel
        // 
        divisionLabel.Font = new Font("Calibri", 13F);
        divisionLabel.Location = new Point(12, 241);
        divisionLabel.Name = "divisionLabel";
        divisionLabel.Size = new Size(334, 40);
        divisionLabel.TabIndex = 6;
        divisionLabel.Text = "Division";
        divisionLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // opposingSignatureLabel
        // 
        opposingSignatureLabel.Font = new Font("Calibri", 11.25F);
        opposingSignatureLabel.Location = new Point(3, 445);
        opposingSignatureLabel.Name = "opposingSignatureLabel";
        opposingSignatureLabel.Size = new Size(193, 23);
        opposingSignatureLabel.TabIndex = 7;
        opposingSignatureLabel.Text = "Opposing Bowler's Signature";
        opposingSignatureLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // bowlerSignatureLabel
        // 
        bowlerSignatureLabel.Font = new Font("Calibri", 13F);
        bowlerSignatureLabel.Location = new Point(3, 373);
        bowlerSignatureLabel.Name = "bowlerSignatureLabel";
        bowlerSignatureLabel.Size = new Size(225, 23);
        bowlerSignatureLabel.TabIndex = 8;
        bowlerSignatureLabel.Text = "Bowler's Signature";
        bowlerSignatureLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // scrollRecapsTimer
        // 
        scrollRecapsTimer.Interval = 50;
        scrollRecapsTimer.Tick += ScrollRecapsTimer_Tick;
        // 
        // recapPrintDocument
        // 
        recapPrintDocument.EndPrint += RecapPrintDocument_EndPrint;
        recapPrintDocument.PrintPage += RecapPrintDocument_PrintPage;
        // 
        // recapsPrintDialog
        // 
        recapsPrintDialog.Document = recapPrintDocument;
        recapsPrintDialog.UseEXDialog = true;
        // 
        // pictureBox1
        // 
        pictureBox1.Dock = DockStyle.Bottom;
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(3, 3);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(352, 175);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 3;
        pictureBox1.TabStop = false;
        // 
        // RecapSheetForm
        // 
        BackColor = Color.White;
        ClientSize = new Size(771, 505);
        Controls.Add(bowlerSignatureLabel);
        Controls.Add(opposingSignatureLabel);
        Controls.Add(divisionLabel);
        Controls.Add(nameLabel);
        Controls.Add(timeLabel);
        Controls.Add(dateLabel);
        Controls.Add(headerPictureBox);
        Controls.Add(gamesFlowPanelLayout);
        Controls.Add(buttonsPanel);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "RecapSheetForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Recap Sheets";
        Paint += RecapSheetForm_Paint;
        buttonsPanel.ResumeLayout(false);
        buttonsPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)recapsTrackBar).EndInit();
        gamesFlowPanelLayout.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)headerPictureBox).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
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
		recapsPrintDialog.Dispose();
		recapPrintDocument.Dispose(); ;

		base.Dispose(disposing);
	}

	private PictureBox headerPictureBox;
	private Label dateLabel;
	private Label timeLabel;
	private Label nameLabel;
	private Label divisionLabel;
	private Label opposingSignatureLabel;
	private Label bowlerSignatureLabel;
	private System.Windows.Forms.Timer scrollRecapsTimer;
	private System.Drawing.Printing.PrintDocument recapPrintDocument;
    private PrintDialog recapsPrintDialog;
    private PictureBox pictureBox1;
}