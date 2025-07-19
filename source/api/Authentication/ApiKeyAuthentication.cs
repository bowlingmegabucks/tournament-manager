using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BowlingMegabucks.TournamentManager.Api.Authentication;

internal sealed class ApiKeyAuthentication
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    internal const string SchemeName = "ApiKey";
    internal const string HeaderName = "x-api-key";

    private readonly string _apiKey = null!; // This should be set from configuration or environment variable

    public ApiKeyAuthentication(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration config)
        : base(options, logger, encoder)
    {
        _apiKey = config["Authentication:ApiKey"]
            ?? throw new InvalidOperationException("API key is not configured.");
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Request.Headers.TryGetValue(HeaderName, out var apiKeyHeader);

        if (!IsPublicEndpoint() && !apiKeyHeader.Equals(_apiKey))
        {
            return Task.FromResult(AuthenticateResult.Fail($"Invalid API credentials. Received:{apiKeyHeader} Should be:{_apiKey}"));
        }

        var identity = new ClaimsIdentity(
            claims: new[] { new Claim(ClaimTypes.Name, "API User") },
            authenticationType: Scheme.Name);

        var principal = new GenericPrincipal(identity, roles: null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private bool IsPublicEndpoint()
        => Context.GetEndpoint()?.Metadata.OfType<AllowAnonymousAttribute>().Any() is null or true;
}