namespace NortheastMegabuck.Controls;

public partial class ProcessingMessage : UserControl
{
    private readonly CancellationTokenSource _cancelToken;

    /// <summary>
    ///
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancelToken"></param>
    public ProcessingMessage(string message, CancellationTokenSource cancelToken)
    {
        InitializeComponent();
        _cancelToken = cancelToken;

        messageLabel.Text = message;

        if (string.IsNullOrWhiteSpace(message))
        {
            messageLabel.Visible = false;
            processingPicture.Location = new Point(processingPicture.Location.X, processingPicture.Location.Y - messageLabel.Height / 2);
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
        => _cancelToken.Cancel();
}
