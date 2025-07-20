using System.Collections.Concurrent;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class InMemoryRateLimiterService
    : IRateLimiterService
{
    private readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> _counters = new();
    private readonly Timer _cleanupTimer;

    public InMemoryRateLimiterService()
    {
        _cleanupTimer = new Timer(CleanupExpiredEntries, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
    }

    private void CleanupExpiredEntries(object? state)
    {
        var now = DateTime.UtcNow;
        foreach (var key in _counters.Keys)
        {
            if (_counters.TryGetValue(key, out var value) && now - value.WindowStart > TimeSpan.FromMinutes(1))
            {
                _counters.TryRemove(key, out _);
            }
        }
    }

    public void Dispose()
    {
        _cleanupTimer.Dispose();
    }
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