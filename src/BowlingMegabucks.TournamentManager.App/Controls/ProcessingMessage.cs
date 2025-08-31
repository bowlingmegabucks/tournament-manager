using System.Runtime.Versioning;
using System.Windows.Forms;

namespace BowlingMegabucks.TournamentManager.App.Controls;

[SupportedOSPlatform("windows")]
internal sealed partial class ProcessingMessage : UserControl
{
    private readonly CancellationTokenSource _cancelToken;
    private string _message
        => messageLabel.Text;

    public ProcessingMessage(string message, CancellationTokenSource cancelToken)
    {
        InitializeComponent();
        _cancelToken = cancelToken;

        messageLabel.Text = message;
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        _cancelToken.Cancel();

        MessageBox.Show("Operation has been canceled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ProcessingMessage_SizeChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_message))
        {
            messageLabel.Visible = false;
            processingPicture.Location = new Point(processingPicture.Location.X, processingPicture.Location.Y - (messageLabel.Height / 2));
        }

        // Center the picture
        processingPicture.Location = new Point((Width - processingPicture.Width) / 2, (Height - processingPicture.Height) / 2);

        cancelButton.Location = new Point(Width - 100, Height - 26);
    }
}
