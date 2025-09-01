using Refit;

namespace BowlingMegabucks.TournamentManager.UnitTests.Presentation;

internal static class PresentationTestsExtensions
{
    public static Task<ApiException> AsApiException(this Exception ex, HttpResponseMessage httpResponseMessage)
        => ApiException.Create(
            message: httpResponseMessage.RequestMessage!,
            httpMethod: httpResponseMessage.RequestMessage?.Method ?? HttpMethod.Get,
            response: httpResponseMessage,
            refitSettings: null!,
            innerException: ex
        );
}
