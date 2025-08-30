using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BowlingMegabucks.TournamentManager.Tests.Infrastructure;


public class InvalidDatabaseWebAppFactory<T>
    : WebApplicationFactory<T>, IAsyncDisposable
    where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // Use an invalid connection string that will cause database connection failures
        builder.UseSetting("ConnectionStrings:TournamentManager",
            "Server=nonexistent-server;Database=nonexistent-db;User=invalid;Password=invalid;Port=99999;Connection Timeout=1;");
    }

    public new async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}
