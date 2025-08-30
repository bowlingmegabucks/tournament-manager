using System.Collections;
using ErrorOr;
using Refit;

namespace BowlingMegabucks.TournamentManager.Presentation.Services;

internal abstract class TournamentManagerAdapter
{
    protected readonly ITournamentManagerApi _tournamentManagerApi;

    protected TournamentManagerAdapter(ITournamentManagerApi tournamentManagerApi)
    {
        _tournamentManagerApi = tournamentManagerApi;
    }

    protected static Error GenerateError(ApiException ex, string errorCode, string errorDescription)
        => Error.Failure(
            code: errorCode,
            description: errorDescription,
            metadata: ex.Data
                .Cast<DictionaryEntry>()
                .Where(entry => entry.Key is string && entry.Value is not null)
                .ToDictionary(
                    entry => (string)entry.Key,
                    entry => entry.Value!,
                    StringComparer.Ordinal));
}
