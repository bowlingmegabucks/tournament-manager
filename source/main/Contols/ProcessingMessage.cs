namespace NewEnglandClassic.Controls;

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

        LabelMessage.Text = message;

        if (string.IsNullOrWhiteSpace(message))
        {
            LabelMessage.Visible = false;
            PictureBoxProcessing.Location = new Point(PictureBoxProcessing.Location.X, PictureBoxProcessing.Location.Y - LabelMessage.Height / 2);
        }
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
        => _cancelToken.Cancel();
}
