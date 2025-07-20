using System.Collections.Concurrent;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class InMemoryRateLimiterService
    : IRateLimiterService
{
    private readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> _counters = new();

    public Task<bool> IsRequestAllowedAsync(string key, int permitLimit, TimeSpan window)
    {
        var now = DateTime.UtcNow;
        var (Count, WindowStart) = _counters.GetOrAdd(key, _ => (0, now));

        if (now - WindowStart > window)
        {
            _counters[key] = (1, now);
            return Task.FromResult(true);
        }

        if (Count < permitLimit)
        {
            _counters[key] = (Count + 1, WindowStart);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}

internal interface IRateLimiterService
{
    Task<bool> IsRequestAllowedAsync(string key, int permitLimit, TimeSpan window);
}