using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace BowlingMegabucks.TournamentManager.Api.Middleware;

internal sealed class InMemoryRateLimiterService
: IRateLimiterService, IDisposable
{
    private readonly TimeSpan _windowDuration;
    private readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> _counters = new();
    private readonly Timer _cleanupTimer;

    public InMemoryRateLimiterService(IOptions<RateLimitingOptions> options)
    {
        _windowDuration = TimeSpan.FromSeconds(options.Value.WindowSeconds);
        _cleanupTimer = new Timer(CleanupExpiredEntries, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
    }

    private void CleanupExpiredEntries(object? state)
    {
        var now = DateTime.UtcNow;
        foreach (var key in _counters.Keys)
        {
            if (_counters.TryGetValue(key, out var value) && now - value.WindowStart > _windowDuration)
            {
                _counters.TryRemove(key, out _);
            }
        }
    }

    public void Dispose() => _cleanupTimer.Dispose();

    public Task<bool> IsRequestAllowedAsync(string key, int permitLimit, TimeSpan window)
    {
        var now = DateTime.UtcNow;
        var allowed = false;
        _counters.AddOrUpdate(key,
            _ => (1, now),
            (_, old) =>
            {
                if (now - old.WindowStart > window)
                {
                    allowed = true;
                    return (1, now);
                }

                if (old.Count < permitLimit)
                {
                    allowed = true;
                    return (old.Count + 1, old.WindowStart);
                }
                
                allowed = false;
                return old;
            });
        return Task.FromResult(allowed);
    }
}

internal interface IRateLimiterService
{
    Task<bool> IsRequestAllowedAsync(string key, int permitLimit, TimeSpan window);
}