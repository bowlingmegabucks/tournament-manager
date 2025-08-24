namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

internal sealed record HealthCheckResponse(string Name, IReadOnlyDictionary<string, HealthCheckDetail> Details);
