using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingMegabucks.TournamentManager.App;

internal static class FormExtensions
{
    public static void AddProcessingMessage(this Form form, string message, CancellationTokenSource cancellationTokenSource)
    {
        var processingMessage = new Controls.ProcessingMessage(message, cancellationTokenSource);

        form.Controls.Add(processingMessage);
        processingMessage.BringToFront();
        processingMessage.Dock = DockStyle.Fill;
        processingMessage.Show();
        processingMessage.Focus();
    }

    public static void RemoveProcessingMessage(this Form form)
    {
        Controls.ProcessingMessage? processingMessage = form.Controls.OfType<Controls.ProcessingMessage>().FirstOrDefault();

        if (processingMessage is not null)
        {
            form.Controls.Remove(processingMessage);
            processingMessage.Dispose();
        }
    }
}
